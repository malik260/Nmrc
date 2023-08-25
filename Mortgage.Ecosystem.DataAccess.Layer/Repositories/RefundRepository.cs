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
    public class RefundRepository : DataRepository, IRefundRepository
    {
        #region Retrieve data
        public async Task<List<RefundEntity>> GetList(RefundListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<RefundEntity>> GetPageList(RefundListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public bool ExistingRefund(string nhfNo, string employerNumber, long id)
        {
            var expression = ExtensionLinq.True<RefundEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (id.IsNullOrZero())
            {
                expression = expression.And(t => t.NhfNumber == nhfNo && t.EmployerNumber == employerNumber);
            }
            else
            {
                expression = expression.And(t => t.NhfNumber == nhfNo && t.EmployerNumber == employerNumber && t.Id != id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        public async Task<RefundEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<RefundEntity>(id)
;
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_Refund");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(RefundEntity entity)
        {
            var expression = ExtensionLinq.True<RefundEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Name == entity.Name);
            }
            else
            {
                expression = expression.And(t => t.Name == entity.Name && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(RefundEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<RefundEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<RefundEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<RefundEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<RefundEntity, bool>> ListFilter(RefundListParam param)
        {
            var expression = ExtensionLinq.True<RefundEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.Name.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}