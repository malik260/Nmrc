using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IPmbRepository
    {
        Task<PmbEntity> GetEntitybyName(string PMBName);
        Task<List<PmbEntity>> GetList(PmbListParam param);
        Task<List<PmbEntity>> GetPageList(PmbListParam param, Pagination pagination);
        //Task<List<PmbEntity>> GetApprovalPageList(PmbListParam param, Pagination pagination);
        Task<PmbEntity> GetEntity(long id);
        bool ExistPmb(PmbEntity entity);
        bool ExistRCNumber(PmbEntity entity);
        Task SaveForm(PmbEntity entity);
        Task<PmbEntity> GetEntitybyEmail(string email);
        Task SaveForms(PmbEntity entity);
        Task DeleteForm(string ids);
        Task<List<PmbEntity>> GetApprovalPageList(PmbListParam param, Pagination pagination);
        Task <bool> ApproveForm(PmbEntity entity, MenuEntity menu, OperatorInfo user);
        Task<PmbEntity> GetEntitybyNhf(string nhf);
        Task SaveNewEmployee(EmployeeEntity entity);
        Task<bool> DisApproveForm(PmbEntity entity);
    }
}
