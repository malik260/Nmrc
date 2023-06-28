using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class SectorService : ISectorService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public SectorService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<SectorEntity>>> GetList(SectorListParam param)
        {
            TData<List<SectorEntity>> obj = new TData<List<SectorEntity>>();
            obj.Data = await _iUnitOfWork.Sectors.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SectorEntity>>> GetPageList(SectorListParam param, Pagination pagination)
        {
            TData<List<SectorEntity>> obj = new TData<List<SectorEntity>>();
            obj.Data = await _iUnitOfWork.Sectors.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<SectorEntity>> GetEntity(long id)
        {
            TData<SectorEntity> obj = new TData<SectorEntity>();
            obj.Data = await _iUnitOfWork.Sectors.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(SectorEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Sectors.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Sectors.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
