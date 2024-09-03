using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Net.NetworkInformation;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class ApprovalSetupService : IApprovalSetupService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ApprovalSetupService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ApprovalSetupEntity>>> GetList(ApprovalSetupListParam param)
        {
            TData<List<ApprovalSetupEntity>> obj = new TData<List<ApprovalSetupEntity>>();
            obj.Data = await _iUnitOfWork.ApprovalSetups.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ApprovalSetupEntity>>> GetPageList(ApprovalSetupListParam param, Pagination pagination)
        {
            TData<List<ApprovalSetupEntity>> obj = new TData<List<ApprovalSetupEntity>>();
            var user = await Operator.Instance.Current();
            param.Company = user.Company;

            obj.Data = await _iUnitOfWork.ApprovalSetups.GetPageList(param, pagination);
            if (obj.Data != null)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                List<BranchEntity> branchList = await _iUnitOfWork.Branches.GetList(new BranchListParam { Ids = obj.Data.Select(p => p.Branch).ToList() });
                List<MenuEntity> menuList = await _iUnitOfWork.Menus.GetList(new MenuListParam { Ids = obj.Data.Select(p => p.MenuId).ToList() });
                List<EmployeeEntity> employeeList = await _iUnitOfWork.Employees.GetList(new EmployeeListParam { Ids = obj.Data.Select(p => p.Authority).ToList() });
                foreach (ApprovalSetupEntity approval in obj.Data)
                {
                    approval.CompanyName = companyList.Where(p => p.Id == approval.Company).Select(p => p.Name).FirstOrDefault();
                    approval.BranchName = branchList.Where(p => p.Id == approval.Branch).Select(p => p.Name).FirstOrDefault();
                    approval.MenuName = menuList.Where(p => p.Id == approval.MenuId).Select(p => p.MenuName).FirstOrDefault();
                    approval.AuthorityName = employeeList.Where(p => p.Id == approval.Authority).Select(p => $"{p.LastName} {p.FirstName}").FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ApprovalSetupEntity>> GetEntity(long id)
        {
            TData<ApprovalSetupEntity> obj = new TData<ApprovalSetupEntity>();
            obj.Data = await _iUnitOfWork.ApprovalSetups.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ApprovalSetupEntity entity)
        {
            var context = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            TData<string> obj = new TData<string>();
            var approvalsetuplist = new ApprovalSetupListParam();
            approvalsetuplist.Authority = entity.Authority1;
            approvalsetuplist.MenuId = entity.MenuId;
            var GetUserAssignedRole = await _iUnitOfWork.ApprovalSetups.GetList(approvalsetuplist);
            if (GetUserAssignedRole.Count > 0)
            {
                obj.Data = entity.Id.ParseToString();
                obj.Tag = 0;
                obj.Message = "Task already assigned to user";
                return obj;

            }
            await _iUnitOfWork.ApprovalSetups.SaveForm(entity);
            var menuid = entity.MenuId;
            var menuinfo = await _iUnitOfWork.Menus.GetEntity(menuid);
            var audit = new AuditTrailEntity();
            audit.Action = SystemOperationCode.AssignPrivilege.ToString();
            audit.ActionRoute = SystemOperationCode.ApprovalSetup.ToStr();
            audit.BaseCreateTime = DateTime.Now;
            audit.Browser = NetHelper.Browser;
            //audit.Company = user.Company;
            audit.UserName = user.Employee.ToStr();
            audit.MacAddress = GetMAC();
            audit.IpAddress = NetHelper.GetPublicIPAddress();
            audit.TargetUserId = entity.Authority1.ToStr();
            var username = await _iUnitOfWork.Employees.GetById(entity.Authority1);
            audit.TargetUserName = username.EmailAddress;
            context.AuditTrails.Add(audit);
            context.SaveChanges();

            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForm2(ApprovalSetupEntity entity)
        {
            var context = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            TData<string> obj = new TData<string>();
            var approvalsetuplist = new ApprovalSetupListParam();
            approvalsetuplist.Authority = entity.Authority1;
            var GetUserAssignedRole = await _iUnitOfWork.ApprovalSetups.GetList(approvalsetuplist);
            var checkDuplicate = GetUserAssignedRole.Where(i => i.MenuId == 563327185478225920 || i.MenuId == 660881219264712704 || i.MenuId == 664553002530508800).ToList();
            foreach (var item in checkDuplicate)
            {
                obj.Data = entity.Id.ParseToString();
                obj.Tag = 0;
                obj.Message = "Permission Already Assigned to user";
                return obj;

            }
            await _iUnitOfWork.ApprovalSetups.SaveForm(entity);
            var menuid = entity.MenuId;
            var menuinfo = await _iUnitOfWork.Menus.GetEntity(menuid);
            var audit = new AuditTrailEntity();
            audit.Action = SystemOperationCode.AssignPrivilege.ToString();
            audit.ActionRoute = SystemOperationCode.ApprovalSetup.ToStr();
            audit.BaseCreateTime = DateTime.Now;
            audit.Browser = NetHelper.Browser;
            //audit.Company = user.Company;
            audit.UserName = user.Employee.ToStr();
            audit.MacAddress = GetMAC();
            audit.IpAddress = NetHelper.GetPublicIPAddress();
            audit.TargetUserId = entity.Authority1.ToStr();
            var username = await _iUnitOfWork.Employees.GetById(entity.Authority1);
            audit.TargetUserName = username.EmailAddress;
            context.AuditTrails.Add(audit);
            context.SaveChanges();

            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }


        private string GetMAC()
        {
            string macAddresses = "";

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }
        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ApprovalSetups.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
