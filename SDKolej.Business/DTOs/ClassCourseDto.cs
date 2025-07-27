using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class ClassCourseDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Sınıf")]
        public int ClassId { get; set; }
        
        [Display(Name = "Ders")]
        public int CourseId { get; set; }
        
        [Display(Name = "Sınıf Adı")]
        public string? ClassName { get; set; }
        
        [Display(Name = "Ders Adı")]
        public string? CourseName { get; set; }
        
        // Navigation properties
        public ClassDto? Class { get; set; }
        public CourseDto? Course { get; set; }
    }
} 