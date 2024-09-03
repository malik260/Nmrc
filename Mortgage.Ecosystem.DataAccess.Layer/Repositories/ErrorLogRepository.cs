using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
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
    public class ErrorLogRepository : DataRepository, IErrorLogRepository
    {
        #region Retrieve data
        public async Task<List<ErrorLogEntity>> GetList(ErrorLogEntity param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }


        #endregion


        public async Task<List<ErrorLogEntity>> GetPageList(ErrorLogEntity param, Pagination pagination)
        {
            try
            {
                var expression = ListFilter(param);
                var list = await BaseRepository().FindList(expression, pagination);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #region Private Methods
        private Expression<Func<ErrorLogEntity, bool>> ListFilter(ErrorLogEntity param)
        {
            var expression = ExtensionLinq.True<ErrorLogEntity>();
            if (param.Username != null)
            {

                expression = expression.And(t => t.Username == param.Username);
            }
            return expression;
        }
        #endregion
    }
}
