using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class DisbursementService : IDisbursementService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public DisbursementService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data

        public async Task<TData<List<DisbursementEntity>>> GetList(DisbursementListParam param)
        {
            TData<List<DisbursementEntity>> obj = new TData<List<DisbursementEntity>>();
            obj.Data = await _iUnitOfWork.Disbursements.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<DisbursementEntity>>> GetPageList(DisbursementListParam param, Pagination pagination)
        {
            TData<List<DisbursementEntity>> obj = new TData<List<DisbursementEntity>>();
            obj.Data = await _iUnitOfWork.Disbursements.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<DisbursementEntity>> GetEntity()
        {
            var user = await Operator.Instance.Current();
            TData<DisbursementEntity> obj = new TData<DisbursementEntity>();
            obj.Data = await _iUnitOfWork.Disbursements.GetEntity(Convert.ToString(user.EmployeeInfo.NHFNumber));
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

        public async Task<TData<string>> SaveForm(DisbursementEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Disbursements.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Disbursements.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        #endregion Submit data
    }
}