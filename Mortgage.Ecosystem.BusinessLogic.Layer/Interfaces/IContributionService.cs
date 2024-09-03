using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IContributionService
    {
        Task<TData<List<ContributionEntity>>> GetEmployerList(ContributionListParam param, Pagination pagination);
        Task<TData<List<ContributionEntity>>> GetList(ContributionListParam param, Pagination pagination);
        Task<TData<List<ContributionEntity>>> GetPageList(ContributionListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeSingleContributionList(ContributionListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ContributionListParam param);
        Task<TData<List<ContributionEntity>>> GetList2(ContributionListParam param, Pagination pagination);
        Task<TData<ContributionEntity>> GetEntity(long id);
        Task<TData<RemitaPaymentDetailsEntity>> EmployerSingleContribution(ContributionEntity entity);
        Task<TData<int>> GetMaxSort();
        Task<TData<EmployeeDetailsVM>> GetCustomerDetails();
        Task<TData<List<EmployeeEntity>>> GetEmployees(long companyId);
        Task<TData<EmployeeDetailsVM>> GetEmployerDetails();
        Task<TData<RemitaPaymentDetailsEntity>> SingleContribution(ContributionEntity entity);
        Task<TData<BacklogSingleContributionResultVM>> BacklogSingleContribution(BacklogUploadVM entity);
        Task<TData<BatchContributionResultVM>> BatchContribution(BatchUploadVM entity);
        Task<TData<string>> SaveForm(ContributionEntity entity);
        Task<TData> DeleteForm(string ids);
        
    }
}
