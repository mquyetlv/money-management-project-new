using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using money_management_service.Core;
using money_management_service.DTOs.Transaction;
using money_management_service.Entities.Transaction;
using money_management_service.Services.Interfaces;
using System.Linq.Expressions;

namespace money_management_service.Controllers
{
    public class TransactionsController : BaseController
    {
        private readonly ITransactionService _service;
        private readonly IValidator<CreateUpdateTransactionDTO> _validator;

        public TransactionsController(ITransactionService service, IValidator<CreateUpdateTransactionDTO> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedApiResponse<List<Transaction>>>> GetTransactions([FromQuery] SearchTransactionRequestDTO searchTransactionRequestDTO)
        {
            CustomQuery<Transaction> customQuery = new CustomQuery<Transaction>()
            {
                Filters = new List<Expression<Func<Transaction, bool>>>
                {
                    entity => entity.Name.Contains(searchTransactionRequestDTO.Name ?? ""),
                    entity => searchTransactionRequestDTO.TransactionTypeId == null || searchTransactionRequestDTO.TransactionTypeId == entity.TransactionTypeId,
                    entity => searchTransactionRequestDTO.InvestmentId == null || searchTransactionRequestDTO.InvestmentId == entity.InvestmentId,
                    entity => searchTransactionRequestDTO.AccountsId == null || searchTransactionRequestDTO.AccountsId == entity.AccountsId,
                },
                OrderBy = searchTransactionRequestDTO.OrderBy,
                IsDescending = searchTransactionRequestDTO.IsDescending,
                Page = searchTransactionRequestDTO.Page,
                Size = searchTransactionRequestDTO.Size,
            };

            var (total, data) = await _service.GetAllWithPagingAsync(customQuery);
            return Ok(new PagedApiResponse<List<Transaction>>(
                "200",
                "Success",
                data,
                new Pagination(searchTransactionRequestDTO.Page, searchTransactionRequestDTO.Size, total)
            ));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Transaction>>> GetTransactionById(Guid id)
        {
            Transaction transaction = await _service.GetByIdAsync(id);
            return Ok(new ApiResponse<Transaction>("200", "Success", transaction));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Transaction>>> CreateTransaction([FromBody] CreateUpdateTransactionDTO createUpdateTransactionDTO)
        {
            var result = await _validator.ValidateAsync(createUpdateTransactionDTO);
            if (!result.IsValid)
            {
                List<ErrorDetail> errorDetails = result.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest();
            }

            Transaction transaction = new Transaction
            {
                Name = createUpdateTransactionDTO.Name,
                Description = createUpdateTransactionDTO.Description,
                TransactionDate = createUpdateTransactionDTO.TransactionDate,
                TotalAmount = createUpdateTransactionDTO.TotalAmount,
                Quantity = createUpdateTransactionDTO.Quantity,
                AccountsId = createUpdateTransactionDTO.AccountsId,
                TransactionTypeId = createUpdateTransactionDTO.TransactionTypeId,
                InvestmentId = createUpdateTransactionDTO.InvestmentId,
            };

            Transaction transactionCreated = await _service.CreateAsync(transaction);
            return Ok(new ApiResponse<Transaction>("200", "Success", transaction));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Transaction>>> UpdateTransaction(Guid id, [FromBody] CreateUpdateTransactionDTO createUpdateTransactionDTO)
        {
            var result = await _validator.ValidateAsync(createUpdateTransactionDTO);
            if (!result.IsValid)
            {
                List<ErrorDetail> errorDetails = result.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest();
            }

            Dictionary<string, object> updateFiels = new Dictionary<string, object>();
            updateFiels.Add(nameof(Transaction.Name), createUpdateTransactionDTO.Name);
            updateFiels.Add(nameof(Transaction.Description), createUpdateTransactionDTO.Description);
            updateFiels.Add(nameof(Transaction.TransactionDate), createUpdateTransactionDTO.TransactionDate);
            updateFiels.Add(nameof(Transaction.TotalAmount), createUpdateTransactionDTO.TotalAmount);
            updateFiels.Add(nameof(Transaction.Quantity), createUpdateTransactionDTO.Quantity);
            updateFiels.Add(nameof(Transaction.AccountsId), createUpdateTransactionDTO.AccountsId);
            updateFiels.Add(nameof(Transaction.TransactionTypeId), createUpdateTransactionDTO.TransactionTypeId);
            updateFiels.Add(nameof(Transaction.InvestmentId), createUpdateTransactionDTO.InvestmentId);

            Transaction transactionUpdated = await _service.UpdateAsync(id, updateFiels);
            return Ok(new ApiResponse<Transaction>("200", "Success", transactionUpdated));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Transaction>>> DeleteTransaction(Guid id)
        {
            Transaction transactionDeleted = await _service.DeleteByIdAsync(id);
            return Ok(new ApiResponse<Transaction>("200", "Success", transactionDeleted));
        }
    }
}
