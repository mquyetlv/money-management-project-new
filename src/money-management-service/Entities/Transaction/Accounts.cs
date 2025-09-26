using money_management_service.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace money_management_service.Entities.Transaction
{
    [Table("ACCOUNTS")]
    public class Accounts : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public long Balance { get; set; }

        public AccountsTypeEnum AccountsType { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
