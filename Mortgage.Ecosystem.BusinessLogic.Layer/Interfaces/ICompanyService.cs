using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ICompanyService
    {
        Task<TData<List<CompanyEntity>>> GetList(CompanyListParam param);
        Task<TData<List<CompanyEntity>>> GetPageList(CompanyListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeCompanyList(CompanyListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CompanyListParam param);
        Task<TData<CompanyEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(CompanyEntity entity);
        Task<TData<string>> SaveForms(CompanyEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<bool> IndividualExiting(CreateCustomerRequestDTO createCustomerRequestDTO);
        Task<bool> UpdateCustomer(CustomerUpdateRequestDTO customerUpdateRequestDTO);
        Task<bool> CustomerExist(string customerCode);
    }
}
