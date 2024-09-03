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
using NPOI.HSSF.Record;
using NPOI.POIFS.Crypt;
using NPOI.SS.Formula.Functions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class EmployeeRepository : DataRepository, IEmployeeRepository
    {
        #region Retrieve data
        public async Task<List<EmployeeEntity>> GetList(EmployeeListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<EmployeeEntity>> GetListByCompany(EmployeeListParam param)
        {
            var expression = ListFilters(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<EmployeeEntity>> GetListByCompanyType(EmployeeListParam param)
        {
            var expression = ListFilters(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<EmployeeEntity>> GetPageList(EmployeeListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<List<EmployeeEntity>> GetApprovalPageList(EmployeeListParam param, Pagination pagination)
        {
            // Build the initial filter expression
            var expression = ListFilter(param);
            expression = expression.And(i => i.Status == GlobalConstant.ZERO && i.UserType == GlobalConstant.ZERO);

            // Get the base list without pagination
            var baseList = await BaseRepository().FindList(expression);

            // Get approval items
            var approvalItems = await new DataRepository().GetEmployeeApprovalItems();

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





        public async Task<EmployeeEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(id);
        }

        public async Task<EmployeeEntity> GetEntityByNhfNumber(long nhfNo)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(x => x.NHFNumber == nhfNo);
        }

        public async Task<EmployeeEntity> GetById(long id)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(id);
        }

        public async Task<EmployeeEntity> GetEmployeeByMobile(string MobileNumber)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(x=> x.MobileNumber == MobileNumber);
        }

        public async Task<EmployeeEntity> GetEmployeeByEmail(string emailAddress)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(x => x.EmailAddress == emailAddress);
        }

        public async Task<EmployeeEntity> GetEmployeeByNIN(string NIN)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(x => x.NIN == NIN);
        }

        public async Task<EmployeeEntity> GetEmployeeByBVN(string BVN)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(x => x.BVN == BVN);
        }



        public async Task<List<EmployeeEntity>> GetLists()
        {
            var list = await BaseRepository().FindList<EmployeeEntity>();
            return list.ToList();
        }

        public bool ExistEmployee(EmployeeEntity entity)
        {
            var expression = ExtensionLinq.True<EmployeeEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Company == entity.Company && t.EmailAddress == entity.EmailAddress);
            }
            else
            {
                expression = expression.And(t => t.Company == entity.Company && t.EmailAddress == entity.EmailAddress && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        public bool IsEmployeeNHFNumberExist(EmployeeListParam param)
        {
            var expression = ExtensionLinq.True<EmployeeEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (param.NHFNumber > 0)
            {
                expression = expression.And(t => t.NHFNumber == param.NHFNumber);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        public bool ExistEmployeeBVN(EmployeeEntity entity)
        {
            var expression = ExtensionLinq.True<EmployeeEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.BVN == entity.BVN);
            }
            else if (!string.IsNullOrEmpty(entity.BVN))
            {
                expression = expression.And(t => t.BVN == entity.BVN && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        public bool ExistEmployeeAccountNumber(EmployeeEntity entity)
        {
            var expression = ExtensionLinq.True<EmployeeEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.BankAccountNumber == entity.BankAccountNumber);
            }
            else if (!string.IsNullOrEmpty(entity.BankAccountNumber))
            {
                expression = expression.And(t => t.BankAccountNumber == entity.BankAccountNumber && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        // Generate Employee NHF Number
        public long GenerateNHFNumber()
        {
            var Exist = true;
            var NHFEmployeeNumber = 0L;

            while (Exist)
            {
                var param = new EmployeeListParam()
                {
                    NHFNumber = NHFEmployeeNumber,
                };

                NHFEmployeeNumber = RandomHelper.RandomLongGenerator(GlobalConstant.NHF_NUMBER_START_RANGE, GlobalConstant.NHF_NUMBER_END_RANGE);

                if (IsEmployeeNHFNumberExist(param))
                {
                    Exist = true;
                }
                else
                {
                    Exist = false;
                }
            }
            return NHFEmployeeNumber;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(EmployeeEntity entity)
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

                // Menu, page and button permissions corresponding to roles
                //if (string.IsNullOrEmpty(entity.MenuIds))
                //{
                //    var xx = new LoginDto();
                //    var MenuIds = xx.GetEmployeeMenus();                    //foreach (long menuId in TextHelper.SplitToArray<long>(entity.MenuIds, ','))
                //    foreach (var menuId in MenuIds)
                //    {
                //        MenuAuthorizeEntity menuAuthorizeEntity = new()
                //        {
                //            AuthorizeId = entity.Id,
                //            MenuId = menuId,
                //            AuthorizeType = AuthorizeTypeEnum.User.ToInt()
                //        };
                //        await menuAuthorizeEntity.Create();
                //        await db.Insert(menuAuthorizeEntity);
                //    }
                //}

                // Next of kin record
                if (!string.IsNullOrEmpty(entity.KinFirstName) || !string.IsNullOrEmpty(entity.KinLastName))
                {
                    NextOfKinEntity nextOfKinEntity = new()
                    {
                        Company = entity.Company,
                        Employee = entity.Id,
                        FirstName = entity.KinFirstName,
                        LastName = entity.KinLastName,
                        MobileNumber = entity.KinMobileNumber,
                        Relationship = entity.KinRelationship,
                        EmailAddress = entity.KinEmailAddress,
                        Address = entity.KinAddress
                    };
                    await nextOfKinEntity.Create();
                    await db.Insert(nextOfKinEntity);
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

                //MailParameter mailParameter = new()
                //{
                //    RealName = entity.FirstName,
                //    UserName = entity.EmailAddress,
                //    UserEmail = entity.EmailAddress,
                //    UserPassword = entity.DecryptedPassword,
                //    UserCompany = entity.CompanyName
                //};

                //if (EmailHelper.IsPasswordEmailSent(mailParameter, out message))
                //{
                //    if (string.IsNullOrEmpty(message))
                //    {
                //        await db.CommitTrans();
                //    }
                //}
                await db.CommitTrans();
            }
            catch (Exception ex)
            {
                await db.RollbackTrans();
                throw;
            }
        }

        public async Task SaveForms(EmployeeEntity entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                // Individual company record
                if (!string.IsNullOrEmpty(entity.CoyName) && !string.IsNullOrEmpty(entity.CoyAddress) && !string.IsNullOrEmpty(entity.CoyRCNumber))
                {
                    var currentMenu = await new DataRepository().GetMenuId(GlobalConstant.COMPANY_MENU_URL);
                    CompanyEntity companyEntity = new()
                    {
                        Name = entity.CoyName,
                        Address = entity.CoyAddress,
                        Sector = entity.CoySector,
                        Subsector = entity.CoySubsector,
                        RCNumber = entity.CoyRCNumber,
                        BaseProcessMenu = currentMenu
                    };
                    await companyEntity.Create();
                    entity.Company = companyEntity.Id;
                    await db.Insert(companyEntity);
                }

                // Individual employee record
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await entity.Modify();
                    await db.Update(entity);
                }

                // Next of kin record
                if (!string.IsNullOrEmpty(entity.KinFirstName) && !string.IsNullOrEmpty(entity.KinLastName))
                {
                    NextOfKinEntity nextOfKinEntity = new()
                    {
                        Company = entity.Company,
                        Employee = entity.Id,
                        FirstName = entity.KinFirstName,
                        LastName = entity.KinLastName,
                        MobileNumber = entity.KinMobileNumber,
                        Relationship = entity.KinRelationship,
                        Address = entity.KinAddress,
                        EmailAddress = entity.KinEmailAddress
                    };
                    await nextOfKinEntity.Create();
                    await db.Insert(nextOfKinEntity);
                }

                // User login record
                if (!string.IsNullOrEmpty(entity.UserName) && !entity.Role.IsNullOrZero())
                {
                    var currentMenu = await new DataRepository().GetMenuId(GlobalConstant.USER_MENU_URL);
                    UserEntity userEntity = new()
                    {
                        Company = entity.Company,
                        Employee = entity.Id,
                        UserName = entity.UserName,
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
                    entity.User = userEntity.Id;
                    await db.Insert(userEntity);
                }

                // Role
                if (!entity.Role.IsNullOrZero())
                {
                    UserBelongEntity userBelongEntity = new UserBelongEntity();
                    userBelongEntity.Company = entity.Company;
                    userBelongEntity.Employee = entity.Id;
                    userBelongEntity.Belong = entity.Role;
                    userBelongEntity.BelongType = UserBelongTypeEnum.Role.ParseToInt();
                    await userBelongEntity.Create();
                    await db.Insert(userBelongEntity);
                }

                MailParameter mailParameter = new()
                {
                    UserName = entity.UserName,
                    UserEmail = entity.EmailAddress,
                    UserPassword = entity.DecryptedPassword
                };

                //if (EmailHelper.IsPasswordEmailSent(mailParameter, out message))
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
        //public async Task<EmployeeEntity> GetEntityByNhfNumber(long nhfNo)
        //{
        //    return await BaseRepository().FindEntity<EmployeeEntity>(x => x.NHFNumber == nhfNo);
        //}
        //public async Task<EmployeeEntity> GetEntityByNhfNumber(long nhfNo)
        //{
        //    return await BaseRepository().FindEntity<EmployeeEntity>(x => x.NHFNumber == nhfNo);
        //}


        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<EmployeeEntity>(idArr);
        }

        public async Task<bool> ApproveForm(EmployeeEntity entity, MenuEntity menu, OperatorInfo user, UserEntity loginProfile)
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

                    mailParameter.UserName = loginProfile.UserName;
                    mailParameter.UserEmail = entity.EmailAddress;
                    mailParameter.UserPassword = loginProfile.DecryptedPassword;
                    mailParameter.RealName = entity.LastName + " " + entity.FirstName;
                    mailParameter.UserCompany = entity.CompanyName;
                    mailParameter.NhfNumber = Convert.ToString(entity.NHFNumber);



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



        public async Task RejectForm(EmployeeEntity entity)
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

        #region Private method
        private Expression<Func<EmployeeEntity, bool>> ListFilter(EmployeeListParam param)
        {
            var expression = ExtensionLinq.True<EmployeeEntity>();
            if (param != null)
            {
                if (param.Company > 0)
                {
                    expression = expression.And(t => t.Company == param.Company);
                }


            }
            return expression;
        }

        //private Expression<Func<EmployeeEntity, bool>> ListFilter(EmployeeListParam param)
        //{
        //    var expression = ExtensionLinq.True<EmployeeEntity>();
        //    if (param != null)
        //    {
        //        if (param.Company != 0)
        //        {
        //            expression = expression.And(t => t.Company == param.Company);
        //        }
        //    }
        //    return expression;
        //}

        private Expression<Func<EmployeeEntity, bool>> ListFilters(EmployeeListParam param)
        {
            var expression = ExtensionLinq.True<EmployeeEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.FirstName))
                {
                    expression = expression.And(t => t.FirstName == param.FirstName);
                }

            }
            return expression;
        }


        #endregion
    }
}