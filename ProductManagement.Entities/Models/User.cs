using System;
using System.Collections.Generic;

namespace ProductManagement.Entities.Models
{
    public partial class User: BaseEntity<int>
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            Products = new HashSet<Product>();
        }

        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? PersonId { get; set; }
        public int? RoleId { get; set; }
        public bool IsVerify { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual Person? Person { get; set; }
      
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
