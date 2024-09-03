using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class LogLoginRepository : DataRepository, ILogLoginRepository
    {
        #region Get data
        //public async Task<List<LogLoginEntity>> GetList(LogLoginListParam param)
        //{
        //    var strSql = new StringBuilder();
        //    List<DbParameter> filter = ListFilter(param, strSql);
        //    var list = await BaseRepository().FindList<LogLoginEntity>(strSql.ToString(), filter.ToArray());
        //    return list.ToList();
        //}

        //public async Task<List<LogLoginEntity>> GetPageList(LogLoginListParam param, Pagination pagination)
        //{
        //    var strSql = new StringBuilder();
        //    List<DbParameter> filter = ListFilter(param, strSql);
        //    var list = await BaseRepository().FindList<LogLoginEntity>(strSql.ToString(), filter.ToArray(), pagination);
        //    return list.ToList();
        //}

        public async Task<List<LogLoginEntity>> GetList(LogLoginListParam param)
        {
            var expression = ListFilters(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<LogLoginEntity>> GetPageList(LogLoginListParam param, Pagination pagination)
        {
            var expression = ListFilters(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<List<LogLoginEntity>> GetUsersPageList(LogLoginListParam param, Pagination pagination)
        {
            var expression = ListFilters2(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<LogLoginEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<LogLoginEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(LogLoginEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert(entity);
            }
            else
            {
                await BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<LogLoginEntity>(idArr);
        }

        public async Task RemoveAllForm()
        {
            await BaseRepository().ExecuteBySql("truncate table tbl_LogLogin");
        }
        #endregion

        #region Private method
        //private List<DbParameter> ListFilter(LogLoginListParam param, StringBuilder strSql)
        //{
        //    strSql.Append(@"SELECT a.Id,
        //                            a.BaseCreateTime,
        //                            a.BaseCreatorId,
        //                            a.LogStatus,
        //                            a.IpAddress,
        //                            a.IpLocation,
        //                            a.Browser,
        //                            a.OS,
        //                            a.Remark,
        //                            b.UserName,
        //                            c.CompanyName
        //                    FROM tbl_LogLogin a
        //                         LEFT JOIN tbl_User b ON a.BaseCreatorId = b.Id
        //                         LEFT JOIN tbl_Company c ON b.Company = c.Id
        //                    WHERE 1 = 1");
        //    var parameter = new List<DbParameter>();
        //    if (param != null)
        //    {
        //        if (!string.IsNullOrEmpty(param.UserName))
        //        {
        //            strSql.Append(" AND b.UserName like @UserName");
        //            parameter.Add(DbParameterExtension.CreateDbParameter("@UserName", '%' + param.UserName + '%'));
        //        }
        //        if (param.LogStatus > -1)
        //        {
        //            strSql.Append("AND a.LogStatus = @LogStatus");
        //            parameter.Add(DbParameterExtension.CreateDbParameter("@LogStatus", param.LogStatus));
        //        }
        //        if (!string.IsNullOrEmpty(param.IpAddress))
        //        {
        //            strSql.Append(" AND a.IpAddress like @IpAddress");
        //            parameter.Add(DbParameterExtension.CreateDbParameter("@IpAddress", '%' + param.IpAddress + '%'));
        //        }
        //        if (!string.IsNullOrEmpty(param.StartTime.ToStr()))
        //        {
        //            strSql.Append(" AND a.BaseCreateTime >= @StartTime");
        //            parameter.Add(DbParameterExtension.CreateDbParameter("@StartTime", param.StartTime));
        //        }
        //        if (!string.IsNullOrEmpty(param.EndTime.ToStr()))
        //        {
        //            param.EndTime = param.EndTime.Value.Date.Add(new TimeSpan(23, 59, 59));
        //            strSql.Append(" AND a.BaseCreateTime <= @EndTime");
        //            parameter.Add(DbParameterExtension.CreateDbParameter("@EndTime", param.EndTime));
        //        }
        //    }
        //    return parameter;
        //}
        private Expression<Func<LogLoginEntity, bool>> ListFilters(LogLoginListParam param)
        {
            var expression = ExtensionLinq.True<LogLoginEntity>();

            if (param.Company != 0)
            {

                expression = expression.And(t => t.Company == param.Company);
            }
            if (param.employee != null)
            {

                expression = expression.And(t => t.BaseCreatorId == param.employee);
            }

            return expression;
        }

        private Expression<Func<LogLoginEntity, bool>> ListFilters2(LogLoginListParam param)
        {
            var expression = ExtensionLinq.True<LogLoginEntity>();


            expression = expression.And(t => t.Company != 0);


            return expression;
        }

        #endregion
    }
}