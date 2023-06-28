using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum MenuTypeEnum
    {
        [Description("Directory")]
        Directory = 1,

        [Description("Page")]
        Menu = 2,

        [Description("Button")]
        Button = 3
    }
}