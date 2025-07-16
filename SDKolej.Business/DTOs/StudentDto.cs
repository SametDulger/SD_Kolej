namespace SDKolej.Business.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? SchoolNumber { get; set; }
        public int? ClassId { get; set; }
        public double? GraduationGPA { get; set; }
        public bool IsGraduated { get; set; }
        public bool IsActive { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
} 