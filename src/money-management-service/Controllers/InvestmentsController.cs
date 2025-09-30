using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using money_management_service.Core;
using money_management_service.DTOs.Investment;
using money_management_service.Entities.Transaction;
using money_management_service.Services.Interfaces;
using System.Linq.Expressions;

namespace money_management_service.Controllers
{
    public class InvestmentsController : BaseController
    {
        private readonly IInvestmentsService _service;
        private readonly IValidator<CreateUpdateInvestmentRequestDTO> _validator;

        public InvestmentsController(IInvestmentsService service, IValidator<CreateUpdateInvestmentRequestDTO> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Investment>>> GetInvestments([FromQuery] SearchInvestmentRequestDTO searchInvestmentRequestDTO)
        {
            CustomQuery<Investment> customQuery = new CustomQuery<Investment>
            {
                Filters = new List<Expression<Func<Investment, bool>>>
                {
                    investment => investment.Name.Contains(searchInvestmentRequestDTO.Name ?? "")
                },
                OrderBy = searchInvestmentRequestDTO.OrderBy,
                IsDescending = searchInvestmentRequestDTO.IsDescending,
                Page = searchInvestmentRequestDTO.Page,
                Size = searchInvestmentRequestDTO.Size,
            };

            var (total, data) = await _service.GetAllWithPagingAsync(customQuery);
            return Ok(new PagedApiResponse<List<Investment>>(
                "200", 
                "Success", 
                data, 
                new Pagination(searchInvestmentRequestDTO.Page, searchInvestmentRequestDTO.Size, total))
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Investment>> GetInvestmentById(Guid id)
        {
            Investment investment = await _service.GetByIdAsync(id);
            return Ok(new ApiResponse<Investment>("200", "Success", investment));
        }

        [HttpPost]
        public async Task<ActionResult<Investment>> CreateInvestment([FromBody] CreateUpdateInvestmentRequestDTO createUpdateInvestmentRequestDTO)
        {
            var result = _validator.Validate(createUpdateInvestmentRequestDTO);
            if (!result.IsValid)
            {
                List<ErrorDetail> errorDetails = result.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse("400", "Invalid body", errorDetails));
            }

            Investment investment = new Investment();
            investment.Name = createUpdateInvestmentRequestDTO.Name;
            investment.CurrentUnitPrice = createUpdateInvestmentRequestDTO.CurrentUnitPrice;

            Investment investmentCreated = await _service.CreateAsync(investment);
            return Ok(new ApiResponse<Investment>("200", "Success", investmentCreated));
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<Investment>>> UpdateInvestment(Guid id, [FromBody] CreateUpdateInvestmentRequestDTO createUpdateInvestmentRequestDTO)
        {
            var result = _validator.Validate(createUpdateInvestmentRequestDTO);
            if (!result.IsValid)
            {
                List<ErrorDetail> errorDetails = result.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse("400", "Invalid body", errorDetails));
            }

            Dictionary<string, object> updateFields = new Dictionary<string, object>();
            updateFields.Add(nameof(Investment.Name), createUpdateInvestmentRequestDTO.Name);
            updateFields.Add(nameof(Investment.CurrentUnitPrice), createUpdateInvestmentRequestDTO.CurrentUnitPrice);

            Investment investmentUpdated = await _service.UpdateAsync(id, updateFields);
            return Ok(new ApiResponse<Investment>("200", "Update success", investmentUpdated));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Investment>> DeleteInvestment(Guid id)
        {
            Investment investmentDeleted = await _service.DeleteByIdAsync(id);
            return Ok(new ApiResponse<Investment>("200", "Deleted success", investmentDeleted));
        }
    }
}
