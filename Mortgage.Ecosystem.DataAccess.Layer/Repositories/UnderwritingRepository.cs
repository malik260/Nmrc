﻿using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
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
    public class UnderwritingRepository : DataRepository, IUnderwritingRepository
    {
        #region Retrieve data

        public async Task<List<LoanInitiationUploadEntity>> GetLists(long id)
        {
            var expression = ListFilter4(id);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }
        public async Task<List<UnderwritingEntity>> GetList(UnderwritingListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<UnderwritingEntity>> GetPageList(UnderwritingListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<List<UnderwritingEntity>> GetLoanBatches(string id, Pagination pagination)
        {
            var expression = ListFilter2(id);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<List<UnderwritingEntity>> GetApprovalPageList()
        {
            var list = await new DataRepository().GetReviewedLoan();
            return list.ToList();
        }

        public async Task<List<UnderwritingEntity>> GetLoanForBatching()
        {
            var list = await new DataRepository().GetApprovedLoan();
            return list.ToList();
        }

        public async Task<List<UnderwritingEntity>> GetBatchedLoan()
        {
            var list = await new DataRepository().GetBatchedLoan();
            return list.ToList();
        }


        public async Task<List<UnderwritingEntity>> GetLoanForReview()
        {
            var list = await new DataRepository().GetRiskRatedLoan();
            return list.ToList();
        }

        public async Task<List<UnderwritingEntity>> GetLoanForUnderwriting()
        {
            var list = await new DataRepository().GetLoanforUnderwriting();
            return list.ToList();
        }


        public async Task<UnderwritingEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<UnderwritingEntity>(id);
        }

        public async Task<UnderwritingEntity> GetEntitybyNHF(string NHF)
        {
            return await BaseRepository().FindEntity<UnderwritingEntity>(i=> i.NHFNumber == NHF);
        }

        public async Task<UnderwritingEntity> GetEntitybyLoanId(string id)
        {
            return await BaseRepository().FindEntity<UnderwritingEntity>(i => i.LoanId == id);
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
        public bool ExistCompany(UnderwritingEntity entity)
        {
            var expression = ExtensionLinq.True<UnderwritingEntity>();
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
        public async Task SaveForm(UnderwritingEntity entity)
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
        private Expression<Func<UnderwritingEntity, bool>> ListFilter(UnderwritingListParam param)
        {
            var expression = ExtensionLinq.True<UnderwritingEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.pmb))
                {
                    expression = expression.And(t => t.NextStafffLevel.Contains(param.pmb));
                }
            }
            return expression;
        }

        private Expression<Func<UnderwritingEntity, bool>> ListFilter2(string id)
        {
            var expression = ExtensionLinq.True<UnderwritingEntity>();
            if (id != null)
            {

                expression = expression.And(t => t.BatchRefNo == id);

            }
            return expression;
        }

        private Expression<Func<LoanInitiationUploadEntity, bool>> ListFilter4(long id)
        {
            var expression = ExtensionLinq.True<LoanInitiationUploadEntity>();
            if (id != 0)
            {

                expression = expression.And(second: t => t.FileId==id);
            }
            return expression;
        }
        #endregion


    }
}