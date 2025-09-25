using FluentValidation;
using money_management_service.DTOs.User;
using money_management_service.Services.Interfaces;

namespace money_management_service.Validations
{
    public class UserValidator : AbstractValidator<CreateUserRequestDTO>
    {
        private readonly IUsersService _usersService;

        public UserValidator(IUsersService userService) 
        {
            _usersService = userService;

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is not empty")
                .MaximumLength(25).WithMessage("Max length is 25 character")
                .MinimumLength(3).WithMessage("Min length is 3 character")
                .MustAsync(async (userName, cancelerToken) => !await _usersService.UserNameExistsAsync(userName))
                .WithMessage("Username already exists");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is not empty")
                .EmailAddress().WithMessage("Email invalid")
                .MustAsync(async (email, cancelerToken) => !await _usersService.EmailExistAsync(email))
                .WithMessage("Email already exists");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is not empty")
                .MinimumLength(3).WithMessage("Min length is 6 character");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is not empty")
                .MaximumLength(50).WithMessage("Max length is 50 character");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is not empty")
                .MaximumLength(50).WithMessage("Max length is 50 character");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("DateOfBirth is not empty");
        }
    }
}
