using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IRefundService
    {
        Task<TData<List<RefundEntity>>> GetList(RefundListParam param);

        Task<TData<List<RefundEntity>>> GetPageList(RefundListParam param, Pagination pagination);

        Task<TData<List<ZtreeInfo>>> GetZtreeRefundList(RefundListParam param);

        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(RefundListParam param);

        Task<TData<CustomerDetailsViewModel>> GetCustomerDetails();

        Task<TData<RefundEntity>> GetEntity(long id);

        Task<TData<int>> GetMaxSort();

        Task<TData<string>> SaveForm(RefundEntity entity);

        Task<TData> DeleteForm(string ids);
    }
}