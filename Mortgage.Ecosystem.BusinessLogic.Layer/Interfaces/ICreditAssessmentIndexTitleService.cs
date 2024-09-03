using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ICreditAssessmentIndexTitleService
    {
        //Task<TData<List<CreditAssessmentIndexTitleEntity>>> GetList(int factorIndexId);
        Task<List<CreditAssessmentIndexTitleEntity>> GetList(int factorIndexId);
        Task<TData<CreditAssessmentIndexTitleEntity>> GetEntity(long id);
        Task<TData<CreditAssessmentIndexTitleEntity>> GetEntities(int id);
        Task<TData<string>> SaveForm(CreditAssessmentIndexTitleEntity entity);
        Task<TData<string>> UpdateForm(CreditAssessmentIndexTitleEntity entity);
        Task<TData<List<CreditAssessmentIndexTitleEntity>>> GetPageList(CreditAssessmentIndexTitleListParam param, Pagination pagination);

        Task<TData> DeleteForm(string ids);
        Task<TData<List<CreditAssessmentIndexTitleEntity>>> GetIndexTitle(int FactorIndexId);
    }
}