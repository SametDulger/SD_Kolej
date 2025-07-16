using System;

namespace SDKolej.Data.Entities
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishDate { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int? ClassId { get; set; }
        public Class Class { get; set; }
    }
} 