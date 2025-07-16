# SD Kolej - Okul Yönetim Sistemi

> ⚠️ **Geliştirme Aşamasında** ⚠️
> 
> Bu proje aktif geliştirme sürecindedir ve henüz production ortamı için hazır değildir. Eksik özellikler, hatalar ve değişiklikler olabilir. Geliştirme sürecinde düzenli güncellemeler yapılmaktadır.

SD Kolej, .NET 9.0 kullanılarak geliştirilmiş modern bir okul yönetim sistemidir. Sistem, öğrenci, öğretmen, veli ve yönetici ihtiyaçlarını karşılamak üzere tasarlanmış kapsamlı bir web uygulamasıdır.

## 🏗️ Proje Mimarisi

Proje, **N-Tier (Çok Katmanlı)** mimari kullanılarak geliştirilmiştir:

### Katmanlar:
- **SDKolej.Data**: Veri erişim katmanı (Entity Framework Core)
- **SDKolej.Business**: İş mantığı katmanı (Services, DTOs, Validations)
- **SDKolej.API**: Web API katmanı (RESTful API)
- **SDKolej.WebUI**: Web arayüzü katmanı (ASP.NET Core MVC)

## 🚀 Teknolojiler

### Backend:
- **.NET 9.0**
- **Entity Framework Core 9.0.7**
- **SQL Server**
- **AutoMapper 12.0.1**
- **FluentValidation 11.11.0**
- **JWT Authentication**
- **ASP.NET Core Identity**
- **Microsoft.AspNetCore.OpenApi** (Swagger desteği mevcut)

### Frontend:
- **ASP.NET Core MVC**
- **Bootstrap**
- **jQuery**
- **Razor Views**

## 📊 Veritabanı Yapısı

### Ana Varlıklar:

#### 🎓 Öğrenci Yönetimi
- **Student**: Öğrenci bilgileri (ad, soyad, okul numarası, sınıf, mezuniyet durumu)
- **Class**: Sınıf bilgileri (sınıf adı, şube)
- **Enrollment**: Öğrenci kayıt bilgileri (yıl, aktiflik durumu)

#### 👨‍🏫 Öğretmen Yönetimi
- **Teacher**: Öğretmen bilgileri (ad, soyad, email, telefon, şube)
- **Course**: Ders bilgileri (ders adı, alt dersler)
- **TeacherCourse**: Öğretmen-ders ilişkisi
- **ClassCourse**: Sınıf-ders ilişkisi

#### 👨‍👩‍👧‍👦 Veli Yönetimi
- **Parent**: Veli bilgileri (ad, soyad, email, telefon)
- **StudentParent**: Öğrenci-veli ilişkisi

#### 📚 Akademik Yönetim
- **Grade**: Not bilgileri (öğrenci, ders, dönem, not değeri)
- **Absence**: Devamsızlık bilgileri (tarih, mazeret durumu, nöbetçi durumu)
- **Branch**: Şube bilgileri (Matematik-Fen, Türkçe-Matematik, vb.)

#### 📢 İletişim ve Belge Yönetimi
- **Announcement**: Duyuru bilgileri (başlık, içerik, yayın tarihi)
- **Message**: Mesajlaşma sistemi (gönderen, alıcı, içerik)
- **Document**: Belge yönetimi (karne, teşekkür, takdir belgeleri)
- **FileUpload**: Dosya yükleme sistemi

## 🔧 Kurulum ve Çalıştırma

### ⚠️ Önemli Notlar:
- Bu proje **geliştirme aşamasında** olduğu için production ortamında kullanılması önerilmez
- Veritabanı şeması değişiklik gösterebilir
- API endpoint'leri güncellenebilir
- Güvenlik özellikleri henüz tamamlanmamıştır

### Gereksinimler:
- .NET 9.0 SDK
- SQL Server (LocalDB veya SQL Server Express)
- Visual Studio 2022 veya VS Code

