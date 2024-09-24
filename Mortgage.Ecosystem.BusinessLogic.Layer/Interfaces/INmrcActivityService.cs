using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface INmrcActivityService
    {
        Task<TData<string>> RejectLoanReview(long Id);
        Task<TData<List<SecondaryLenderChecklistProcedureEntity>>> GetPmbChecklist(long id);
        Task<TData<string>> DisburseNonNhfLoan(long Id);
        Task<TData<List<NmrcRefinancingEntity>>> GetLoanForDisbursment();
        Task<TData<string>> ApproveLoanReview(long Id);
        Task<TData<List<NmrcRefinancingEntity>>> GetLoanForReview();
        Task<TData<List<RefinancingEntity>>> GetListByBatchId(RefinancingEntity param);
        Task<TData<List<RefinancingEntity>>> GetList(RefinancingEntity param);
        Task<TData<List<NmrcRefinancingEntity>>> GetPageList(NmrcRefinancingEntity param, Pagination pagination);
        Task<TData<RefinancingEntity>> GetEntity(long Id);
        Task<TData<string>> SaveForm(RefinancingEntity entity);
        Task<TData<string>> ApproveUnderwriting(long id);

    }
}
