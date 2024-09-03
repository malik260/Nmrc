using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IAuditTrailRepository
    {
        Task<List<AuditTrailEntity>> GetList(AuditTrailListParam param);
        Task<List<AuditTrailEntity>> GetPageList(AuditTrailListParam param, Pagination pagination);
        Task<AuditTrailEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(AuditTrailEntity entity);

        Task SaveForms(List<AuditTrailEntity> entity);
        Task DeleteForm(string ids);
    }
}