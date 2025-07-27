using System.ComponentModel.DataAnnotations;

namespace SDKolej.Data.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Şube adı zorunludur")]
        [StringLength(100, ErrorMessage = "Şube adı maksimum 100 karakter olabilir")]
        public string Name { get; set; } = string.Empty;
        
        public ICollection<Class> Classes { get; set; } = new List<Class>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
} 