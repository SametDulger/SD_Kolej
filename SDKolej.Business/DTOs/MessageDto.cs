using System;

namespace SDKolej.Business.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string? SenderRole { get; set; }
        public int ReceiverId { get; set; }
        public string? ReceiverRole { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public string? Subject { get; set; }
        public DateTime SentDate => SentAt;
    }
} 