### Kurulum Adımları:

1. **Projeyi klonlayın:**
```bash
git clone https://github.com/SametDulger/SD_Kolej.git
cd SD_Kolej
```

2. **Veritabanı bağlantı ayarlarını yapılandırın:**
   - `SDKolej.API/appsettings.json` dosyasında connection string'i güncelleyin (varsayılan: SQL Server Express)
   - `SDKolej.WebUI/appsettings.json` dosyasında API URL'ini güncelleyin (varsayılan: http://localhost:5000)

3. **Veritabanını oluşturun:**
```bash
cd SDKolej.API
dotnet ef database update
```

4. **API projesini çalıştırın:**
```bash
dotnet run
# API http://localhost:5000 adresinde çalışacak
```

5. **Web UI projesini çalıştırın:**
```bash
cd ../SDKolej.WebUI
dotnet run
# Web UI http://localhost:5001 adresinde çalışacak
```

## 📋 Özellikler

### 🎯 Öğrenci Yönetimi
- Öğrenci kayıt ve güncelleme
- Sınıf atama
- Okul numarası yönetimi (100-25 formatında)
- Mezuniyet durumu takibi
- Aktiflik durumu kontrolü

### 👨‍🏫 Öğretmen Yönetimi
- Öğretmen profil yönetimi
- Şube atama
- Ders atama
- İletişim bilgileri

### 📊 Akademik Takip
- Not girişi ve görüntüleme
- Devamsızlık takibi
- Dönem bazlı değerlendirme
- GPA hesaplama

### 📢 İletişim Sistemi
- Duyuru yayınlama
- Mesajlaşma sistemi
- Veli bilgilendirme

### 📁 Belge Yönetimi
- Karne, teşekkür, takdir belgeleri
- Dosya yükleme sistemi
- Belge arşivleme

## 🔐 Güvenlik

> ⚠️ **Güvenlik Özellikleri Geliştirme Aşamasında**
> 
> Aşağıdaki güvenlik özellikleri henüz tamamlanmamıştır ve production ortamı için yeterli değildir:

- **JWT Token** tabanlı kimlik doğrulama (temel implementasyon)
- **ASP.NET Core Identity** kullanıcı yönetimi (temel implementasyon)
- **Role-based authorization** (User rolü mevcut, geliştirilecek)
- **CORS** politikaları (AllowAll - production için güvenli değil)
- **Input validation** (FluentValidation)
- **Exception handling middleware** ile hata yönetimi

### 🔒 Güvenlik Eksiklikleri:
- Password policy henüz tanımlanmamış
- Rate limiting mevcut değil
- Audit logging eksik
- HTTPS zorunluluğu yok
- SQL injection koruması test edilmemiş

## 🏛️ Mimari Detayları

### Repository Pattern
- Generic `IRepository<T>` interface
- Entity Framework Core ile implementasyon
- CRUD operasyonları için standart metodlar

### Service Layer
- Business logic encapsulation
- DTO mapping (AutoMapper)
- Validation (FluentValidation)
- Dependency injection

### API Design
- RESTful API endpoints
- Standardized response format (`ApiResponse<T>`)
- Exception handling middleware
- HTTP status codes

### Web UI
- MVC pattern
- API service integration
- Session management
- Error handling

## 📝 API Endpoints

### Kimlik Doğrulama
- `POST /api/Auth/register` - Kullanıcı kaydı
- `POST /api/Auth/login` - Kullanıcı girişi (JWT token döner)

### Öğrenci İşlemleri
- `GET /api/Student` - Tüm öğrencileri listele
- `GET /api/Student/{id}` - Öğrenci detayı
- `POST /api/Student` - Yeni öğrenci ekle
- `PUT /api/Student/{id}` - Öğrenci güncelle
- `DELETE /api/Student/{id}` - Öğrenci sil

