using System.Collections.Generic;

namespace SDKolej.Data.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? BranchId { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<ClassCourse> ClassCourses { get; set; }
    }
} 