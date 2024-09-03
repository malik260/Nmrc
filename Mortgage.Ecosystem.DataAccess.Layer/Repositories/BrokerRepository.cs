using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using System.Linq.Expressions;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Request;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class BrokerRepository: DataRepository, IBrokerRepository
    {
        public async Task<List<BrokerEntity>> GetApprovalPageList(BrokerListParam param, Pagination pagination)
        {
            var list = await new DataRepository().GetBrokerApprovalItems();
            return list.ToList();
        }

        public async Task<BrokerEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<BrokerEntity>(x => x.Id == id);
        }

        public async Task ApproveForm(BrokerEntity entity, MenuEntity menu, OperatorInfo user)
        {
            var message = string.Empty;
            var approvalLog = new ApprovalLogEntity();
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (menu.ApprovalLogList?.Count < menu.ApprovalLevel)
                {
                    approvalLog.Company = user.Company;
                    approvalLog.MenuId = menu.Id;
                    approvalLog.MenuType = menu.MenuType;
                    approvalLog.Authority = user.Employee;
                    approvalLog.Record = entity.Id;
                    approvalLog.ApprovalCount = (int)(menu.ApprovalLogList?.Count + 1);
                    approvalLog.ApprovalLevel = menu.ApprovalLevel;
                    approvalLog.Status = approvalLog.ApprovalCount == approvalLog.ApprovalLevel ? (int)ApprovalEnum.Approved : (int)ApprovalEnum.Pending;
                    approvalLog.Remark = approvalLog.ApprovalCount == approvalLog.ApprovalLevel ? "Approved" : "Partial approval";

                    await approvalLog.Create();
                    await db.Insert(approvalLog);
                }

                if (approvalLog.ApprovalCount == approvalLog.ApprovalLevel)
                {


                    entity.Status = (int)ApprovalEnum.Approved;
                    await entity.Modify();
                    await db.Update(entity);


                }

                if (approvalLog.ApprovalCount < approvalLog.ApprovalLevel)
                {
                    await db.CommitTrans();
                }
                else if (approvalLog.ApprovalCount == approvalLog.ApprovalLevel)
                {
                    MailParameter mailParameter = new()
                    {
                        UserName = user.UserName,
                        UserEmail = entity.EmailAddress,
                        UserPassword = user.DecryptedPassword
                    };

                    if (EmailHelper.IsPasswordEmailSent(mailParameter, out message))
                    {
                        if (string.IsNullOrEmpty(message))
                        {
                            await db.CommitTrans();
                        }
                    }
                }
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }


        public async Task<List<BrokerEntity>> GetPageList(BrokerListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        #region Private method
        private Expression<Func<BrokerEntity, bool>> ListFilter(BrokerListParam param)
        {
            var expression = ExtensionLinq.True<BrokerEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.Name.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion

    }
}
