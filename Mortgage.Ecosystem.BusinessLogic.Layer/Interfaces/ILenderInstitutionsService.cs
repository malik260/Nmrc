using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ILenderInstitutionsService
    {
        Task<TData<List<NonNhf>>> GetNonNhfSecondaryLenders(PmbListParam param);
        Task<TData<List<NonNhf>>> GetNonNhfList(PmbListParam param);
        Task<TData<List<EmployeeEntity>>> GetPmbEmployee(EmployeeListParam param);
        Task<TData<List<LenderInstitutionsEntity>>> GetList(PmbListParam param);
        Task<TData<List<LenderInstitutionsEntity>>> GetPageList(PmbListParam param, Pagination pagination);
        //Task<TData<List<PmbEntity>>> GetApprovalPageList(PmbListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreePmbList(PmbListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(PmbListParam param);
        Task<TData<LenderInstitutionsEntity>> GetEntity(long id);
        Task<TData<string>> SaveForms(LenderInstitutionsEntity entity);
        Task<TData> DeleteForm(string ids);
        //Task<TData> ApproveForm(PmbEntity entity);
        Task<bool> IndividualExiting(CreateCustomerRequestDTO createCustomerRequestDTO);
        Task<bool> UpdateCustomer(CustomerUpdateRequestDTO customerUpdateRequestDTO);
        Task<bool> CustomerExist(string customerCode);
        Task<TData<List<LenderInstitutionsEntity>>> GetApprovalPageList(PmbListParam param, Pagination pagination);
        Task<TData> ApproveForm(LenderInstitutionsEntity entity);
        Task<TData<string>> SaveNewEmployee(EmployeeEntity entity);
        Task<TData> DisApproveForm(LenderInstitutionsEntity entity, string Remark);
    }
}
