using FluentValidation;
using money_management_service.DTOs.Investment;

namespace money_management_service.Validations
{
    public class CreateUpdateInvestmentValidator : AbstractValidator<CreateUpdateInvestmentRequestDTO>
    {
        public CreateUpdateInvestmentValidator()
        {
            RuleFor(entity => entity.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(entity => entity.CurrentUnitPrice)
                .NotNull().WithMessage("Current unit price is required");
        }
    }
}
