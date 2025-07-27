using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class Class
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Sınıf adı zorunludur")]
        [StringLength(20, ErrorMessage = "Sınıf adı maksimum 20 karakter olabilir")]
        public string Name { get; set; } = string.Empty;
        
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
        
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<ClassCourse> ClassCourses { get; set; } = new List<ClassCourse>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
} 