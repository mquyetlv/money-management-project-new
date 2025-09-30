using money_management_service.Enums;

namespace money_management_service.DTOs.Accounts
{
    public class SearchAccountRequestDTO : BaseRequestPagingDTO
    {
        public string? Name { get; set; }

        public AccountsTypeEnum? AccountsType { get; set; }
    }
}
