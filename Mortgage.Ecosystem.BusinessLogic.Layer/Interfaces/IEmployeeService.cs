using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IEmployeeService
    {
        Task<TData<List<EmployeeEntity>>> GetList(EmployeeListParam param);
        Task<TData<List<EmployeeEntity>>> GetPageList(EmployeeListParam param, Pagination pagination);
        Task<TData<List<EmployeeEntity>>> GetApprovalPageList(EmployeeListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeEmployeeList(EmployeeListParam param);
        Task<TData<EmployeeEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(EmployeeEntity entity);
        Task<TData<string>> SaveForms(EmployeeEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<string>> ApproveForm(EmployeeEntity entity);
    }
}
