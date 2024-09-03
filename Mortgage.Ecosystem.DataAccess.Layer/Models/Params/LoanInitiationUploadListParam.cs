namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class LoanInitiationUploadListParam
    {
        public long Company { get; set; }
        public int FileId { get; set; }
        public string? Images { get; set; }
        public double Size { get; set; }

        public string? Type { get; set; }
        public string? Label { get; set; }

    }
}