using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ILogLoginRepository
    {
        Task<List<LogLoginEntity>> GetList(LogLoginListParam param);
        Task<List<LogLoginEntity>> GetPageList(LogLoginListParam param, Pagination pagination);
        Task<LogLoginEntity> GetEntity(long id);
        Task SaveForm(LogLoginEntity entity);
        Task DeleteForm(string ids);
        Task RemoveAllForm();
    }
}
