using money_management_service.Entities.Users;

namespace money_management_service.Services.Interfaces
{
    public interface IUsersService : IBaseService<User>
    {
        Task<bool> EmailExistAsync(string email);

        Task<bool> UserNameExistsAsync(string userName);
    }
}
