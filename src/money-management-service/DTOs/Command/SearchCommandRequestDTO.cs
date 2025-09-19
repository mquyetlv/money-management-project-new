namespace money_management_service.DTOs.Command
{
    public class SearchCommandRequestDTO : BaseRequestPagingDTO
    {
        public string? Name { get; set; }
    }
}
