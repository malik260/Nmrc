using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IAutoJobLogRepository
    {
        Task<List<AutoJobLogEntity>> GetList(AutoJobLogListParam param);
        Task<List<AutoJobLogEntity>> GetPageList(AutoJobLogListParam param, Pagination pagination);
        Task<AutoJobLogEntity> GetEntity(long id);
        Task SaveForm(AutoJobLogEntity entity);
        Task DeleteForm(string ids);
    }
}
