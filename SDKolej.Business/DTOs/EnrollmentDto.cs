using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Öğrenci")]
        public int StudentId { get; set; }
        
        [Display(Name = "Sınıf")]
        public int ClassId { get; set; }
        
        [Display(Name = "Yıl")]
        public int Year { get; set; }
        
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
        
        // Navigation properties
        public StudentDto? Student { get; set; }
        public ClassDto? Class { get; set; }
    }
} 