namespace SDKolej.Data.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
    }
} 