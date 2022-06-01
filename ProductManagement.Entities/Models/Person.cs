using System;
using System.Collections.Generic;

namespace ProductManagement.Entities.Models
{
    public partial class Person
    {
        public Person()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FatherName { get; set; }
        public string FullName { get; set; } = null!;
        public string? PersonalCode { get; set; }
        public string? NationalId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Job { get; set; }
        public string? Passport { get; set; }
       
        public int? GenderId { get; set; }
    
        public string? Email { get; set; }
        public string? Mobile { get; set; }

        public bool IsForeignNational { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
        public Gender Gender { get; set; }
    }
}
