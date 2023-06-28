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
    public class EmployeeRepository : DataRepository, IEmployeeRepository
    {
        #region Retrieve data
        public async Task<List<EmployeeEntity>> GetList(EmployeeListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<EmployeeEntity>> GetPageList(EmployeeListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<EmployeeEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(id);
        }

        public async Task<EmployeeEntity> GetById(long id)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(id);
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
                if (!string.IsNullOrEmpty(entity.MenuIds))
                {
                    foreach (long menuId in TextHelper.SplitToArray<long>(entity.MenuIds, ','))
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
                        ApiToken = string.Empty
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

                if (EmailHelper.IsPasswordEmailSent(mailParameter, out message))
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        await db.CommitTrans();
                    }
                }
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
                    CompanyEntity companyEntity = new()
                    {
                        Name = entity.CoyName,
                        Address = entity.CoyAddress,
                        Sector = entity.CoySector,
                        Subsector = entity.CoySubsector,
                        RCNumber = entity.CoyRCNumber
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

                // User login record
                if (!string.IsNullOrEmpty(entity.UserName) && !entity.Role.IsNullOrZero())
                {
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
            await BaseRepository().Delete<EmployeeEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<EmployeeEntity, bool>> ListFilter(EmployeeListParam param)
        {
            var expression = ExtensionLinq.True<EmployeeEntity>();
            if (param != null)
            {
                if (param.NHFNumber > 0)
                {
                    expression = expression.And(t => t.NHFNumber == param.NHFNumber);
                }

                if (!string.IsNullOrEmpty(param.FirstName) && !string.IsNullOrEmpty(param.LastName))
                {
                    expression = expression.And(t => t.FirstName.Contains(param.FirstName) && t.LastName.Contains(param.LastName));
                }
            }
            return expression;
        }
        #endregion
    }
}