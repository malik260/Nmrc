using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Caching;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public UserService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Get data
        public async Task<TData<List<UserEntity>>> GetList(UserListParam param)
        {
            TData<List<UserEntity>> obj = new TData<List<UserEntity>>();
            obj.Data = await _iUnitOfWork.Users.GetList(param);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<UserEntity>>> GetPageList(UserListParam param, Pagination pagination)
        {
            TData<List<UserEntity>> obj = new TData<List<UserEntity>>();
            if (param?.Company != null && param?.Company > 0)
            {
                param.UserIdList = await GetUserIdList(null, param.Company, param.Employee);
            }
            else
            {
                OperatorInfo user = await Operator.Instance.Current();
                param.UserIdList = await GetUserIdList(null, user.Company, user.Employee);
            }
            obj.Data = await _iUnitOfWork.Users.GetPageList(param, pagination);
            List<UserBelongEntity> userBelongList = await _iUnitOfWork.UserBelongs.GetList(new UserBelongEntity { UserIds = obj.Data.Select(p => p.Id).ToStrs<long>() });
            if (obj.Data.Count > 0)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                foreach (UserEntity user in obj.Data)
                {
                    user.CompanyName = companyList.Where(p => p.Id == user.Company).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<UserEntity>> GetEntity(int id)
        {
            TData<UserEntity> obj = new TData<UserEntity>();
            obj.Data = await _iUnitOfWork.Users.GetEntity(id);

            await GetUserBelong(obj.Data);

            if (obj.Data.Company > 0)
            {
                CompanyEntity companyEntity = await _iUnitOfWork.Companies.GetEntity(obj.Data.Company);
                if (companyEntity != null)
                {
                    obj.Data.CompanyName = companyEntity.Name;
                }
            }

            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<UserEntity>> CheckLogin(string userName, string password, int platform)
        {
            TData<UserEntity> obj = new TData<UserEntity>();
            if (userName.IsNull() || password.IsNull())
            {
                obj.Message = "Username or password cannot be empty";
                return obj;
            }
            UserEntity user = await _iUnitOfWork.Users.CheckLogin(userName);
            if (user != null)
            {
                if (user.UserStatus == (int)StatusEnum.Yes)
                {
                    if (user.Password == EncryptUserPassword(password, user.Salt))
                    {
                        #region Check for multiple logins
                        if (!GlobalContext.SystemConfig.LoginMultiple)
                        {
                            if (user.IsOnline == 1) // If the same user is not allowed to log in multiple times, when the user logs out, he/she is not online
                            {
                                obj.Message = "The user is already online.";
                                goto branch;
                            }
                        }
                        #endregion

                        user.LoginCount++;
                        user.IsOnline = 1;

                        #region Set date
                        if (user.FirstVisit == GlobalConstant.DefaultTime)
                        {
                            user.FirstVisit = DateTime.Now;
                        }
                        if (user.PreviousVisit == GlobalConstant.DefaultTime)
                        {
                            user.PreviousVisit = DateTime.Now;
                        }
                        if (user.LastVisit != GlobalConstant.DefaultTime)
                        {
                            user.PreviousVisit = user.LastVisit;
                        }
                        user.LastVisit = DateTime.Now;
                        #endregion

                        switch (platform)
                        {
                            case (int)PlatformEnum.Web:
                                #region Check the login token
                                if (string.IsNullOrEmpty(user.WebToken))
                                {
                                    user.WebToken = SecurityHelper.GetGuid(true);
                                }
                                #endregion                                                                
                                break;

                            case (int)PlatformEnum.WebApi:
                                user.ApiToken = SecurityHelper.GetGuid(true);
                                break;
                        }
                        await GetUserBelong(user);

                        obj.Data = user;
                        obj.Message = "Login successful";
                        obj.Tag = 1;
                    }
                    else
                    {
                        obj.Message = "The password is incorrect, please re-enter";
                    }
                }
                else
                {
                    obj.Message = "The account is disabled, please contact the administrator";
                }
            }
            else
            {
                obj.Message = "The account does not exist, please re-enter";
            }
        branch:
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(UserEntity entity)
        {
            TData<string> obj = new TData<string>();
            if (_iUnitOfWork.Users.ExistUserName(entity))
            {
                obj.Message = "Username already exists!";
                return obj;
            }
            if (entity.Id.IsNullOrZero())
            {
                entity.Salt = GetPasswordSalt();
                entity.Password = EncryptUserPassword(entity.Password, entity.Salt);
            }

            await _iUnitOfWork.Users.SaveForm(entity);

            await RemoveCacheById(entity.Id);

            obj.Data = entity.Id.ToStr();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            if (string.IsNullOrEmpty(ids))
            {
                obj.Message = "Parameter cannot be empty";
                return obj;
            }
            await _iUnitOfWork.Users.DeleteForm(ids);

            await RemoveCacheById(ids);

            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<long>> ResetPassword(UserEntity entity)
        {
            TData<long> obj = new TData<long>();
            if (entity.Id > 0)
            {
                UserEntity dbUserEntity = await _iUnitOfWork.Users.GetEntity(entity.Id);
                if (dbUserEntity.Password == entity.Password)
                {
                    obj.Message = "Password not changed";
                    return obj;
                }
                entity.Salt = GetPasswordSalt();
                entity.Password = EncryptUserPassword(entity.Password, entity.Salt);
                await _iUnitOfWork.Users.ResetPassword(entity);

                await RemoveCacheById(entity.Id);

                obj.Data = entity.Id;
                obj.Tag = 1;
            }
            return obj;
        }

        public async Task<TData<long>> ChangePassword(ChangePasswordParam param)
        {
            TData<long> obj = new TData<long>();
            if (param.Id > 0)
            {
                if (string.IsNullOrEmpty(param.Password) || string.IsNullOrEmpty(param.NewPassword))
                {
                    obj.Message = "New password cannot be empty";
                    return obj;
                }
                UserEntity dbUserEntity = await _iUnitOfWork.Users.GetEntity(param.Id);
                if (dbUserEntity.Password != EncryptUserPassword(param.Password, dbUserEntity.Salt))
                {
                    obj.Message = "The old password is incorrect";
                    return obj;
                }
                dbUserEntity.Salt = GetPasswordSalt();
                dbUserEntity.Password = EncryptUserPassword(param.NewPassword, dbUserEntity.Salt);
                await _iUnitOfWork.Users.ResetPassword(dbUserEntity);

                await RemoveCacheById(param.Id);

                obj.Data = dbUserEntity.Id;
                obj.Tag = 1;
            }
            return obj;
        }

        // Users modify their own information
        // <param name="entity"></param>
        // <returns></returns>
        public async Task<TData<long>> ChangeUser(UserEntity entity)
        {
            TData<long> obj = new TData<long>();
            if (entity.Id > 0)
            {
                await _iUnitOfWork.Users.ChangeUser(entity);

                await RemoveCacheById(entity.Id);

                obj.Data = entity.Id;
                obj.Tag = 1;
            }
            return obj;
        }

        public async Task<TData> UpdateUser(UserEntity entity)
        {
            TData obj = new TData();
            await _iUnitOfWork.Users.UpdateUser(entity);

            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> ImportUser(ImportParam param, List<UserEntity> list)
        {
            TData obj = new TData();
            if (list.Any())
            {
                foreach (UserEntity entity in list)
                {
                    UserEntity dbEntity = await _iUnitOfWork.Users.GetEntity(entity.UserName);
                    if (dbEntity != null)
                    {
                        entity.Id = dbEntity.Id;
                        if (param.IsOverride == 1)
                        {
                            await _iUnitOfWork.Users.SaveForm(entity);
                            await RemoveCacheById(entity.Id);
                        }
                    }
                    else
                    {
                        await _iUnitOfWork.Users.SaveForm(entity);
                        await RemoveCacheById(entity.Id);
                    }
                }
                obj.Tag = 1;
            }
            else
            {
                obj.Message = "Imported data not found";
            }
            return obj;
        }
        #endregion

        #region public method
        // Generate Default Password
        // <returns></returns>
        public string GenerateDefaultPassword()
        {
            return new Random().Next(1, 100000).ToString();
        }

        // Password MD5 processing
        // <param name="password"></param>
        // <param name="salt"></param>
        // <returns></returns>
        public string EncryptUserPassword(string password, string salt)
        {
            string md5Password = SecurityHelper.MD5ToHex(password);
            string encryptPassword = SecurityHelper.MD5ToHex(md5Password.ToLower() + salt).ToLower();
            return encryptPassword;
        }

        // password salt
        // <returns></returns>
        public string GetPasswordSalt()
        {
            return new Random().Next(1, 100000).ToString();
        }
        #endregion

        #region private method
        // Remove the token from the cache
        // <param name="id"></param>
        private async Task RemoveCacheById(string ids)
        {
            foreach (int id in ids.Split(',').Select(p => long.Parse(p)))
            {
                await RemoveCacheById(id);
            }
        }

        private async Task RemoveCacheById(int id)
        {
            var dbEntity = await _iUnitOfWork.Users.GetEntity(id);
            if (dbEntity != null)
            {
                CacheFactory.Cache.RemoveCache(dbEntity.WebToken);
            }
        }

        // Get the user's position and role
        // <param name="user"></param>
        private async Task GetUserBelong(UserEntity user)
        {
            List<UserBelongEntity> userBelongList = await _iUnitOfWork.UserBelongs.GetList(new UserBelongEntity { Employee = user.Employee });

            List<UserBelongEntity> roleBelongList = userBelongList.Where(p => p.BelongType == UserBelongTypeEnum.Role.ToInt()).ToList();
            if (roleBelongList.Count > 0)
            {
                user.RoleIds = string.Join(",", roleBelongList.Select(p => p.Belong).ToList());
            }

            List<UserBelongEntity> designationBelongList = userBelongList.Where(p => p.BelongType == UserBelongTypeEnum.Designation.ToInt()).ToList();
            if (designationBelongList.Count > 0)
            {
                user.DesignationIds = string.Join(",", designationBelongList.Select(p => p.Belong).ToList());
            }
        }
        #endregion

        #region public methods
        // Get the current user and all the following users
        // <param name="userList"></param>
        // <param name="userId"></param>
        // <returns></returns>
        public async Task<List<int>> GetUserIdList(List<UserEntity>? userList, long company, long employee)
        {
            if (userList == null)
            {
                userList = await _iUnitOfWork.Users.GetList(null);
            }
            List<int> userIdList = new List<int>();
            var userRecord = await _iUnitOfWork.Users.GetEntity(company, employee);
            if (userRecord != null)
            {
                userIdList.Add(userRecord.Id);
            }
            GetUserIdList(userList, company, userIdList);
            return userIdList;
        }
        #endregion

        #region private method
        // Get all sub-users under the user
        // <param name="userList"></param>
        // <param name="userId"></param>
        // <param name="userIdList"></param>
        private void GetUserIdList(List<UserEntity> userList, long company, List<int> userIdList)
        {
            var children = userList.Where(p => p.Company == company).Select(p => p.Id).ToList();
            if (children.Count > 0)
            {
                userIdList.AddRange(children);
                foreach (long id in children)
                {
                    GetUserIdList(userList, id, userIdList);
                }
            }
        }
        #endregion
    }
}