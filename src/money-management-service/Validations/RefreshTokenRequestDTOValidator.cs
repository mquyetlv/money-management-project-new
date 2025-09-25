using FluentValidation;
using money_management_service.DTOs.User;

namespace money_management_service.Validations
{
    public class RefreshTokenRequestDTOValidator : AbstractValidator<RefreshTokenRequestDTO>
    {
        public RefreshTokenRequestDTOValidator() 
        {
            RuleFor(item => item.RefreshToken)
                .NotEmpty().WithMessage("Refresh Token is not empty");
        }
    }
}
