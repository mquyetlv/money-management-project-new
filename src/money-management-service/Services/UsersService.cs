using Microsoft.EntityFrameworkCore;
using money_management_service.Data;
using money_management_service.Entities.Users;
using money_management_service.Services.Interfaces;

namespace money_management_service.Services
{
    public class UsersService : BaseService<User>, IUsersService
    {
        public UsersService(ApplicationDBContext dbContext) : base(dbContext) { }

        public async Task<bool> EmailExistAsync(string email)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.UserName == userName);
            return user != null;
        }
    }
}
