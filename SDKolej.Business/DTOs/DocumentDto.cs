using System;

namespace SDKolej.Business.DTOs
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? DocumentType { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Title => DocumentType;
        public string? Description { get; set; }
        public DateTime UploadDate => CreatedAt;
        public string? UploadedBy { get; set; }
    }
} 