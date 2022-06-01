using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DTO.User
{
    public class CurrentUserInfoDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Mobile { get; set; }
        public string ProfileImage { get; set; }
        public int RoleId { get; set; }
    }
    public class CurrentUserDashboardDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public int CountTicket { get; set; }
    }
}
