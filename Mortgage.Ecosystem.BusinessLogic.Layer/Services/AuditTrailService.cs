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
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Net.NetworkInformation;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class AuditTrailService : IAuditTrailService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public AuditTrailService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<AuditTrailEntity>>> GetList(AuditTrailListParam param)
        {
            TData<List<AuditTrailEntity>> obj = new TData<List<AuditTrailEntity>>();
            obj.Data = await _iUnitOfWork.AuditTrails.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AuditTrailEntity>>> GetPageList(AuditTrailListParam param, Pagination pagination)
        {
            var context = new ApplicationDbContext();
            TData<List<AuditTrailEntity>> obj = new TData<List<AuditTrailEntity>>();
            obj.Data = await _iUnitOfWork.AuditTrails.GetPageList(param, pagination);
            var Admins = new UserListParam();
            var AdminUsers = _iUnitOfWork.Users.GetList(Admins).Result.Select(i => i.Company).ToList();
            var query = from t1 in obj.Data
                        join t2 in context.UserEntity on t1.UserName equals t2.Employee.ToString()
                        where t2.IsSystem == 0 && t2.UserStatus == 1
                        select t1;
            obj.Data = query.ToList().OrderByDescending(i=> i.Id).ToList();
            foreach (var item in obj.Data)
            {
                var EmployeeInfo = await _iUnitOfWork.Employees.GetById(long.Parse(item.UserName));
                var CompanyInfo = await _iUnitOfWork.Companies.GetById(EmployeeInfo.Company);
                item.UserName = EmployeeInfo.FirstName + " " + EmployeeInfo.LastName;
                item.Company = CompanyInfo.Name;
                item.Action = item?.Action?.GetDescriptionByEnum<SystemOperationCode>().ToString();

            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<AuditTrailEntity>>> GetAdminPageList(AuditTrailListParam param, Pagination pagination)
        {
            var context = new ApplicationDbContext();
            TData<List<AuditTrailEntity>> obj = new TData<List<AuditTrailEntity>>();
            var user = await Operator.Instance.Current();
            param.UserName = user.Employee.ToString();
            //param.Company = user.Company;
            obj.Data = await _iUnitOfWork.AuditTrails.GetPageList(param, pagination);
            var Admins = new UserListParam();
            //var AdminUsers = _iUnitOfWork.Users.GetList(Admins).Result.Select(i=> i.Company).ToList();
            //var query = from t1 in obj.Data
            //            join t2 in context.UserEntity on t1.UserName equals t2.Employee.ToString()
            //            where t2.IsSystem == 1 && t2.UserStatus == 1 
            //            select t1;
            //obj.Data = query.ToList().OrderByDescending(i=> i.Id).ToList();
            obj.Data = obj.Data.ToList().OrderByDescending(i=> i.Id).ToList();

            foreach (var item in obj.Data)
            {
                var EmployeeInfo = await _iUnitOfWork.Employees.GetById(long.Parse(item.UserName));
                var CompanyInfo = await _iUnitOfWork.Companies.GetById(EmployeeInfo.Company);
                item.UserName = EmployeeInfo.FirstName + " " + EmployeeInfo.LastName;
                item.Company = CompanyInfo.Name;
                item.Action = item?.Action?.GetDescriptionByEnum<SystemOperationCode>().ToString();

            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<ZtreeInfo>>> GetZtreeFinanceTransactionList(AuditTrailListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<AuditTrailEntity> auiditTrailList = await _iUnitOfWork.AuditTrails.GetList(param);
            foreach (AuditTrailEntity auiditTrail in auiditTrailList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = auiditTrail.Id,
                    name = auiditTrail.UserName
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(AuditTrailListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<AuditTrailEntity> auiditTrailList = await _iUnitOfWork.AuditTrails.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (AuditTrailEntity auiditTrail in auiditTrailList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = auiditTrail.Id,
                    name = auiditTrail.UserName
                });
                List<long> userIdList = userList.Where(t => t.Company == auiditTrail.Id).Select(t => t.Employee).ToList();
                foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
                {
                    obj.Data.Add(new ZtreeInfo
                    {
                        id = user.Id,
                        name = user.RealName
                    });
                }
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<AuditTrailEntity>> GetEntity(long id)
        {
            TData<AuditTrailEntity> obj = new TData<AuditTrailEntity>();
            obj.Data = await _iUnitOfWork.AuditTrails.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.AuditTrails.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

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

        #region Submit data
        public async Task<TData<string>> SaveForm(AuditTrailEntity entity)
        {
            string CurrentUser = string.Empty;
            var user = Operator.Instance.Current();
            if (string.IsNullOrEmpty(entity.UserName))
            {
                CurrentUser = user.Result.Employee.ToStr();

            } else 
            {
                CurrentUser = entity.UserName;  
            }
            var IpAddress = NetHelper.GetLocalIPAddress();
            entity.MacAddress = GetMAC();
            entity.IpAddress = IpAddress;
            entity.UserName = CurrentUser;
            entity.Browser = NetHelper.Browser;
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.AuditTrails.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.AuditTrails.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}