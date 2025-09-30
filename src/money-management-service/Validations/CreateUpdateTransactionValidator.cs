using FluentValidation;
using money_management_service.DTOs.Transaction;
using money_management_service.Entities.Transaction;
using money_management_service.Services.Interfaces;

namespace money_management_service.Validations
{
    public class CreateUpdateTransactionValidator : AbstractValidator<CreateUpdateTransactionDTO>
    {
        private readonly IInvestmentsService _investmentsService;
        private readonly ITransactionTypeService _transactionTypeService;
        private readonly IAccountsService _accountsService;

        public CreateUpdateTransactionValidator(
            IInvestmentsService investmentsService,
            ITransactionTypeService transactionTypeService,
            IAccountsService accountsService
        )
        {
            _investmentsService = investmentsService;
            _transactionTypeService = transactionTypeService;
            _accountsService = accountsService;

            RuleFor(entity => entity.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Maximum length of Name is 100 characters");

            RuleFor(entity => entity.Description)
                .MinimumLength(500).WithMessage("Maximum length of Name is 500 characters");

            RuleFor(entity => entity.TotalAmount)
                .NotNull().WithMessage("Total Amount is required")
                .GreaterThan(0).WithMessage("Total amount must be greater than 0");

            RuleFor(entity => entity.Quantity)
                .NotNull().WithMessage("Quantity is required")
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be greater than or equal to 1");

            RuleFor(entity => entity.AccountsId)
                .NotNull().WithMessage("Accounts is required")
                .MustAsync(async (accountId, cancallation) => await _accountsService.ExistsAsync(accountId, cancallation))
                .WithMessage("Accounts does not exist");

            RuleFor(entity => entity.TransactionTypeId)
                .NotNull().WithMessage("Transaction Type is required")
                .MustAsync(async (transactionTypeId, cancellation) => await _transactionTypeService.ExistsAsync(transactionTypeId, cancellation))
                .WithMessage("Transaction Type does not exist");

            RuleFor(entity => entity.InvestmentId)
                .MustAsync(async (investmentId, cancellation) =>
                {
                    if (investmentId == null)
                    {
                        return true;
                    }

                    return await _investmentsService.ExistsAsync(investmentId.Value, cancellation);
                })
                .WithMessage("Investment does not exist");
        }
    }
}
