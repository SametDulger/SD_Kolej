using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [StringLength(50, ErrorMessage = "Ad maksimum 50 karakter olabilir")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [StringLength(50, ErrorMessage = "Soyad maksimum 50 karakter olabilir")]
        public string LastName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Okul numarası zorunludur")]
        [RegularExpression(@"^\d+-\d{2}$", ErrorMessage = "Okul numarası 100-25 formatında olmalıdır")]
        public string SchoolNumber { get; set; } = string.Empty;
        
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        
        public int? ClassId { get; set; }
        public Class? Class { get; set; }
        
        [Range(0, 100, ErrorMessage = "Mezuniyet GPA'si 0-100 arasında olmalıdır")]
        public double? GraduationGPA { get; set; }
        
        public bool IsGraduated { get; set; }
        public bool IsActive { get; set; } = true;
        
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Absence> Absences { get; set; } = new List<Absence>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
} 