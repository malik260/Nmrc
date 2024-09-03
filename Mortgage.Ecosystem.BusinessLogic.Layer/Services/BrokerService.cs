using Mortgage.Ecosystem.BusinessLogic.Layer.Helpers;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class BrokerService : IBrokerService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public BrokerService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }
        public async Task<TData<List<BrokerEntity>>> GetApprovalPageList(BrokerListParam param, Pagination pagination)
        {
            TData<List<BrokerEntity>> obj = new TData<List<BrokerEntity>>();
            obj.Data = await _iUnitOfWork.Brokers.GetApprovalPageList(param, pagination);
            //if (obj.Data.Count > 0)
            //{
            //    List<SectorEntity> sectorList = await _iUnitOfWork.Sectors.GetList(new SectorListParam { Ids = obj.Data.Select(p => p.Sector).ToList() });
            //    List<CompanyClassEntity> companyClassList = await _iUnitOfWork.CompanyClasses.GetList(new CompanyClassListParam { Ids = obj.Data.Select(p => p.CompanyClass).ToList() });
            //    List<CompanyTypeEntity> companyTypeList = await _iUnitOfWork.CompanyTypes.GetList(new CompanyTypeListParam { Ids = obj.Data.Select(p => p.CompanyType).ToList() });
            //    foreach (CompanyEntity company in obj.Data)
            //    {
            //        company.SectorName = sectorList.Where(p => p.Id == company.Sector).Select(p => p.Name).FirstOrDefault();
            //        company.CompanyClassName = companyClassList.Where(p => p.Id == company.CompanyClass).Select(p => p.Name).FirstOrDefault();
            //        company.CompanyTypeName = companyTypeList.Where(p => p.Id == company.CompanyType).Select(p => p.Name).FirstOrDefault();
            //    }
            //}
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData> ApproveForm(BrokerEntity entity)
        {
            TData<long> obj = new TData<long>();
            var user = await Operator.Instance.Current();
            var entityRecord = await _iUnitOfWork.Brokers.GetEntity(entity.Id);
            var menuRecord = await _iUnitOfWork.Menus.GetEntity(entityRecord.BaseProcessMenu);
            var loginProfile = await _iUnitOfWork.Users.GetEntityByCompany(entity.Id);
            loginProfile.DecryptedPassword = EncryptionHelper.Decrypt(loginProfile.Password, loginProfile.Salt);
            user.DecryptedPassword = loginProfile.DecryptedPassword;
            user.UserName = loginProfile.UserName;

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = menuRecord.Id,
                //Authority = user.Employee,
                Record = entity.Id
            };
            var approvalLogRecords = await _iUnitOfWork.ApprovalLogs.GetList(approvalLogListParam);
            menuRecord.ApprovalLogList = approvalLogRecords;
            await _iUnitOfWork.Brokers.ApproveForm(entityRecord, menuRecord, user);
            obj.Message = "Broker Approved Successfully";
            obj.Data = entity.Id;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<BrokerEntity>>> GetPageList(BrokerListParam param, Pagination pagination)
        {
            TData<List<BrokerEntity>> obj = new TData<List<BrokerEntity>>();
            obj.Data = await _iUnitOfWork.Brokers.GetPageList(param, pagination);
            //if (obj.Data.Count > 0)
            //{
            //    List<SectorEntity> sectorList = await _iUnitOfWork.Sectors.GetList(new SectorListParam { Ids = obj.Data.Select(p => p.Sector).ToList() });
                
            //    List<AgentTypeEntity> companyTypeList = await _iUnitOfWork.AgentTypes.GetList(new AgentTypeListParam { Ids = obj.Data.Select(p => p.CompanyType).ToList() });
            //    foreach (BrokerEntity company in obj.Data)
            //    {
            //        company.SectorName = sectorList.Where(p => p.Id == company.Sector).Select(p => p.Name).FirstOrDefault();
            //        company.CompanyClassName = companyClassList.Where(p => p.Id == company.CompanyClass).Select(p => p.Name).FirstOrDefault();
            //        company.CompanyTypeName = companyTypeList.Where(p => p.Id == company.CompanyType).Select(p => p.Name).FirstOrDefault();
            //    }
            //}
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }


    }
}
