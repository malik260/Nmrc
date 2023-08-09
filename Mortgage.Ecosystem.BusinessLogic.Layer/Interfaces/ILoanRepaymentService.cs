using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ILoanRepaymentService
    {
        Task<TData<List<LoanRepaymentEntity>>> GetList(LoanRepaymentListParam param);
        Task<TData<List<LoanRepaymentEntity>>> GetPageList(LoanRepaymentListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeLoanRepaymentList(LoanRepaymentListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(LoanRepaymentListParam param);
        Task<TData<LoanRepaymentEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(LoanRepaymentEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<RemitaPaymentDetailsEntity>> SingleLoanRepayment(LoanRepaymentDto entity);
    }
}
