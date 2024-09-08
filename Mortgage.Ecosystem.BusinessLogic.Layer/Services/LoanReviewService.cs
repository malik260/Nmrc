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
    public class LoanReviewService : ILoanReviewService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public LoanReviewService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data

        public async Task<TData<List<LoanReviewEntity>>> GetList(LoanReviewListParam param)
        {
            TData<List<LoanReviewEntity>> obj = new TData<List<LoanReviewEntity>>();
            obj.Data = await _iUnitOfWork.LoanReviews.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LoanReviewEntity>>> GetPageList(LoanReviewListParam param, Pagination pagination)
        {
            TData<List<LoanReviewEntity>> obj = new TData<List<LoanReviewEntity>>();
            obj.Data = await _iUnitOfWork.LoanReviews.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<LoanReviewEntity>> GetEntity()
        {
            var user = await Operator.Instance.Current();
            TData<LoanReviewEntity> obj = new TData<LoanReviewEntity>();
            obj.Data = await _iUnitOfWork.LoanReviews.GetEntity(Convert.ToString(user.EmployeeInfo.NHFNumber));
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

        public async Task<TData<string>> SaveForm(LoanReviewEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.LoanReviews.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.LoanReviews.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        #endregion Submit data
    }
}