using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public DesignationService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<DesignationEntity>>> GetList(DesignationListParam param)
        {
            TData<List<DesignationEntity>> obj = new TData<List<DesignationEntity>>();
            obj.Data = await _iUnitOfWork.Designations.GetList(param);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<DesignationEntity>>> GetPageList(DesignationListParam param, Pagination pagination)
        {
            TData<List<DesignationEntity>> obj = new TData<List<DesignationEntity>>();
            obj.Data = await _iUnitOfWork.Designations.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<DesignationEntity>> GetEntity(long id)
        {
            TData<DesignationEntity> obj = new TData<DesignationEntity>();
            obj.Data = await _iUnitOfWork.Designations.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.Designations.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(DesignationEntity entity)
        {
            TData<string> obj = new TData<string>();
            if (_iUnitOfWork.Designations.ExistDesignationName(entity))
            {
                obj.Message = "Job title already exists!";
                return obj;
            }
            await _iUnitOfWork.Designations.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await _iUnitOfWork.Designations.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}