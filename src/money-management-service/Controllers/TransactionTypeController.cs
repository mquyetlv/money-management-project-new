using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using money_management_service.Core;
using money_management_service.DTOs.TransactionType;
using money_management_service.Entities.Transaction;
using money_management_service.Services.Interfaces;
using System.Linq.Expressions;

namespace money_management_service.Controllers
{
    public class TransactionTypeController : BaseController
    {
        private readonly ITransactionTypeService _service;
        private readonly IValidator<CreateUpdateTransactionTypeDTO> _validator;

        public TransactionTypeController(ITransactionTypeService service, IValidator<CreateUpdateTransactionTypeDTO> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ApiResponse<List<TransactionType>>>> GetAll()
        {
            List<TransactionType> transactionTypes = await _service.GetAllAsync();
            return Ok(new ApiResponse<List<TransactionType>>("200", "Success", transactionTypes));
        }

        [HttpGet]
        public async Task<ActionResult<PagedApiResponse<List<TransactionType>>>> GetTransactionType([FromQuery]SearchTransactionTypeDTO searchTransactionTypeDTO)
        {
            CustomQuery<TransactionType> customQuery = new CustomQuery<TransactionType>
            {
                Filters = new List<Expression<Func<TransactionType, bool>>>
                {
                    entity => entity.Name.Contains(searchTransactionTypeDTO.Name ?? "")
                },
                OrderBy = searchTransactionTypeDTO.OrderBy,
                IsDescending = searchTransactionTypeDTO.IsDescending,
                Page = searchTransactionTypeDTO.Page,
                Size = searchTransactionTypeDTO.Size,
            };

            var (total, data) = await _service.GetAllWithPagingAsync(customQuery);
            return Ok(new PagedApiResponse<List<TransactionType>>(
                "200", 
                "Success", 
                data, 
                new Pagination(searchTransactionTypeDTO.Page, searchTransactionTypeDTO.Size, total))
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionType>> GetTransactionTypeById(Guid id)
        {
            TransactionType transactionType = await _service.GetByIdAsync(id);
            return Ok(new ApiResponse<TransactionType>("200", "Success", transactionType));
        }

        [HttpPost]
        public async Task<ActionResult<TransactionType>> CreateTransactionType([FromBody] CreateUpdateTransactionTypeDTO createUpdateTransactionTypeDTO)
        {
            var result = _validator.Validate(createUpdateTransactionTypeDTO);
            if (!result.IsValid)
            {
                List<ErrorDetail> errorDetails = result.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse("400", "Body invalid", errorDetails));
            }

            TransactionType transactionType = new TransactionType();
            transactionType.Name = createUpdateTransactionTypeDTO.Name;
            transactionType.BalanceType = createUpdateTransactionTypeDTO.BalanceType;

            TransactionType transactionTypeCreated = await _service.CreateAsync(transactionType);
            return Ok(new ApiResponse<TransactionType>("200", "Create Success", transactionTypeCreated));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionType>> UpdateTransactionType(Guid id, [FromBody] CreateUpdateTransactionTypeDTO createUpdateTransactionTypeDTO)
        {
            var result = _validator.Validate(createUpdateTransactionTypeDTO);
            if (!result.IsValid)
            {
                List<ErrorDetail> errorDetails = result.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse("400", "Body invalid", errorDetails));
            }

            Dictionary<string, object> updateFields = new Dictionary<string, object>();
            updateFields.Add(nameof(TransactionType.Name), createUpdateTransactionTypeDTO.Name);
            updateFields.Add(nameof(TransactionType.BalanceType), createUpdateTransactionTypeDTO.BalanceType);
            TransactionType transationTypeUpdated = await _service.UpdateAsync(id, updateFields);
            return Ok(new ApiResponse<TransactionType>("200", "Update Success", transationTypeUpdated));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TransactionType>> DeleteTransactionType(Guid id)
        {
            TransactionType transactionTypeDeleted = await _service.DeleteByIdAsync(id);
            return Ok(new ApiResponse<TransactionType>("200", "Delete Success", transactionTypeDeleted));
        }
    }
}
