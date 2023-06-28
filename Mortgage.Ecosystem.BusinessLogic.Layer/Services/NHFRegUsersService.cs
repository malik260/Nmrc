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
    public class NHFRegUsersService : INHFRegUsersService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public NHFRegUsersService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<NHFRegUsersEntity>>> GetList(NHFRegUsersListParam param)
        {
            TData<List<NHFRegUsersEntity>> obj = new TData<List<NHFRegUsersEntity>>();
            obj.Data = await _iUnitOfWork.NHFRegUsers.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<NHFRegUsersEntity>>> GetPageList(NHFRegUsersListParam param, Pagination pagination)
        {
            TData<List<NHFRegUsersEntity>> obj = new TData<List<NHFRegUsersEntity>>();
            obj.Data = await _iUnitOfWork.NHFRegUsers.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeNHFRegUsersList(NHFRegUsersListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<NHFRegUsersEntity> nhfRegUsersList = await _iUnitOfWork.NHFRegUsers.GetList(param);
            foreach (NHFRegUsersEntity nhfRegUsers in nhfRegUsersList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = nhfRegUsers.Id,
                    name = nhfRegUsers.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(NHFRegUsersListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<NHFRegUsersEntity> nhfRegUsersList = await _iUnitOfWork.NHFRegUsers.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (NHFRegUsersEntity nhfRegUsers in nhfRegUsersList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = nhfRegUsers.Id,
                    name = nhfRegUsers.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == nhfRegUsers.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<NHFRegUsersEntity>> GetEntity(long id)
        {
            TData<NHFRegUsersEntity> obj = new TData<NHFRegUsersEntity>();
            obj.Data = await _iUnitOfWork.NHFRegUsers.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.NHFRegUsers.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(NHFRegUsersEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.NHFRegUsers.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.NHFRegUsers.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
