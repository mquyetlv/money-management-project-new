using System.ComponentModel.DataAnnotations.Schema;

namespace money_management_service.Entities.Transaction
{
    [Table("INVESTMENT")]
    public class Investment : BaseEntity
    {
        public string Name { get; set; }

        public long CurrentUnitPrice { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
