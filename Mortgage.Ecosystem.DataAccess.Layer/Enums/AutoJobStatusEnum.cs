using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum AutoJobStatusEnum
    {
        [Description("Running")]
        Yes = 1,

        [Description("Stop")]
        No = 2
    }
}
