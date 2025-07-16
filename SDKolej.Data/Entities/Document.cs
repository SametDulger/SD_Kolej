using System;

namespace SDKolej.Data.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string DocumentType { get; set; } // Karne, Teşekkür, Takdir, Onur
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 