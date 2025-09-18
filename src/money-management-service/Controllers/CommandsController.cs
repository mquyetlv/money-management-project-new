

using Microsoft.AspNetCore.Mvc;
using money_management_service.Core;
using money_management_service.Dtos.Command;
using money_management_service.Entities.Users;
using money_management_service.Services.Interfaces;

namespace money_management_service.Controllers
{
    public class CommandsController : BaseController
    {
        private ICommandService _commandService;

        public CommandsController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Command>>>> GetAllCommands()
        {
            var commands = await _commandService.GetAllAsync();
            return Ok(new PagedApiResponse<List<Command>>("200", "Success", commands, new Pagination(0, 10, 100)));
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
