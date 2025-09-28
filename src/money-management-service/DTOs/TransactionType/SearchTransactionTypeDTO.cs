namespace money_management_service.DTOs.TransactionType
{
    public class SearchTransactionTypeDTO : BaseRequestPagingDTO
    {
        public string? Name { get; set; }
    }
}
