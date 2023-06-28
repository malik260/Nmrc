using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IAllNHFSubscriberService
    {
        Task<TData<List<AllNHFSubscriberEntity>>> GetList(AllNHFSubscriberListParam param);
        Task<TData<List<AllNHFSubscriberEntity>>>GetPageList(AllNHFSubscriberListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeAllNHFSubscriberList(AllNHFSubscriberListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(AllNHFSubscriberListParam param);
        Task<TData<AllNHFSubscriberEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(AllNHFSubscriberEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
