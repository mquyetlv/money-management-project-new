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

        public UsersController(IUsersService usersService) 
        {
            _usersService = usersService;
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

    }
}
