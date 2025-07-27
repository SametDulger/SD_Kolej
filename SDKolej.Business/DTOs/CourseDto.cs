using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Ders Adı")]
        public string? Name { get; set; }
        
        [Display(Name = "Üst Ders")]
        public int? ParentCourseId { get; set; }
    }
} 