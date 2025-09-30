namespace money_management_service.DTOs.Transaction
{
    public class SearchTransactionRequestDTO : BaseRequestPagingDTO
    {

        public string? Name { get; set; }

        public DateTime TransactionDate { get; set; }

        public Guid TransactionTypeId { get; set; }

        public Guid AccountsId { get; set; }

        public Guid InvestmentId { get; set; }
    }
}
