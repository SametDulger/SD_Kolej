using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class GradeDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Öğrenci")]
        public int StudentId { get; set; }
        
        [Display(Name = "Ders")]
        public int CourseId { get; set; }
        
        [Display(Name = "Puan")]
        public double Score { get; set; }
        
        [Display(Name = "Dönem")]
        public int Term { get; set; }
        
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        
        [Display(Name = "Not")]
        public double Value => Score;
        
        // Navigation properties
        public StudentDto? Student { get; set; }
        public CourseDto? Course { get; set; }
    }
} 