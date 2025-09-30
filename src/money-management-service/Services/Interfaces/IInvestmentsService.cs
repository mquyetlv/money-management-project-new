using money_management_service.Entities.Transaction;

namespace money_management_service.Services.Interfaces
{
    public interface IInvestmentsService : IBaseService<Investment>
    {
        Task<bool> ExistsAsync(Guid id, CancellationToken cancel);
    }
}
