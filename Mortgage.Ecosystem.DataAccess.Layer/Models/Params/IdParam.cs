namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class IdParam
    {
        // Primary key for all tables
        // When the long returns to the front-end js, the precision will be lost, so it is converted into a string
        public long? Id { get; set; }
    }
}