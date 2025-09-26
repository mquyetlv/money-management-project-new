using System.ComponentModel;

namespace money_management_service.Enums
{
    public enum AccountsTypeEnum
    {
        [Description("Tiền mặt")]
        CASH,

        [Description("Ngân hàng")]
        BANKS,

        [Description("Thẻ")]
        CARD,

        [Description("Ví điện tử")]
        E_WALLET
    }
}
