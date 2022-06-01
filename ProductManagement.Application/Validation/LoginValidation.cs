


namespace ProductManagement.Application.Validation
{
    public class LoginValidation : AbstractValidator<LoginDto>
    {
        public LoginValidation(IStringLocalizer<SharedResources> stringLocalizer)
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(stringLocalizer["Required.UserName"]);
            RuleFor(x => x.Password).NotEmpty().WithMessage(stringLocalizer["Required.Password"]);
        }
    }
}
