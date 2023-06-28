namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class ImportParam
    {
        // The path after the imported file is uploaded to the server
        public string? FilePath { get; set; }

        // Whether to update the existing data
        public int? IsOverride { get; set; }
    }
}