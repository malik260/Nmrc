using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Data.Common;
using System.Text;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class LogOperateRepository : DataRepository, ILogOperateRepository
    {
        #region Get data
        public async Task<List<LogOperateEntity>> GetList(LogOperateListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await BaseRepository().FindList<LogOperateEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<LogOperateEntity>> GetPageList(LogOperateListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await BaseRepository().FindList<LogOperateEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<LogOperateEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<LogOperateEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(LogOperateEntity entity)
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
            await BaseRepository().Delete<LogOperateEntity>(idArr);
        }

        public async Task RemoveAllForm()
        {
            await BaseRepository().ExecuteBySql("truncate table tbl_LogOperate");
        }
        #endregion

        #region private method
        private List<DbParameter> ListFilter(LogOperateListParam param, StringBuilder strSql)
        {
            strSql.Append(@"SELECT a.Id,
                                    a.BaseCreateTime,
                                    a.BaseCreatorId,
                                    a.LogStatus,
                                    a.IpAddress,
                                    a.IpLocation,
                                    a.Remark,
                                    a.ExecuteUrl,
                                    a.ExecuteParam,
                                    a.ExecuteResult,
                                    a.ExecuteTime,
                                    b.UserName,
                                    c.CompanyName
                            FROM tbl_LogOperate a
                                    LEFT JOIN tbl_User b ON a.BaseCreatorId = b.Id
                                    LEFT JOIN tbl_Company c ON b.Company = c.Id
                            WHERE 1 = 1");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.UserName))
                {
                    strSql.Append(" AND b.UserName like @UserName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@UserName", '%' + param.UserName + '%'));
                }
                if (param.LogStatus > -1)
                {
                    strSql.Append("AND a.LogStatus = @LogStatus");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@LogStatus", param.LogStatus));
                }
                if (!string.IsNullOrEmpty(param.ExecuteUrl))
                {
                    strSql.Append(" AND a.ExecuteUrl like @ExecuteUrl");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ExecuteUrl", '%' + param.ExecuteUrl + '%'));
                }
                if (!string.IsNullOrEmpty(param.StartTime.ToStr()))
                {
                    strSql.Append(" AND a.BaseCreateTime >= @StartTime");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@StartTime", param.StartTime));
                }
                if (!string.IsNullOrEmpty(param.EndTime.ToStr()))
                {
                    param.EndTime = param.EndTime.Value.Date.Add(new TimeSpan(23, 59, 59));
                    strSql.Append(" AND a.BaseCreateTime <= @EndTime");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@EndTime", param.EndTime));
                }
            }
            return parameter;
        }
        #endregion
    }
}