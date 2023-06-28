using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IAlertTypeRepository
    {
        Task<List<AlertTypeEntity>> GetList(AlertTypeListParam param);
        Task<List<AlertTypeEntity>> GetPageList(AlertTypeListParam param, Pagination pagination);
        Task<AlertTypeEntity> GetEntity(long id);
        Task SaveForm(AlertTypeEntity entity);
        Task DeleteForm(string ids);
    }
}
