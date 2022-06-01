using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Dto
{
    public class CustomeException:Exception
    {
        public string Errors { get; set; }
        public CustomeException()
        {
                
        }
    }

}
