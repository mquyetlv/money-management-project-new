using FluentValidation;
using money_management_service.DTOs.User;

namespace money_management_service.Validations
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator() 
        {
            RuleFor(dto => dto.UserName)
                .NotEmpty().WithMessage("Username is not empty")
                .MinimumLength(3).WithMessage("User name greate than 3 character or more")
                .MaximumLength(50).WithMessage("User name less than equal 50 character");

            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Password is empty")
                .MinimumLength(6).WithMessage("Password greate than 6 character or more")
                .MaximumLength(50).WithMessage("Password less than equal 50 character");
        }
    }
}
