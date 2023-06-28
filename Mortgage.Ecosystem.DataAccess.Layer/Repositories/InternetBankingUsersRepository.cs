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
    public class InternetBankingUsersRepository : DataRepository, IInternetBankingUsersRepository
    {
        #region Retrieve data
        public async Task<List<InternetBankingUsersEntity>> GetList(InternetBankingUsersListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<InternetBankingUsersEntity>> GetPageList(InternetBankingUsersListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<InternetBankingUsersEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<InternetBankingUsersEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_Conpany");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(InternetBankingUsersEntity entity)
        {
            var expression = ExtensionLinq.True<InternetBankingUsersEntity>();
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
        public async Task SaveForm(InternetBankingUsersEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<InternetBankingUsersEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<InternetBankingUsersEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<InternetBankingUsersEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<InternetBankingUsersEntity, bool>> ListFilter(InternetBankingUsersListParam param)
        {
            var expression = ExtensionLinq.True<InternetBankingUsersEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.AccountNo))
                {
                    expression = expression.And(t => t.AccountNo.Contains(param.AccountNo));
                }
            }
            return expression;
        }
        #endregion
    }
}