namespace money_management_service.DTOs.Transaction
{
    public class CreateUpdateTransactionDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }

        public long TotalAmount { get; set; }

        public int Quantity { get; set; }

        public Guid TransactionTypeId { get; set; }

        public Guid AccountsId { get; set; }

        public Guid? InvestmentId { get; set; }
    }
}
