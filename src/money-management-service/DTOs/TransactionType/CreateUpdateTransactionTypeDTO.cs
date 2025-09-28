using money_management_service.Enums;

namespace money_management_service.DTOs.TransactionType
{
    public class CreateUpdateTransactionTypeDTO
    {
        public string Name { get; set; }

        public BalanceTypeEnum BalanceType { get; set; }
    }
}
