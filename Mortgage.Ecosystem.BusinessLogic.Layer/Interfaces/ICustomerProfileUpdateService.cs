using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ICustomerProfileUpdateService
    {
        Task<TData<List<CustomerProfileUpdateEntity>>> GetList(CustomerProfileUpdateListParam param);
        Task<TData<List<CustomerProfileUpdateEntity>>> GetPageList(CustomerProfileUpdateListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeCustomerProfileUpdateList(CustomerProfileUpdateListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CustomerProfileUpdateListParam param);
        //Task<TData<string>> UpdateCustomerProfile(CustomerProfileUpdateEntity entity);
        Task<TData<int>> GetMaxSort();
        Task<TData<CustomerDetailsViewModel>> GetCustomerDetails();
        Task<TData<string>> SaveForm(CustomerProfileUpdateEntity entity);
        Task<TData> DeleteForm(string ids);
        //Task<TData<CustomerProfileUpdateEntity>> GetEntity(int id);
        //Task<TData<EmployeeEntity>> GetEmployeeEntity(long id);
        Task<TData<CustomerProfileUpdateEntity>> GetEntity(long id);
    }
}
