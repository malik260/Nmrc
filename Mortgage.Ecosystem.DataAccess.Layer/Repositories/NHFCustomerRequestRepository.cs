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
    public class NHFCustomerRequestRepository : DataRepository, INHFCustomerRequestRepository
    {
        #region Retrieve data
        public async Task<List<NHFCustomerRequestEntity>> GetList(NHFCustomerRequestListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<NHFCustomerRequestEntity>> GetPageList(NHFCustomerRequestListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<NHFCustomerRequestEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<NHFCustomerRequestEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_NHFCustomerRequest");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(NHFCustomerRequestEntity entity)
        {
            var expression = ExtensionLinq.True<NHFCustomerRequestEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.AccountNumber == entity.AccountNumber);
            }
            else
            {
                expression = expression.And(t => t.AccountNumber == entity.AccountNumber && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(NHFCustomerRequestEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<NHFCustomerRequestEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<NHFCustomerRequestEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<NHFCustomerRequestEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<NHFCustomerRequestEntity, bool>> ListFilter(NHFCustomerRequestListParam param)
        {
            var expression = ExtensionLinq.True<NHFCustomerRequestEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.AccountNumber))
                {
                    expression = expression.And(t => t.AccountNumber.Contains(param.AccountNumber));
                }
            }
            return expression;
        }
        #endregion
    }
}