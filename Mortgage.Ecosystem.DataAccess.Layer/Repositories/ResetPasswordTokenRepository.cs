using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class ResetPasswordTokenRepository : DataRepository, IResetPasswordTokenRepository
    {
        #region Retrieve data
        public async Task<List<ResetPasswordTokenEntity>> GetList(ResetPasswordTokenListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ResetPasswordTokenEntity>> GetPageList(ResetPasswordTokenListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ResetPasswordTokenEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ResetPasswordTokenEntity>(id);
        }


        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_ChargeSetup");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(ResetPasswordTokenEntity entity)
        {
            var expression = ExtensionLinq.True<ResetPasswordTokenEntity>();
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


        // Method to check if a token exists in the database
        public bool ExistToken(string token)
        {
            var expression = ExtensionLinq.True<ResetPasswordTokenEntity>();
            expression = expression.And(t => t.PasswordToken == token);

            // Check if any record exists with the provided token
            return BaseRepository().IQueryable(expression).Any();
        }
        #endregion

        #region Submit data
        public async Task SaveForm(ResetPasswordTokenEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<ResetPasswordTokenEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<ResetPasswordTokenEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ResetPasswordTokenEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ResetPasswordTokenEntity, bool>> ListFilter(ResetPasswordTokenListParam param)
        {
            var expression = ExtensionLinq.True<ResetPasswordTokenEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.EmailAddress))
                {
                    expression = expression.And(t => t.EmailAddress.Contains(param.EmailAddress));
                }
            }
            return expression;
        }
        #endregion
    }
}