using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeEntity>> GetList(EmployeeListParam param);
        Task<List<EmployeeEntity>> GetPageList(EmployeeListParam param, Pagination pagination);
        Task<List<EmployeeEntity>> GetApprovalPageList(EmployeeListParam param, Pagination pagination);
        Task<EmployeeEntity> GetEntity(long id);
        Task<EmployeeEntity> GetById(long id);
        bool ExistEmployee(EmployeeEntity entity);
        bool IsEmployeeNHFNumberExist(EmployeeListParam param);
        bool ExistEmployeeBVN(EmployeeEntity entity);
        long GenerateNHFNumber();
        Task SaveForm(EmployeeEntity entity);
        Task SaveForms(EmployeeEntity entity);
        Task DeleteForm(string ids);
        Task ApproveForm(EmployeeEntity entity, MenuEntity menu, OperatorInfo user);
    }
}
