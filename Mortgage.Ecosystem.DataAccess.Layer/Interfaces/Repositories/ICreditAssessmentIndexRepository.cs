using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICreditAssessmentIndexRepository
    {
        Task<List<CreditAssessmentIndexEntity>> GetList(int indexTitleId);
        Task<CreditAssessmentIndexEntity> GetEntity(long id);
        //Task<int> GetMaxSort();
        Task SaveForm(CreditAssessmentIndexEntity entity);
        Task DeleteForm(string ids);
        Task<List<CreditAssessmentIndexEntity>> GetPageList(CreditAssessmentIndexListParam param, Pagination pagination);
    }
}