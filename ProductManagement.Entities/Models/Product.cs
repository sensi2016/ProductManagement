using System;
using System.Collections.Generic;

namespace ProductManagement.Entities.Models
{
    public partial class Product: BaseEntity<int>
    {
        public Product()
        {
        }
       
        public string? Title { get; set; }
        public string? Code { get; set; }
        public decimal? Price { get; set; }
        public int? Count { get; set; }
        public string? Brand { get; set; }
        public bool IsActive { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public ProductType ProductType { get; set; }
        public int? ProductTypeId { get; set; }

    }
}
