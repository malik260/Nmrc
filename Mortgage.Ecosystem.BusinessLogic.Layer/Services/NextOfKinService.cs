using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class NextOfKinService : INextOfKinService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public NextOfKinService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<NextOfKinEntity>>> GetList(NextOfKinListParam param)
        {
            TData<List<NextOfKinEntity>> obj = new TData<List<NextOfKinEntity>>();
            obj.Data = await _iUnitOfWork.NextOfKins.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<NextOfKinEntity>>> GetPageList(NextOfKinListParam param, Pagination pagination)
        {
            TData<List<NextOfKinEntity>> obj = new TData<List<NextOfKinEntity>>();
            obj.Data = await _iUnitOfWork.NextOfKins.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<NextOfKinEntity>> GetEntity(long id)
        {
            TData<NextOfKinEntity> obj = new TData<NextOfKinEntity>();
            obj.Data = await _iUnitOfWork.NextOfKins.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(NextOfKinEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.NextOfKins.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.NextOfKins.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
