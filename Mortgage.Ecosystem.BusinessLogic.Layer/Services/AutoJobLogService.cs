using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class AutoJobLogService : IAutoJobLogService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public AutoJobLogService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<AutoJobLogEntity>>> GetList(AutoJobLogListParam param)
        {
            TData<List<AutoJobLogEntity>> obj = new TData<List<AutoJobLogEntity>>();
            obj.Data = await _iUnitOfWork.AutoJobLogs.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AutoJobLogEntity>>> GetPageList(AutoJobLogListParam param, Pagination pagination)
        {
            TData<List<AutoJobLogEntity>> obj = new TData<List<AutoJobLogEntity>>();
            obj.Data = await _iUnitOfWork.AutoJobLogs.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<AutoJobLogEntity>> GetEntity(long id)
        {
            TData<AutoJobLogEntity> obj = new TData<AutoJobLogEntity>();
            obj.Data = await _iUnitOfWork.AutoJobLogs.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(AutoJobLogEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.AutoJobLogs.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.AutoJobLogs.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Private method
        #endregion
    }
}