using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum EmploymentTypeEnum
    {
        [Description("Employed")]
        Employed = 1,

        [Description("Self Employed")]
        SelfEmployed = 2
    }
}