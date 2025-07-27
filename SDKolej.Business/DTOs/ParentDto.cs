using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class ParentDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Ad")]
        public string? FirstName { get; set; }
        
        [Display(Name = "Soyad")]
        public string? LastName { get; set; }
        
        [Display(Name = "E-posta")]
        public string? Email { get; set; }
        
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
    }
} 