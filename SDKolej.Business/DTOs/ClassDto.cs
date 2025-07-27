using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class ClassDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Sınıf Adı")]
        public string? Name { get; set; }
        
        [Display(Name = "Şube")]
        public int? BranchId { get; set; }
        
        // Navigation property
        public BranchDto? Branch { get; set; }
    }
} 