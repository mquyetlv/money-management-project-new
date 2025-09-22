using Microsoft.AspNetCore.Mvc;
using money_management_service.Core;
using money_management_service.DTOs.Role;
using money_management_service.Entities.Users;
using money_management_service.Services.Interfaces;
using System.Linq.Expressions;

namespace money_management_service.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService) 
        {
            _rolesService = rolesService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ApiResponse<List<Role>>>> GetAllRoles()
        {
            List<Role> roles = await _rolesService.GetAllAsync();
            return Ok(new ApiResponse<List<Role>>("200", "Success", roles));
        }

        [HttpGet]
        public async Task<ActionResult<PagedApiResponse<List<Role>>>> GetRoles([FromQuery] SearchRoleRequestDTO searchRole)
        {
            CustomQuery<Role> customQuey = new CustomQuery<Role>
            {
                Filters = new List<Expression<Func<Role, bool>>>
                {
                    entity => entity.Name.Contains(searchRole.Name ?? ""),
                },
                OrderBy = searchRole.OrderBy,
                IsDescending = searchRole.IsDescending,
                Page = searchRole.Page,
                Size = searchRole.Size,
            };
            var (total, data) = await _rolesService.GetAllWithPagingAsync(customQuey);
            return Ok(new PagedApiResponse<List<Role>>("200", "Success", data, new Pagination(searchRole.Page, searchRole.Size, total)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Role>>> GetRoleById(Guid id)
        {
            Role role = await _rolesService.GetByIdAsync(id);
            return Ok(new ApiResponse<Role>("200", "Success", role));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Role>>> CreateRole([FromBody] CreateUpdateRoleRequestDTO createRoleRequestDto)
        {
            Role role = new Role();
            role.Name = createRoleRequestDto.Name;
            Role roleCreated = await _rolesService.CreateAsync(role);
            return Ok(new ApiResponse<Role>("200", "Created Role Success", roleCreated));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Role>>> UpdateRole(Guid id, [FromBody] CreateUpdateRoleRequestDTO updateRoleRequestDto)
        {
            Dictionary<string, object> updateFields = new Dictionary<string, object>(); 
            updateFields.Add(nameof(Role.Name), updateRoleRequestDto.Name);

            Role role = await _rolesService.UpdateAsync(id, updateFields);

            return Ok(new ApiResponse<Role>("200", "Update Role Success", role));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Role>>> DeleteRole(Guid id)
        {
            Role role = await _rolesService.DeleteByIdAsync(id);
            return Ok(new ApiResponse<Role>("200", "Delete Role Success", role));
        }
    }
}
