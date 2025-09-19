namespace money_management_service.DTOs
{
    public class BaseRequestPagingDTO
    {
        public int Page { get; set; } = 0;

        public int Size { get; set; } = 10;

        public string? OrderBy { get; set; }

        public bool? IsDescending { get; set; } = false;
    }
}
