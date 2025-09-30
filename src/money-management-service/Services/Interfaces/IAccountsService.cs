using money_management_service.Entities.Transaction;

namespace money_management_service.Services.Interfaces
{
    public interface IAccountsService : IBaseService<Accounts>
    {
        Task<bool> ExistsAsync(Guid id, CancellationToken cancel);
    }
}
