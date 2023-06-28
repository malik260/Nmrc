using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class CompanyClassService : ICompanyClassService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CompanyClassService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<CompanyClassEntity>>> GetList(CompanyClassListParam param)
        {
            TData<List<CompanyClassEntity>> obj = new TData<List<CompanyClassEntity>>();
            obj.Data = await _iUnitOfWork.CompanyClasses.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CompanyClassEntity>>> GetPageList(CompanyClassListParam param, Pagination pagination)
        {
            TData<List<CompanyClassEntity>> obj = new TData<List<CompanyClassEntity>>();
            obj.Data = await _iUnitOfWork.CompanyClasses.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<CompanyClassEntity>> GetEntity(long id)
        {
            TData<CompanyClassEntity> obj = new TData<CompanyClassEntity>();
            obj.Data = await _iUnitOfWork.CompanyClasses.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(CompanyClassEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.CompanyClasses.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.CompanyClasses.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
