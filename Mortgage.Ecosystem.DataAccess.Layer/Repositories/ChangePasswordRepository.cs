using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
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
    public class ChangePasswordRepository : DataRepository, IChangePasswordRepository
    {
        #region Retrieve data
        public async Task<List<ChangePasswordEntity>> GetList(ChangePasswordListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ChangePasswordEntity>> GetPageList(ChangePasswordListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ChangePasswordEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ChangePasswordEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_Conpany");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the ChangePassword name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistChangePassword(ChangePasswordEntity entity)
        {
            var expression = ExtensionLinq.True<ChangePasswordEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Id == entity.Id);
            }
            else
            {
                expression = expression.And(t => t.Id == entity.Id && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(ChangePasswordEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<ChangePasswordEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<ChangePasswordEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ChangePasswordEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ChangePasswordEntity, bool>> ListFilter(ChangePasswordListParam param)
        {
            var expression = ExtensionLinq.True<ChangePasswordEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.OldPassword.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}