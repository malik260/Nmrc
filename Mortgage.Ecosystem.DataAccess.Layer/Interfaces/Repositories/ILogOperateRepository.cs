using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ILogOperateRepository
    {
        Task<List<LogOperateEntity>> GetList(LogOperateListParam param);
        Task<List<LogOperateEntity>> GetPageList(LogOperateListParam param, Pagination pagination);
        Task<LogOperateEntity> GetEntity(long id);
        Task SaveForm(LogOperateEntity entity);
        Task DeleteForm(string ids);
        Task RemoveAllForm();
    }
}
