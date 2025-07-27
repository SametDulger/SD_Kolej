using System;
using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Gönderen ID zorunludur")]
        public string SenderId { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Alıcı ID zorunludur")]
        public string ReceiverId { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Mesaj içeriği zorunludur")]
        public string Content { get; set; } = string.Empty;
        
        public DateTime SentDate { get; set; } = DateTime.Now;
        
        public bool IsRead { get; set; }
    }
} 