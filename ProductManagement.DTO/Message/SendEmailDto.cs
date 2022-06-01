using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.DTO
{
    public class SendEmailDto
    {
        public string Email { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }


    public class SendSmsDto
    {
        public string Mobile { get; set; }
        public string Message { get; set; }
    }
}
