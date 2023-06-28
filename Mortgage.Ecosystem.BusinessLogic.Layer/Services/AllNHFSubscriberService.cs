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
    public class AllNHFSubscriberService : IAllNHFSubscriberService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public AllNHFSubscriberService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<AllNHFSubscriberEntity>>> GetList(AllNHFSubscriberListParam param)
        {
            TData<List<AllNHFSubscriberEntity>> obj = new TData<List<AllNHFSubscriberEntity>>();
            obj.Data = await _iUnitOfWork.AllNHFSubscribers.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AllNHFSubscriberEntity>>> GetPageList(AllNHFSubscriberListParam param, Pagination pagination)
        {
            TData<List<AllNHFSubscriberEntity>> obj = new TData<List<AllNHFSubscriberEntity>>();
            obj.Data = await _iUnitOfWork.AllNHFSubscribers.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

  

        public async Task<TData<List<ZtreeInfo>>> GetZtreeAllNHFSubscriberList(AllNHFSubscriberListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<AllNHFSubscriberEntity> allNHFSubscriberList = await _iUnitOfWork.AllNHFSubscribers.GetList(param);
            foreach (AllNHFSubscriberEntity allNHFSubscriber in allNHFSubscriberList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = allNHFSubscriber.Id,
                    name = allNHFSubscriber.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(AllNHFSubscriberListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<AllNHFSubscriberEntity> allNHFSubscriberList = await _iUnitOfWork.AllNHFSubscribers.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (AllNHFSubscriberEntity allNHFSubscriber in allNHFSubscriberList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = allNHFSubscriber.Id,
                    name = allNHFSubscriber.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == allNHFSubscriber.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<AllNHFSubscriberEntity>> GetEntity(long id)
        {
            TData<AllNHFSubscriberEntity> obj = new TData<AllNHFSubscriberEntity>();
            obj.Data = await _iUnitOfWork.AllNHFSubscribers.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.AllNHFSubscribers.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(AllNHFSubscriberEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.AllNHFSubscribers.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.AllNHFSubscribers.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
