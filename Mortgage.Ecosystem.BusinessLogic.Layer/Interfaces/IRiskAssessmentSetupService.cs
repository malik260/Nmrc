using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IRiskAssessmentSetupService
    {
        Task<TData<List<RiskAssessmentSetupEntity>>> GetList(RiskAssessmentSetupListParam param);
        Task<TData<List<RiskAssessmentSetupEntity>>> GetPageList(RiskAssessmentSetupListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeRiskAssessmentSetupList(RiskAssessmentSetupListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(RiskAssessmentSetupListParam param);
        Task<TData<RiskAssessmentSetupEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(RiskAssessmentSetupEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
