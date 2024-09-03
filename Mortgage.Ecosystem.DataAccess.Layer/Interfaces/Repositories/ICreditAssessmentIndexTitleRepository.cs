using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICreditAssessmentIndexTitleRepository
    {
        Task<List<CreditAssessmentIndexTitleEntity>> GetList(int factorIndexId);
        Task<CreditAssessmentIndexTitleEntity> GetEntity(long id);
        Task<CreditAssessmentIndexTitleEntity> GetEntities(int id);
        //Task<int> GetMaxSort();
        Task SaveForm(CreditAssessmentIndexTitleEntity entity);
        Task<CreditAssessmentIndexTitleEntity> GetEntityByIndextitleId(int id);
        Task DeleteForm(string ids);
        Task<List<CreditAssessmentIndexTitleEntity>> GetPageList(CreditAssessmentIndexTitleListParam param, Pagination pagination);
        Task<List<CreditAssessmentIndexTitleEntity>> GetListbyProduct(string productcode);
    }
}