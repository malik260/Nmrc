using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class TitleService : ITitleService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public TitleService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<TitleEntity>>> GetList(TitleListParam param)
        {
            TData<List<TitleEntity>> obj = new TData<List<TitleEntity>>();
            obj.Data = await _iUnitOfWork.Titles.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<TitleEntity>>> GetPageList(TitleListParam param, Pagination pagination)
        {
            TData<List<TitleEntity>> obj = new TData<List<TitleEntity>>();
            obj.Data = await _iUnitOfWork.Titles.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<TitleEntity>> GetEntity(long id)
        {
            TData<TitleEntity> obj = new TData<TitleEntity>();
            obj.Data = await _iUnitOfWork.Titles.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(TitleEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Titles.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Titles.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
