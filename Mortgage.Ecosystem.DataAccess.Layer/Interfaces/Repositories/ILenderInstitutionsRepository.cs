using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ILenderInstitutionsRepository
    {
        Task SaveNmrcEmployee(EmployeeEntity entity);
        Task<LenderInstitutionsEntity> GetEntitybyName(string PMBName);
        Task<List<LenderInstitutionsEntity>> GetList(PmbListParam param);
        Task<List<LenderInstitutionsEntity>> GetPageList(PmbListParam param, Pagination pagination);
        //Task<List<PmbEntity>> GetApprovalPageList(PmbListParam param, Pagination pagination);
        Task<LenderInstitutionsEntity> GetEntity(long id);
        bool ExistPmb(LenderInstitutionsEntity entity);
        bool ExistRCNumber(LenderInstitutionsEntity entity);
        Task SaveForm(LenderInstitutionsEntity entity);
        Task<LenderInstitutionsEntity> GetEntitybyEmail(string email);
        Task SaveForms(LenderInstitutionsEntity entity);
        Task DeleteForm(string ids);
        Task<List<LenderInstitutionsEntity>> GetApprovalPageList(PmbListParam param, Pagination pagination);
        Task <bool> ApproveForm(LenderInstitutionsEntity entity, MenuEntity menu, OperatorInfo user);
        Task<LenderInstitutionsEntity> GetEntitybyNhf(string nhf);
        Task SaveNewEmployee(EmployeeEntity entity);
        Task<bool> DisApproveForm(LenderInstitutionsEntity entity);
    }
}
