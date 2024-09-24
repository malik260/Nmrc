using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class NmrcRefinanceRepository : DataRepository, INmrcRefinanceRepository
    {
        #region Retrieve data
        public async Task<List<NmrcRefinancingEntity>> GetList(NmrcRefinancingEntity param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }
         public async Task<List<NmrcRefinancingEntity>> GetListByRefinanceNumber(NmrcRefinancingEntity param)
        {
            var expression = ListFilterByRefinanceNumber(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<NmrcRefinancingEntity>> GetPageList(NmrcRefinancingEntity param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<List<NmrcRefinancingEntity>> GetLoanBatches(string id, Pagination pagination)
        {
            var expression = ListFilter2(id);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        //public async Task<List<NmrcRefinancingEntity>> GetApprovalPageList()
        //{
        //    var list = await new DataRepository().GetReviewedLoan();
        //    return list.ToList();
        //}

      

      

        public async Task<NmrcRefinancingEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<NmrcRefinancingEntity>(id);
        }

        public async Task<NmrcRefinancingEntity> GetEntitybyNHF(string NHF)
        {
            return await BaseRepository().FindEntity<NmrcRefinancingEntity>(i => i.NHFNumber == NHF);
        }

        public async Task<NmrcRefinancingEntity> GetEntitybyLoanId(string id)
        {
            return await BaseRepository().FindEntity<NmrcRefinancingEntity>(i => i.LoanId == id);
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
        
        #endregion

        #region Submit data
        public async Task SaveForm(NmrcRefinancingEntity entity)
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
            await BaseRepository().Delete<UnderwritingEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<NmrcRefinancingEntity, bool>> ListFilter(NmrcRefinancingEntity param)
        {
            var expression = ExtensionLinq.True<NmrcRefinancingEntity>();
            if (param != null)
            {
                if (param.LenderID != 0)
                {
                    expression = expression.And(t => t.LenderID == param.LenderID);
                }
            }
            return expression;
        }

        private Expression<Func<NmrcRefinancingEntity, bool>> ListFilterByRefinanceNumber(NmrcRefinancingEntity param)
        {
            var expression = ExtensionLinq.True<NmrcRefinancingEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.RefinanceNumber))
                {
                    expression = expression.And(t => t.RefinanceNumber == param.RefinanceNumber);
                }
            }
            return expression;
        }

        private Expression<Func<NmrcRefinancingEntity, bool>> ListFilter2(string id)
        {
            var expression = ExtensionLinq.True<NmrcRefinancingEntity>();
            if (id != null)
            {

                expression = expression.And(t => t.RefinanceNumber == id);

            }
            return expression;
        }
        #endregion
    }
}
