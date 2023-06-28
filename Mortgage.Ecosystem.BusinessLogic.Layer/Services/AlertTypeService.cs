using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class AlertTypeService : IAlertTypeService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public AlertTypeService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<AlertTypeEntity>>> GetList(AlertTypeListParam param)
        {
            TData<List<AlertTypeEntity>> obj = new TData<List<AlertTypeEntity>>();
            obj.Data = await _iUnitOfWork.AlertTypes.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AlertTypeEntity>>> GetPageList(AlertTypeListParam param, Pagination pagination)
        {
            TData<List<AlertTypeEntity>> obj = new TData<List<AlertTypeEntity>>();
            obj.Data = await _iUnitOfWork.AlertTypes.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<AlertTypeEntity>> GetEntity(long id)
        {
            TData<AlertTypeEntity> obj = new TData<AlertTypeEntity>();
            obj.Data = await _iUnitOfWork.AlertTypes.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(AlertTypeEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.AlertTypes.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.AlertTypes.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
