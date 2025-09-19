

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using money_management_service.Core;
using money_management_service.Dtos.Command;
using money_management_service.DTOs.Command;
using money_management_service.Entities.Users;
using money_management_service.Services.Interfaces;
using System.Linq.Expressions;

namespace money_management_service.Controllers
{
    public class CommandsController : BaseController
    {
        private readonly ICommandService _commandService;
        private readonly IValidator<CreateUpdateCommandRequestDTO> _validator;

        public CommandsController(ICommandService commandService, IValidator<CreateUpdateCommandRequestDTO> validator)
        {
            _commandService = commandService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Command>>>> GetAllCommands([FromQuery] SearchCommandRequestDTO searchCommandRequestDTO)
        {
            CustomQuery<Command> customQuery = new CustomQuery<Command>
            {
                Filters = new List<Expression<Func<Command, bool>>>
                {
                    (entity) => entity.Name.Contains((searchCommandRequestDTO.Name ?? ""))
                },
                OrderBy = searchCommandRequestDTO.OrderBy,
                IsDescending = searchCommandRequestDTO.IsDescending,
                Page = searchCommandRequestDTO.Page,
                Size = searchCommandRequestDTO.Size,
            };
            var commands = await _commandService.GetAllWithPagingAsync(customQuery);
            return Ok(new PagedApiResponse<List<Command>>(
                "200", 
                "Success", 
                commands.data, 
                new Pagination(searchCommandRequestDTO.Page, searchCommandRequestDTO.Size, commands.total))
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Command>> GetCommandById(Guid id)
        {
            var command = await _commandService.GetByIdAsync(id);
            return Ok(new ApiResponse<Command>("200", "Success", command));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Command>>> CreateCommand(CreateUpdateCommandRequestDTO dto)
        {

            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                List<ErrorDetail> errorsDetail = result.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse("400", "Validate invalid", errorsDetail));
            }

            var command = new Command();
            command.Name = dto.Name;
            var commandCreated = await _commandService.CreateAsync(command);
            return Ok(new ApiResponse<Command>("200", "Created Success", commandCreated));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Command>>> UpdateCommand(Guid id, [FromBody] CreateUpdateCommandRequestDTO dto)
        {
            Dictionary<string, object> updateFiels = new Dictionary<string, object>();
            updateFiels.Add("Name", dto.Name);

            var commandUpdated = await _commandService.UpdateAsync(id, updateFiels);

            return Ok(new ApiResponse<Command>("200", "Update Success", commandUpdated));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Command>>> DeleteCommand(Guid id)
        {
            var commandDeleted = await _commandService.DeleteByIdAsync(id);
            return Ok(new ApiResponse<Command>("200", "Delete Success", commandDeleted));
        }
    }
}
