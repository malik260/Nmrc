using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class LoanDisbursementRepository : DataRepository, ILoanDisbursementRepository
    {
        #region Retrieve data
        public async Task<List<LoanDisbursementEntity>> GetList(LoanDisbursementDto param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

       

        public async Task<List<LoanDisbursementEntity>> GetPageList(LoanDisbursementDto param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

      
        public async Task<LoanDisbursementEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<LoanDisbursementEntity>(id)
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
        #endregion

        #region Submit data
        public async Task SaveForm(LoanDisbursementEntity entity)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await BaseRepository().Insert<LoanDisbursementEntity>(entity);
                }
                else
                {
                    await entity.Modify();
                    await BaseRepository().Update<LoanDisbursementEntity>(entity);
                }
                await db.CommitTrans();
            }
            catch (Exception)
            {
                await db.RollbackTrans();
                throw;
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<RefundEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<LoanDisbursementEntity, bool>> ListFilter(LoanDisbursementDto param)
        {
            var expression = ExtensionLinq.True<LoanDisbursementEntity>();
            if (param != null)
            {
                if (param.StartDate != null && param.EndDate != null)
                {
                    expression = expression.And(t => t.DisbursementDate >= param.StartDate && t.DisbursementDate <= param.EndDate && t.PmbId == param.PmbId);
                }
            }
            return expression;
        }
        #endregion


    }

}

