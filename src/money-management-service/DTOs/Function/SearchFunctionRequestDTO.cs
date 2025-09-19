namespace money_management_service.DTOs.Function
{
    public class SearchFunctionRequestDTO : BaseRequestPagingDTO
    {
        public string? Name { get; set; }

        public string? Url { get; set; }
    }
}
