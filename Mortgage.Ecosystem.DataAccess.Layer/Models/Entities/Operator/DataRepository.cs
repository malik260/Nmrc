using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories.Base;
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
                                    a.WebToken,
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
                    strSql.Append(@"SELECT a.Id, a.Company, a.Branch, a.Department, a.BVN, a.NIN,
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

                #region roles

                if (!string.IsNullOrEmpty(operatorInfo.Company.ToString()) && !string.IsNullOrEmpty(operatorInfo.Employee.ToString()))
                {
                    strSql.Clear();
                    strSql.Append(@"SELECT a.Belong as RoleId
                                FROM tbl_UserBelong a
                                WHERE a.Company = " + operatorInfo.Company + " AND a.Employee = " + operatorInfo.Employee + " AND a.BelongType = " + UserBelongTypeEnum.Role.ToInt());
                    IEnumerable<RoleInfo> roleList = await BaseRepository().FindList<RoleInfo>(strSql.ToString());
                    operatorInfo.RoleIds = string.Join(",", roleList.Select(p => p.RoleId).ToArray());
                }

                #endregion roles
            }
            return operatorInfo;
        }
    }
}