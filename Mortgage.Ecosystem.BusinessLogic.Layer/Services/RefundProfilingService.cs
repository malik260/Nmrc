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
    public class RefundProfilingService : IRefundProfilingService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public RefundProfilingService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<RefundProfilingEntity>>> GetList(RefundProfilingListParam param)
        {
            TData<List<RefundProfilingEntity>> obj = new TData<List<RefundProfilingEntity>>();
            obj.Data = await _iUnitOfWork.RefundProfilings.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RefundProfilingEntity>>> GetPageList(RefundProfilingListParam param, Pagination pagination)
        {
            TData<List<RefundProfilingEntity>> obj = new TData<List<RefundProfilingEntity>>();
            obj.Data = await _iUnitOfWork.RefundProfilings.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeRefundProfilingList(RefundProfilingListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RefundProfilingEntity> refundProfilingList = await _iUnitOfWork.RefundProfilings.GetList(param);
            foreach (RefundProfilingEntity refundProfiling in refundProfilingList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = refundProfiling.Id,
                    name = refundProfiling.NhfNo
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(RefundProfilingListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RefundProfilingEntity> refundProfilingList = await _iUnitOfWork.RefundProfilings.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (RefundProfilingEntity refundProfiling in refundProfilingList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = refundProfiling.Id,
                    name = refundProfiling.NhfNo
                });
                List<long> userIdList = userList.Where(t => t.Company == refundProfiling.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<RefundProfilingEntity>> GetEntity(long id)
        {
            TData<RefundProfilingEntity> obj = new TData<RefundProfilingEntity>();
            obj.Data = await _iUnitOfWork.RefundProfilings.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.RefundProfilings.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(RefundProfilingEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.RefundProfilings.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.RefundProfilings.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
