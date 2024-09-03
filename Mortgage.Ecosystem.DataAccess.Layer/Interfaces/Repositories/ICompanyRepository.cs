using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<CompanyEntity> GetByName(string CompanyName);
        Task<List<CompanyEntity>> GetList(CompanyListParam param);
        Task<List<CompanyEntity>> GetPageList(CompanyListParam param, Pagination pagination);
        Task<List<CompanyEntity>> GetApprovalPageList(CompanyListParam param, Pagination pagination);
        Task<CompanyEntity> GetEntity(long id);
        Task<CompanyEntity> GetById(long id);
        bool ExistCompany(CompanyEntity entity);
        bool ExistRCNumber(CompanyEntity entity);
        Task SaveForm(CompanyEntity entity);
        Task SaveForms(CompanyEntity entity);
        Task DeleteForm(string ids);
        Task<bool> ApproveForm(CompanyEntity entity, MenuEntity menu, OperatorInfo user);
        Task<List<CompanyEntity>> GetPageList2(CompanyListParam param, Pagination pagination);
        Task<bool> DisApproveForm(CompanyEntity entity, OperatorInfo user);
    }
}
