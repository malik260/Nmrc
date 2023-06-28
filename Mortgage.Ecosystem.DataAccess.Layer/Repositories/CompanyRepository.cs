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

        public async Task<CompanyEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CompanyEntity>(id);
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
                if (!string.IsNullOrEmpty(entity.IndBVN) && !string.IsNullOrEmpty(entity.IndFirstName) && !string.IsNullOrEmpty(entity.IndLastName))
                {
                    EmployeeEntity employeeEntity = new()
                    {
                        Company = entity.Id,
                        Branch = GlobalConstant.ZERO,
                        Department = GlobalConstant.ZERO,
                        NHFNumber = entity.NHFNumber,
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
                        PortraitType = string.Empty
                    };
                    await employeeEntity.Create();
                    entity.Employee = employeeEntity.Id;
                    await db.Insert(employeeEntity);
                }

                // User login record
                if (!string.IsNullOrEmpty(entity.UserName) && !entity.Role.IsNullOrZero())
                {
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
                        ApiToken = string.Empty
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

                MailParameter mailParameter = new()
                {
                    RealName = entity.IndFirstName,
                    UserName = entity.UserName,
                    UserEmail = entity.EmailAddress,
                    UserPassword = entity.DecryptedPassword,
                    UserCompany = entity.Name
                };

                if (EmailHelper.IsPasswordEmailSent(mailParameter, out message))
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        await db.CommitTrans();
                    }
                }
                //await db.CommitTrans();
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
            await BaseRepository().Delete<CompanyEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<CompanyEntity, bool>> ListFilter(CompanyListParam param)
        {
            var expression = ExtensionLinq.True<CompanyEntity>();
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