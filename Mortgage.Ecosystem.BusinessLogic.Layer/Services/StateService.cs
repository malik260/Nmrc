using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class StateService : IStateService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public StateService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<StateEntity>>> GetList(StateListParam param)
        {
            TData<List<StateEntity>> obj = new TData<List<StateEntity>>();
            obj.Data = await _iUnitOfWork.States.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<StateEntity>>> GetPageList(StateListParam param, Pagination pagination)
        {
            TData<List<StateEntity>> obj = new TData<List<StateEntity>>();
            obj.Data = await _iUnitOfWork.States.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<StateEntity>> GetEntity(long id)
        {
            TData<StateEntity> obj = new TData<StateEntity>();
            obj.Data = await _iUnitOfWork.States.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(StateEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.States.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.States.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
