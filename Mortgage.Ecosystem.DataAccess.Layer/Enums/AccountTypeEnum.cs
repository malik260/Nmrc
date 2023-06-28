using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum AccountTypeEnum
    {
        [Description("Minor")]
        Minor = 1,

        [Description("Individual")]
        Individual = 2,

        [Description("Joint")]
        Joint = 3,

        [Description("Corporate")]
        Corporate = 4,

        [Description("Estate")]
        Estate = 5,

        [Description("UnIncorporated")]
        UnIncorporated = 6
    }
}