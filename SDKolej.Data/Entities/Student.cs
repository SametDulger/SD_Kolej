using System;
using System.Collections.Generic;

namespace SDKolej.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SchoolNumber { get; set; } // 100-25 formatında
        public DateTime RegistrationDate { get; set; }
        public int? ClassId { get; set; }
        public Class Class { get; set; }
        public double? GraduationGPA { get; set; }
        public bool IsGraduated { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Absence> Absences { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<StudentParent> StudentParents { get; set; }
    }
} 