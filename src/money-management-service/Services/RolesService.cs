using money_management_service.Data;
using money_management_service.Entities.Users;
using money_management_service.Services.Interfaces;

namespace money_management_service.Services
{
    public class RolesService : BaseService<Role>, IRolesService
    {
        public RolesService(ApplicationDBContext dbContext) : base(dbContext) { }
    }
}
