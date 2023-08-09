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
    public class RemitaPaymentDetailsService : IRemitaPaymentDetailsService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public RemitaPaymentDetailsService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<RemitaPaymentDetailsEntity>>> GetList(RemitaPaymentDetailsListParam param)
        {
            TData<List<RemitaPaymentDetailsEntity>> obj = new TData<List<RemitaPaymentDetailsEntity>>();
            obj.Data = await _iUnitOfWork.RemitaPaymentDetails.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RemitaPaymentDetailsEntity>>> GetPageList(RemitaPaymentDetailsListParam param, Pagination pagination)
        {
            TData<List<RemitaPaymentDetailsEntity>> obj = new TData<List<RemitaPaymentDetailsEntity>>();
            obj.Data = await _iUnitOfWork.RemitaPaymentDetails.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeRemitaPaymentDetailsList(RemitaPaymentDetailsListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RemitaPaymentDetailsEntity> remitaPaymentDetailsList = await _iUnitOfWork.RemitaPaymentDetails.GetList(param);
            foreach (RemitaPaymentDetailsEntity remitaPaymentDetails in remitaPaymentDetailsList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = remitaPaymentDetails.Id,
                    name = remitaPaymentDetails.TransactionId
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(RemitaPaymentDetailsListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RemitaPaymentDetailsEntity> refundList = await _iUnitOfWork.RemitaPaymentDetails.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (RemitaPaymentDetailsEntity refund in refundList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = refund.Id,
                    name = refund.TransactionId
                });
                List<long> userIdList = userList.Where(t => t.Company == refund.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<RemitaPaymentDetailsEntity>> GetEntity(long id)
        {
            TData<RemitaPaymentDetailsEntity> obj = new TData<RemitaPaymentDetailsEntity>();
            obj.Data = await _iUnitOfWork.RemitaPaymentDetails.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.RemitaPaymentDetails.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(RemitaPaymentDetailsEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.RemitaPaymentDetails.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.RemitaPaymentDetails.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
