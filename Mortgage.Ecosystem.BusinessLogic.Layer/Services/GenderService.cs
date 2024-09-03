using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class GenderService : IGenderService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public GenderService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data

        public async Task<TData<List<GenderEntity>>> GetList(GenderListParam param)
        {
            TData<List<GenderEntity>> obj = new TData<List<GenderEntity>>();
            obj.Data = await _iUnitOfWork.Genders.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<GenderEntity>>> GetPageList(GenderListParam param, Pagination pagination)
        {
            TData<List<GenderEntity>> obj = new TData<List<GenderEntity>>();
            obj.Data = await _iUnitOfWork.Genders.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<GenderEntity>> GetEntity(int id)
        {
            TData<GenderEntity> obj = new TData<GenderEntity>();
            obj.Data = await _iUnitOfWork.Genders.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        #endregion Retrieve data

        #region Submit data

        public async Task<TData<string>> SaveForm(GenderEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Genders.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Genders.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        #endregion Submit data
    }
}