using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ISecondaryLenderRepository
    {
        Task<SecondaryLenderEntity> GetEntitybyName(string SecondaryLenderName);
        Task<List<SecondaryLenderEntity>> GetList(SecondaryLenderListParam param);
        Task<List<SecondaryLenderEntity>> GetPageList(SecondaryLenderListParam param, Pagination pagination);
        //Task<List<PmbEntity>> GetApprovalPageList(PmbListParam param, Pagination pagination);
        Task<SecondaryLenderEntity> GetEntity(long id);
        bool ExistSecondaryLender(SecondaryLenderEntity entity);
        bool ExistRCNumber(SecondaryLenderEntity entity);
        Task SaveForm(SecondaryLenderEntity entity);
        Task<SecondaryLenderEntity> GetEntitybyEmail(string email);
        Task SaveForms(SecondaryLenderEntity entity);
        Task DeleteForm(string ids);
        Task<List<SecondaryLenderEntity>> GetApprovalPageList(SecondaryLenderListParam param, Pagination pagination);
        Task <bool> ApproveForm(SecondaryLenderEntity entity, MenuEntity menu, OperatorInfo user);
        Task<SecondaryLenderEntity> GetEntitybyNhf(string nhf);
        Task SaveNewEmployee(EmployeeEntity entity);
        Task<bool> DisApproveForm(SecondaryLenderEntity entity);
    }
}
