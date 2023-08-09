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
    public class DiasporaUserService : IDiasporaUserService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public DiasporaUserService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<DiasporaUserEntity>>> GetList(DiasporaUserListParam param)
        {
            TData<List<DiasporaUserEntity>> obj = new TData<List<DiasporaUserEntity>>();
            obj.Data = await _iUnitOfWork.DiasporaUsers.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<DiasporaUserEntity>>> GetPageList(DiasporaUserListParam param, Pagination pagination)
        {
            TData<List<DiasporaUserEntity>> obj = new TData<List<DiasporaUserEntity>>();
            obj.Data = await _iUnitOfWork.DiasporaUsers.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeDiasporaUserList(DiasporaUserListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<DiasporaUserEntity> DiasporaUserList = await _iUnitOfWork.DiasporaUsers.GetList(param);
            foreach (DiasporaUserEntity DiasporaUser in DiasporaUserList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = DiasporaUser.Id,                    
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(DiasporaUserListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<DiasporaUserEntity> DiasporaUserList = await _iUnitOfWork.DiasporaUsers.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (DiasporaUserEntity DiasporaUser in DiasporaUserList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = DiasporaUser.Id,                    
                });
                List<long> userIdList = userList.Where(t => t.Company == DiasporaUser.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<DiasporaUserEntity>> GetEntity(long id)
        {
            TData<DiasporaUserEntity> obj = new TData<DiasporaUserEntity>();
            obj.Data = await _iUnitOfWork.DiasporaUsers.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.DiasporaUsers.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(DiasporaUserEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.DiasporaUsers.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.DiasporaUsers.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
