using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.DataAccess.Layer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class DeveloperRepository: DataRepository, IDeveloperRepository
    {
        #region Retrieve data
        public async Task<List<DeveloperEntity>> GetList(DeveloperListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }


        public async Task<List<DeveloperEntity>> GetApprovalPageList(DeveloperListParam param, Pagination pagination)
        {
            var list = await new DataRepository().GetDeveloperApprovalItems();
            return list.ToList();
        }


        public async Task<List<DeveloperEntity>> GetPageList(DeveloperListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        //public async Task<List<PmbEntity>> GetApprovalPageList(PmbListParam param, Pagination pagination)
        //{
        //    var list = await new DataRepository().GetPmbApprovalItems();
        //    return list.ToList();
        //}

        public async Task<DeveloperEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<DeveloperEntity>(id);
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistDeveloper(DeveloperEntity entity)
        {
            var expression = ExtensionLinq.True<DeveloperEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Name == entity.Name);
            }
            else
            {
                expression = expression.And(t => t.Name == entity.Name && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        public bool ExistRCNumber(DeveloperEntity entity)
        {
            var expression = ExtensionLinq.True<DeveloperEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.RCNumber == entity.RCNumber);
            }
            else
            {
                expression = expression.And(t => t.Id != entity.Id && t.RCNumber == entity.RCNumber);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(DeveloperEntity entity)
        {
            if (entity.Id.IsNullOrZero() || entity.Id.ToStr().Length < 15)
            {
                await entity.Create();
                await BaseRepository().Insert<DeveloperEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<DeveloperEntity>(entity);
            }
        }

        public async Task SaveForms(DeveloperEntity entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero() || entity.Id.ToStr().Length < 15)
                {

                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await entity.Modify();
                    await db.Update(entity);
                }
                // Individual employee record
                //if (!string.IsNullOrEmpty(entity.IndFirstName) && !string.IsNullOrEmpty(entity.IndLastName))
                //{
                //var currentMenu = await new DataRepository().GetMenuId(GlobalConstant.EMPLOYEE_MENU_URL);
                //    EmployeeEntity employeeEntity = new()
                //    {
                //        Company = entity.Id,
                //        Branch = GlobalConstant.ZERO,
                //        Department = GlobalConstant.ZERO,
                //        NHFNumber = entity.NHFNumber,
                //        BVN = entity.IndBVN,
                //        NIN = string.Empty,
                //        EmploymentType = EmploymentTypeEnum.Employed.ParseToInt(),
                //        DateOfEmployment = DateTime.MinValue,
                //        Title = GlobalConstant.ZERO,
                //        FirstName = entity.IndFirstName,
                //        LastName = entity.IndLastName,
                //        OtherName = string.Empty,
                //        Gender = GlobalConstant.ZERO,
                //        DateOfBirth = entity.IndDateOfBirth,
                //        MaritalStatus = GlobalConstant.ZERO,
                //        PostalAddress = string.Empty,
                //        EmailAddress = entity.EmailAddress,
                //        MobileNumber = entity.MobileNumber,
                //        StaffNumber = string.Empty,
                //        CustomerBank = string.Empty,
                //        BankAccountNumber = string.Empty,
                //        AccountType = GlobalConstant.ZERO,
                //        MonthlySalary = GlobalConstant.ZERO,
                //        AlertType = GlobalConstant.ZERO,
                //        Portrait = null,
                //        PortraitType = string.Empty,
                //        BaseProcessMenu = currentMenu
                //    };
                //    await employeeEntity.Create();
                //    entity.Employee = employeeEntity.Id;
                //    await db.Insert(employeeEntity);
                //}

                // User login record
                if (!string.IsNullOrEmpty(entity.UserName) && !entity.Role.IsNullOrZero())
                {
                    var currentMenu = await new DataRepository().GetMenuId(GlobalConstant.USER_MENU_URL);
                    UserEntity userEntity = new()
                    {
                        Company = entity.Id,
                        Developer = entity.Id,
                        //Employee = entity.NHFNumber,
                        UserName = entity.UserName,
                        Salt = entity.Salt,
                        Password = entity.Password,
                        RealName = entity.IndFirstName,
                        LoginCount = GlobalConstant.ZERO,
                        UserStatus = GlobalConstant.ONE,
                        IsSystem = GlobalConstant.ZERO,
                        IsOnline = GlobalConstant.ZERO,
                        WebToken = SecurityHelper.GetGuid(true),
                        ApiToken = string.Empty,
                        BaseProcessMenu = currentMenu
                    };
                    await userEntity.Create();
                    entity.User = userEntity.Id;
                    await db.Insert(userEntity);
                }

                // Role
                if (!entity.Role.IsNullOrZero())
                {
                    UserBelongEntity userBelongEntity = new UserBelongEntity();
                    userBelongEntity.Company = entity.Id;
                    //userBelongEntity.Employee = entity.Employee;
                    userBelongEntity.Belong = entity.Role;
                    userBelongEntity.BelongType = UserBelongTypeEnum.Role.ParseToInt();
                    await userBelongEntity.Create();
                    await db.Insert(userBelongEntity);
                }



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
            await BaseRepository().Delete<DeveloperEntity>(idArr);
        }

        public async Task ApproveForm(DeveloperEntity entity, MenuEntity menu, OperatorInfo user)
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
                    // Menu, page and button permissions corresponding to roles
                    var xx = new LoginDto();
                    var MenuIds = xx.GetDeveloperMenus();

                    foreach (long menuId in MenuIds)
                    {
                        MenuAuthorizeEntity menuAuthorizeEntity = new()
                        {
                            AuthorizeId = entity.Id,
                            MenuId = menuId,
                            AuthorizeType = AuthorizeTypeEnum.User.ToInt()
                        };
                        await menuAuthorizeEntity.Create();
                        await db.Insert(menuAuthorizeEntity);
                    }



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
        #endregion

        #region Private method
        private Expression<Func<DeveloperEntity, bool>> ListFilter(DeveloperListParam param)
        {
            var expression = ExtensionLinq.True<DeveloperEntity>();
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
