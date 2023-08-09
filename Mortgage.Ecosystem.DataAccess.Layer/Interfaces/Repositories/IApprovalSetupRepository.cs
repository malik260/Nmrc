using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IApprovalSetupRepository
    {
        Task<List<ApprovalSetupEntity>> GetList(ApprovalSetupListParam param);
        Task<List<ApprovalSetupEntity>> GetPageList(ApprovalSetupListParam param, Pagination pagination);
        Task<ApprovalSetupEntity> GetEntity(long id);
        Task SaveForm(ApprovalSetupEntity entity);
        Task DeleteForm(string ids);
    }
}
