using System.Collections.Generic;

namespace SDKolej.Data.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCourseId { get; set; }
        public Course ParentCourse { get; set; }
        public ICollection<Course> SubCourses { get; set; }
        public ICollection<ClassCourse> ClassCourses { get; set; }
        public ICollection<TeacherCourse> TeacherCourses { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
} 