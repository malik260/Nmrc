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
    public class LenderTypeRepository : DataRepository, ILenderTypeRepository
    {
        #region Retrieve data
        public async Task<List<LenderTypeEntity>> GetList(LenderTypeListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }


        #endregion


        #region Private method
        private Expression<Func<LenderTypeEntity, bool>> ListFilter(LenderTypeListParam param)
        {
            var expression = ExtensionLinq.True<LenderTypeEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.LenderTypeName))
                {
                    expression = expression.And(second: t => t.LenderType.Contains(param.LenderTypeName));
                }
            }
            return expression;
        }
        #endregion
    }
}