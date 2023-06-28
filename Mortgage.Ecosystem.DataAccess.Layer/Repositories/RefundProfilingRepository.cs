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
    public class RefundProfilingRepository : DataRepository, IRefundProfilingRepository
    {
        #region Retrieve data
        public async Task<List<RefundProfilingEntity>> GetList(RefundProfilingListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<RefundProfilingEntity>> GetPageList(RefundProfilingListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<RefundProfilingEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<RefundProfilingEntity>(id);
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
        public bool ExistCompany(RefundProfilingEntity entity)
        {
            var expression = ExtensionLinq.True<RefundProfilingEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.CustName == entity.CustName);
            }
            else
            {
                expression = expression.And(t => t.CustName == entity.CustName && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(RefundProfilingEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<RefundProfilingEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<RefundProfilingEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<RefundProfilingEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<RefundProfilingEntity, bool>> ListFilter(RefundProfilingListParam param)
        {
            var expression = ExtensionLinq.True<RefundProfilingEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.CustName))
                {
                    expression = expression.And(t => t.CustName.Contains(param.CustName));
                }
            }
            return expression;
        }
        #endregion
    }
}