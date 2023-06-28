using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class CompanyTypeService : ICompanyTypeService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CompanyTypeService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<CompanyTypeEntity>>> GetList(CompanyTypeListParam param)
        {
            TData<List<CompanyTypeEntity>> obj = new TData<List<CompanyTypeEntity>>();
            obj.Data = await _iUnitOfWork.CompanyTypes.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CompanyTypeEntity>>> GetPageList(CompanyTypeListParam param, Pagination pagination)
        {
            TData<List<CompanyTypeEntity>> obj = new TData<List<CompanyTypeEntity>>();
            obj.Data = await _iUnitOfWork.CompanyTypes.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<CompanyTypeEntity>> GetEntity(long id)
        {
            TData<CompanyTypeEntity> obj = new TData<CompanyTypeEntity>();
            obj.Data = await _iUnitOfWork.CompanyTypes.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(CompanyTypeEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.CompanyTypes.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.CompanyTypes.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
