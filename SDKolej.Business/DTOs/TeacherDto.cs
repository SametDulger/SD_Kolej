namespace SDKolej.Business.DTOs
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int BranchId { get; set; }
    }
} 