using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.DataAccess.Layer.Request;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class ApprovalSetupRepository : DataRepository, IApprovalSetupRepository
    {
        #region Retrieve data
        public async Task<List<ApprovalSetupEntity>> GetList(ApprovalSetupListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ApprovalSetupEntity>> GetPageList(ApprovalSetupListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ApprovalSetupEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ApprovalSetupEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(ApprovalSetupEntity entity)
        {
            var message = string.Empty;
            List<ApprovalSetupEntity> entities = new List<ApprovalSetupEntity>();
            var mailParameters = new List<MailParameter>();
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    if (entity.ApprovalLevel > GlobalConstant.ZERO)
                    {
                        var companyRecord = await new CompanyRepository().GetEntity(entity.Company);
                        var menuRecord = await new MenuRepository().GetEntity(entity.MenuId);
                        var authorities = TextHelper.SplitToArray<long>(entity.Authorities, ',');
                        foreach (var authority in authorities)
                        {
                            var approvalSetupRecord = new ApprovalSetupEntity();
                            var employeeRecord = await new EmployeeRepository().GetEntity(authority);
                            MailParameter mailParameter = new()
                            {
                                RealName = $"{employeeRecord.LastName} {employeeRecord.FirstName}",
                                UserEmail = employeeRecord.EmailAddress,
                                ProcessName = menuRecord.MenuName,
                                UserCompany = companyRecord.Name
                            };
                            await entity.Create();
                            entity.Authority = authority;
                            entity.Priority = Array.IndexOf(authorities, authority) + 1;
                            approvalSetupRecord = MapHelper.Map(entity, approvalSetupRecord);
                            entities.Add(approvalSetupRecord);
                            mailParameters.Add(mailParameter);
                        }
                        await db.Insert(entities.AsEnumerable());
                    }
                }
                else
                {
                    //await db.Delete<ApprovalSetupEntity>(t => t.MenuId == entity.MenuId);
                    await entity.Modify();
                    await db.Update(entity);
                }

                //if (EmailHelper.IsApprovalSetupSent(mailParameters, out message))
                //{
                //    if (string.IsNullOrEmpty(message))
                //    {
                //        await db.CommitTrans();
                //    }
                //}
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
            await BaseRepository().Delete<ApprovalSetupEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ApprovalSetupEntity, bool>> ListFilter(ApprovalSetupListParam param)
        {
            var expression = ExtensionLinq.True<ApprovalSetupEntity>();
            if (param != null)
            {
                if (param.Company > 0)
                {
                    expression = expression.And(t => t.Company == param.Company);
                }

                if (param.Branch > 0)
                {
                    expression = expression.And(t => t.Branch == param.Branch);
                }

                if (param.MenuId > 0)
                {
                    expression = expression.And(t => t.MenuId == param.MenuId);
                }

                if (param.Authority > 0)
                {
                    expression = expression.And(t => t.Authority == param.Authority);
                }

                if (param.Priority > 0)
                {
                    expression = expression.And(t => t.Priority == param.Priority);
                }
            }
            return expression;
        }
        #endregion
    }
}