using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IFeedBackFormService
    {
        Task<TData<List<FeedBackFormEntity>>> GetList(FeedBackFormListParam param);
        Task<TData<List<FeedBackFormEntity>>> GetPageList(FeedBackFormListParam param, Pagination pagination);
        Task<TData<List<FeedBackFormEntity>>> GetEmployeePageList(FeedBackFormListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeFeedBackFormList(FeedBackFormListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(FeedBackFormListParam param);
        Task<TData<FeedBackFormEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<EmployeeDetailsVM>> GetCustomerDetails();
        Task<TData<string>> SaveForm(FeedBackFormEntity entity);
        Task<TData<string>> SaveForms(FeedBackFormEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
