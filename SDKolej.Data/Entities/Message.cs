using System;

namespace SDKolej.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderRole { get; set; } // Öğretmen, Veli, Yönetici
        public int ReceiverId { get; set; }
        public string ReceiverRole { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
} 