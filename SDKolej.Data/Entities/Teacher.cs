using System.Collections.Generic;

namespace SDKolej.Data.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public ICollection<TeacherCourse> TeacherCourses { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
    }
} 