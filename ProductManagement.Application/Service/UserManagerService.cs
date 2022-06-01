using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Service
{
    public class UserManagerService : IUserManagerService, IRegisterScoped
    {

        private readonly DataBaseContext _dataBaseContext;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IConfiguration _configuration;
        public UserManagerService(DataBaseContext dataBaseContext, IStringLocalizer<SharedResources> stringLocalizer, IConfiguration configuration)
        {
            _dataBaseContext = dataBaseContext;
            _stringLocalizer = stringLocalizer;
            _configuration = configuration;
        }

        public async Task<BaseResponseDto> Login(LoginDto loginDto)
        {
            var productValidation = new Validation.LoginValidation(_stringLocalizer);
            var result = productValidation.Validate(loginDto);

            if (!result.IsValid)
            {
                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Errors = result.Errors.Select(g=>g.ErrorMessage).ToList(),
                };
            }

            var passHash = Utilities.GenerateHashSHA256(loginDto.Password);
            var user = await _dataBaseContext.Users.Where(d => d.UserName == loginDto.UserName && d.Password== passHash).FirstOrDefaultAsync();

            if(user is null)
            {
                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotFound,
                    Errors = _stringLocalizer["User.NotFound"].Value,
                };
            }

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = new ResponseLoginDto
                {
                    UserName = loginDto.UserName,
                    Token = Utilities.BuildToken(_configuration, user.UserName, user.Id.ToString()),
                }
            };

        }
    }
}
