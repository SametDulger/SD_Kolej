using System;
using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class Document
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Belge adı zorunludur")]
        [StringLength(200, ErrorMessage = "Belge adı maksimum 200 karakter olabilir")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Belge türü zorunludur")]
        [StringLength(50, ErrorMessage = "Belge türü maksimum 50 karakter olabilir")]
        public string Type { get; set; } = string.Empty; // Karne, Teşekkür, Takdir
        
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        [StringLength(500, ErrorMessage = "Açıklama maksimum 500 karakter olabilir")]
        public string? Description { get; set; }
    }
} 