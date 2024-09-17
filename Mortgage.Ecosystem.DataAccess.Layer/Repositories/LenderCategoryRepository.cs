using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class LenderCategoryRepository : DataRepository, ILenderCategoryRepository
    {
        #region Retrieve data
        public async Task<List<LenderCategoryEntity>> GetList(LenderCategoryEntity param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        #endregion

        #region Private method
        private Expression<Func<LenderCategoryEntity, bool>> ListFilter(LenderCategoryEntity param)
        {
            var expression = ExtensionLinq.True<LenderCategoryEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.LenderInstitution))
                {
                    expression = expression.And(second: t => t.LenderInstitution == param.LenderInstitution);
                }
            }
            return expression;
        }

        #endregion

    }
}
