using System;

namespace SDKolej.Business.DTOs
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishDate { get; set; }
        public int? TeacherId { get; set; }
        public int? ClassId { get; set; }
        public DateTime Date => PublishDate;
    }
} 