using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using money_management_service.Core;
using money_management_service.DTOs.Accounts;
using money_management_service.Entities.Transaction;
using money_management_service.Services.Interfaces;
using System.Linq.Expressions;

namespace money_management_service.Controllers
{
    public class AccountsService : BaseController
    {
        private readonly IAccountsService _service;
        private readonly IValidator<CreateUpdateAccountsRequestDTO> _validator;

        public AccountsService(IAccountsService service, IValidator<CreateUpdateAccountsRequestDTO> validator) {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedApiResponse<Accounts>>> GetAccounts([FromQuery] SearchAccountRequestDTO searchAccountRequestDTO)
        {
            CustomQuery<Accounts> customQuery = new CustomQuery<Accounts>()
            {
                Filters = new List<Expression<Func<Accounts, bool>>>
                {
                    entity => entity.Name.Contains(searchAccountRequestDTO.Name ?? ""),
                    entity => searchAccountRequestDTO.AccountsType == null ? true : entity.AccountsType == searchAccountRequestDTO.AccountsType,
                },
                OrderBy = searchAccountRequestDTO.OrderBy,
                IsDescending = searchAccountRequestDTO.IsDescending,
                Page = searchAccountRequestDTO.Page,
                Size = searchAccountRequestDTO.Size,
            };

            var (total, data) = await _service.GetAllWithPagingAsync(customQuery);
            return Ok(new PagedApiResponse<List<Accounts>>(
                "200",
                "Success",
                data,
                new Pagination(searchAccountRequestDTO.Page, searchAccountRequestDTO.Size, total)
            ));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Accounts>> GetAccountById(Guid id)
        {
            Accounts account = await _service.GetByIdAsync(id);
            return Ok(new ApiResponse<Accounts>("200", "Success", account));
        }

        [HttpPost]
        public async Task<ActionResult<Accounts>> CreateAccounts([FromBody] CreateUpdateAccountsRequestDTO createUpdateAccountsRequestDTO)
        {
            var result = _validator.Validate(createUpdateAccountsRequestDTO);
            if (!result.IsValid)
            {
                List<ErrorDetail> errorDetails = result.Errors.Select(error => new ErrorDetail(error.PropertyName, error.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse("400", "Body invalid", errorDetails));
            }

            Accounts account = new Accounts
            {
                Name = createUpdateAccountsRequestDTO.Name,
                Description = createUpdateAccountsRequestDTO.Description,
                Balance = createUpdateAccountsRequestDTO.Balance,
                AccountsType = createUpdateAccountsRequestDTO.AccountsType,
            };

            Accounts accountCreated = await _service.CreateAsync(account);
            return Ok(new ApiResponse<Accounts>("200", "Create success", accountCreated));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Accounts>> UpdateAccount(Guid id, [FromBody] CreateUpdateAccountsRequestDTO createUpdateAccountsRequestDTO)
        {
            var result = _validator.Validate(createUpdateAccountsRequestDTO);
            if (!result.IsValid)
            {
                List<ErrorDetail> errorDetails = result.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse("400", "Body invalid", errorDetails));
            }

            Dictionary<string, object> updateFiels = new Dictionary<string, object>();
            updateFiels.Add(nameof(Accounts.Name), createUpdateAccountsRequestDTO.Name);
            updateFiels.Add(nameof(Accounts.Balance), createUpdateAccountsRequestDTO.Balance);
            updateFiels.Add(nameof(Accounts.Description), createUpdateAccountsRequestDTO.Description);
            updateFiels.Add(nameof(Accounts.AccountsType), createUpdateAccountsRequestDTO.AccountsType);

            Accounts accountUpdated = await _service.UpdateAsync(id, updateFiels);
            return Ok(new ApiResponse<Accounts>("200", "Update success", accountUpdated));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Accounts>> DeleteAccounts(Guid id)
        {
            Accounts accountDeleted = await _service.DeleteByIdAsync(id);
            return Ok(new ApiResponse<Accounts>("200", "Delete success", accountDeleted));  
        }
    }
}
