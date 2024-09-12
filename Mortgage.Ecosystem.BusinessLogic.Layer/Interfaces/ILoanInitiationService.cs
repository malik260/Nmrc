using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ILoanInitiationService
    {
        Task<TData<CustomerDetailsViewModel>> GetCustomerDetails2();
        Task<TData<List<LoanInitiationEntity>>> GetList(LoanInitiationListParam param);
        Task<TData<List<LoanInitiationEntity>>> GetPageList(LoanInitiationListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeLoanInitiationList(LoanInitiationListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(LoanInitiationListParam param);
        Task<TData<LoanInitiationEntity>> GetEntity();
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(LoanInitiationEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<AffordabilityResponseDto> Performaffordability(InitiateLoanDto initiateLoanDto);        Task<TData<LoanInitiationEntity>> LoanApplication(InitiateLoanDto initiateLoanDto); Task<TData<List<LoanApplications>>> GetLoans(string nhfNo);
        Task<TData<CustomerDetailsViewModel>> GetCustomerDetails();
        Task<TData<LoanInitiationEntity>> NonMortgageLoanApplication(InitiateLoanDto initiateLoanDto);
    }
}