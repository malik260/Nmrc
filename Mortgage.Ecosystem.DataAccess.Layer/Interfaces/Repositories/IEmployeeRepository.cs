using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeEntity> GetEmployeeByNIN(string NIN);
        Task<EmployeeEntity> GetEmployeeByBVN(string BVN);
        Task<EmployeeEntity> GetEmployeeByMobile(string MobileNumber);
        Task<EmployeeEntity> GetEmployeeByEmail(string emailAddress);
        Task<List<EmployeeEntity>> GetList(EmployeeListParam param);
        Task<List<EmployeeEntity>> GetListByCompany(EmployeeListParam param);
        Task<List<EmployeeEntity>> GetPageList(EmployeeListParam param, Pagination pagination);
        Task<List<EmployeeEntity>> GetApprovalPageList(EmployeeListParam param, Pagination pagination);
        Task<EmployeeEntity> GetEntity(long id);
        Task<EmployeeEntity> GetEntityByNhfNumber(long nhfNo);
        Task<List<EmployeeEntity>> GetLists();
        Task<EmployeeEntity> GetById(long id);
        bool ExistEmployee(EmployeeEntity entity);
        bool IsEmployeeNHFNumberExist(EmployeeListParam param);
        bool ExistEmployeeBVN(EmployeeEntity entity);
        long GenerateNHFNumber();
        Task SaveForm(EmployeeEntity entity);
        Task SaveForms(EmployeeEntity entity);
        Task DeleteForm(string ids);
       Task<bool> ApproveForm(EmployeeEntity entity, MenuEntity menu, OperatorInfo user, UserEntity loginProfile);
        Task RejectForm(EmployeeEntity entity);
    }
}