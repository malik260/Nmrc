using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICreditAssessmentIndexTitleRepository
    {
        Task<List<CreditAssessmentIndexTitleEntity>> GetList(int factorIndexId);
        Task<CreditAssessmentIndexTitleEntity> GetEntity(long id);
        //Task<int> GetMaxSort();
        Task SaveForm(CreditAssessmentIndexTitleEntity entity);
        Task DeleteForm(string ids);
    }
}