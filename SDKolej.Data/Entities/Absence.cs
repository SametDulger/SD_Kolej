using System;
using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class Absence
    {
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        
        [Required(ErrorMessage = "Tarih zorunludur")]
        public DateTime Date { get; set; }
        
        public bool IsExcused { get; set; }
        
        [StringLength(500, ErrorMessage = "Açıklama maksimum 500 karakter olabilir")]
        public string? Description { get; set; }
        
        public bool IsGuardianDuty { get; set; }
    }
} 