namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class RefundConditionListParam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Year { get; set; }
        public string Code { get; set; }
        public string Remark { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
