using System;
using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class FileUpload
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Dosya adı zorunludur")]
        [StringLength(255, ErrorMessage = "Dosya adı maksimum 255 karakter olabilir")]
        public string FileName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Dosya yolu zorunludur")]
        public string FilePath { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Dosya türü zorunludur")]
        [StringLength(50, ErrorMessage = "Dosya türü maksimum 50 karakter olabilir")]
        public string ContentType { get; set; } = string.Empty;
        
        public long FileSize { get; set; }
        
        public DateTime UploadDate { get; set; } = DateTime.Now;
        
        [StringLength(500, ErrorMessage = "Açıklama maksimum 500 karakter olabilir")]
        public string? Description { get; set; }
    }
} 