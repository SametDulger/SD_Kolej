using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        
        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;
        
        [Required(ErrorMessage = "Akademik yıl zorunludur")]
        [StringLength(9, ErrorMessage = "Akademik yıl 9 karakter olmalıdır (2024-2025)")]
        public string AcademicYear { get; set; } = string.Empty;
        
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        
        public bool IsActive { get; set; } = true;
    }
} 