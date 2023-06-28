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
    public class RoleRepository : DataRepository, IRoleRepository
    {
        #region Get data
        public async Task<List<RoleEntity>> GetList(RoleListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<RoleEntity>> GetPageList(RoleListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<RoleEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<RoleEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(RoleSort) FROM tbl_Role");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        public bool ExistRoleName(RoleEntity entity)
        {
            var expression = ExtensionLinq.True<RoleEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Company == entity.Company && t.RoleName == entity.RoleName);
            }
            else
            {
                expression = expression.And(t => t.Company == entity.Company && t.RoleName == entity.RoleName && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(RoleEntity entity)
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
                    await db.Delete<MenuAuthorizeEntity>(t => t.AuthorizeId == entity.Id);
                    await entity.Modify();
                    await db.Update(entity);
                }
                // Menu, page and button permissions corresponding to roles
                if (!string.IsNullOrEmpty(entity.MenuIds))
                {
                    foreach (long menuId in TextHelper.SplitToArray<long>(entity.MenuIds, ','))
                    {
                        MenuAuthorizeEntity menuAuthorizeEntity = new()
                        {
                            AuthorizeId = entity.Id,
                            MenuId = menuId,
                            AuthorizeType = AuthorizeTypeEnum.Role.ToInt()
                        };
                        await menuAuthorizeEntity.Create();
                        await db.Insert(menuAuthorizeEntity);
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
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<RoleEntity>(idArr);
        }
        #endregion

        #region private method
        private static Expression<Func<RoleEntity, bool>> ListFilter(RoleListParam param)
        {
            var expression = ExtensionLinq.True<RoleEntity>();
            if (param != null)
            {
                if (!param.Company.IsNullOrZero() && !string.IsNullOrEmpty(param.RoleName))
                {
                    expression = expression.And(t => t.Company == param.Company && t.RoleName.Contains(param.RoleName));
                }
                if (!param.Company.IsNullOrZero() && !string.IsNullOrEmpty(param.RoleIds))
                {
                    long[] roleIdArr = TextHelper.SplitToArray<long>(param.RoleIds, ',');
                    expression = expression.And(t => roleIdArr.Contains(t.Id));
                }
                if (!param.Company.IsNullOrZero() && param.RoleStatus > -1)
                {
                    expression = expression.And(t => t.Company == param.Company && t.RoleStatus == param.RoleStatus);
                }
                if (!param.Company.IsNullOrZero() && !string.IsNullOrEmpty(param.StartTime.ToStr()))
                {
                    expression = expression.And(t => t.Company == param.Company && t.BaseModifyTime >= param.StartTime);
                }
                if (!param.Company.IsNullOrZero() && !string.IsNullOrEmpty(param.EndTime.ToStr()))
                {
                    param.EndTime = param.EndTime.Value.Date.Add(new TimeSpan(23, 59, 59));
                    expression = expression.And(t => t.Company == param.Company && t.BaseModifyTime <= param.EndTime);
                }
                if (!param.Mode.IsNullOrZero() && !string.IsNullOrEmpty(param.RoleName))
                {
                    expression = expression.And(t => t.Mode == param.Mode && t.RoleName.Contains(param.RoleName));
                }
            }
            return expression;
        }
        #endregion
    }
}