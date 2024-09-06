using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class DisbursementRepository : DataRepository, IDisbursementRepository
    {
        #region Retrieve data
        public async Task<List<DisbursementEntity>> GetList(DisbursementListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<DisbursementEntity>> GetPageList(DisbursementListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<DisbursementEntity> GetEntity(string code)
        {
            return await BaseRepository().FindEntity<DisbursementEntity>(x => x.NHFNumber == code);
        }

       
        public async Task<DisbursementEntity> GetEntityById(long id)
        {
            return await BaseRepository().FindEntity<DisbursementEntity>(x => x.Id == id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_Conpany");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        public bool ExistCompany(DisbursementEntity entity)
        {
            var expression = ExtensionLinq.True<DisbursementEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.RefinanceNumber == entity.RefinanceNumber);
            }
            else
            {
                expression = expression.And(t => t.NHFNumber == entity.NHFNumber && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(DisbursementEntity entity)
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

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<DisbursementEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<DisbursementEntity, bool>> ListFilter(DisbursementListParam param)
        {
            var expression = ExtensionLinq.True<DisbursementEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.NHFNumber))
                {
                    expression = expression.And(t => t.NHFNumber.Contains(param.NHFNumber));
                }
            }
            return expression;
        }
        #endregion
    }
}