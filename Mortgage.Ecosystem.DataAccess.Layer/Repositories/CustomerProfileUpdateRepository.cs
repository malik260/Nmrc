using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.DataAccess.Layer.Request;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class CustomerProfileUpdateRepository : DataRepository, ICustomerProfileUpdateRepository
    {
        #region Retrieve data
        public async Task<List<CustomerProfileUpdateEntity>> GetList(CustomerProfileUpdateListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<CustomerProfileUpdateEntity>> GetPageList(CustomerProfileUpdateListParam param, Pagination pagination)
        {
            var DB = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            var employeeDetails = DB.EmployeeEntity.Where(i => i.Id == user.Employee).FirstOrDefault();
            var expression = ListFilter(param);

            expression = expression.And(customerProfileUpdate => customerProfileUpdate.NHFNumber == employeeDetails.NHFNumber);

            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<List<CustomerProfileUpdateEntity>> GetApprovalPageList(CustomerProfileUpdateListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            expression = expression.And(i => i.Status == GlobalConstant.ZERO);
            var baseList = await BaseRepository().FindList(expression, pagination); 
            var approvalItems = await new DataRepository().GetCustomerUpdateApprovalItems();

            var combinedList = baseList.Concat(approvalItems).ToList();

            return combinedList;
        }

        public async Task<CustomerProfileUpdateEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CustomerProfileUpdateEntity>(id);
        }

        public async Task<EmployeeEntity> GetEmployeeEntity(long id)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_CustomerProfileUpdate");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(CustomerProfileUpdateEntity entity)
        {
            var expression = ExtensionLinq.True<CustomerProfileUpdateEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.MobileNumber == entity.MobileNumber);
            }
            else
            {
                expression = expression.And(t => t.MobileNumber == entity.MobileNumber && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(CustomerProfileUpdateEntity entity)
        {
            var message = string.Empty;
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
                    await db.Delete<MenuAuthorizeEntity>(t => t.AuthorizeId == entity.Id);
                    await entity.Modify();
                    await db.Update(entity);
                }

                await db.CommitTrans();
            }
            catch (Exception ex)
            {
                await db.RollbackTrans();
                throw;
            }
        }



        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<CustomerProfileUpdateEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<CustomerProfileUpdateEntity, bool>> ListFilter(CustomerProfileUpdateListParam param)
        {
            var expression = ExtensionLinq.True<CustomerProfileUpdateEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.FirstName))
                {
                    expression = expression.And(t => t.FirstName.Contains(param.FirstName));
                }
            }
            return expression;
        }

        public async Task<bool> ApproveForm(CustomerProfileUpdateEntity entity, MenuEntity menu, OperatorInfo user, UserEntity loginProfile)
        {
            var message = string.Empty;
            var approvalLog = new ApprovalLogEntity();
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (menu.ApprovalLogList?.Count < menu.ApprovalLevel)
                {
                    approvalLog.Company = user.Company;
                    approvalLog.Branch = entity.Branch;
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
                    MailParameter mailParameter = new MailParameter();

                    //mailParameter.UserName = loginProfile.UserName;
                    mailParameter.UserEmail = entity.EmailAddress;
                    //mailParameter.UserPassword = loginProfile.DecryptedPassword;
                    mailParameter.RealName = entity.LastName + " " + entity.FirstName;
                    mailParameter.UserCompany = entity.CompanyName;
                    mailParameter.NhfNumber = Convert.ToString(entity.NHFNumber);



                    if (EmailHelper.IsApprovedCustomerUpdateEmailSent(mailParameter, out message))
                    {
                        if (string.IsNullOrEmpty(message))
                        {
                            await db.CommitTrans();
                        }
                    }
                }
                await db.CommitTrans();
                return true;

            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }



        public async Task RejectForm(CustomerProfileUpdateEntity entity)
        {
            var message = string.Empty;
            var approvalLog = new ApprovalLogEntity();
            var db = await BaseRepository().BeginTrans();
            try
            {

                entity.Status = (int)ApprovalEnum.Rejected;
                await entity.Modify();
                await db.Update(entity);

                await db.CommitTrans();

            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }
        #endregion
    }
}