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
    public class ApproveAgentsRepository : DataRepository, IApproveAgentsRepository
    {
        #region Retrieve data
        public async Task<List<ApproveAgentsEntity>> GetList(ApproveAgentsListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ApproveAgentsEntity>> GetPageList(ApproveAgentsListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ApproveAgentsEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ApproveAgentsEntity>(id);
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
        public bool ExistCompany(ApproveAgentsEntity entity)
        {
            var expression = ExtensionLinq.True<ApproveAgentsEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Company == entity.Company);
            }
            else
            {
                expression = expression.And(t => t.Company == entity.Company && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(ApproveAgentsEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<ApproveAgentsEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<ApproveAgentsEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ApproveAgentsEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ApproveAgentsEntity, bool>> ListFilter(ApproveAgentsListParam param)
        {
            var expression = ExtensionLinq.True<ApproveAgentsEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.Company.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}