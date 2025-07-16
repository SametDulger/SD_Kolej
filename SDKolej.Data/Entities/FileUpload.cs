using System;

namespace SDKolej.Data.Entities
{
    public class FileUpload
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
        public string Description { get; set; }
    }
} 