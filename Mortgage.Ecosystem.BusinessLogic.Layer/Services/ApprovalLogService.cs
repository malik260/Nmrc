using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class ApprovalLogService : IApprovalLogService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ApprovalLogService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ApprovalLogEntity>>> GetList(ApprovalLogListParam param)
        {
            TData<List<ApprovalLogEntity>> obj = new TData<List<ApprovalLogEntity>>();
            obj.Data = await _iUnitOfWork.ApprovalLogs.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ApprovalLogEntity>>> GetPageList(ApprovalLogListParam param, Pagination pagination)
        {
            TData<List<ApprovalLogEntity>> obj = new TData<List<ApprovalLogEntity>>();
            obj.Data = await _iUnitOfWork.ApprovalLogs.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ApprovalLogEntity>> GetEntity(long id)
        {
            TData<ApprovalLogEntity> obj = new TData<ApprovalLogEntity>();
            obj.Data = await _iUnitOfWork.ApprovalLogs.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ApprovalLogEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.ApprovalLogs.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ApprovalLogs.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
