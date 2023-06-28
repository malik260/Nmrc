using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum AlertTypeEnum
    {
        [Description("Direct deposit")]
        DirectDeposit = 1,

        [Description("Unusual account activity")]
        UnusualAccountActivity = 2,

        [Description("Large purchase")]
        LargePurchase = 3,

        [Description("Large ATM withdrawal")]
        LargeATMWithdrawal = 4,

        [Description("Debit card")]
        DebitCard = 5
    }
}