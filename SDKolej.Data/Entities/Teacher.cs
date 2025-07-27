using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [StringLength(50, ErrorMessage = "Ad maksimum 50 karakter olabilir")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [StringLength(50, ErrorMessage = "Soyad maksimum 50 karakter olabilir")]
        public string LastName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [StringLength(100, ErrorMessage = "Email maksimum 100 karakter olabilir")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Telefon alanı zorunludur")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [StringLength(15, ErrorMessage = "Telefon maksimum 15 karakter olabilir")]
        public string Phone { get; set; } = string.Empty;
        
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
        
        public ICollection<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();
        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
} 