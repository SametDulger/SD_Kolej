using System;
using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class FileUploadDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Öğrenci")]
        public int StudentId { get; set; }
        
        [Display(Name = "Dosya Adı")]
        public string? FileName { get; set; }
        
        [Display(Name = "Dosya Yolu")]
        public string? FilePath { get; set; }
        
        [Display(Name = "Yükleme Tarihi")]
        public DateTime UploadedAt { get; set; }
        
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        
        [Display(Name = "Dosya Boyutu")]
        public long? FileSize { get; set; }
        
        [Display(Name = "Dosya Türü")]
        public string? FileType { get; set; }
        
        [Display(Name = "Yükleme Tarihi")]
        public DateTime UploadDate => UploadedAt;
        
        [Display(Name = "Yükleyen")]
        public string? UploadedBy { get; set; }
        
        // Navigation property
        public StudentDto? Student { get; set; }
    }
} 