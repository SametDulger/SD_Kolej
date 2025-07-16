namespace SDKolej.Business.DTOs
{
    public class GradeDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Term { get; set; }
        public double Value { get; set; }
        public string? Description { get; set; }
        public double Score => Value;
    }
} 