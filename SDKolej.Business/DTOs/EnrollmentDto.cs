namespace SDKolej.Business.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
    }
} 