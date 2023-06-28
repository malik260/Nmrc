using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IAutoJobRepository
    {
        Task<List<AutoJobEntity>> GetList(AutoJobListParam param);
        Task<List<AutoJobEntity>> GetPageList(AutoJobListParam param, Pagination pagination);
        Task<AutoJobEntity> GetEntity(long id);
        bool ExistJob(AutoJobEntity entity);
        Task SaveForm(AutoJobEntity entity);
        Task DeleteForm(string ids);
    }
}
