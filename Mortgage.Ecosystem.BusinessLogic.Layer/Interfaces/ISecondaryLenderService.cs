using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ISecondaryLenderService
    {
        Task<TData<List<EmployeeEntity>>> GetSecondaryLenderEmployee(EmployeeListParam param);
        Task<TData<List<SecondaryLenderEntity>>> GetList(SecondaryLenderListParam param);
        Task<TData<List<SecondaryLenderEntity>>> GetPageList(SecondaryLenderListParam param, Pagination pagination);
        //Task<TData<List<PmbEntity>>> GetApprovalPageList(PmbListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeSecondaryLenderList(SecondaryLenderListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(SecondaryLenderListParam param);
        Task<TData<SecondaryLenderEntity>> GetEntity(long id);
        Task<TData<string>> SaveForms(SecondaryLenderEntity entity);
        Task<TData> DeleteForm(string ids);
        //Task<TData> ApproveForm(PmbEntity entity);
        Task<bool> IndividualExiting(CreateCustomerRequestDTO createCustomerRequestDTO);
        Task<bool> UpdateCustomer(CustomerUpdateRequestDTO customerUpdateRequestDTO);
        Task<bool> CustomerExist(string customerCode);
        Task<TData<List<SecondaryLenderEntity>>> GetApprovalPageList(SecondaryLenderListParam param, Pagination pagination);
        Task<TData> ApproveForm(SecondaryLenderEntity entity);
        Task<TData<string>> SaveNewEmployee(EmployeeEntity entity);
        Task<TData> DisApproveForm(SecondaryLenderEntity entity, string Remark);
    }
}
