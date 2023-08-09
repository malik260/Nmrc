using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICreditAssessmentFactorIndexRepository
    {
        Task<List<CreditAssessmentFactorIndexEntity>> GetList(int riskFactorId);
        Task<CreditAssessmentFactorIndexEntity> GetEntity(long id);
        //Task<int> GetMaxSort();
        Task SaveForm(CreditAssessmentFactorIndexEntity entity);
        Task DeleteForm(string ids);
    }
}