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
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ChangePasswordService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ChangePasswordEntity>>> GetList(ChangePasswordListParam param)
        {
            TData<List<ChangePasswordEntity>> obj = new TData<List<ChangePasswordEntity>>();
            obj.Data = await _iUnitOfWork.ChangePasswords.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ChangePasswordEntity>>> GetPageList(ChangePasswordListParam param, Pagination pagination)
        {
            TData<List<ChangePasswordEntity>> obj = new TData<List<ChangePasswordEntity>>();
            obj.Data = await _iUnitOfWork.ChangePasswords.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeChangePasswordList(ChangePasswordListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ChangePasswordEntity> changePasswordList = await _iUnitOfWork.ChangePasswords.GetList(param);
            foreach (ChangePasswordEntity changePassword in changePasswordList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = changePassword.Id,                    
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ChangePasswordListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ChangePasswordEntity> changePasswordList = await _iUnitOfWork.ChangePasswords.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ChangePasswordEntity changePassword in changePasswordList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = changePassword.Id,                  
                });
                List<long> userIdList = userList.Where(t => t.Company == changePassword.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<ChangePasswordEntity>> GetEntity(long id)
        {
            TData<ChangePasswordEntity> obj = new TData<ChangePasswordEntity>();
            obj.Data = await _iUnitOfWork.ChangePasswords.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.ChangePasswords.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ChangePasswordEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.ChangePasswords.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ChangePasswords.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
