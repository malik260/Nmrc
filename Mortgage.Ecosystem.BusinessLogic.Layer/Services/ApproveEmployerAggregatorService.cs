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
    public class ApproveEmployerAggregatorService : IApproveEmployerAggregatorService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ApproveEmployerAggregatorService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ApproveEmployerAggregatorEntity>>> GetList(ApproveEmployerAggregatorListParam param)
        {
            TData<List<ApproveEmployerAggregatorEntity>> obj = new TData<List<ApproveEmployerAggregatorEntity>>();
            obj.Data = await _iUnitOfWork.ApproveEmployerAggregators.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ApproveEmployerAggregatorEntity>>> GetPageList(ApproveEmployerAggregatorListParam param, Pagination pagination)
        {
            TData<List<ApproveEmployerAggregatorEntity>> obj = new TData<List<ApproveEmployerAggregatorEntity>>();
            obj.Data = await _iUnitOfWork.ApproveEmployerAggregators.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeApproveEmployerAggregatorList(ApproveEmployerAggregatorListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ApproveEmployerAggregatorEntity> approveEmployerAggregatorList = await _iUnitOfWork.ApproveEmployerAggregators.GetList(param);
            foreach (ApproveEmployerAggregatorEntity approveEmployerAggregator in approveEmployerAggregatorList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = approveEmployerAggregator.Id,
                    name = approveEmployerAggregator.EmployerName
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ApproveEmployerAggregatorListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ApproveEmployerAggregatorEntity> approveEmployerAggregatorList = await _iUnitOfWork.ApproveEmployerAggregators.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ApproveEmployerAggregatorEntity approveEmployerAggregator in approveEmployerAggregatorList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = approveEmployerAggregator.Id,
                    name = approveEmployerAggregator.EmployerName
                });
                List<long> userIdList = userList.Where(t => t.Company == approveEmployerAggregator.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<ApproveEmployerAggregatorEntity>> GetEntity(long id)
        {
            TData<ApproveEmployerAggregatorEntity> obj = new TData<ApproveEmployerAggregatorEntity>();
            obj.Data = await _iUnitOfWork.ApproveEmployerAggregators.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.ApproveEmployerAggregators.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ApproveEmployerAggregatorEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.ApproveEmployerAggregators.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ApproveEmployerAggregators.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
