using System;

namespace SDKolej.Business.DTOs
{
    public class AbsenceDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsExcused { get; set; }
        public bool IsOnDuty { get; set; }
        public string? Description { get; set; }
        public int CourseId { get; set; }
        public string? Reason { get; set; }
        public bool? IsDutyStudent { get; set; }
    }
} 