### Diğer Modüller
Benzer CRUD operasyonları şu modüller için mevcuttur:
- Teacher, Course, Class, Parent
- Grade, Absence, Announcement
- Message, Document, FileUpload
- Branch, Enrollment, ClassCourse, TeacherCourse, StudentParent

## 🗄️ Veritabanı İlişkileri

### Many-to-Many İlişkiler:
- `StudentParent`: Öğrenci-Veli
- `ClassCourse`: Sınıf-Ders
- `TeacherCourse`: Öğretmen-Ders

### One-to-Many İlişkiler:
- `Branch` → `Class`, `Teacher`
- `Class` → `Student`, `Enrollment`
- `Student` → `Grade`, `Absence`, `Document`
- `Teacher` → `Announcement`

## 🌱 Seed Data

Sistem ilk çalıştırıldığında otomatik olarak örnek veriler yüklenir:
- 4 şube (Matematik-Fen, Türkçe-Matematik, Sosyal Bilimler, Yabancı Dil)
- 8 sınıf (9-A, 9-B, 10-A, 10-B, 11-A, 11-B, 12-A, 12-B)
- 10 ders (Matematik, Fizik, Kimya, Biyoloji, Türkçe, Tarih, Coğrafya, İngilizce, Felsefe, Beden Eğitimi)
- 4 öğretmen (örnek email ve telefon bilgileri ile)
- 4 öğrenci (100-25, 101-25, 102-25, 103-25 numaraları ile)
- 4 veli (örnek email ve telefon bilgileri ile)
- Öğrenci-veli ilişkileri
- 2 örnek duyuru

## 🚀 Geliştirme

### 📋 Bilinen Eksiklikler ve Hatalar:
- **Authentication**: JWT token refresh mekanizması eksik
- **Authorization**: Role-based access control henüz tamamlanmamış
- **Validation**: Bazı entity'ler için validation kuralları eksik
- **Error Handling**: Bazı edge case'ler için hata yönetimi eksik
- **Testing**: Unit test ve integration test'ler yazılmamış
- **Documentation**: API documentation (Swagger) henüz aktif değil
- **Performance**: Database query optimization yapılmamış
- **UI/UX**: Frontend tasarımı geliştirilecek

### 🔄 Geliştirme Roadmap:
- [ ] Authentication sistemi tamamlanacak
- [ ] Role-based authorization implementasyonu
- [ ] API documentation (Swagger) aktif edilecek
- [ ] Unit test'ler yazılacak
- [ ] Performance optimization
- [ ] UI/UX iyileştirmeleri
- [ ] Security hardening
- [ ] Production deployment hazırlıkları

### Yeni Özellik Ekleme:
1. Entity oluştur (SDKolej.Data/Entities)
2. DbContext'e ekle
3. Migration oluştur
4. DTO oluştur (SDKolej.Business/DTOs)
5. AutoMapper mapping ekle
6. Service interface ve implementasyonu oluştur
7. Validation ekle
8. API Controller oluştur
9. Web UI Controller ve View'ları oluştur

### Validation Kuralları:
- Öğrenci okul numarası: `100-25` formatında (regex: `^\d+-\d{2}$`)
- Not değerleri: 0-100 arası
- Ad/Soyad: Maksimum 50 karakter, boş olamaz
- Email format kontrolü
- Zorunlu alanlar için validation



## 🐛 Hata Bildirimi

Geliştirme sürecinde karşılaştığınız hataları veya önerilerinizi GitHub Issues üzerinden bildirebilirsiniz. Proje aktif geliştirme sürecinde olduğu için düzenli güncellemeler yapılmaktadır.


## 📄 Lisans

Bu proje [MIT License](LICENSE) altında lisanslanmıştır.

---

**SD Kolej** - Modern okul yönetim sistemi ile eğitim süreçlerinizi dijitalleştirin! 🎓

> ⚠️ **Not**: Bu proje geliştirme aşamasındadır. Production ortamında kullanmadan önce güvenlik ve performans testlerinin yapılması önerilir. 