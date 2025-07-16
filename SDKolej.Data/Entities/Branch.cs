using System.Collections.Generic;

namespace SDKolej.Data.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
} 