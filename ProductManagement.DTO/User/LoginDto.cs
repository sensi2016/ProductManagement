using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DTO.User
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    } 
    
    public class LoginDashboardDto:LoginDto
    {
      //  public string VerifyCode { get; set; }
    }   
}
