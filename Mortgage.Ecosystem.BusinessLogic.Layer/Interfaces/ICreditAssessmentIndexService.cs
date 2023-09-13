using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ICreditAssessmentIndexService
    {
        Task<TData<List<CreditAssessmentIndexEntity>>> GetList(int indexTitleId);
        Task<TData<CreditAssessmentIndexEntity>> GetEntity(long id);        
        Task<TData<string>> SaveForm(CreditAssessmentIndexEntity entity);
        Task<TData<string>> UpdateForm(CreditAssessmentIndexEntity entity);

        Task<TData> DeleteForm(string ids);
       
    }
}