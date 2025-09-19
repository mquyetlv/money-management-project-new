using money_management_service.Core;
using money_management_service.Data;
using money_management_service.Entities.Users;
using money_management_service.Services.Interfaces;

namespace money_management_service.Services
{
    public class FunctionsService : BaseService<Function>, IFunctionsService
    {
        public FunctionsService (ApplicationDBContext dbContext) : base(dbContext) { }
    }
}
