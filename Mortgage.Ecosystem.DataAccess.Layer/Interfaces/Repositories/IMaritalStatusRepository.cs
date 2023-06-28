using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IMaritalStatusRepository
    {
        Task<List<MaritalStatusEntity>> GetList(MaritalStatusListParam param);
        Task<List<MaritalStatusEntity>> GetPageList(MaritalStatusListParam param, Pagination pagination);
        Task<MaritalStatusEntity> GetEntity(long id);
        Task SaveForm(MaritalStatusEntity entity);
        Task DeleteForm(string ids);
    }
}
