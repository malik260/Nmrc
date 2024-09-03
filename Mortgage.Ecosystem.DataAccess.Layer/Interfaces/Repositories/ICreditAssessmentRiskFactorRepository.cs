using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICreditAssessmentRiskFactorRepository
    {
        Task<List<CreditAssessmentRiskFactorEntity>> GetList(string productcode);
         //Task<List<CreditAssessmentRiskFactor>> GetPageList(CreditAssessmentRiskFactorListParam param, Pagination pagination);
        Task<CreditAssessmentRiskFactorEntity> GetEntity(long id);
        //Task<int> GetMaxSort();
        Task SaveForm(CreditAssessmentRiskFactorEntity entity);
        Task DeleteForm(string ids);
        Task<List<CreditAssessmentRiskFactorEntity>> GetPageList(CreditAssessmentRiskFactorListParam param, Pagination pagination);
        Task<CreditAssessmentRiskFactorEntity> GetEntities(int id);
        Task<CreditAssessmentRiskFactorEntity> GetEntitiesByRiskId(int id);
    }
}