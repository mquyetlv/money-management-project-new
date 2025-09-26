using money_management_service.Data;
using money_management_service.Entities.Transaction;
using money_management_service.Services.Interfaces;

namespace money_management_service.Services
{
    public class TransactionTypeService : BaseService<TransactionType>, ITransactionTypeService
    {
        public TransactionTypeService(ApplicationDBContext _dbContext) : base(_dbContext)
        { }
    }
}
