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
using System.Linq;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class CompanyRepository : DataRepository, ICompanyRepository
    {
        #region Retrieve data
        public async Task<List<CompanyEntity>> GetList(CompanyListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<CompanyEntity>> GetPageList(CompanyListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }
        public async Task<List<CompanyEntity>> GetPageList2(CompanyListParam param, Pagination pagination)
        {
            var expression = ListFilter2(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        //public async Task<List<CompanyEntity>> GetApprovalPageList(CompanyListParam param, Pagination pagination)
        //{
        //    var list = await new DataRepository().GetCompanyApprovalItems();
        //    return list.ToList();
        //}

        //public async Task<List<CompanyEntity>> GetApprovalPageList(CompanyListParam param, Pagination pagination)
        //{
        //    var expression = ListFilter(param);
        //    expression = expression.And(i => i.Status == GlobalConstant.ZERO && i.CompanyType == GlobalConstant.ONE);
        //    var baseList = await BaseRepository().FindList(expression, pagination);
        //    var approvalItems = await new DataRepository().GetCompanyApprovalItems();
        //    var combinedList = baseList.Concat(approvalItems)
        //                              .GroupBy(e => e.Id)
        //                              .Select(g => g.First())
        //                              .ToList();
        //    return combinedList;
        //}

        public async Task<List<CompanyEntity>> GetApprovalPageList(CompanyListParam param, Pagination pagination)
        {
            // Build the initial filter expression
            var expression = ListFilter(param);
            expression = expression.And(i => i.Status == GlobalConstant.ZERO && i.CompanyType == GlobalConstant.ONE);

            // Get the base list without pagination
            var baseList = await BaseRepository().FindList(expression);

            // Get approval items
            var approvalItems = await new DataRepository().GetCompanyApprovalItems();

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

        public async Task<CompanyEntity> GetById(long id)
        {
            return await BaseRepository().FindEntity<CompanyEntity>(id);
        }

        public async Task<CompanyEntity> GetByName(string CompanyName)
        {
            return await BaseRepository().FindEntity<CompanyEntity>(x => x.Name == CompanyName);
        }


        public async Task<CompanyEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CompanyEntity>(x => x.Id == id);
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(CompanyEntity entity)
        {
            var expression = ExtensionLinq.True<CompanyEntity>();
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

        public bool ExistRCNumber(CompanyEntity entity)
        {
            var expression = ExtensionLinq.True<CompanyEntity>();
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
        public async Task SaveForm(CompanyEntity entity)
        {
            if (entity.Id.IsNullOrZero() || entity.Id.ToStr().Length < 15)
            {
                await entity.Create();
                await BaseRepository().Insert<CompanyEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<CompanyEntity>(entity);
            }
        }

        public async Task SaveForms(CompanyEntity entity)
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
                if (!string.IsNullOrEmpty(entity.IndFirstName) && !string.IsNullOrEmpty(entity.IndLastName))
                {
                    var currentMenu = await new DataRepository().GetMenuId(GlobalConstant.EMPLOYEE_MENU_URL);
                    EmployeeEntity employeeEntity = new()
                    {
                        Company = entity.Id,
                        Branch = GlobalConstant.ZERO,
                        Department = GlobalConstant.ZERO,
                        NHFNumber = long.Parse(entity.EmployerNhfNumber),
                        BVN = entity.IndBVN,
                        NIN = string.Empty,
                        EmploymentType = EmploymentTypeEnum.Employed.ParseToInt(),
                        DateOfEmployment = DateTime.MinValue,
                        Title = GlobalConstant.ZERO,
                        FirstName = entity.IndFirstName,
                        LastName = entity.IndLastName,
                        OtherName = string.Empty,
                        Gender = GlobalConstant.ZERO,
                        DateOfBirth = entity.IndDateOfBirth,
                        MaritalStatus = GlobalConstant.ZERO,
                        PostalAddress = string.Empty,
                        EmailAddress = entity.EmailAddress,
                        MobileNumber = entity.MobileNumber,
                        StaffNumber = string.Empty,
                        CustomerBank = string.Empty,
                        BankAccountNumber = string.Empty,
                        AccountType = GlobalConstant.ZERO,
                        MonthlySalary = GlobalConstant.ZERO,
                        AlertType = GlobalConstant.ZERO,
                        Portrait = null,
                        PortraitType = string.Empty,
                        BaseProcessMenu = currentMenu,
                        UserType = 1
                        
                    };
                    await employeeEntity.Create();
                    entity.Employee = employeeEntity.Id;
                    await db.Insert(employeeEntity);
                }


                // User login record
                //if (!string.IsNullOrEmpty(entity.UserName) && !entity.Role.IsNullOrZero())
                if (!string.IsNullOrEmpty(entity.UserName))
                {
                    var currentMenu = await new DataRepository().GetMenuId(GlobalConstant.USER_MENU_URL);
                    UserEntity userEntity = new()
                    {
                        Company = entity.Id,
                        Employee = entity.Employee,
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

                if (entity.AgentType == GlobalConstant.SIX.ToString() && entity.LenderType != null)
                {
                    var currentMenu = await new DataRepository().GetMenuId(GlobalConstant.LENDERS_INSTITUTION_MENU_URL);
                    LenderInstitutionsEntity pmbEntity = new LenderInstitutionsEntity();
                    pmbEntity.Id = entity.Id;
                    pmbEntity.RCNumber = entity.RCNumber;
                    pmbEntity.Name = entity.Name;
                    pmbEntity.Website = entity.Website;
                    pmbEntity.MobileNumber = entity.MobileNumber;
                    pmbEntity.Address = entity.Address;
                    pmbEntity.DateOfIncorporation = entity.DateOfIncorporation;
                    pmbEntity.EmailAddress = entity.EmailAddress;
                    pmbEntity.Sector = entity.Sector;
                    pmbEntity.ContributionFrequency = entity.ContributionFrequency;
                    pmbEntity.Subsector = entity.Subsector;
                    pmbEntity.BaseProcessMenu = currentMenu;
                    pmbEntity.BaseCreateTime = DateTime.Now;
                    pmbEntity.BaseCreatorId = entity.BaseCreatorId;
                    pmbEntity.BaseModifierId = entity.BaseModifierId;
                    pmbEntity.NHFNumber = Convert.ToString(entity.EmployerNhfNumber);
                    pmbEntity.Category = Convert.ToInt32(entity.LenderType);

                    await db.Insert(pmbEntity);

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
            catch (Exception ex)
            {
                await db.RollbackTrans();
                throw;
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<CompanyEntity>(idArr);
        }

        public async Task<bool> ApproveForm(CompanyEntity entity, MenuEntity menu, OperatorInfo user)
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
                        NhfNumber = entity.EmployerNhfNumber
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
            catch (Exception e)
            {
                await db.RollbackTrans();
                throw;
            }
        }


        public async Task<bool> DisApproveForm(CompanyEntity entity, OperatorInfo user)
        {
            var message = string.Empty;
            var approvalLog = new ApprovalLogEntity();
            var db = await BaseRepository().BeginTrans();
            try
            {

                entity.Status = (int)ApprovalEnum.Rejected;
                await entity.Modify();
                await db.Update(entity);

                //user.UserStatus = (int)ApprovalEnum.Rejected;
                //await entity.Modify();
                //await db.Update(user);


                await db.CommitTrans();
                return true;
            }
            catch (Exception e)
            {
                await db.RollbackTrans();
                throw;
            }
        }



        #endregion

        #region Private method
        private Expression<Func<CompanyEntity, bool>> ListFilter(CompanyListParam param)
        {
            // param.CompanyType = 1;
            var expression = ExtensionLinq.True<CompanyEntity>();
            if (param != null)
            {
                if (param.Name != null)
                {
                    expression = expression.And(t => t.Name.Contains(param.Name));
                }
            }
            return expression;
        }

        private Expression<Func<CompanyEntity, bool>> ListFilter2(CompanyListParam param)
        {
            param.CompanyType = 1;
            var expression = ExtensionLinq.True<CompanyEntity>();
            if (param != null)
            {
                if (param.CompanyType != 0)
                {
                    expression = expression.And(t => t.CompanyType == param.CompanyType);
                }
            }
            return expression;
        }

        #endregion
    }
}