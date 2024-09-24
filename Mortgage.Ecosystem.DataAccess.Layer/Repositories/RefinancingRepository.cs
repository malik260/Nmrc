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
    public class RefinancingRepository : DataRepository, IRefinancingRepository
    {
        #region Retrieve data
        public async Task<List<RefinancingEntity>> GetList(RefinancingListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<RefinancingEntity>> GetPageList(RefinancingListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<RefinancingEntity> GetEntity(string code)
        {
            return await BaseRepository().FindEntity<RefinancingEntity>(x => x.NHFNumber == code);
        }

       
        public async Task<RefinancingEntity> GetEntityById(long id)
        {
            return await BaseRepository().FindEntity<RefinancingEntity>(x => x.Id == id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_Conpany");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        public bool ExistCompany(RefinancingEntity entity)
        {
            var expression = ExtensionLinq.True<RefinancingEntity>();
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
        public async Task SaveForm(RefinancingEntity entity)
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
            catch(Exception ex) 
            {
                await db.RollbackTrans();
                throw;
            }

        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<RefinancingEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<RefinancingEntity, bool>> ListFilter(RefinancingListParam param)
        {
            var expression = ExtensionLinq.True<RefinancingEntity>();
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