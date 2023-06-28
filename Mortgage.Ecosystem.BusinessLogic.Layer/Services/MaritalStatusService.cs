using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class MaritalStatusService : IMaritalStatusService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public MaritalStatusService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<MaritalStatusEntity>>> GetList(MaritalStatusListParam param)
        {
            TData<List<MaritalStatusEntity>> obj = new TData<List<MaritalStatusEntity>>();
            obj.Data = await _iUnitOfWork.MaritalStatus.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<MaritalStatusEntity>>> GetPageList(MaritalStatusListParam param, Pagination pagination)
        {
            TData<List<MaritalStatusEntity>> obj = new TData<List<MaritalStatusEntity>>();
            obj.Data = await _iUnitOfWork.MaritalStatus.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<MaritalStatusEntity>> GetEntity(long id)
        {
            TData<MaritalStatusEntity> obj = new TData<MaritalStatusEntity>();
            obj.Data = await _iUnitOfWork.MaritalStatus.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(MaritalStatusEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.MaritalStatus.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.MaritalStatus.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
