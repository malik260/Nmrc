using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class AutoJobService : IAutoJobService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public AutoJobService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<AutoJobEntity>>> GetList(AutoJobListParam param)
        {
            TData<List<AutoJobEntity>> obj = new TData<List<AutoJobEntity>>();
            obj.Data = await _iUnitOfWork.AutoJobs.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AutoJobEntity>>> GetPageList(AutoJobListParam param, Pagination pagination)
        {
            TData<List<AutoJobEntity>> obj = new TData<List<AutoJobEntity>>();
            obj.Data = await _iUnitOfWork.AutoJobs.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<AutoJobEntity>> GetEntity(long id)
        {
            TData<AutoJobEntity> obj = new TData<AutoJobEntity>();
            obj.Data = await _iUnitOfWork.AutoJobs.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(AutoJobEntity entity)
        {
            TData<string> obj = new TData<string>();
            if (_iUnitOfWork.AutoJobs.ExistJob(entity))
            {
                obj.Message = "Mission already exists!";
                return obj;
            }
            await _iUnitOfWork.AutoJobs.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            foreach (long id in TextHelper.SplitToArray<long>(ids, ','))
            {
                AutoJobEntity dbEntity = await _iUnitOfWork.AutoJobs.GetEntity(id);
                if (dbEntity.JobStatus == StatusEnum.Yes.ParseToInt())
                {
                    obj.Message = "Please pause " + dbEntity.JobName + " timed task";
                    return obj;
                }
            }
            await _iUnitOfWork.AutoJobs.DeleteForm(ids);

            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> ChangeJobStatus(AutoJobEntity entity)
        {
            TData obj = new TData();
            await _iUnitOfWork.AutoJobs.SaveForm(entity);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Private method
        #endregion
    }
}