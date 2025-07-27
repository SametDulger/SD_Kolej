using System;
using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class AbsenceDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Öğrenci")]
        public int StudentId { get; set; }
        
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }
        
        [Display(Name = "Özürlü")]
        public bool IsExcused { get; set; }
        
        [Display(Name = "Görevli")]
        public bool IsOnDuty { get; set; }
        
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        
        [Display(Name = "Ders")]
        public int CourseId { get; set; }
        
        [Display(Name = "Sebep")]
        public string? Reason { get; set; }
        
        [Display(Name = "Nöbetçi Öğrenci")]
        public bool? IsDutyStudent { get; set; }
        
        // Navigation properties
        public StudentDto? Student { get; set; }
        public CourseDto? Course { get; set; }
    }
} 