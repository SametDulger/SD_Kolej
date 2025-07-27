using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class BranchDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Şube Adı")]
        public string? Name { get; set; }
        
        [Display(Name = "Adres")]
        public string? Address { get; set; }
        
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
    }
} 