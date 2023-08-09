namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class PropertyGalleryListParam
    {
        public long Id { get; set; }
        public int PropertyType { get; set; }
        public int Location{ get; set; }
        public decimal PriceRangeMin { get; set; }

        public decimal PriceRangeMax { get; set; }
        public int Sort { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

    }
}
