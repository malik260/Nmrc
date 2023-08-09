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
    public class RemitaPaymentDetailsRepository : DataRepository, IRemitaPaymentDetailsRepository
    {
        #region Retrieve data
        public async Task<List<RemitaPaymentDetailsEntity>> GetList(RemitaPaymentDetailsListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<RemitaPaymentDetailsEntity>> GetPageList(RemitaPaymentDetailsListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<RemitaPaymentDetailsEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<RemitaPaymentDetailsEntity>(id);
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
        public bool ExistCompany(RemitaPaymentDetailsEntity entity)
        {
            var expression = ExtensionLinq.True<RemitaPaymentDetailsEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.TransactionId == entity.TransactionId);
            }
            else
            {
                expression = expression.And(t => t.TransactionId == entity.TransactionId && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(RemitaPaymentDetailsEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<RemitaPaymentDetailsEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<RemitaPaymentDetailsEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<RemitaPaymentDetailsEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<RemitaPaymentDetailsEntity, bool>> ListFilter(RemitaPaymentDetailsListParam param)
        {
            var expression = ExtensionLinq.True<RemitaPaymentDetailsEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.TransactionId))
                {
                    expression = expression.And(t => t.TransactionId.Contains(param.TransactionId));
                }
            }
            return expression;
        }
        #endregion
    }
}