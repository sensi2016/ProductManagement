using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DTO.User
{
    public class RegisterDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
         public string VerifyCode { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }

    public class VerifyUserDto
    {
        public string Mobile { get; set; }
        public string VerifyCode { get; set; }
    }

    public class SendVerifyCodeDto
    {
        public string Mobile { get; set; }
    }

    public class ForgetPasswordDto
    {
        public string Mobile { get; set; }
        public string VerifyCode { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
    
    public class ResetPasswordDto
    {
        public string Mobile { get; set; }
    }

    public class DealFilterDto:IPaging
    {
        public int? OfficeId { get; set; }
        public int? DealTypeId { get; set; }
        public int? CityId { get; set; }
        public string? FullName  { get; set; }
        public string? TrackingCode { get; set; }
        public string OrderBy { get; set; } 
        public int PageSize { get; set; }
        public int PageNumber { get; set; }       
    }

    public class ForgetUserNameOrPasswordDto
    {
        public string Type { get; set; }
        public string NationalId { get; set; }
        public string IdentityCardId { get; set; }
        public string Mobile { get; set; }
        public string BirthDate { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string GreatGrandFatherName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Finger { get; set; }    
    }
}
