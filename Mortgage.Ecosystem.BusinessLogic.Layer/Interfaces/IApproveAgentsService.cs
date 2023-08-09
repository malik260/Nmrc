using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IApproveAgentsService
    {
        Task<TData<List<ApproveAgentsEntity>>> GetList(ApproveAgentsListParam param);
        Task<TData<List<ApproveAgentsEntity>>> GetPageList(ApproveAgentsListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeApproveAgentsList(ApproveAgentsListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ApproveAgentsListParam param);
        Task<TData<ApproveAgentsEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(ApproveAgentsEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
