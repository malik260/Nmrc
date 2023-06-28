using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface INextOfKinRepository
    {
        Task<List<NextOfKinEntity>> GetList(NextOfKinListParam param);
        Task<List<NextOfKinEntity>> GetPageList(NextOfKinListParam param, Pagination pagination);
        Task<NextOfKinEntity> GetEntity(long id);
        Task SaveForm(NextOfKinEntity entity);
        Task DeleteForm(string ids);
    }
}
