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
    public class LoanInitiationUploadRepository : DataRepository, ILoanInitiationUploadRepository
    {
        #region Retrieve data
        public async Task<List<LoanInitiationUploadEntity>> GetList(long id)
        {
            var expression = ListFilter2(id);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<LoanInitiationUploadEntity>> GetPageList(LoanInitiationUploadListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<LoanInitiationUploadEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<LoanInitiationUploadEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(LoanInitiationUploadEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<LoanInitiationUploadEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<LoanInitiationUploadEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<LoanInitiationUploadEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<LoanInitiationUploadEntity, bool>> ListFilter(LoanInitiationUploadListParam param)
        {
            var expression = ExtensionLinq.True<LoanInitiationUploadEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Company.ToString()) && (!string.IsNullOrEmpty(param.Images) || !string.IsNullOrEmpty(param.Type)))
                {
                    expression = expression.And(second: t => t.Company == param.Company && t.Type.Contains(param.Type) && t.Images.Contains(param.Images));
                }
            }
            return expression;
        }



        private Expression<Func<LoanInitiationUploadEntity, bool>> ListFilter2(long id)
        {
            var expression = ExtensionLinq.True<LoanInitiationUploadEntity>();
            if (id != 0)
            {

                expression = expression.And(second: t => t.FileId == id);
            }
            return expression;
        }
        #endregion
    }
}