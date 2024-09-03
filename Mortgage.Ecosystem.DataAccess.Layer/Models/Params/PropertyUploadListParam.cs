namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class PropertyUploadListParam
    {
        public long Pmb { get; set; }
        public int ParselId { get; set; }
        public string? Images { get; set; }
        public double Size { get; set; }

        public string? Type { get; set; }
        public string? Label { get; set; }

    }
}