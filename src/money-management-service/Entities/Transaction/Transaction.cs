using System.ComponentModel.DataAnnotations.Schema;

namespace money_management_service.Entities.Transaction
{
    [Table("TRANSACTIONS")]
    public class Transaction : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }

        public long TotalAmount { get; set; }

        public int? Quantity { get; set; }

        public Guid TransactionTypeId { get; set; }

        public TransactionType TransactionType { get; set; }

        public Guid AccountsId { get; set; }

        public Accounts Accounts { get; set; }

        public Guid InvestmentId { get; set; }

        public Investment Investment { get; set; }
    }
}
