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
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class LenderInstitutionsRepository : DataRepository, ILenderInstitutionsRepository
    {
        #region Retrieve data
        public async Task<List<LenderInstitutionsEntity>> GetList(PmbListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<LenderInstitutionsEntity>> GetPageList(PmbListParam param, Pagination pagination)
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

        //public async Task<List<PmbEntity>> GetApprovalPageList(PmbListParam param, Pagination pagination)
        //{
        //    var expression = ListFilter(param);
        //    expression = expression.And(i => i.Status == GlobalConstant.ZERO);
        //    var baseList = await BaseRepository().FindList(expression, pagination);
        //    var approvalItems = await new DataRepository().GetPmbApprovalItems();

        //    var combinedList = baseList.Concat(approvalItems)
        //                              .GroupBy(e => e.Id)
        //                              .Select(g => g.First())
        //                              .ToList();

        //    return combinedList;
        //}

        public async Task<List<LenderInstitutionsEntity>> GetApprovalPageList(PmbListParam param, Pagination pagination)
        {
            // Build the initial filter expression
            var expression = ListFilter(param);
            expression = expression.And(i => i.Status == GlobalConstant.ZERO);

            // Get the base list without pagination
            var baseList = await BaseRepository().FindList(expression);

            // Get approval items
            var approvalItems = await new DataRepository().GetPmbApprovalItems();

            // Perform the join operation
            var joinedList = (from baseItem in baseList
                              join approvalItem in approvalItems
                              on baseItem.Id equals approvalItem.Id
                              select baseItem).ToList();

            // Set the total count for pagination before applying Skip and Take
            pagination.TotalCount = joinedList.Count;

            // Apply pagination to the joined list
            var paginatedList = joinedList
                .Skip((pagination.PageIndex - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            return paginatedList;
        }

        public async Task<LenderInstitutionsEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<LenderInstitutionsEntity>(x => x.Id == id);
        }
        public async Task<LenderInstitutionsEntity> GetEntitybyNhf(string nhf)
        {
            return await BaseRepository().FindEntity<LenderInstitutionsEntity>(x => x.NHFNumber == nhf || x.Name == nhf);
        }

        public async Task<LenderInstitutionsEntity> GetEntitybyEmail(string email)
        {
            return await BaseRepository().FindEntity<LenderInstitutionsEntity>(x => x.EmailAddress == email);
        }

        public async Task<LenderInstitutionsEntity> GetEntitybyName(string PMBName)
        {
            return await BaseRepository().FindEntity<LenderInstitutionsEntity>(x => x.Name == PMBName);
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistPmb(LenderInstitutionsEntity entity)
        {
            var expression = ExtensionLinq.True<LenderInstitutionsEntity>();
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

        public async Task SaveNewEmployee(EmployeeEntity entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    entity.Status = 1;
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await db.Delete<MenuAuthorizeEntity>(t => t.AuthorizeId == entity.Id);
                    await entity.Modify();
                    await db.Update(entity);
                }

                if (string.IsNullOrEmpty(entity.MenuIds))
                {
                    var xx = new LoginDto();
                    var MenuIds = xx.GetPmbEmployeeMenus();
                    //foreach (long menuId in TextHelper.SplitToArray<long>(entity.MenuIds, ','))
                    foreach (var menuId in MenuIds)
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
                }


                // User login record
                if (!string.IsNullOrEmpty(entity.EmailAddress))
                {
                    var currentMenu = await new DataRepository().GetMenuId(GlobalConstant.USER_MENU_URL);
                    UserEntity userEntity = new()
                    {
                        Company = entity.Company,
                        Employee = entity.Id,
                        UserName = entity.EmailAddress,
                        Salt = entity.Salt,
                        Password = entity.Password,
                        RealName = entity.FirstName,
                        LoginCount = GlobalConstant.ZERO,
                        UserStatus = GlobalConstant.ONE,
                        IsSystem = GlobalConstant.ZERO,
                        IsOnline = GlobalConstant.ZERO,
                        WebToken = SecurityHelper.GetGuid(true),
                        ApiToken = string.Empty,
                        BaseProcessMenu = currentMenu
                    };
                    await userEntity.Create();
                    await db.Insert(userEntity);
                }

                MailParameter mailParameter = new()
                {
                    RealName = entity.FirstName,
                    UserName = entity.EmailAddress,
                    UserEmail = entity.EmailAddress,
                    UserPassword = entity.DecryptedPassword,
                    UserCompany = entity.CompanyName
                };

                var sendEmail = EmailHelper.IsPasswordEmailSent(mailParameter, out message);
                

                await db.CommitTrans();
                // return true;
            }
            catch (Exception ex)
            {
                await db.RollbackTrans();
                throw;
            }
        }



        public bool ExistRCNumber(LenderInstitutionsEntity entity)
        {
            var expression = ExtensionLinq.True<LenderInstitutionsEntity>();
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
        public async Task SaveForm(LenderInstitutionsEntity entity)
        {
            if (entity.Id.IsNullOrZero() || entity.Id.ToStr().Length < 15)
            {
                await entity.Create();
                await BaseRepository().Insert<LenderInstitutionsEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<LenderInstitutionsEntity>(entity);
            }
        }

        public async Task SaveForms(LenderInstitutionsEntity entity)
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
                //    var currentMenu = await new DataRepository().GetMenuId(GlobalConstant.EMPLOYEE_MENU_URL);
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
                        Pmb = entity.Id,
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
                    userBelongEntity.Employee = entity.Employee;
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
            await BaseRepository().Delete<LenderInstitutionsEntity>(idArr);
        }

        public async Task<bool> ApproveForm(LenderInstitutionsEntity entity, MenuEntity menu, OperatorInfo user)
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
                        UserPassword = user.DecryptedPassword,
                        NhfNumber = entity.NHFNumber
                    };

                    if (EmailHelper.IsPasswordEmailSent(mailParameter, out message))
                    {
                        if (string.IsNullOrEmpty(message))
                        {
                            await db.CommitTrans();
                            return true;
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


        public async Task<bool> DisApproveForm(LenderInstitutionsEntity entity)
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
                return true;
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }

        #endregion

        #region Private method
        private Expression<Func<LenderInstitutionsEntity, bool>> ListFilter(PmbListParam param)
        {
            var expression = ExtensionLinq.True<LenderInstitutionsEntity>();
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