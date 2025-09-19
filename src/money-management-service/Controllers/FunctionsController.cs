using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using money_management_service.Core;
using money_management_service.DTOs.Function;
using money_management_service.Entities.Users;
using money_management_service.Services.Interfaces;
using System.Linq.Expressions;

namespace money_management_service.Controllers
{
    public class FunctionsController : BaseController
    {
        private readonly IFunctionsService _functionsService;
        private readonly IValidator<CreateFunctionRequestDTO> _validator;

        public FunctionsController(IFunctionsService functionsService, IValidator<CreateFunctionRequestDTO> validator)
        {
            _functionsService = functionsService;
            _validator = validator;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ApiResponse<List<Function>>>> GetAllFunctions()
        {
            List<Function> result = await _functionsService.GetAllAsync();
            return Ok(new ApiResponse<List<Function>>("200", "Success", result));
        }

        [HttpGet]
        public async Task<ActionResult<PagedApiResponse<List<Function>>>> GetAll([FromQuery] SearchFunctionRequestDTO searchFunctionRequestDTO)
        {
            CustomQuery<Function> customQuery = new CustomQuery<Function>
            {
                Filters = new List<Expression<Func<Function, bool>>>
                {
                    entity => entity.Name.Contains(searchFunctionRequestDTO.Name ?? ""),
                    entity => entity.Url.Contains(searchFunctionRequestDTO.Url ?? ""),
                },
                OrderBy = searchFunctionRequestDTO.OrderBy,
                IsDescending = searchFunctionRequestDTO.IsDescending,
                Page = searchFunctionRequestDTO.Page,
                Size = searchFunctionRequestDTO.Size,
            };
            var result = await _functionsService.GetAllWithPagingAsync(customQuery);
            return Ok(new PagedApiResponse<List<Function>>(
                "200",
                "Success",
                result.data,
                new Pagination(searchFunctionRequestDTO.Page, searchFunctionRequestDTO.Size, result.total)
            ));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Function>>> GetFunctionById(Guid id)
        {
            Function func = await _functionsService.GetByIdAsync(id);
            return Ok(new ApiResponse<Function>("200", "Success", func));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Function>>> CreateFunction(CreateFunctionRequestDTO createFunctionRequestDTO)
        {
            var resultValidator = await _validator.ValidateAsync(createFunctionRequestDTO);
            if (!resultValidator.IsValid)
            {
                List<ErrorDetail> details = resultValidator.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse("400", "Invalid body", details));
            }

            Function function = new Function();
            function.Name = createFunctionRequestDTO.Name;
            function.Url = createFunctionRequestDTO.Url;

            Function functionCreated = await _functionsService.CreateAsync(function);
            return Ok(new ApiResponse<Function>("200", "Created success", functionCreated));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Function>>> UpdateFunction(Guid id, [FromBody] CreateFunctionRequestDTO updateFunctionRequestDTO)
        {
            Dictionary<string, object> updateFiles = new Dictionary<string, object>();
            updateFiles.Add(nameof(Function.Name), updateFunctionRequestDTO.Name);
            updateFiles.Add(nameof(Function.Url), updateFunctionRequestDTO.Url);
            Function funcUpdated = await _functionsService.UpdateAsync(id, updateFiles);
            return Ok(new ApiResponse<Function>("200", "Updated success", funcUpdated));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Function>>> DeleteFunction(Guid id)
        {
            Function func = await _functionsService.DeleteByIdAsync(id);
            return Ok(new ApiResponse<Function>("200", "Deleted success", func));
        }
    }
}
