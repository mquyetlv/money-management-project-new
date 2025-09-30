using money_management_service.Enums;

namespace money_management_service.DTOs.Accounts
{
    public class CreateUpdateAccountsRequestDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public long Balance { get; set; }

        public AccountsTypeEnum AccountsType { get; set; }
    }
}
