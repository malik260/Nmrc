using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class RiskAssessmentSetupService : IRiskAssessmentSetupService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public RiskAssessmentSetupService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<RiskAssessmentSetupEntity>>> GetList(RiskAssessmentSetupListParam param)
        {
            TData<List<RiskAssessmentSetupEntity>> obj = new TData<List<RiskAssessmentSetupEntity>>();
            obj.Data = await _iUnitOfWork.RiskAssessmentSetups.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RiskAssessmentSetupEntity>>> GetPageList(RiskAssessmentSetupListParam param, Pagination pagination)
        {
            TData<List<RiskAssessmentSetupEntity>> obj = new TData<List<RiskAssessmentSetupEntity>>();
            obj.Data = await _iUnitOfWork.RiskAssessmentSetups.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeRiskAssessmentSetupList(RiskAssessmentSetupListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RiskAssessmentSetupEntity> riskAssessmentSetupList = await _iUnitOfWork.RiskAssessmentSetups.GetList(param);
            foreach (RiskAssessmentSetupEntity riskAssessmentSetup in riskAssessmentSetupList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = riskAssessmentSetup.Id,
                    name = riskAssessmentSetup.AssessmentFactors
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(RiskAssessmentSetupListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RiskAssessmentSetupEntity> riskAssessmentSetupList = await _iUnitOfWork.RiskAssessmentSetups.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (RiskAssessmentSetupEntity riskAssessmentSetup in riskAssessmentSetupList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = riskAssessmentSetup.Id,
                    name = riskAssessmentSetup.AssessmentFactors
                });
                List<long> userIdList = userList.Where(t => t.Company == riskAssessmentSetup.Id).Select(t => t.Employee).ToList();
                foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
                {
                    obj.Data.Add(new ZtreeInfo
                    {
                        id = user.Id,
                        name = user.RealName
                    });
                }
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<RiskAssessmentSetupEntity>> GetEntity(long id)
        {
            TData<RiskAssessmentSetupEntity> obj = new TData<RiskAssessmentSetupEntity>();
            obj.Data = await _iUnitOfWork.RiskAssessmentSetups.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.RiskAssessmentSetups.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(RiskAssessmentSetupEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.RiskAssessmentSetups.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.RiskAssessmentSetups.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
