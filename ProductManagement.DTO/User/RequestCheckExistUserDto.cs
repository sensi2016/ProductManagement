using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DTO.User
{
    public class RequestCheckExistUserDto
    {
        public string UserName { get; set; }
    }
    
    public class ResponseCheckExistUserDto
    {
        public bool NotExist { get; set; }
        public List<string> UserNames { get; set; }
    }
}
