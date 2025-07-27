using System;
using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class Announcement
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Duyuru başlığı zorunludur")]
        [StringLength(200, ErrorMessage = "Başlık maksimum 200 karakter olabilir")]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Duyuru içeriği zorunludur")]
        public string Content { get; set; } = string.Empty;
        
        public DateTime PublishDate { get; set; } = DateTime.Now;
        
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
} 