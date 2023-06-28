using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class NationalityService : INationalityService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public NationalityService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<NationalityEntity>>> GetList(NationalityListParam param)
        {
            TData<List<NationalityEntity>> obj = new TData<List<NationalityEntity>>();
            obj.Data = await _iUnitOfWork.Nationalities.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<NationalityEntity>>> GetPageList(NationalityListParam param, Pagination pagination)
        {
            TData<List<NationalityEntity>> obj = new TData<List<NationalityEntity>>();
            obj.Data = await _iUnitOfWork.Nationalities.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<NationalityEntity>> GetEntity(long id)
        {
            TData<NationalityEntity> obj = new TData<NationalityEntity>();
            obj.Data = await _iUnitOfWork.Nationalities.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(NationalityEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Nationalities.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Nationalities.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
