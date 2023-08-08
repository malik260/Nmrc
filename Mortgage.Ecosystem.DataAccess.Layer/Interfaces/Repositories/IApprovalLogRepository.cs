using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IApprovalLogRepository
    {
        Task<List<ApprovalLogEntity>> GetList(ApprovalLogListParam param);
        Task<List<ApprovalLogEntity>> GetPageList(ApprovalLogListParam param, Pagination pagination);
        Task<ApprovalLogEntity> GetEntity(long id);
        Task SaveForm(ApprovalLogEntity entity);
        Task DeleteForm(string ids);
    }
}
