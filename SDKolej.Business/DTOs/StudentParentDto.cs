using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class StudentParentDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Öğrenci")]
        public int StudentId { get; set; }
        
        [Display(Name = "Veli")]
        public int ParentId { get; set; }
        
        [Display(Name = "Öğrenci Adı")]
        public string? StudentName { get; set; }
        
        [Display(Name = "Veli Adı")]
        public string? ParentName { get; set; }
        
        // Navigation properties
        public StudentDto? Student { get; set; }
        public ParentDto? Parent { get; set; }
    }
} 