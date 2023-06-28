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
    public class ApproveEmployerAggregatorRepository : DataRepository, IApproveEmployerAggregatorRepository
    {
        #region Retrieve data
        public async Task<List<ApproveEmployerAggregatorEntity>> GetList(ApproveEmployerAggregatorListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ApproveEmployerAggregatorEntity>> GetPageList(ApproveEmployerAggregatorListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ApproveEmployerAggregatorEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ApproveEmployerAggregatorEntity>(id);
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
        public bool ExistCompany(ApproveEmployerAggregatorEntity entity)
        {
            var expression = ExtensionLinq.True<ApproveEmployerAggregatorEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.EmployerName == entity.EmployerName);
            }
            else
            {
                expression = expression.And(t => t.EmployerName == entity.EmployerName && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(ApproveEmployerAggregatorEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<ApproveEmployerAggregatorEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<ApproveEmployerAggregatorEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ApproveEmployerAggregatorEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ApproveEmployerAggregatorEntity, bool>> ListFilter(ApproveEmployerAggregatorListParam param)
        {
            var expression = ExtensionLinq.True<ApproveEmployerAggregatorEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.EmployerName.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}