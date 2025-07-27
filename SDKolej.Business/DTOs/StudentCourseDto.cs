namespace SDKolej.Business.DTOs
{
    public class StudentCourseDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int ClassId { get; set; }
        public int? TeacherId { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime EnrollmentDate { get; set; }
        
        // Navigation properties
        public StudentDto? Student { get; set; }
        public CourseDto? Course { get; set; }
        public ClassDto? Class { get; set; }
        public TeacherDto? Teacher { get; set; }
    }
} 