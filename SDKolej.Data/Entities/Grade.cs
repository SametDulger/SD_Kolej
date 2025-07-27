using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        
        [Required(ErrorMessage = "Dönem bilgisi zorunludur")]
        [StringLength(20, ErrorMessage = "Dönem maksimum 20 karakter olabilir")]
        public string Semester { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Not değeri zorunludur")]
        [Range(0, 100, ErrorMessage = "Not değeri 0-100 arasında olmalıdır")]
        public double Value { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
} 