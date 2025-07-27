using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class TeacherCourseDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Öğretmen")]
        public int TeacherId { get; set; }
        
        [Display(Name = "Ders")]
        public int CourseId { get; set; }
        
        [Display(Name = "Öğretmen Adı")]
        public string? TeacherName { get; set; }
        
        [Display(Name = "Ders Adı")]
        public string? CourseName { get; set; }
        
        // Navigation properties
        public TeacherDto? Teacher { get; set; }
        public CourseDto? Course { get; set; }
    }
} 