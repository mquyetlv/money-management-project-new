using Microsoft.EntityFrameworkCore;
using money_management_service.Data;
using money_management_service.Entities.Transaction;
using money_management_service.Exceptions;
using money_management_service.Services.Interfaces;

namespace money_management_service.Services
{
    public class TransactionService : BaseService<Transaction>, ITransactionService
    {
        public TransactionService(ApplicationDBContext _dbContext) : base(_dbContext)
        {
        }

        public override async Task<Transaction> CreateAsync(Transaction transaction)
        {
            Accounts account = await _dbContext.Accounts.FirstOrDefaultAsync(item => item.Id == transaction.AccountsId);

            if (account.Balance < transaction.TotalAmount)
            {
                throw new InsufficientBalanceException("Wallet/card/account balance is insufficient");
            }

            account.Balance = account.Balance - transaction.TotalAmount;
            await _dbSet.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
            return transaction;
        }

        public override async Task<Transaction> UpdateAsync(Guid id, Dictionary<string, object> updateFields)
        {
            Transaction transaction = await _dbSet.FindAsync(id);
            var entry = _dbSet.Entry(transaction);

            if (entry == null)
            {
                throw new NotFoundException("Not found");
            }

            Accounts account = await _dbContext.Accounts.FirstOrDefaultAsync(item => item.Id == transaction.AccountsId);
            if (updateFields.TryGetValue(nameof(transaction.TotalAmount), out var amountObj) && amountObj is long amount)
            {
                if (amount != transaction.TotalAmount)
                {
                    if (account.Balance + transaction.TotalAmount < amount)
                    {
                        throw new InsufficientBalanceException("Wallet/card/account balance is insufficient");
                    }

                    account.Balance = account.Balance + transaction.TotalAmount - amount;
                }
            }

            foreach (var item in updateFields)
            {
                var property = entry.Property(item.Key);
                if (property != null)
                {
                    property.CurrentValue = item.Value;
                    property.IsModified = true;
                }
            }

            await _dbContext.SaveChangesAsync();

            return transaction;
        }

        public override async Task<Transaction> DeleteByIdAsync(Guid id)
        {

            Transaction transaction = await _dbSet.FindAsync(id);
            if (transaction == null)
            {
                throw new NotFoundException("Not found");
            }

            Accounts account = await _dbContext.Accounts.FirstOrDefaultAsync(item => item.Id == transaction.AccountsId);
            if (account != null)
            {
                account.Balance = account.Balance + transaction.TotalAmount;
            }

            _dbSet.Remove(transaction);
            await _dbContext.SaveChangesAsync();
            return transaction;
        }
    }
}
