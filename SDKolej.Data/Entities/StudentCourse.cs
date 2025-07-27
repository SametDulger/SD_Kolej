using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class StudentCourse
    {
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        
        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;
        
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        
        [Required(ErrorMessage = "Akademik yıl zorunludur")]
        [StringLength(9, ErrorMessage = "Akademik yıl 9 karakter olmalıdır (2024-2025)")]
        public string AcademicYear { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
} 