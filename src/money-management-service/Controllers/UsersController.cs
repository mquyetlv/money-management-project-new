using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using money_management_service.Core;
using money_management_service.DTOs.User;
using money_management_service.Entities.Users;
using money_management_service.Services.Interfaces;
using System.Linq.Expressions;

namespace money_management_service.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService _usersService;
        private readonly IValidator<CreateUserRequestDTO> _validator;

        public UsersController(IUsersService usersService, IValidator<CreateUserRequestDTO> validator) 
        {
            _usersService = usersService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedApiResponse<List<User>>>> GetUsers([FromQuery] SearchUserRequestDTO searchUserDto)
        {
            CustomQuery<User> query = new CustomQuery<User> 
            {
                Filters = new List<Expression<Func<User, bool>>>
                {
                    entity => entity.UserName.Contains(searchUserDto.UserName),
                    entity => entity.FirstName.Contains(searchUserDto.FirstName),
                    entity => entity.LastName.Contains(searchUserDto.LastName),
                    entity => entity.Email.Contains(searchUserDto.Email),
                }
            };

            var (total, data) = await _usersService.GetAllWithPagingAsync(query);

            return Ok(new PagedApiResponse<List<User>>("200", "Success", data, new Pagination(searchUserDto.Page, searchUserDto.Size, total)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> GetUserById(Guid id)
        {
            User user = await _usersService.GetByIdAsync(id);
            return Ok(new ApiResponse<User>("200", "Success", user));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<User>>> CreateUser(CreateUserRequestDTO createUserRequestDto)
        {
            var result = _validator.Validate(createUserRequestDto);
            if (!result.IsValid)
            {
                List<ErrorDetail> errorDetails = result.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse("400", "Body invalid", errorDetails));
            }

            User user = new User();
            user.UserName = createUserRequestDto.UserName;
            user.Email = createUserRequestDto.Email;
            user.Password = createUserRequestDto.Password;
            user.FirstName = createUserRequestDto.FirstName;
            user.LastName = createUserRequestDto.LastName;
            user.DateOfBirth = createUserRequestDto.DateOfBirth;

            User userCreated = await _usersService.CreateAsync(user);
            return Ok(new ApiResponse<User>("200", "Create success", userCreated));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> UpdateUser(Guid id, [FromBody] CreateUserRequestDTO createUserRequestDto)
        {
            Dictionary<string, object> updateFields = new Dictionary<string, object>();
            updateFields.Add(nameof(CreateUserRequestDTO.FirstName), createUserRequestDto.FirstName);
            updateFields.Add(nameof(CreateUserRequestDTO.LastName), createUserRequestDto.LastName);
            updateFields.Add(nameof(CreateUserRequestDTO.Email), createUserRequestDto.Email);

            User userUpdated = await _usersService.UpdateAsync(id, updateFields);
            return Ok(new ApiResponse<User>("200", "Update success", userUpdated));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> DeleteUser(Guid id)
        {
            User deletedUser = await _usersService.DeleteByIdAsync(id);
            return Ok(new ApiResponse<User>("200", "Delete success", deletedUser));
        }
    }
}
