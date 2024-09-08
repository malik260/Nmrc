using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IDisbursementRepository
    {
        Task<List<DisbursementEntity>> GetList(DisbursementListParam param);
        Task<List<DisbursementEntity>> GetPageList(DisbursementListParam param, Pagination pagination);
        Task<DisbursementEntity> GetEntity(string code);
        Task<int> GetMaxSort();
        Task SaveForm(DisbursementEntity entity);
        Task DeleteForm(string ids);
        Task<DisbursementEntity> GetEntityById(long id);
    }
}
