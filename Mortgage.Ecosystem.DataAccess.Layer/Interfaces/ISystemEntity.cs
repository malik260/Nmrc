namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces
{
    public interface ISystemEntity : IEntity
    {
        public bool System { get; set; }
    }
}
