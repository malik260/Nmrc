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
    public class AccountTypeRepository : DataRepository, IAccountTypeRepository
    {
        #region Retrieve data
        public async Task<List<AccountTypeEntity>> GetList(AccountTypeListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<AccountTypeEntity>> GetPageList(AccountTypeListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<AccountTypeEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<AccountTypeEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(AccountTypeEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<AccountTypeEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<AccountTypeEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<AccountTypeEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<AccountTypeEntity, bool>> ListFilter(AccountTypeListParam param)
        {
            var expression = ExtensionLinq.True<AccountTypeEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(second: t => t.Name.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}