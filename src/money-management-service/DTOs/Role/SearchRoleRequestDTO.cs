namespace money_management_service.DTOs.Role
{
    public class SearchRoleRequestDTO : BaseRequestPagingDTO
    {
        public string? Name { get; set; }
    }
}
