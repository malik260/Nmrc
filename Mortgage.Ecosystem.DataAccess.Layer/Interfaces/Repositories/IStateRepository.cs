using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IStateRepository
    {
        Task<List<StateEntity>> GetList(StateListParam param);
        Task<List<StateEntity>> GetPageList(StateListParam param, Pagination pagination);
        Task<StateEntity> GetEntity(long id);
        Task SaveForm(StateEntity entity);
        Task DeleteForm(string ids);
    }
}
