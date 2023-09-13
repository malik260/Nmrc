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
    public class UnlockAdminUserService : IUnlockAdminUserService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public UnlockAdminUserService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<UnlockAdminUserEntity>>> GetList(UnlockAdminUserListParam param)
        {
            TData<List<UnlockAdminUserEntity>> obj = new TData<List<UnlockAdminUserEntity>>();
            obj.Data = await _iUnitOfWork.UnlockAdminUsers.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<UnlockAdminUserEntity>>> GetPageList(UnlockAdminUserListParam param, Pagination pagination)
        {
            TData<List<UnlockAdminUserEntity>> obj = new TData<List<UnlockAdminUserEntity>>();
            obj.Data = await _iUnitOfWork.UnlockAdminUsers.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUnlockAdminUserList(UnlockAdminUserListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<UnlockAdminUserEntity> UnlockAdminUserList = await _iUnitOfWork.UnlockAdminUsers.GetList(param);
            foreach (UnlockAdminUserEntity UnlockAdminUser in UnlockAdminUserList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = UnlockAdminUser.Id,                    
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(UnlockAdminUserListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<UnlockAdminUserEntity> UnlockAdminUserList = await _iUnitOfWork.UnlockAdminUsers.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (UnlockAdminUserEntity UnlockAdminUser in UnlockAdminUserList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = UnlockAdminUser.Id,                    
                });
                List<long> userIdList = userList.Where(t => t.Company == UnlockAdminUser.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<UnlockAdminUserEntity>> GetEntity(long id)
        {
            TData<UnlockAdminUserEntity> obj = new TData<UnlockAdminUserEntity>();
            obj.Data = await _iUnitOfWork.UnlockAdminUsers.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.UnlockAdminUsers.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(UnlockAdminUserEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.UnlockAdminUsers.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.UnlockAdminUsers.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
