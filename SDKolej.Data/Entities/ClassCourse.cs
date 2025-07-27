namespace SDKolej.Data.Entities
{
    public class ClassCourse
    {
        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
} 