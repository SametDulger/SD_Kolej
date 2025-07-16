using SDKolej.Data.Contexts;
using SDKolej.Data.Entities;

namespace SDKolej.Data.SeedData
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolDbContext context)
        {
            context.Database.EnsureCreated();

            // Şubeler
            if (!context.Branches.Any())
            {
                var branches = new List<Branch>
                {
                    new Branch { Name = "Matematik-Fen" },
                    new Branch { Name = "Türkçe-Matematik" },
                    new Branch { Name = "Sosyal Bilimler" },
                    new Branch { Name = "Yabancı Dil" }
                };
                context.Branches.AddRange(branches);
                context.SaveChanges();
            }

            // Sınıflar
            if (!context.Classes.Any())
            {
                var classes = new List<Class>
                {
                    new Class { Name = "9-A", BranchId = 1 },
                    new Class { Name = "9-B", BranchId = 1 },
                    new Class { Name = "10-A", BranchId = 2 },
                    new Class { Name = "10-B", BranchId = 2 },
                    new Class { Name = "11-A", BranchId = 3 },
                    new Class { Name = "11-B", BranchId = 3 },
                    new Class { Name = "12-A", BranchId = 4 },
                    new Class { Name = "12-B", BranchId = 4 }
                };
                context.Classes.AddRange(classes);
                context.SaveChanges();
            }

            // Dersler
            if (!context.Courses.Any())
            {
                var courses = new List<Course>
                {
                    new Course { Name = "Matematik" },
                    new Course { Name = "Fizik" },
                    new Course { Name = "Kimya" },
                    new Course { Name = "Biyoloji" },
                    new Course { Name = "Türkçe" },
                    new Course { Name = "Tarih" },
                    new Course { Name = "Coğrafya" },
                    new Course { Name = "İngilizce" },
                    new Course { Name = "Felsefe" },
                    new Course { Name = "Beden Eğitimi" }
                };
                context.Courses.AddRange(courses);
                context.SaveChanges();
            }

            // Öğretmenler
            if (!context.Teachers.Any())
            {
                var teachers = new List<Teacher>
                {
                    new Teacher { FirstName = "Ahmet", LastName = "Yılmaz", Email = "ahmet.yilmaz@sdkoej.com", Phone = "05321234567", BranchId = 1 },
                    new Teacher { FirstName = "Ayşe", LastName = "Demir", Email = "ayse.demir@sdkoej.com", Phone = "05321234568", BranchId = 2 },
                    new Teacher { FirstName = "Mehmet", LastName = "Kaya", Email = "mehmet.kaya@sdkoej.com", Phone = "05321234569", BranchId = 3 },
                    new Teacher { FirstName = "Fatma", LastName = "Çelik", Email = "fatma.celik@sdkoej.com", Phone = "05321234570", BranchId = 4 }
                };
                context.Teachers.AddRange(teachers);
                context.SaveChanges();
            }

            // Öğrenciler
            if (!context.Students.Any())
            {
                var students = new List<Student>
                {
                    new Student { FirstName = "Ali", LastName = "Özkan", SchoolNumber = "100-25", RegistrationDate = DateTime.Now.AddDays(-30), ClassId = 1, IsActive = true },
                    new Student { FirstName = "Zeynep", LastName = "Arslan", SchoolNumber = "101-25", RegistrationDate = DateTime.Now.AddDays(-29), ClassId = 1, IsActive = true },
                    new Student { FirstName = "Can", LastName = "Yıldız", SchoolNumber = "102-25", RegistrationDate = DateTime.Now.AddDays(-28), ClassId = 2, IsActive = true },
                    new Student { FirstName = "Elif", LastName = "Kurt", SchoolNumber = "103-25", RegistrationDate = DateTime.Now.AddDays(-27), ClassId = 2, IsActive = true }
                };
                context.Students.AddRange(students);
                context.SaveChanges();
            }

            // Veliler
            if (!context.Parents.Any())
            {
                var parents = new List<Parent>
                {
                    new Parent { FirstName = "Hasan", LastName = "Özkan", Email = "hasan.ozkan@email.com", Phone = "05321234571" },
                    new Parent { FirstName = "Sevgi", LastName = "Arslan", Email = "sevgi.arslan@email.com", Phone = "05321234572" },
                    new Parent { FirstName = "Mustafa", LastName = "Yıldız", Email = "mustafa.yildiz@email.com", Phone = "05321234573" },
                    new Parent { FirstName = "Hatice", LastName = "Kurt", Email = "hatice.kurt@email.com", Phone = "05321234574" }
                };
                context.Parents.AddRange(parents);
                context.SaveChanges();
            }

            // Öğrenci-Veli İlişkileri
            if (!context.StudentParents.Any())
            {
                var studentParents = new List<StudentParent>
                {
                    new StudentParent { StudentId = 1, ParentId = 1 },
                    new StudentParent { StudentId = 2, ParentId = 2 },
                    new StudentParent { StudentId = 3, ParentId = 3 },
                    new StudentParent { StudentId = 4, ParentId = 4 }
                };
                context.StudentParents.AddRange(studentParents);
                context.SaveChanges();
            }

            // Duyurular
            if (!context.Announcements.Any())
            {
                var announcements = new List<Announcement>
                {
                    new Announcement { Title = "Veli Toplantısı", Content = "Bu hafta cuma günü veli toplantısı yapılacaktır.", PublishDate = DateTime.Now.AddDays(-5) },
                    new Announcement { Title = "Sınav Takvimi", Content = "1. dönem sınavları başlayacaktır.", PublishDate = DateTime.Now.AddDays(-3) }
                };
                context.Announcements.AddRange(announcements);
                context.SaveChanges();
            }
        }
    }
} 