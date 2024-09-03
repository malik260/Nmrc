using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Helpers;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class DeveloperService: IDeveloperService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public DeveloperService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
            
        }

        #region Retrieve data


        public async Task<TData<List<DeveloperEntity>>> GetList(DeveloperListParam param)
        {
            TData<List<DeveloperEntity>> obj = new TData<List<DeveloperEntity>>();
            var allDevelopers = await _iUnitOfWork.Developers.GetList(param);
            // Filter the list to include only approved companies
            var approvedDevelopers = allDevelopers.Where(pmb => pmb.Status == (int)ApprovalEnum.Approved).ToList();
            obj.Data = approvedDevelopers;
            obj.Total = approvedDevelopers.Count;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<DeveloperEntity>>> GetPageList(DeveloperListParam param, Pagination pagination)
        {
            TData<List<DeveloperEntity>> obj = new TData<List<DeveloperEntity>>();
            obj.Data = await _iUnitOfWork.Developers.GetPageList(param, pagination);

            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<ZtreeInfo>>> GetZtreePmbList(DeveloperListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<DeveloperEntity> DeveloperList = await _iUnitOfWork.Developers.GetList(param);
            foreach (DeveloperEntity x in DeveloperList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = x.Id,
                    name = x.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<DeveloperEntity>>> GetApprovalPageList(DeveloperListParam param, Pagination pagination)
        {
            TData<List<DeveloperEntity>> obj = new TData<List<DeveloperEntity>>();
            obj.Data = await _iUnitOfWork.Developers.GetApprovalPageList(param, pagination);
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



        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(DeveloperListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<DeveloperEntity> pmbList = await _iUnitOfWork.Developers.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (DeveloperEntity pmb in pmbList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = pmb.Id,
                    name = pmb.Name
                });
                List<long> userIdList = userList.Where(t => t.Pmb == pmb.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<DeveloperEntity>> GetEntity(long id)
        {
            TData<DeveloperEntity> obj = new TData<DeveloperEntity>();
            obj.Data = await _iUnitOfWork.Developers.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region Submit data


        public async Task<TData<string>> SaveForms(DeveloperEntity entity)
        {
            TData<string> obj = new TData<string>();

            if (string.IsNullOrEmpty(entity.Name))
            {
                obj.Message = "Developer name must be provided!";
                return obj;
            }

            if (_iUnitOfWork.Developers.ExistDeveloper(entity))
            {
                obj.Message = "Developer name already exists!";
                return obj;
            }

            if (_iUnitOfWork.Users.CheckUserName(entity.EmailAddress))
            {
                obj.Message = "Email already exists!";
                return obj;
            }
            else
            {
                entity.Salt = new UserService(_iUnitOfWork).GetPasswordSalt();
                entity.DecryptedPassword = new UserService(_iUnitOfWork).GenerateDefaultPassword();
                //entity.Password = new UserService(_iUnitOfWork).EncryptUserPassword(entity.DecryptedPassword, entity.Salt);
                entity.Password = EncryptionHelper.Encrypt(entity.DecryptedPassword, entity.Salt);
            }

            if (string.IsNullOrEmpty(entity.Address))
            {
                obj.Message = "Company address must be provided!";
                return obj;
            }



            if (string.IsNullOrEmpty(entity.RCNumber))
            {
                obj.Message = "Company RC-Number must be provided!";
                return obj;
            }
            else if (_iUnitOfWork.Developers.ExistRCNumber(entity))
            {
                obj.Message = "Company RC-Number already exists!";
                return obj;
            }

            if (!string.IsNullOrEmpty(entity.IndBVN) && entity.IndBVN.Length != 11)
            {
                obj.Message = "BVN must be 11 digits if provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.UserName))
            {
                obj.Message = "Login : User name must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.IndFirstName))
            {
                obj.Message = "Login : First name must be provided!";
                return obj;
            }

            //if (string.IsNullOrEmpty(entity.IndLastName))
            //{
            //    obj.Message = "Login : Last name must be provided!";
            //    return obj;
            //}

            if (entity.Role.IsNullOrZero())
            {
                obj.Message = "Login : Role must be selected!";
                return obj;
            }

            if (!string.IsNullOrEmpty(entity.IndFirstName) && !string.IsNullOrEmpty(entity.IndLastName))
            {
                entity.ContactPersonFirstName = entity.IndFirstName;
                entity.ContactPersonLastName= entity.IndLastName;
            }

            var currentMenu = await new DataRepository().GetMenuId(GlobalConstant.DEVELOPER_MENU_URL);

            entity.NHFNumber = _iUnitOfWork.Employees.GenerateNHFNumber();
            entity.BaseProcessMenu = currentMenu;
            await _iUnitOfWork.Developers.SaveForms(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Pmbs.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> ApproveForm(DeveloperEntity entity)
        {
            TData<long> obj = new TData<long>();
            var user = await Operator.Instance.Current();
            var entityRecord = await _iUnitOfWork.Developers.GetEntity(entity.Id);
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
            await _iUnitOfWork.Developers.ApproveForm(entityRecord, menuRecord, user);
            //obj.Message = string.Empty;
            obj.Data = entity.Id;
            obj.Tag = 1;
            return obj;
        }



      

       
        #endregion


    }
}
