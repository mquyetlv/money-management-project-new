using FluentValidation;
using money_management_service.DTOs.Function;

namespace money_management_service.Validations
{
    public class FunctionValidation : AbstractValidator<CreateFunctionRequestDTO>
    {
        public FunctionValidation() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is not empty")
                .MaximumLength(255).WithMessage("The maximum length of Name is 255");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Name is not empty")
                .MaximumLength(255).WithMessage("The maximum length of Name is 255");
        }
    }
}
