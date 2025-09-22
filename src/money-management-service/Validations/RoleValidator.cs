using FluentValidation;
using money_management_service.DTOs.Role;

namespace money_management_service.Validations
{
    public class RoleValidator : AbstractValidator<CreateUpdateRoleRequestDTO>
    {
        public RoleValidator() 
        {
            RuleFor(item => item.Name)
                .NotEmpty().WithMessage("Name is not empty");
        }
    }
}
