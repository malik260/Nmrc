using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ICreditAssessmentFactorIndexService
    {
        Task<List<CreditAssessmentFactorIndexEntity>> GetList(int riskFactorId);
        Task<TData<CreditAssessmentFactorIndexEntity>> GetEntity(long id);        
        Task<TData<string>> SaveForm(CreditAssessmentFactorIndexEntity entity);
        Task<TData<string>> UpdateForm(CreditAssessmentFactorIndexEntity entity);

        Task<TData> DeleteForm(string ids);
        Task<TData<List<CreditAssessmentFactorIndexEntity>>> GetFactorIndex(int riskFactorId);
        Task<TData<List<CreditAssessmentFactorIndexEntity>>> GetPageList(CreditAssessmentFactorIndexListParam param, Pagination pagination);

        Task<TData<CreditAssessmentFactorIndexEntity>> GetEntities(int id);
    }
}