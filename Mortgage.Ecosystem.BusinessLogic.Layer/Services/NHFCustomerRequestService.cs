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
    public class NHFCustomerRequestService : INHFCustomerRequestService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public NHFCustomerRequestService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<NHFCustomerRequestEntity>>> GetList(NHFCustomerRequestListParam param)
        {
            TData<List<NHFCustomerRequestEntity>> obj = new TData<List<NHFCustomerRequestEntity>>();
            obj.Data = await _iUnitOfWork.NHFCustomerRequests.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<NHFCustomerRequestEntity>>> GetPageList(NHFCustomerRequestListParam param, Pagination pagination)
        {
            TData<List<NHFCustomerRequestEntity>> obj = new TData<List<NHFCustomerRequestEntity>>();
            obj.Data = await _iUnitOfWork.NHFCustomerRequests.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeNHFCustomerRequestList(NHFCustomerRequestListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<NHFCustomerRequestEntity> nhfCustomerRequestList = await _iUnitOfWork.NHFCustomerRequests.GetList(param);
            foreach (NHFCustomerRequestEntity nhfCustomerRequest in nhfCustomerRequestList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = nhfCustomerRequest.Id,
                    name = nhfCustomerRequest.AccountNumber
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(NHFCustomerRequestListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<NHFCustomerRequestEntity> nhfCustomerRequestList = await _iUnitOfWork.NHFCustomerRequests.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (NHFCustomerRequestEntity nhfCustomerRequest in nhfCustomerRequestList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = nhfCustomerRequest.Id,
                    name = nhfCustomerRequest.AccountNumber
                });
                List<long> userIdList = userList.Where(t => t.Company == nhfCustomerRequest.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<NHFCustomerRequestEntity>> GetEntity(long id)
        {
            TData<NHFCustomerRequestEntity> obj = new TData<NHFCustomerRequestEntity>();
            obj.Data = await _iUnitOfWork.NHFCustomerRequests.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.NHFCustomerRequests.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(NHFCustomerRequestEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.NHFCustomerRequests.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.NHFCustomerRequests.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
