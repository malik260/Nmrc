using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Helpers;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly EmployeeService _iEmployeeService;
        private readonly HttpClient _client;

        public CompanyService(IUnitOfWork iUnitOfWork, EmployeeService iEmployeeService)
        {
            _iUnitOfWork = iUnitOfWork;
            _iEmployeeService = iEmployeeService;
            _client = new HttpClient
            {
                BaseAddress = new Uri(ApiResource.baseAddress)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiResource.ApplicationJson));

        }

        #region Retrieve data
        public async Task<TData<List<CompanyEntity>>> GetList(CompanyListParam param)
        {
            TData<List<CompanyEntity>> obj = new TData<List<CompanyEntity>>();
            obj.Data = await _iUnitOfWork.Companies.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CompanyEntity>>> GetPageList(CompanyListParam param, Pagination pagination)
        {
            TData<List<CompanyEntity>> obj = new TData<List<CompanyEntity>>();
            obj.Data = await _iUnitOfWork.Companies.GetPageList(param, pagination);
            if (obj.Data.Count > 0)
            {
                List<SectorEntity> sectorList = await _iUnitOfWork.Sectors.GetList(new SectorListParam { Ids = obj.Data.Select(p => p.Sector).ToList() });
                List<CompanyClassEntity> companyClassList = await _iUnitOfWork.CompanyClasses.GetList(new CompanyClassListParam { Ids = obj.Data.Select(p => p.CompanyClass).ToList() });
                List<CompanyTypeEntity> companyTypeList = await _iUnitOfWork.CompanyTypes.GetList(new CompanyTypeListParam { Ids = obj.Data.Select(p => p.CompanyType).ToList() });
                foreach (CompanyEntity company in obj.Data)
                {
                    company.SectorName = sectorList.Where(p => p.Id == company.Sector).Select(p => p.Name).FirstOrDefault();
                    company.CompanyClassName = companyClassList.Where(p => p.Id == company.CompanyClass).Select(p => p.Name).FirstOrDefault();
                    company.CompanyTypeName = companyTypeList.Where(p => p.Id == company.CompanyType).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeCompanyList(CompanyListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(param);
            foreach (CompanyEntity company in companyList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = company.Id,
                    name = company.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CompanyListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (CompanyEntity company in companyList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = company.Id,
                    name = company.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == company.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<CompanyEntity>> GetEntity(long id)
        {
            TData<CompanyEntity> obj = new TData<CompanyEntity>();
            obj.Data = await _iUnitOfWork.Companies.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(CompanyEntity entity)
        {
            TData<string> obj = new TData<string>();
            if (_iUnitOfWork.Companies.ExistCompany(entity))
            {
                obj.Message = "Company name already exists!";
                return obj;
            }

            await _iUnitOfWork.Companies.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(CompanyEntity entity)
        {
            TData<string> obj = new TData<string>();

            if (string.IsNullOrEmpty(entity.Name))
            {
                obj.Message = "Company name must be provided!";
                return obj;
            }

            if (_iUnitOfWork.Companies.ExistCompany(entity))
            {
                obj.Message = "Company name already exists!";
                return obj;
            }

            if (_iUnitOfWork.Users.CheckUserName(entity.EmailAddress))
            {
                obj.Message = "Username already exists!";
                return obj;
            }
            else
            {
                entity.Salt = new UserService(_iUnitOfWork).GetPasswordSalt();
                entity.DecryptedPassword = new UserService(_iUnitOfWork).GenerateDefaultPassword();
                entity.Password = new UserService(_iUnitOfWork).EncryptUserPassword(entity.DecryptedPassword, entity.Salt);
            }

            if (string.IsNullOrEmpty(entity.Address))
            {
                obj.Message = "Company address must be provided!";
                return obj;
            }

            if (int.Parse(entity.AgentType.ToStr()) > GlobalConstant.ZERO && int.Parse(entity.AgentType.ToStr()) == GlobalConstant.ONE)
            {
                if (entity.Sector < GlobalConstant.ONE)
                {
                    obj.Message = "Company sector must be selected!";
                    return obj;
                }
            }

            if (string.IsNullOrEmpty(entity.RCNumber))
            {
                obj.Message = "Company RC-Number must be provided!";
                return obj;
            }
            else if (_iUnitOfWork.Companies.ExistRCNumber(entity))
            {
                obj.Message = "Company RC-Number already exists!";
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

            if (string.IsNullOrEmpty(entity.IndLastName))
            {
                obj.Message = "Login : Last name must be provided!";
                return obj;
            }

            if (entity.Role.IsNullOrZero())
            {
                obj.Message = "Login : Role must be selected!";
                return obj;
            }

            entity.NHFNumber = _iEmployeeService.GenerateNHFNumber();

            await _iUnitOfWork.Companies.SaveForms(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Companies.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        [HttpGet]
        public async Task<bool> CustomerExist(string customerCode)
        {
            try
            {
                var response = await _client.GetAsync(ApiResource.customerExist + customerCode);
                string response1 = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomerExistResponse>(response1);

                if (result.Success == true)
                {
                    // Customer exists
                    return true;
                }

                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<bool> UpdateCustomer(CustomerUpdateRequestDTO customerUpdateRequestDTO)
        {
            try
            {
                var jsonParameters = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(customerUpdateRequestDTO), Encoding.UTF8, ApiResource.ApplicationJson);
                var response = await _client.PostAsync(ApiResource.updateCustomer, jsonParameters);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomerExistResponse>(responseContent);

                if (response.IsSuccessStatusCode && result.Success == true)
                {
                    // Customer update was successful
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        [HttpPost]
        public async Task<bool> IndividualExiting(CreateCustomerRequestDTO createCustomerRequestDTO)
        {
            try
            {


                var jsonParameters = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(createCustomerRequestDTO), Encoding.UTF8, ApiResource.ApplicationJson);

                var response = await _client.PostAsync(ApiResource.individualExisting, jsonParameters);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomerExistResponse>(responseContent);

                if (response.IsSuccessStatusCode && result.Success == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
