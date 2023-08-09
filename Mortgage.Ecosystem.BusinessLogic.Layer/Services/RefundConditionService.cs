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
    public class RefundConditionService : IRefundConditionService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public RefundConditionService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<RefundConditionEntity>>> GetList(RefundConditionListParam param)
        {
            TData<List<RefundConditionEntity>> obj = new TData<List<RefundConditionEntity>>();
            obj.Data = await _iUnitOfWork.RefundConditions.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RefundConditionEntity>>> GetPageList(RefundConditionListParam param, Pagination pagination)
        {
            TData<List<RefundConditionEntity>> obj = new TData<List<RefundConditionEntity>>();
            obj.Data = await _iUnitOfWork.RefundConditions.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeRefundConditionList(RefundConditionListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RefundConditionEntity> refundConditionList = await _iUnitOfWork.RefundConditions.GetList(param);
            foreach (RefundConditionEntity refundCondition in refundConditionList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = refundCondition.Id,
                    name = refundCondition.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(RefundConditionListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RefundConditionEntity> refundConditionList = await _iUnitOfWork.RefundConditions.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (RefundConditionEntity refundCondition in refundConditionList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = refundCondition.Id,
                    name = refundCondition.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == refundCondition.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<RefundConditionEntity>> GetEntity(long id)
        {
            TData<RefundConditionEntity> obj = new TData<RefundConditionEntity>();
            obj.Data = await _iUnitOfWork.RefundConditions.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.RefundConditions.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(RefundConditionEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.RefundConditions.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.RefundConditions.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
