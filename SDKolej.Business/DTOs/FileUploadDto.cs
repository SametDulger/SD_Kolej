using System;

namespace SDKolej.Business.DTOs
{
    public class FileUploadDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
        public string? Description { get; set; }
        public long? FileSize { get; set; }
        public string? FileType { get; set; }
        public DateTime UploadDate => UploadedAt;
        public string? UploadedBy { get; set; }
    }
} 