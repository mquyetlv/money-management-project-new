using money_management_service.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace money_management_service.Entities.Transaction
{
    [Table("TRANSACTION_TYPES")]
    public class TransactionType : BaseEntity
    {
        public string Name { get; set; }

        public BalanceTypeEnum BalanceType { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
