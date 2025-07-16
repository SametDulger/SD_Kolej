namespace SDKolej.Business.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentCourseId { get; set; }
    }
} 