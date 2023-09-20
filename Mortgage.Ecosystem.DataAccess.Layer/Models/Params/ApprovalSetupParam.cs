namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class ApprovalSetupListParam
    {
        public long Company { get; set; }

        public long? Branch { get; set; }

        public long MenuId { get; set; }

        public long Authority { get; set; }

        public int Priority { get; set; }
    }
}
