using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories.Base;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Utilities.Zlib;
using System.Text;

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
                                    a.ApiToken,
                                    a.IsSystem,
                                    a.Pmb,
                                    a.Developer
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

                #region Pmb Info

                if (!string.IsNullOrEmpty(operatorInfo.Pmb.ToString()))
                {
                    strSql.Clear();
                    strSql.Append(@"SELECT a.Id, a.Name, a.Address, a.MobileNumber, a.EmailAddress,a.NHFNumber,
                                        a.RCNumber                                        
                                FROM tbl_Pmb a
                                WHERE a.Id = " + operatorInfo.Pmb);
                    var pmb = await BaseRepository().FindObject<PmbListParam>(strSql.ToString());
                    operatorInfo.PmbInfo = MapHelper.Map(pmb, operatorInfo.PmbInfo);
                }

                #endregion Pmb Info

                #region developer Info

                if (!string.IsNullOrEmpty(operatorInfo.Developer.ToString()))
                {
                    strSql.Clear();
                    strSql.Append(@"SELECT a.Id, a.Name, a.Address, a.MobileNumber, a.EmailAddress,
                                        a.RCNumber                                        
                                FROM tbl_Developer a
                                WHERE a.Id = " + operatorInfo.Developer);
                    var developer = await BaseRepository().FindObject<DeveloperListParam>(strSql.ToString());
                    operatorInfo.DeveloperInfo = MapHelper.Map(developer, operatorInfo.DeveloperInfo);
                }

                #endregion developer Info


                #region Employee Info

                if (!string.IsNullOrEmpty(operatorInfo.Employee.ToString()) && !string.IsNullOrEmpty(operatorInfo.Company.ToString()))
                {
                    strSql.Clear();
                    strSql.Append(@"SELECT a.Id, a.Company, a.Branch, a.Department, a.BVN, a.NHFNumber, a.NIN,
                                        a.EmploymentType, a.Title, a.LastName, a.FirstName, a.EmailAddress,
                                        a.StaffNumber, a.EmployerType, a.DateOfBirth, a.Gender, a.MobileNumber, a.Portrait, a.PortraitType
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
            try
            {

                var strSql = new StringBuilder();
                strSql.Clear();
                var user = await Operator.Instance.Current();

                IEnumerable<EmployeeEntity> employeeLists = Enumerable.Empty<EmployeeEntity>();
                if (user.EmployeeInfo == null)
                {
                    return employeeLists;
                }

                strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Employee a 
                    INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id
                    WHERE a.Company = " + user.Company);
                var process = await BaseRepository().FindList<EmployeeProcessParam>(strSql.ToString());

                var approvalLogListParam = new ApprovalLogListParam()
                {
                    Company = user.Company,
                    MenuId = process.FirstOrDefault().BaseProcessMenu,
                    Authority = user.Employee
                };
                var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
                var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
                var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
                strSql.Clear();
                if (approvalLogRecords.Count <= 0)
                {
                    strSql.Append(@"SELECT a.* FROM tbl_Employee a
                        INNER JOIN tbl_ApprovalSetup b ON b.MenuId = a.BaseProcessMenu
                        INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND b.MenuId = c.Id
                        WHERE b.Authority = " + user.Employee + " AND b.Priority = 1 AND c.ApprovalLevel > 0 AND a.UserType != 1 AND a.Status != 1");
                }
                else
                {
                    strSql.Append(@"SELECT a.* FROM tbl_Employee a
                        INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                        INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                        INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                        WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount <= d.ApprovalLevel AND a.Status != 1");
                }
                IEnumerable<EmployeeEntity> employeeList = await BaseRepository().FindList<EmployeeEntity>(strSql.ToString());
                return employeeList.GroupBy(x => new { x.Id, x.Company })
                                    .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                                    .OrderByDescending(x => x.BaseModifyTime)
                                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CompanyEntity>> GetCompanyApprovalItems()
        {
            var strSql = new StringBuilder();
            strSql.Clear();
            var user = await Operator.Instance.Current();

            IEnumerable<CompanyEntity> employeeLists = Enumerable.Empty<CompanyEntity>();
            if (user.EmployeeInfo == null)
            {
                return employeeLists;
            }

            strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Company a 
                    INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id
                    WHERE a.Id = " + user.Company);
            var process = await BaseRepository().FindList<EmployeeProcessParam>(strSql.ToString());

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = process.FirstOrDefault()?.BaseProcessMenu,
                Authority = user.Employee
            };

            var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
            var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
            var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
            strSql.Clear();
            if (approvalLogRecords.Count <= 0)
            {
                //strSql.Append(@"SELECT a.* FROM tbl_Company a
                //                INNER JOIN tbl_ApprovalSetup b ON a.Id = b.Company
                //                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND b.MenuId = c.Id
                //                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND b.Priority = 1 AND c.ApprovalLevel > 0 AND a.Status != 1");

                strSql.Append(@"SELECT a.* FROM tbl_Company a
                        INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id
                        INNER JOIN tbl_ApprovalSetup b ON c.Id = b.MenuId
                        INNER JOIN tbl_Employee e ON a.Id = e.Company
                        WHERE b.Authority  = " + user.Employee + "  AND b.Priority = 1 AND c.ApprovalLevel > 0 AND e.UserType = 1 AND a.CompanyType = 1 AND a.Status != 1 ");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_Company a
                        INNER JOIN tbl_ApprovalSetup b ON a.Id > 0
                        INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                        INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                        WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount <= d.ApprovalLevel AND a.Status != 1");
            }
            IEnumerable<CompanyEntity> companyList = await BaseRepository().FindList<CompanyEntity>(strSql.ToString());
            return companyList.GroupBy(x => new { x.Id, x.Name })
                        .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                        .OrderByDescending(x => x.BaseModifyTime)
                        .ToList();
        }



        public async Task<IEnumerable<PmbEntity>> GetPmbApprovalItems()
        {
            var strSql = new StringBuilder();
            strSql.Clear();
            var user = await Operator.Instance.Current();

            strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Pmb a 
                    INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id");

            var process = await BaseRepository().FindList<PmbProcessParam>(strSql.ToString());

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = process.FirstOrDefault()?.BaseProcessMenu,
                Authority = user.Employee
            };

            var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
            var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
            var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
            strSql.Clear();
            if (approvalLogRecords.Count <= 0)
            {
                strSql.Append(@"SELECT a.* FROM tbl_Pmb a
                        INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id
                        INNER JOIN tbl_ApprovalSetup b ON c.Id = b.MenuId
                        INNER JOIN tbl_Employee e ON a.Id = e.Company
                        WHERE b.Authority  = " + user.Employee + "  AND b.Priority = 1 AND c.ApprovalLevel > 0 AND e.UserType = 1 AND a.Status != 1");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_Pmb a
                        INNER JOIN tbl_ApprovalSetup b ON a.Id > 0
                        INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                        INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                        WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount <= d.ApprovalLevel AND a.Status != 1");
            }
            IEnumerable<PmbEntity> PmbList = await BaseRepository().FindList<PmbEntity>(strSql.ToString());
            return PmbList.GroupBy(x => new { x.Id, x.Name })
                        .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                        .OrderByDescending(x => x.BaseModifyTime)
                        .ToList();
        }


        public async Task<IEnumerable<BrokerEntity>> GetBrokerApprovalItems()
        {
            var strSql = new StringBuilder();
            strSql.Clear();
            var user = await Operator.Instance.Current();

            strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Broker a 
                            INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id");

            var process = await BaseRepository().FindList<PmbProcessParam>(strSql.ToString());

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = process.SingleOrDefault().BaseProcessMenu,
                Authority = user.Employee
            };

            var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
            var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
            var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
            strSql.Clear();
            if (approvalLogRecords.Count <= 0)
            {
                strSql.Append(@"SELECT a.* FROM tbl_Broker a
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id
                                INNER JOIN tbl_ApprovalSetup b ON c.Id = b.MenuId
                                WHERE b.Authority  = " + user.Employee + "  AND b.Priority = 1 AND c.ApprovalLevel > 0 AND a.Status != 1");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_Broker a
                                INNER JOIN tbl_ApprovalSetup b ON a.Id > 0
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount <= d.ApprovalLevel AND a.Status != 1");
            }
            IEnumerable<BrokerEntity> BrokerList = await BaseRepository().FindList<BrokerEntity>(strSql.ToString());
            return BrokerList.GroupBy(x => new { x.Id, x.Name })
                        .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                        .OrderByDescending(x => x.BaseModifyTime)
                        .ToList();
        }



        public async Task<IEnumerable<DeveloperEntity>> GetDeveloperApprovalItems()
        {
            var strSql = new StringBuilder();
            strSql.Clear();
            var user = await Operator.Instance.Current();

            strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Developer a 
                            INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id");

            var process = await BaseRepository().FindList<PmbProcessParam>(strSql.ToString());

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = process.SingleOrDefault().BaseProcessMenu,
                Authority = user.Employee
            };

            var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
            var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
            var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
            strSql.Clear();
            if (approvalLogRecords.Count <= 0)
            {
                strSql.Append(@"SELECT a.* FROM tbl_Developer a
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id
                                INNER JOIN tbl_ApprovalSetup b ON c.Id = b.MenuId
                                WHERE b.Authority  = " + user.Employee + "  AND b.Priority = 1 AND c.ApprovalLevel > 0 AND a.Status != 1");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_Developer a
                                INNER JOIN tbl_ApprovalSetup b ON a.Id > 0
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount <= d.ApprovalLevel AND a.Status != 1");
            }
            IEnumerable<DeveloperEntity> PmbList = await BaseRepository().FindList<DeveloperEntity>(strSql.ToString());
            return PmbList.GroupBy(x => new { x.Id, x.Name })
                        .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                        .OrderByDescending(x => x.BaseModifyTime)
                        .ToList();
        }


        public async Task<IEnumerable<ETicketEntity>> GetETicketApprovalItems()
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

            var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
            var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
            var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
            strSql.Clear();
            if (approvalLogRecords.Count <= 0)
            {
                strSql.Append(@"SELECT a.* FROM tbl_ETicket a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND b.MenuId = c.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND b.Priority = 1 AND c.ApprovalLevel > 0");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_ETicket a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount < d.ApprovalLevel");
            }
            IEnumerable<ETicketEntity> eTicketList = await BaseRepository().FindList<ETicketEntity>(strSql.ToString());
            return eTicketList.GroupBy(x => new { x.Id, x.Company })
                    .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                    .OrderByDescending(x => x.BaseModifyTime)
                    .ToList();
        }

        public async Task<IEnumerable<UnderwritingEntity>> GetRiskRatedLoan()
        {
            var strSql = new StringBuilder();
            strSql.Clear();
            var user = await Operator.Instance.Current();

            strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Employee a 
                            INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id
                            WHERE a.Id = " + user.Employee);
            var process = await BaseRepository().FindList<EmployeeProcessParam>(strSql.ToString());

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = process.FirstOrDefault().BaseProcessMenu,
                Authority = user.Employee
            };

            var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
            var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
            var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
            strSql.Clear();
            if (approvalLogRecords.Count <= 0)
            {
                strSql.Append(@"SELECT a.* FROM tbl_Underwriting a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND '563327185478225920' = c.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND b.Priority = 1 AND a.CheckList = 1  AND a.Rated = 1 AND a.Reviewed != 1 AND a.Approved != 1 AND c.ApprovalLevel > 0");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_Underwriting a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount < d.ApprovalLevel");
            }
            IEnumerable<UnderwritingEntity> risk = await BaseRepository().FindList<UnderwritingEntity>(strSql.ToString());
            return risk.GroupBy(x => new { x.Id })
                    .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                    .OrderByDescending(x => x.BaseModifyTime)
                    .ToList();
        }


        public async Task<IEnumerable<UnderwritingEntity>> GetLoanforUnderwriting()
        {
            var strSql = new StringBuilder();
            strSql.Clear();
            var user = await Operator.Instance.Current();

            strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Employee a 
                            INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id
                            WHERE a.Id = " + user.Employee);
            var process = await BaseRepository().FindList<EmployeeProcessParam>(strSql.ToString());

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = process.FirstOrDefault().BaseProcessMenu,
                Authority = user.Employee
            };

            var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
            var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
            var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
            strSql.Clear();
            if (approvalLogRecords.Count <= 0)
            {
                strSql.Append(@"SELECT a.* FROM tbl_Underwriting a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND b.MenuId = c.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND b.Priority = 1 AND a.CheckList = 0  AND a.Rated = 0 AND a.Approved != 1 AND c.ApprovalLevel > 0");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_Underwriting a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount < d.ApprovalLevel");
            }
            IEnumerable<UnderwritingEntity> risk = await BaseRepository().FindList<UnderwritingEntity>(strSql.ToString());
            return risk.GroupBy(x => new { x.Id })
                    .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                    .OrderByDescending(x => x.BaseModifyTime)
                    .ToList();
        }




        public async Task<IEnumerable<UnderwritingEntity>> GetReviewedLoan()
        {
            var strSql = new StringBuilder();
            strSql.Clear();
            var user = await Operator.Instance.Current();

            strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Employee a 
                            INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id
                            WHERE a.Id = " + user.Employee);
            var process = await BaseRepository().FindList<EmployeeProcessParam>(strSql.ToString());

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = process.SingleOrDefault().BaseProcessMenu,
                Authority = user.Employee
            };

            var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
            var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
            var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
            strSql.Clear();
            if (approvalLogRecords.Count <= 0)
            {
                strSql.Append(@"SELECT a.* FROM tbl_Underwriting a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND b.MenuId = c.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND b.Priority = 1 AND a.CheckList = 1  AND a.Rated = 1 AND a.Reviewed = 1 AND a.Approved != 1 AND c.ApprovalLevel > 0");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_Underwriting a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount < d.ApprovalLevel");
            }
            IEnumerable<UnderwritingEntity> risk = await BaseRepository().FindList<UnderwritingEntity>(strSql.ToString());
            return risk.GroupBy(x => new { x.Id })
                    .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                    .OrderByDescending(x => x.BaseModifyTime)
                    .ToList();
        }
        public async Task<IEnumerable<CustomerProfileUpdateEntity>> GetCustomerUpdateApprovalItems()
        {
            try
            {

                var strSql = new StringBuilder();
                strSql.Clear();
                var user = await Operator.Instance.Current();

                IEnumerable<CustomerProfileUpdateEntity> employeeUpdateLists = Enumerable.Empty<CustomerProfileUpdateEntity>();
                if (user.EmployeeInfo == null)
                {
                    return employeeUpdateLists;
                }

                strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Employee a 
                            INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id
                            WHERE a.Company = " + user.Company);
                var process = await BaseRepository().FindList<EmployeeProcessParam>(strSql.ToString());

                var approvalLogListParam = new ApprovalLogListParam()
                {
                    Company = user.Company,
                    MenuId = process.FirstOrDefault().BaseProcessMenu,
                    Authority = user.Employee
                };
                var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
                var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
                var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
                strSql.Clear();
                if (approvalLogRecords.Count <= 0)
                {
                    strSql.Append(@"SELECT a.* FROM tbl_CustomerProfileUpdate a
                                INNER JOIN tbl_ApprovalSetup b ON b.MenuId = a.BaseProcessMenu
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND b.MenuId = c.Id
                                WHERE b.Authority = " + user.Employee + " AND b.Priority = 1 AND c.ApprovalLevel > 0 AND a.Status = 0");
                }
                else
                {
                    strSql.Append(@"SELECT a.* FROM tbl_CustomerProfileUpdate a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount <= d.ApprovalLevel AND a.Status != 1");
                }
                IEnumerable<CustomerProfileUpdateEntity> employeeUpdateList = await BaseRepository().FindList<CustomerProfileUpdateEntity>(strSql.ToString());
                return employeeUpdateList.GroupBy(x => new { x.Id, x.Company })
                                    .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                                    .OrderByDescending(x => x.BaseModifyTime)
                                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<UnderwritingEntity>> GetApprovedLoan()
        {
            var strSql = new StringBuilder();
            strSql.Clear();
            var user = await Operator.Instance.Current();

            strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Employee a 
                            INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id
                            WHERE a.Id = " + user.Employee);
            var process = await BaseRepository().FindList<EmployeeProcessParam>(strSql.ToString());

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = process.SingleOrDefault().BaseProcessMenu,
                Authority = user.Employee
            };

            var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
            var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
            var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
            strSql.Clear();
            string menu = "664553002530508800";
            if (approvalLogRecords.Count <= 0)
            {
                

                strSql.Append(@"SELECT a.* FROM tbl_Underwriting a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_Employee d ON d.Id = b.Authority 
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND '664553002530508800' = c.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND b.Priority = 1 AND a.CheckList = 1  AND a.Rated = 1 AND a.Reviewed = 1 AND a.isBatched = 0 AND c.ApprovalLevel > 0");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_Underwriting a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount < d.ApprovalLevel");
            }
            IEnumerable<UnderwritingEntity> risk = await BaseRepository().FindList<UnderwritingEntity>(strSql.ToString());
            return risk.GroupBy(x => new { x.Id })
                    .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                    .OrderByDescending(x => x.BaseModifyTime)
                    .ToList();
        }


        public async Task<IEnumerable<UnderwritingEntity>> GetBatchedLoan()
        {
            var strSql = new StringBuilder();
            strSql.Clear();
            var user = await Operator.Instance.Current();

            strSql.Append(@"SELECT DISTINCT a.BaseProcessMenu FROM tbl_Employee a 
                            INNER JOIN tbl_Menu b ON a.BaseProcessMenu = b.Id
                            WHERE a.Id = " + user.Employee);
            var process = await BaseRepository().FindList<EmployeeProcessParam>(strSql.ToString());

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = process.SingleOrDefault().BaseProcessMenu,
                Authority = user.Employee
            };

            var approvalLogRecords = await new ApprovalLogRepository().GetList(approvalLogListParam);
            var approvalLogList = approvalLogRecords.Select(el => $"{el.Record},").Aggregate("", (el1, el2) => el1 + el2).TrimEnd(',');
            var approvalLogBracketList = $"({approvalLogList})".Replace("\"", "");
            strSql.Clear();
            if (approvalLogRecords.Count <= 0)
            {
                strSql.Append(@"SELECT a.* FROM tbl_Underwriting a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND '664553002530508800' = c.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND b.Priority = 1 AND a.CheckList = 1  AND a.Rated = 1 AND a.Reviewed = 1 AND a.isBatched = 1 AND c.ApprovalLevel > 0");
            }
            else
            {
                strSql.Append(@"SELECT a.* FROM tbl_Underwriting a
                                INNER JOIN tbl_ApprovalSetup b ON a.Company = b.Company
                                INNER JOIN tbl_ApprovalLog c ON b.Company = c.Company AND b.Authority = c.Authority AND b.MenuId = c.MenuId
                                INNER JOIN tbl_Menu d ON a.BaseProcessMenu = d.Id AND b.MenuId = d.Id
                                WHERE b.Company = " + user.Company + " AND b.Authority = " + user.Employee + " AND a.Id NOT IN " + approvalLogBracketList + " AND b.Priority = c.ApprovalCount AND d.ApprovalLevel > 0 AND c.ApprovalCount < d.ApprovalLevel");
            }
            IEnumerable<UnderwritingEntity> risk = await BaseRepository().FindList<UnderwritingEntity>(strSql.ToString());
            return risk.GroupBy(x => new { x.Id })
                    .SelectMany(x => x.OrderByDescending(y => y.BaseCreateTime).Take(1))
                    .OrderByDescending(x => x.BaseModifyTime)
                    .ToList();
        }


    }
}