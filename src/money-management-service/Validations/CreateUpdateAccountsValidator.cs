using FluentValidation;
using money_management_service.DTOs.Accounts;

namespace money_management_service.Validations
{
    public class CreateUpdateAccountsValidator : AbstractValidator<CreateUpdateAccountsRequestDTO>
    {
        public CreateUpdateAccountsValidator()
        {
            RuleFor(entity => entity.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(255).WithMessage("Max length is 255 character");

            RuleFor(entity => entity.Description)
                .MaximumLength(500).WithMessage("Max length is 500 character");

            RuleFor(entity => entity.Balance)
                .NotNull().WithMessage("Balance is required");

            RuleFor(entity => entity.AccountsType)
                .NotNull().WithMessage("Account Type is required")
                .IsInEnum().WithMessage("Account Type is invalid");
        }
    }
}
