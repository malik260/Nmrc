using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IPmbService
    {
        Task<TData<List<NonNhf>>> GetNonNhfList(PmbListParam param);
        Task<TData<List<EmployeeEntity>>> GetPmbEmployee(EmployeeListParam param);
        Task<TData<List<PmbEntity>>> GetList(PmbListParam param);
        Task<TData<List<PmbEntity>>> GetPageList(PmbListParam param, Pagination pagination);
        //Task<TData<List<PmbEntity>>> GetApprovalPageList(PmbListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreePmbList(PmbListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(PmbListParam param);
        Task<TData<PmbEntity>> GetEntity(long id);
        Task<TData<string>> SaveForms(PmbEntity entity);
        Task<TData> DeleteForm(string ids);
        //Task<TData> ApproveForm(PmbEntity entity);
        Task<bool> IndividualExiting(CreateCustomerRequestDTO createCustomerRequestDTO);
        Task<bool> UpdateCustomer(CustomerUpdateRequestDTO customerUpdateRequestDTO);
        Task<bool> CustomerExist(string customerCode);
        Task<TData<List<PmbEntity>>> GetApprovalPageList(PmbListParam param, Pagination pagination);
        Task<TData> ApproveForm(PmbEntity entity);
        Task<TData<string>> SaveNewEmployee(EmployeeEntity entity);
        Task<TData> DisApproveForm(PmbEntity entity, string Remark);
    }
}
