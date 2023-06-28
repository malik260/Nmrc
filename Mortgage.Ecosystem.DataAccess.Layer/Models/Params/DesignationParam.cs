namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class DesignationListParam
    {
        public long Company { get; set; }

        public string? Name { get; set; }

        // Multiple Designation Ids
        public string? Ids { get; set; }
    }
}
