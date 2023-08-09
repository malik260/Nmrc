using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum AuthorizeTypeEnum
    {
        [Description("Role")]
        Role = 1,

        [Description("User")]
        User = 2,
    }
}