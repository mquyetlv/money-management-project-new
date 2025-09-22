namespace money_management_service.DTOs.User
{
    public class SearchUserRequestDTO : BaseRequestPagingDTO
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? DateOfBirdStartDate { get; set; }

        public DateTime? DateOfBirdStartEnd { get; set; }
    }
}
