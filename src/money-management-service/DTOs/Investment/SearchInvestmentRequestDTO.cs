namespace money_management_service.DTOs.Investment
{
    public class SearchInvestmentRequestDTO : BaseRequestPagingDTO
    {
        public string? Name { get; set; }
    }
}
