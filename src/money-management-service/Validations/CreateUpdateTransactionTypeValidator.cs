using FluentValidation;
using money_management_service.DTOs.TransactionType;

namespace money_management_service.Validations
{
    public class CreateUpdateTransactionTypeValidator : AbstractValidator<CreateUpdateTransactionTypeDTO>
    {
        public CreateUpdateTransactionTypeValidator() 
        {
            RuleFor(item => item.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(item => item.BalanceType)
                .NotNull().WithMessage("Balance type is required")
                .IsInEnum().WithMessage("Balance type is invalid");
        }
    }
}
