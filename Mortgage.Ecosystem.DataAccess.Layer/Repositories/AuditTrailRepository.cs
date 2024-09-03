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
    public class AuditTrailRepository : DataRepository, IAuditTrailRepository
    {
        #region Retrieve data
        public async Task<List<AuditTrailEntity>> GetList(AuditTrailListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<AuditTrailEntity>> GetLists(String NhfNumber)
        {
            var expression = ListFilters(NhfNumber);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }


        public async Task<List<AuditTrailEntity>> GetPageList(AuditTrailListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<AuditTrailEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<AuditTrailEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_AuditTrail");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(AuditTrailEntity entity)
        {
            var expression = ExtensionLinq.True<AuditTrailEntity>();
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
        #endregion

        #region Submit data
        public async Task SaveForm(AuditTrailEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<AuditTrailEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<AuditTrailEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<AuditTrailEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<AuditTrailEntity, bool>> ListFilter(AuditTrailListParam param)
        {
            var expression = ExtensionLinq.True<AuditTrailEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.UserName))
                {
                    expression = expression.And(t => t.UserName == param.UserName);
                }
            }
            return expression;
        }

        private Expression<Func<AuditTrailEntity, bool>> ListFilters(string NhfNumber)
        {
            var expression = ExtensionLinq.True<AuditTrailEntity>();
            if (NhfNumber != null)
            {
                if (!string.IsNullOrEmpty(NhfNumber))
                {
                    expression = expression.And(t => t.UserName == NhfNumber);
                }
            }
            return expression;
        }

        public async Task SaveForms(List<AuditTrailEntity> entity)
        {
            foreach (var item in entity)
            {
                if (item.Id.IsNullOrZero())
                {
                    await item.Create();
                    await BaseRepository().Insert<AuditTrailEntity>(item);
                }
                else
                {
                    await item.Modify();
                    await BaseRepository().Update<AuditTrailEntity>(item);
                }

            }


        }
        #endregion
    }
}