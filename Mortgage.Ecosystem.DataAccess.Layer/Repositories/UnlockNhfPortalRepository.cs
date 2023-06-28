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
    public class UnlockNhfPortalRepository : DataRepository, IUnlockNhfPortalRepository
    {
        #region Retrieve data
        public async Task<List<UnlockNhfPortalEntity>> GetList(UnlockNhfPortalListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<UnlockNhfPortalEntity>> GetPageList(UnlockNhfPortalListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<UnlockNhfPortalEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<UnlockNhfPortalEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_UnlockNhfPortal");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(UnlockNhfPortalEntity entity)
        {
            var expression = ExtensionLinq.True<UnlockNhfPortalEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.AccountNo == entity.AccountNo);
            }
            else
            {
                expression = expression.And(t => t.AccountNo == entity.AccountNo && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(UnlockNhfPortalEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<UnlockNhfPortalEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<UnlockNhfPortalEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<UnlockNhfPortalEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<UnlockNhfPortalEntity, bool>> ListFilter(UnlockNhfPortalListParam param)
        {
            var expression = ExtensionLinq.True<UnlockNhfPortalEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.AccountNo.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}