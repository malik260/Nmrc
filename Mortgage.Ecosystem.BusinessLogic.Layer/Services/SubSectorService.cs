using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class SubSectorService : ISubSectorService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public SubSectorService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<SubSectorEntity>>> GetList(SubSectorListParam param)
        {
            TData<List<SubSectorEntity>> obj = new TData<List<SubSectorEntity>>();
            obj.Data = await _iUnitOfWork.SubSectors.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SubSectorEntity>>> GetPageList(SubSectorListParam param, Pagination pagination)
        {
            TData<List<SubSectorEntity>> obj = new TData<List<SubSectorEntity>>();
            obj.Data = await _iUnitOfWork.SubSectors.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<SubSectorEntity>> GetEntity(long id)
        {
            TData<SubSectorEntity> obj = new TData<SubSectorEntity>();
            obj.Data = await _iUnitOfWork.SubSectors.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(SubSectorEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.SubSectors.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.SubSectors.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
