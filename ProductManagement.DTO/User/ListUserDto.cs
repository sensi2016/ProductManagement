using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DTO.User
{
    public class ListUserDto
    {
        public int Id { get; set; }
        public string   UserName { get; set; }
        public string   FullName { get; set; }
        public string ImageProfile { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string CreateDate { get; set; }
        public string PersonalCode { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; }
    }
}
