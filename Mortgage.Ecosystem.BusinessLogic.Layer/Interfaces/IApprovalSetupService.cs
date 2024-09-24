using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IApprovalSetupService
    {
        Task<TData<string>> SaveForm3(ApprovalSetupEntity entity);
        Task<TData<List<ApprovalSetupEntity>>> GetList(ApprovalSetupListParam param);
        Task<TData<List<ApprovalSetupEntity>>> GetPageList(ApprovalSetupListParam param, Pagination pagination);
        Task<TData<ApprovalSetupEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(ApprovalSetupEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<string>> SaveForm2(ApprovalSetupEntity entity);
    }
}
