using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IRiskAssessmentSetupRepository
    {
        Task<List<RiskAssessmentSetupEntity>> GetList(RiskAssessmentSetupListParam param);
        Task<List<RiskAssessmentSetupEntity>> GetPageList(RiskAssessmentSetupListParam param, Pagination pagination);
        Task<RiskAssessmentSetupEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(RiskAssessmentSetupEntity entity);
        Task DeleteForm(string ids);
    }
}
