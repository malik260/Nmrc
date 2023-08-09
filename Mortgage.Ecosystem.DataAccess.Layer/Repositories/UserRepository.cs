using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class UserRepository : DataRepository, IUserRepository
    {
        #region get data

        public async Task<List<UserEntity>> GetList(UserListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<UserEntity>> GetPageList(UserListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<UserEntity> GetEntity(int id)
        {
            return await BaseRepository().FindEntity<UserEntity>(id);
        }

        public async Task<UserEntity> GetEntity(string userName)
        {
            return await BaseRepository().FindEntity<UserEntity>(p => p.UserName == userName);
        }

        public async Task<UserEntity> GetEntity(long company, long employee)
        {
            return await BaseRepository().FindEntity<UserEntity>(p => p.Company == company && p.Employee == employee);
        }

        public async Task<UserEntity> CheckLogin(string userName)
        {
            var expression = ExtensionLinq.True<UserEntity>();
            expression = expression.And(t => t.UserName == userName);
            //expression = expression.Or(t => t.Mobile == userName);
            //expression = expression.Or(t => t.Email == userName);
            return await BaseRepository().FindEntity(expression);
        }

        public bool ExistUserName(UserEntity entity)
        {
            var expression = ExtensionLinq.True<UserEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.UserName == entity.UserName);
            }
            else
            {
                expression = expression.And(t => t.UserName == entity.UserName && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        public bool CheckUserName(string? userName)
        {
            var expression = ExtensionLinq.True<UserEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (!userName.IsNullOrZero())
            {
                expression = expression.And(t => t.UserName == userName);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        #endregion get data

        #region Submit data

        public async Task UpdateUser(UserEntity entity)
        {
            await BaseRepository().Update(entity);
        }

        public async Task SaveForm(UserEntity entity)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await db.Delete<UserBelongEntity>(t => t.Employee == entity.Id);

                    // The password is not updated, there is a separate method to update the password
                    entity.Password = null;
                    await entity.Modify();
                    await db.Update(entity);
                }
                // Designation
                //if (!string.IsNullOrEmpty(entity.DesignationIds))
                //{
                //    foreach (long positionId in TextHelper.SplitToArray<long>(entity.DesignationIds, ','))
                //    {
                //        UserBelongEntity positionBelongEntity = new UserBelongEntity();
                //        positionBelongEntity.User = entity.Id;
                //        positionBelongEntity.Belong = positionId;
                //        positionBelongEntity.BelongType = UserBelongTypeEnum.Designation.ToInt();
                //        await positionBelongEntity.Create();
                //        await db.Insert(positionBelongEntity);
                //    }
                //}
                // Role
                if (!string.IsNullOrEmpty(entity.RoleIds))
                {
                    foreach (long roleId in TextHelper.SplitToArray<long>(entity.RoleIds, ','))
                    {
                        UserBelongEntity departmentBelongEntity = new UserBelongEntity();
                        departmentBelongEntity.Employee = entity.Id;
                        departmentBelongEntity.Belong = roleId;
                        departmentBelongEntity.BelongType = UserBelongTypeEnum.Role.ToInt();
                        await departmentBelongEntity.Create();
                        await db.Insert(departmentBelongEntity);
                    }
                }
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }

        public async Task DeleteForm(string ids)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
                await db.Delete<UserEntity>(idArr);
                await db.Delete<UserBelongEntity>(t => idArr.Contains(t.Employee));
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }

        public async Task ResetPassword(UserEntity entity)
        {
            await entity.Modify();
            await BaseRepository().Update(entity);
        }

        public async Task ChangeUser(UserEntity entity)
        {
            await entity.Modify();
            await BaseRepository().Update(entity);
        }

        #endregion Submit data

        #region private method

        private Expression<Func<UserEntity, bool>> ListFilter(UserListParam param)
        {
            var expression = ExtensionLinq.True<UserEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.UserName))
                {
                    expression = expression.And(t => t.UserName.Contains(param.UserName));
                }
                if (!string.IsNullOrEmpty(param.UserIds))
                {
                    long[] userIdList = TextHelper.SplitToArray<long>(param.UserIds, ',');
                    expression = expression.And(t => userIdList.Contains(t.Id));
                }
                if (param.UserStatus > 0)
                {
                    expression = expression.And(t => t.UserStatus == param.UserStatus);
                }
                if (!string.IsNullOrEmpty(param.StartTime.ToStr()))
                {
                    expression = expression.And(t => t.BaseModifyTime >= param.StartTime);
                }
                if (!string.IsNullOrEmpty(param.EndTime.ToStr()))
                {
                    param.EndTime = param.EndTime.Value.Date.Add(new TimeSpan(23, 59, 59));
                    expression = expression.And(t => t.BaseModifyTime <= param.EndTime);
                }
                if (param.UserIdList != null)
                {
                    expression = expression.And(t => param.UserIdList.Contains(t.Id));
                }
            }
            return expression;
        }

        #endregion private method
    }
}