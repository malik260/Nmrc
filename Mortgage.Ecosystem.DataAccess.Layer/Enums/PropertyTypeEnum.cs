using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum PropertyTypeEnum
    {
        [Description("Residential")]
        Residential = 1,

        [Description("Bungalow")]
        Bungalow = 2,

        [Description("Duplex")]
        Duplex = 3,

        [Description("Flats")]
        Flats = 4,

        [Description("Semi-Detached House")]
        SemiDetachedHouse = 5,

        [Description("Detached House")]
        DetachedHouse = 6,


    }
}