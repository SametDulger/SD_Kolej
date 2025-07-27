using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Ad")]
        public string? FirstName { get; set; }
        
        [Display(Name = "Soyad")]
        public string? LastName { get; set; }
        
        [Display(Name = "Okul Numarası")]
        public string? SchoolNumber { get; set; }
        
        [Display(Name = "Sınıf")]
        public int? ClassId { get; set; }
        
        [Display(Name = "Mezuniyet GPA")]
        public double? GraduationGPA { get; set; }
        
        [Display(Name = "Mezun")]
        public bool IsGraduated { get; set; }
        
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
        
        [Display(Name = "Kayıt Tarihi")]
        public DateTime? RegistrationDate { get; set; }
        
        // Navigation property
        public ClassDto? Class { get; set; }
    }
} 