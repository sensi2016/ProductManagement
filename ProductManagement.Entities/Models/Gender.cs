using System;
using System.Collections.Generic;

namespace ProductManagement.Entities.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Persons = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Code { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}
