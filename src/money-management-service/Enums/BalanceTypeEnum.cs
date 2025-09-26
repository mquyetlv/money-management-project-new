using System.ComponentModel;

namespace money_management_service.Enums
{
    public enum BalanceTypeEnum
    {
        [Description("Khoản chi")]
        EXPENCE,

        [Description("Khoản thu")]
        INCOME,

        [Description("Đầu tư")]
        INVESMENT,

        [Description("Chuyển tiền giữa các tài khoản")]
        TRANSFER,
    }
}
