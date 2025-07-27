using System;
using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class DocumentDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Öğrenci")]
        public int StudentId { get; set; }
        
        [Display(Name = "Belge Türü")]
        public string? DocumentType { get; set; }
        
        [Display(Name = "Dosya Yolu")]
        public string? FilePath { get; set; }
        
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedAt { get; set; }
        
        [Display(Name = "Başlık")]
        public string? Title => DocumentType;
        
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        
        [Display(Name = "Yükleme Tarihi")]
        public DateTime UploadDate => CreatedAt;
        
        [Display(Name = "Yükleyen")]
        public string? UploadedBy { get; set; }
        
        // Navigation property
        public StudentDto? Student { get; set; }
    }
} 