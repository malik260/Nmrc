using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum MaritalStatusEnum
    {
        [Description("Single")]
        Single = 1,

        [Description("Married")]
        Married = 2,

        [Description("Separated")]
        Separated = 3,

        [Description("Divorced")]
        Divorced = 4,

        [Description("Widow")]
        Widow = 5,

        [Description("Widower")]
        Widower = 6
    }
}