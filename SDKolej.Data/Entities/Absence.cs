using System;

namespace SDKolej.Data.Entities
{
    public class Absence
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime Date { get; set; }
        public bool IsExcused { get; set; } // Raporlu mu?
        public bool IsOnDuty { get; set; } // Nöbetçi öğrenci mi?
        public string Description { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string Reason { get; set; }
    }
} 