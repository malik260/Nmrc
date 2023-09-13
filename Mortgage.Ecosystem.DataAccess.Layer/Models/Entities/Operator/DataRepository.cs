using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories.Base;
using NPOI.SS.Formula.Functions;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator
{
    public class DataRepository : RepositoryFactory
    {
        public async Task<OperatorInfo?> GetUserByToken(string token)
        {
            if (!SecurityHelper.IsSafeSqlParam(token))
            {
                return null;
            }
            token = token.ToStr().Trim();

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT a.Id,
                                    a.Company,
                                    a.Employee,
                                    a.LoginCount,
                                    a.UserStatus,
                                    a.IsOnline,
                                    a.UserName,
                                    a.RealName,
                                    a.Password,
                                    a.Salt,
                                    a.WebToken,
                                    a.Password,
                                    a.Salt,
                                    a.ApiToken,
                                    a.IsSystem
                            FROM tbl_User a
                            WHERE WebToken = '" + token + "' or ApiToken = '" + token + "' ");
            var operatorInfo = await BaseRepository().FindObject<OperatorInfo>(strSql.ToString());
            if (operatorInfo != null)
            {
                #region Company Info

                if (!string.IsNullOrEmpty(operatorInfo.Company.ToString()))
                {
                    strSql.Clear();
                    strSql.Append(@"SELECT a.Id, a.Name, a.Address, a.MobileNumber, a.EmailAddress,
                                        a.RCNumber, a.CompanyClass, a.CompanyType,
                                        a.Logo, a.LogoType
                                FROM tbl_Company a
                                WHERE a.Id = " + operatorInfo.Company);
                    var company = await BaseRepository().FindObject<CompanyListParam>(strSql.ToString());
                    operatorInfo.CompanyInfo = MapHelper.Map(company, operatorInfo.CompanyInfo);
                }

                #endregion Company Info

                #region Employee Info

                if (!string.IsNullOrEmpty(operatorInfo.Employee.ToString()) && !string.IsNullOrEmpty(operatorInfo.Company.ToString()))
                {
                    strSql.Clear();
                    strSql.Append(@"SELECT a.Id, a.Company, a.Branch, a.Department, a.BVN, a.NHFNumber, a.NIN,
                                        a.EmploymentType, a.Title, a.LastName, a.FirstName, a.EmailAddress,
                                        a.StaffNumber, a.DateOfBirth, a.Gender, a.Portrait, a.PortraitType
                                FROM tbl_Employee a
                                WHERE a.Id = " + operatorInfo.Employee + " AND a.Company = " + operatorInfo.Company);
                    var employee = await BaseRepository().FindObject<EmployeeListParam>(strSql.ToString());
                    operatorInfo.EmployeeInfo = MapHelper.Map(employee, operatorInfo.EmployeeInfo);
                }

                #endregion Employee Info

                #region Branch Info

                if (!string.IsNullOrEmpty(operatorInfo.EmployeeInfo?.Branch.ToString()) && !string.IsNullOrEmpty(operatorInfo.EmployeeInfo?.Company.ToString()))
                {
                    strSql.Clear();
                    strSql.Append(@"SELECT a.Id, a.Company, a.Name, a.Address, a.MobileNumber,
                                        a.Location, a.State, a.Nationality, a.Manager
                                FROM tbl_Branch a
                                WHERE a.Id = " + operatorInfo.EmployeeInfo?.Branch + " AND a.Company = " + operatorInfo.EmployeeInfo?.Company);
                    var branch = await BaseRepository().FindObject<BranchListParam>(strSql.ToString());
                    operatorInfo.BranchInfo = MapHelper.Map(branch, operatorInfo.BranchInfo);
                }

                #endregion Branch Info

                #region Department Info

                if (!string.IsNullOrEmpty(operatorInfo.EmployeeInfo?.Department.ToString()) && !string.IsNullOrEmpty(operatorInfo.EmployeeInfo?.Branch.ToString()) && !string.IsNullOrEmpty(operatorInfo.EmployeeInfo?.Company.ToString()))
                {
                    strSql.Clear();
                    strSql.Append(@"SELECT a.Id, a.Company, a.Branch, a.Name,
                                        a.Telephone, a.Email, a.Principal
                                FROM tbl_Department a
                                WHERE a.Id = " + operatorInfo.EmployeeInfo?.Department + " AND a.Branch = " + operatorInfo.EmployeeInfo?.Branch + " AND a.Company = " + operatorInfo.EmployeeInfo?.Company);
                    var department = await BaseRepository().FindObject<DepartmentListParam>(strSql.ToString());
                    operatorInfo.DepartmentInfo = MapHelper.Map(department, operatorInfo.DepartmentInfo);
                }

                #endregion Department Info

                #region Roles

                if (!string.IsNullOrEmpty(operatorInfo.Company.ToString()) && !string.IsNullOrEmpty(operatorInfo.Employee.ToString()))
                {
                    strSql.Clear();
                    strSql.Append(@"SELECT a.Belong as RoleId
                                FROM tbl_UserBelong a
                                WHERE a.Company = " + operatorInfo.Company + " AND a.Employee = " + operatorInfo.Employee + " AND a.BelongType = " + UserBelongTypeEnum.Role.ToInt());
                    IEnumerable<RoleInfo> roleList = await BaseRepository().FindList<RoleInfo>(strSql.ToString());
                    operatorInfo.RoleIds = string.Join(",", roleList.Select(p => p.RoleId).ToArray());
                }

                #endregion Roles

                #region Approval Employee Notification
                if (!string.IsNullOrEmpty(operatorInfo.Company.ToString()) && !string.IsNullOrEmpty(operatorInfo.Employee.ToString()))
                {
                    strSql.Clear();
                    operatorInfo.ApprovalItemCount = 0;
                    var approvalLogListParam = new ApprovalLogListParam()
                    {
                        Company = operatorInfo.Company,
                        //Branch = operatorInfo.Branch,
                        Authority = operatorInfo.Employee
                    };
                    var approvalLogRecords = new ApprovalLogRepository().GetList(approvalLogListParam);
                    if (approvalLogRecords != null)
                    {
                        strSql.Append(@"SELECT a.* FROM tbl_Employee a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND b.MenuId = c.Id
                                WHERE b.Company = " + operatorInfo.Company + " AND b.Authority = " + operatorInfo.Employee + " AND c.ApprovalLevel > 0");
                    }
                    else
                    {
                        strSql.Append(@"SELECT a.* FROM tbl_Employee a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + operatorInfo.Company + " AND b.Authority = " + operatorInfo.Employee + " AND c.ApprovalCount <= 0 AND d.ApprovalLevel > 0 AND c.ApprovalCount < d.ApprovalLevel");
                    }
                    IEnumerable<EmployeeEntity> employeeList = await BaseRepository().FindList<EmployeeEntity>(strSql.ToString());
                    operatorInfo.ApprovalEmployeeItems = MapHelper.Map(employeeList, operatorInfo.ApprovalEmployeeItems);
                    operatorInfo.ApprovalEmployeeItems = operatorInfo.ApprovalEmployeeItems.ToList();
                    operatorInfo.ApprovalItemCount = operatorInfo.ApprovalEmployeeItems.Count;
                }
                #endregion Approval Employee Notification

                #region Approval Employer Notification
                if (!string.IsNullOrEmpty(operatorInfo.Company.ToString()) && !string.IsNullOrEmpty(operatorInfo.Employee.ToString()))
                {
                    strSql.Clear();
                    var approvalLogListParam = new ApprovalLogListParam()
                    {
                        Company = operatorInfo.Company,
                        //Branch = operatorInfo.Branch,
                        Authority = operatorInfo.Employee
                    };
                    var approvalLogRecords = new ApprovalLogRepository().GetList(approvalLogListParam);
                    if (approvalLogRecords != null)
                    {
                        strSql.Append(@"SELECT a.* FROM tbl_Company a
                                INNER JOIN tbl_ApprovalSetup b ON a.Id = b.Company
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND b.MenuId = c.Id
                                WHERE b.Company = " + operatorInfo.Company + " AND b.Authority = " + operatorInfo.Employee + " AND c.ApprovalLevel > 0");
                    }
                    else
                    {
                        strSql.Append(@"SELECT a.* FROM tbl_Company a
                                INNER JOIN tbl_ApprovalSetup b ON a.Id = b.Company
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + operatorInfo.Company + " AND b.Authority = " + operatorInfo.Employee + " AND c.ApprovalCount <= 0 AND d.ApprovalLevel > 0 AND c.ApprovalCount < d.ApprovalLevel");
                    }
                    IEnumerable<CompanyEntity> companyList = await BaseRepository().FindList<CompanyEntity>(strSql.ToString());
                    operatorInfo.ApprovalEmployerItems = MapHelper.Map(companyList, operatorInfo.ApprovalEmployerItems);
                    operatorInfo.ApprovalEmployerItems = operatorInfo.ApprovalEmployerItems.ToList();
                    operatorInfo.ApprovalItemCount += operatorInfo.ApprovalEmployerItems.Count;
                }
                #endregion Approval Employer Notification
            }
            return operatorInfo;
        }

        public async Task<long> GetMenuId(string menuUrl)
        {
            menuUrl = menuUrl.ToStr().Trim();
            var menuId = 0L;
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT a.* FROM tbl_Menu a WHERE a.MenuUrl = " + $"'{menuUrl}'");
            var menu = await BaseRepository().FindObject<MenuEntity>(strSql.ToString());
            if (menu != null)
            {
                menuId = menu.Id;
            }
            return menuId;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetEmployeeApprovalItems()
        {
            var strSql = new StringBuilder();
            strSql.Clear();
            var user = await Operator.Instance.Current();

            strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Employee a 
                            INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id
                            WHERE a.Company = " + user.Company);
            var process = await BaseRepository().FindList<EmployeeProcessParam>(strSql.ToString());

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = process.SingleOrDefault().BaseProcessMenu,
                Authority = user.Employee
            };
            var approvalLogRecords = new ApprovalLogRepository().GetList(approvalLogListParam);
            if (approvalLogRecords != null)
            {
                strSql.Append(@"SELECT a.* FROM tbl_Employee a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND b.MenuId = c.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND c.ApprovalLevel > 0");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_Employee a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND c.ApprovalCount <= 0 AND d.ApprovalLevel > 0 AND c.ApprovalCount < d.ApprovalLevel");
            }
            IEnumerable<EmployeeEntity> employeeList = await BaseRepository().FindList<EmployeeEntity>(strSql.ToString());
            return employeeList;
        }

        public async Task<IEnumerable<CompanyEntity>> GetCompanyApprovalItems()
        {
            var strSql = new StringBuilder();
            strSql.Clear();
            var user = await Operator.Instance.Current();

            strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Company a 
                            INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id
                            WHERE a.Id = " + user.Company);
            var process = await BaseRepository().FindList<EmployeeProcessParam>(strSql.ToString());

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = process.SingleOrDefault().BaseProcessMenu,
                Authority = user.Employee
            };
            var approvalLogRecords = new ApprovalLogRepository().GetList(approvalLogListParam);
            if (approvalLogRecords != null)
            {
                strSql.Append(@"SELECT a.* FROM tbl_Company a
                                INNER JOIN tbl_ApprovalSetup b ON a.Id = b.Company
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND b.MenuId = c.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND c.ApprovalLevel > 0");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_Company a
                                INNER JOIN tbl_ApprovalSetup b ON a.Id = b.Company
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND c.ApprovalCount <= 0 AND d.ApprovalLevel > 0 AND c.ApprovalCount < d.ApprovalLevel");
            }
            IEnumerable<CompanyEntity> companyList = await BaseRepository().FindList<CompanyEntity>(strSql.ToString());
            return companyList;
        }

    }
}