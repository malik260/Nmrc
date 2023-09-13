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
    public class ContributionRepository : DataRepository, IContributionRepository
    {
        #region Retrieve data
        public async Task<List<ContributionEntity>> GetList(ContributionListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }



        public async Task<List<ContributionEntity>> GetPageList(ContributionListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ContributionEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ContributionEntity>(id);
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
        public bool ExistSingleContribution(ContributionEntity entity)
        {
            var expression = ExtensionLinq.True<ContributionEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.employeeNumber == entity.employeeNumber);
            }
            else
            {
                expression = expression.And(t => t.employeeNumber == entity.employeeNumber && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(ContributionEntity entity)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await entity.Modify();
                    await db.Update(entity);
                }
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }

        public async Task SaveForms(List<ContributionEntity> entity)
        {
            foreach (var item in entity)
            {
                if (item.Id.IsNullOrZero())
                {
                    await item.Create();
                    await BaseRepository().Insert<ContributionEntity>(item);
                }
                else
                {
                    await item.Modify();
                    await BaseRepository().Update<ContributionEntity>(item);
                }

            }


        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ContributionEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ContributionEntity, bool>> ListFilter(ContributionListParam param)
        {
            var expression = ExtensionLinq.True<ContributionEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.employeeNumber.Contains(param.EmployerNumber));
                }
            }
            return expression;
        }

        #endregion
    }
}
