using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IApprovalLogService
    {
        Task<TData<List<ApprovalLogEntity>>> GetList(ApprovalLogListParam param);
        Task<TData<List<ApprovalLogEntity>>> GetPageList(ApprovalLogListParam param, Pagination pagination);
        Task<TData<ApprovalLogEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(ApprovalLogEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
