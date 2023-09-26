using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ICreditAssessmentRiskFactorService
    {
        Task<List<CreditAssessmentRiskFactorEntity>> GetList(string productcode);        //Task<TData<List<CreditAssessmentRiskFactor>>> GetPageList(CreditAssessmentRiskFactorListParam param, Pagination pagination);
        //Task<TData<List<ZtreeInfo>>> GetZtreeCreditAssesstmentRiskFactorList(CreditAssessmentRiskFactorListParam param);
        //Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CreditAssessmentRiskFactorListParam param);
        Task<TData<List<CreditAssessmentRiskFactorEntity>>> Getrisks(string productcode);
        Task<TData<CreditAssessmentRiskFactorEntity>> GetEntity(long id);
        //Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(CreditAssessmentRiskFactorEntity entity);
        Task<TData<string>> UpdateForm(CreditAssessmentRiskFactorEntity entity);

        Task<TData> DeleteForm(string ids);
       
    }
}