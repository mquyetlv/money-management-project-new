using Microsoft.EntityFrameworkCore;
using money_management_service.Data;
using money_management_service.Entities.Transaction;
using money_management_service.Services.Interfaces;

namespace money_management_service.Services
{
    public class InvestmentsService : BaseService<Investment>, IInvestmentsService
    {
        public InvestmentsService(ApplicationDBContext dbContext) : base(dbContext) { }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken canncel)
        {
            return await _dbSet.AnyAsync(item => item.Id == id, canncel);
        }
    }
}
