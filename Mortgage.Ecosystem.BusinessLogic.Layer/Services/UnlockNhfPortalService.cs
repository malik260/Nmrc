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
    public class UnlockNhfPortalService : IUnlockNhfPortalService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public UnlockNhfPortalService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<UnlockNhfPortalEntity>>> GetList(UnlockNhfPortalListParam param)
        {
            TData<List<UnlockNhfPortalEntity>> obj = new TData<List<UnlockNhfPortalEntity>>();
            obj.Data = await _iUnitOfWork.UnlockNhfPortals.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<UnlockNhfPortalEntity>>> GetPageList(UnlockNhfPortalListParam param, Pagination pagination)
        {
            TData<List<UnlockNhfPortalEntity>> obj = new TData<List<UnlockNhfPortalEntity>>();
            obj.Data = await _iUnitOfWork.UnlockNhfPortals.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUnlockNhfPortalList(UnlockNhfPortalListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<UnlockNhfPortalEntity> UnlockNhfPortalList = await _iUnitOfWork.UnlockNhfPortals.GetList(param);
            foreach (UnlockNhfPortalEntity UnlockNhfPortal in UnlockNhfPortalList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = UnlockNhfPortal.Id,                    
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(UnlockNhfPortalListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<UnlockNhfPortalEntity> UnlockNhfPortalList = await _iUnitOfWork.UnlockNhfPortals.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (UnlockNhfPortalEntity UnlockNhfPortal in UnlockNhfPortalList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = UnlockNhfPortal.Id,                    
                });
                List<long> userIdList = userList.Where(t => t.Company == UnlockNhfPortal.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<UnlockNhfPortalEntity>> GetEntity(long id)
        {
            TData<UnlockNhfPortalEntity> obj = new TData<UnlockNhfPortalEntity>();
            obj.Data = await _iUnitOfWork.UnlockNhfPortals.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.UnlockNhfPortals.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(UnlockNhfPortalEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.UnlockNhfPortals.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.UnlockNhfPortals.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
