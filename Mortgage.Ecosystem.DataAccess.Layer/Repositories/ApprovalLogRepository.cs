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
    public class ApprovalLogRepository : DataRepository, IApprovalLogRepository
    {
        #region Retrieve data
        public async Task<List<ApprovalLogEntity>> GetList(ApprovalLogListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ApprovalLogEntity>> GetPageList(ApprovalLogListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ApprovalLogEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ApprovalLogEntity>(id);
        }

        public async Task<ApprovalLogEntity> GetEntity(long company, long menu, long record)
        {
            return await BaseRepository().FindEntity<ApprovalLogEntity>(p => p.Company == company && p.MenuId == menu && p.Record == record);
        }

        #endregion

        #region Submit data
        public async Task SaveForm(ApprovalLogEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<ApprovalLogEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<ApprovalLogEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ApprovalLogEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ApprovalLogEntity, bool>> ListFilter(ApprovalLogListParam param)
        {
            var expression = ExtensionLinq.True<ApprovalLogEntity>();
            if (param != null)
            {
                if (param.Company > 0)
                {
                    expression = expression.And(t => t.Company == param.Company);
                }

                if (param.Branch > 0)
                {
                    expression = expression.And(t => t.Branch == param.Branch);
                }

                if (param.MenuId > 0)
                {
                    expression = expression.And(t => t.MenuId == param.MenuId);
                }

                if (param.Authority > 0)
                {
                    expression = expression.And(t => t.Authority == param.Authority);
                }
            }
            return expression;
        }
        #endregion
    }
}