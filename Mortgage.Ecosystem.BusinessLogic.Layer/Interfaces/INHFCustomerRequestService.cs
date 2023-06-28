using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface INHFCustomerRequestService
    {
        Task<TData<List<NHFCustomerRequestEntity>>> GetList(NHFCustomerRequestListParam param);
        Task<TData<List<NHFCustomerRequestEntity>>> GetPageList(NHFCustomerRequestListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeNHFCustomerRequestList(NHFCustomerRequestListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(NHFCustomerRequestListParam param);
        Task<TData<NHFCustomerRequestEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(NHFCustomerRequestEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
