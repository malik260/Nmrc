using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum SortByEnum
    {
        [Description("Popularity")]
        Popularity = 1,

        [Description("Newest")]
        Newest = 2,

        [Description("Price: Low to High")]
        PriceLowtoHigh = 3,

        [Description("Price: High to Low")]
        PriceHightoLow = 4,

        [Description("Property Rating")]
        PropertyRating = 5,

    }
}
