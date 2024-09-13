using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class NmrcActivityService: INmrcActivityService
    {

        private readonly IUnitOfWork _iUnitOfWork;

        public NmrcActivityService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data

        public async Task<TData<List<RefinancingEntity>>> GetList(RefinancingEntity param)
        {
            TData<List<RefinancingEntity>> obj = new TData<List<RefinancingEntity>>();
            obj.Data = await _iUnitOfWork.NmrcActivity.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RefinancingEntity>>> GetPageList(RefinancingEntity param, Pagination pagination)
        {
            TData<List<RefinancingEntity>> obj = new TData<List<RefinancingEntity>>();
            obj.Data = await _iUnitOfWork.NmrcActivity.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<RefinancingEntity>> GetEntity(long Id)
        {
            TData<RefinancingEntity> obj = new TData<RefinancingEntity>();
            obj.Data = await _iUnitOfWork.NmrcActivity.GetEntity(Id);
            obj.Tag = 1;
            return obj;
        }

        //public async Task<TData<RefinancingEntity>> GetEntity(int id)
        //{
        //    TData<RefinancingEntity> obj = new TData<RefinancingEntity>();
        //    obj.Data = await _iUnitOfWork.Refinancings.GetEntity(id);
        //    obj.Tag = 1;
        //    return obj;
        //}

        #endregion Retrieve data

        #region Submit data

        public async Task<TData<string>> SaveForm(RefinancingEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.NmrcActivity.SaveForm(entity);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Refinancings.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        #endregion Submit data



    }
}
