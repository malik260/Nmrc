using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class PaymentHistoryService : IPaymentHistoryService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public PaymentHistoryService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<PaymentHistoryEntity>>> GetList(PaymentHistoryListParam param)
        {
            TData<List<PaymentHistoryEntity>> obj = new TData<List<PaymentHistoryEntity>>();
            obj.Data = await _iUnitOfWork.PaymentHistories.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<PaymentHistoryEntity>>> GetPageList(PaymentHistoryListParam param, Pagination pagination)
        {
            TData<List<PaymentHistoryEntity>> obj = new TData<List<PaymentHistoryEntity>>();
            obj.Data = await _iUnitOfWork.PaymentHistories.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreePaymentHistoryList(PaymentHistoryListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<PaymentHistoryEntity> paymentHistoryList = await _iUnitOfWork.PaymentHistories.GetList(param);
            foreach (PaymentHistoryEntity paymentHistory in paymentHistoryList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = paymentHistory.Id,
                    name = paymentHistory.NHFNumber
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(PaymentHistoryListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<PaymentHistoryEntity> paymentHistoryList = await _iUnitOfWork.PaymentHistories.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (PaymentHistoryEntity paymentHistory in paymentHistoryList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = paymentHistory.Id,
                    name = paymentHistory.NHFNumber
                });
                List<long> userIdList = userList.Where(t => t.Company == paymentHistory.Id).Select(t => t.Employee).ToList();
                foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
                {
                    obj.Data.Add(new ZtreeInfo
                    {
                        id = user.Id,
                        name = user.RealName
                    });
                }
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<PaymentHistoryEntity>> GetEntity(long id)
        {
            TData<PaymentHistoryEntity> obj = new TData<PaymentHistoryEntity>();
            obj.Data = await _iUnitOfWork.PaymentHistories.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.PaymentHistories.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(PaymentHistoryEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.PaymentHistories.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.PaymentHistories.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
