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
    public class RefundService : IRefundService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public RefundService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<RefundEntity>>> GetList(RefundListParam param)
        {
            TData<List<RefundEntity>> obj = new TData<List<RefundEntity>>();
            obj.Data = await _iUnitOfWork.Refunds.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RefundEntity>>> GetPageList(RefundListParam param, Pagination pagination)
        {
            TData<List<RefundEntity>> obj = new TData<List<RefundEntity>>();
            obj.Data = await _iUnitOfWork.Refunds.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeRefundList(RefundListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RefundEntity> refundList = await _iUnitOfWork.Refunds.GetList(param);
            foreach (RefundEntity refund in refundList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = refund.Id,
                    name = refund.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(RefundListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RefundEntity> refundList = await _iUnitOfWork.Refunds.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (RefundEntity refund in refundList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = refund.Id,
                    name = refund.Name
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

        public async Task<TData<RefundEntity>> GetEntity(long id)
        {
            TData<RefundEntity> obj = new TData<RefundEntity>();
            obj.Data = await _iUnitOfWork.Refunds.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.Refunds.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(RefundEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Refunds.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Refunds.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
