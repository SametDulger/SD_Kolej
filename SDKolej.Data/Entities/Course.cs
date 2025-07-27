using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class Course
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Ders adı zorunludur")]
        [StringLength(100, ErrorMessage = "Ders adı maksimum 100 karakter olabilir")]
        public string Name { get; set; } = string.Empty;
        
        public int? ParentCourseId { get; set; }
        public Course? ParentCourse { get; set; }
        public ICollection<Course> SubCourses { get; set; } = new List<Course>();
        public ICollection<ClassCourse> ClassCourses { get; set; } = new List<ClassCourse>();
        public ICollection<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<Absence> Absences { get; set; } = new List<Absence>();
    }
} 