using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
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
    public class ETicketRepository : DataRepository, IETicketRepository
    {
        #region Retrieve data
        public async Task<List<ETicketEntity>> GetList(ETicketListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ETicketEntity>> GetPageList(ETicketListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<List<ETicketEntity>> GetApprovalPageList(ETicketListParam param, Pagination pagination)
        {
            var list = await new DataRepository().GetETicketApprovalItems();
            return list.ToList();
        }


        public async Task<ETicketEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ETicketEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_Conpany");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(ETicketEntity entity)
        {
            var expression = ExtensionLinq.True<ETicketEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.RequestNumber == entity.RequestNumber);
            }
            else
            {
                expression = expression.And(t => t.RequestNumber == entity.RequestNumber && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(ETicketEntity entity)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await BaseRepository().Insert<ETicketEntity>(entity);
                }
                else
                {
                    await entity.Modify();
                    await BaseRepository().Update<ETicketEntity>(entity);
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
            await BaseRepository().Delete<ETicketEntity>(idArr);
        }

        public async Task ApproveForm(ETicketEntity entity, MenuEntity menu, OperatorInfo user)
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

                if (approvalLog.ApprovalCount == menu.ApprovalLevel)
                {
                    entity.Status = (int)ApprovalEnum.Approved;
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
        #endregion

        #region Private method
        private Expression<Func<ETicketEntity, bool>> ListFilter(ETicketListParam param)
        {
            var expression = ExtensionLinq.True<ETicketEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.RequestNumber))
                {
                    expression = expression.And(t => t.RequestNumber.Contains(param.RequestNumber));
                }
            }
            return expression;
        }
        #endregion
    }
}