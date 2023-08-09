using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Models.Data
{
    public abstract class BaseModel
    {
        protected BaseModel(IEntity model)
        {
            Id = model.Id;
        }

        protected BaseModel(Guid id)
        {
            Id = id;
        }

        protected BaseModel()
        {

        }

        public Guid? Id { get; set; }
    }
}
