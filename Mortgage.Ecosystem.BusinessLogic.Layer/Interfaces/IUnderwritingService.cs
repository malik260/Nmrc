using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IUnderwritingService
    {
        Task<TData<string>> DisburseNonNhfLoan(long Id);
        Task<TData<List<UnderwritingEntity>>> GetLoanForDisbursment();
        Task<TData<string>> RejectLoanUnderwriting(long Id, string remark);
        Task<TData<string>> DisapproveUnderwriting(long id);
        Task<TData<string>> ApproveUnderwriting(long id);
        Task<TData<List<UnderwritingEntity>>> GetList(UnderwritingListParam param);
        Task<TData<List<UnderwritingEntity>>> GetPageList(UnderwritingListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeUnderwritingList(UnderwritingListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(UnderwritingListParam param);
        Task<TData<UnderwritingEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(UnderwritingEntity entity);
        Task<TData> DeleteForm(string ids);
        //Task<TData<string>> ProceedLoan(string NHFNumber);
        Task<TData<List<AffordabilityDetails>>> PerformAffordability(string NHFNumber);
        Task<TData<List<UnderwritingEntity>>> GetApprovalPageList();
        Task<TData<string>> ApproveLoanReview(long NHFNumber);
        Task<TData<List<UnderwritingEntity>>> GetLoanForReview();
        Task<TData<string>> RejectLoanReview(long Id, string remark);
        Task<TData<List<UnderwritingEntity>>> GetLoanForUnderwriting();
        Task<TData<List<UnderwritingEntity>>> GetLoanForBatching();
        Task<TData<string>> batchLoan(string lists);
        Task<TData<string>> UnbatchLoan(string lists);
        Task<TData<List<UnderwritingEntity>>> GetBatched();
        Task<TData<List<UnderwritingEntity>>> GetBatchedLoans(long id);
        Task<TData<string>> ApproveBatchedLoan(string lists);
        Task<TData<List<UnderwritingEntity>>> GetLists(long id);

    }
}
