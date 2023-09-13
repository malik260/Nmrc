using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum ConditionForApplicationEnum
    {
        [Description("Age")]
        Age = 1,

        [Description("Incapacitation")]
        Incapacitation = 2,

        [Description("Death")]

        Death = 3,
    }
}
