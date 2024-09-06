using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ILoanReviewService
    {
        Task<TData<List<LoanReviewEntity>>> GetList(LoanReviewListParam param);
        Task<TData<List<LoanReviewEntity>>> GetPageList(LoanReviewListParam param, Pagination pagination);
        Task<TData<LoanReviewEntity>> GetEntity();
        Task<TData<string>> SaveForm(LoanReviewEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
