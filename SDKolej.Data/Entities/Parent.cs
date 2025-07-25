using System.Collections.Generic;

namespace SDKolej.Data.Entities
{
    public class Parent
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<StudentParent> StudentParents { get; set; }
    }
} 