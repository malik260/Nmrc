using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class NmrcCategoryService : INmrcCategoryService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public NmrcCategoryService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data

        public async Task<TData<List<NmrcCategoryEntity>>> GetList(NmrcCategoryListParam param)
        {
            TData<List<NmrcCategoryEntity>> obj = new TData<List<NmrcCategoryEntity>>();
            obj.Data = await _iUnitOfWork.NmrcCategories.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<NmrcCategoryEntity>>> GetPageList(NmrcCategoryListParam param, Pagination pagination)
        {
            TData<List<NmrcCategoryEntity>> obj = new TData<List<NmrcCategoryEntity>>();
            obj.Data = await _iUnitOfWork.NmrcCategories.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<NmrcCategoryEntity>> GetEntity(int id)
        {
            TData<NmrcCategoryEntity> obj = new TData<NmrcCategoryEntity>();
            obj.Data = await _iUnitOfWork.NmrcCategories.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        #endregion Retrieve data

        #region Submit data

        public async Task<TData<string>> SaveForm(NmrcCategoryEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.NmrcCategories.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.NmrcCategories.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        #endregion Submit data
    }
}