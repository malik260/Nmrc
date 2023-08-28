using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Web;
using static Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos.LoanApplicationDTO;
//using static Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos.LoanProductDTO;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class LoanInitiationService : ILoanInitiationService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly HttpClient _client;
        private readonly ICompanyService _companyService;

        public LoanInitiationService(IUnitOfWork iUnitOfWork, ICompanyService companyService)
        {
            _iUnitOfWork = iUnitOfWork;
            _companyService = companyService;
            _client = new HttpClient
            {
                BaseAddress = new Uri(ApiResource.baseAddress)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiResource.ApplicationJson));



        }

        #region Retrieve data
        public async Task<TData<List<LoanInitiationEntity>>> GetList(LoanInitiationListParam param)
        {
            TData<List<LoanInitiationEntity>> obj = new TData<List<LoanInitiationEntity>>();
            obj.Data = await _iUnitOfWork.LoanInitiations.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LoanInitiationEntity>>> GetPageList(LoanInitiationListParam param, Pagination pagination)
        {
            TData<List<LoanInitiationEntity>> obj = new TData<List<LoanInitiationEntity>>();
            obj.Data = await _iUnitOfWork.LoanInitiations.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeLoanInitiationList(LoanInitiationListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<LoanInitiationEntity> loanInitiationList = await _iUnitOfWork.LoanInitiations.GetList(param);
            foreach (LoanInitiationEntity loanInitiation in loanInitiationList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = loanInitiation.Id,
                    name = loanInitiation.LoanProduct
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(LoanInitiationListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<LoanInitiationEntity> loanInitiationList = await _iUnitOfWork.LoanInitiations.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (LoanInitiationEntity loanInitiation in loanInitiationList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = loanInitiation.Id,
                    name = loanInitiation.LoanProduct
                });
                List<long> userIdList = userList.Where(t => t.Company == loanInitiation.Id).Select(t => t.Employee).ToList();
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

        public string GetLoggedUser()
        {
            string userName = Environment.UserName;
            return userName;
        }
        public async Task<TData<LoanInitiationEntity>> GetEntity(long id)
        {
            TData<LoanInitiationEntity> obj = new TData<LoanInitiationEntity>();
            obj.Data = await _iUnitOfWork.LoanInitiations.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.LoanInitiations.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }



        [HttpGet]

        public async Task<TData<LoanProductResponse>> LoanProduct()
        {

            // using (var httpClient = new HttpClient())




            var response = await _client.GetAsync(ApiResource.loanProduct);
            string response1 = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoanProductResponse>(response1);

            if (result.success == true)
            {
                TData<LoanProductResponse> obj = new TData<LoanProductResponse>();
                obj.Data = result;

                obj.Tag = 1;
                return obj;

            }
            else
            {
                TData<LoanProductResponse> obj = new TData<LoanProductResponse>();
                obj.Data = result;

                obj.Tag = 0;
                return obj;
            }

        }


        [HttpGet]
        public async Task<TData<List<LoanApplications>>> GetLoans(string nhfNo)
        {
            try
            {
                //LoanApplications CustomerLoan = new LoanApplications();
                var response = await _client.GetAsync(ApiResource.getLoans + nhfNo);
                string response1 = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetLoansResponse>(response1);

                if (result.success == true && result.count > 0)

                {
                    TData<List<LoanApplications>> obj = new TData<List<LoanApplications>>();
                    obj.Data = result.result;
                    obj.Total = result.count;
                    obj.Tag = 1;
                    return obj;
                }
                else
                {
                    TData<List<LoanApplications>> obj = new TData<List<LoanApplications>>();
                    obj.Data = result.result;
                    obj.Total = result.count;
                    obj.Tag = 0;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            };

        }



        [HttpPost]

        public async Task<TData<string>> UpdateLoanApplication(LoanApplicationRequestDTO loanApplicationRequest)
        {

            try
            {
                var jsonPayload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(loanApplicationRequest), Encoding.UTF8, ApiResource.ApplicationJson);

                var response = await _client.PostAsync(ApiResource.loanApplicationUpdate, jsonPayload);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoanApplicationUpdateDTO>(responseContent);
                if (response.IsSuccessStatusCode && result.success == true)
                {
                    // loan application was successful
                    TData<string> obj = new TData<string>();

                    obj.Data = result.result.applicationReferenceNumber;
                    obj.Tag = 1;
                    return obj;
                }
                else
                {
                    TData<string> obj = new TData<string>();

                    obj.Data = string.Empty;
                    obj.Tag = 1;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]

        public async Task<List<messages>> UpdateLoanAffordability(checkafford affordabilityVM)
        {

            try
            {
                var jsonPayload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(affordabilityVM), Encoding.UTF8, ApiResource.ApplicationJson);

                var response = await _client.PostAsync(ApiResource.loanAffordability, jsonPayload);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AffordabilityVM>(responseContent);
                if (response.IsSuccessStatusCode && result.success == true)
                {
                    List<messages> obj = new List<messages>();

                    obj = result.result;
                    return obj;
                }
                else
                {
                    List<messages> obj = new List<messages>();

                    obj = result.result;

                    return obj;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(LoanInitiationEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.LoanInitiations.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.LoanInitiations.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Perform Afforddability 
        public async Task<TData<AffordabilityResponseDto>> Performaffordability(InitiateLoanDto initiateLoanDto)
        {
            OperatorInfo loggedUserinfo = new OperatorInfo();
            var EmployeeInformation = await _iUnitOfWork.Employees.GetEntity(loggedUserinfo.Employee);
            bool employeeExist = await _companyService.CustomerExist(loggedUserinfo.Employee.ToString());
            string Message = string.Empty;
            AffordabilityResponseDto result = new AffordabilityResponseDto();
            if (!employeeExist)
            {
                CreateCustomerRequestDTO customercreateRequest = new CreateCustomerRequestDTO();
                ContactAddress address = new ContactAddress()
                {
                    contactAddress = EmployeeInformation.PostalAddress,
                    NearestLandmark = EmployeeInformation.PostalAddress,
                    MailingAddress = EmployeeInformation.EmailAddress,
                    CityId = 3017,
                    StateId = 1,
                    AddressTypeId = 3,
                    UtilityBillNo = "3456789"


                };
                NextofKin NOK = new NextofKin()
                {
                    firstName = EmployeeInformation.NOKName,
                    lastName = EmployeeInformation.NOKName,
                    contactAddress = EmployeeInformation.NOKAddress,
                    emailAddress = EmployeeInformation.NOKEmailAddress,
                    mobilePhoneNo = EmployeeInformation.NOKNumber,
                    nearestLandmark = "",
                    cityId = 3017,
                    relationship = EmployeeInformation.KinRelationship.ToString(),
                    dateOfBirth = DateTime.MinValue,
                    gender = EmployeeInformation.Gender.ToString(),

                };
                ContactPhone phone = new ContactPhone()
                {
                    OfficeLandNo = EmployeeInformation.MobileNumber,
                    MobilePhoneNo = EmployeeInformation.MobileNumber
                };
                AccountDetails acctdetails = new AccountDetails()
                {
                    AccountStatusName = "Active",
                    ProductAccountName = "NHF",
                    AccountNumber = EmployeeInformation.Id.ToString(),
                    DateOfEmployment = EmployeeInformation.DateOfEmployment,
                    MonthlyIncome = EmployeeInformation.MonthlySalary.ToString(),
                    OtherBankAccountNumber = EmployeeInformation.BankAccountNumber,
                    OtherBankSortCode = "214"
                };

                customercreateRequest.FirstName = EmployeeInformation.FirstName;
                customercreateRequest.Gender = EmployeeInformation.FirstName;
                customercreateRequest.LastName = EmployeeInformation.LastName;
                customercreateRequest.MiddleName = EmployeeInformation.OtherName;
                customercreateRequest.Nationality = string.Empty;
                customercreateRequest.DateOfBirth = Convert.ToDateTime(EmployeeInformation.DateOfBirth);
                customercreateRequest.EmailAddress = EmployeeInformation.EmailAddress;
                customercreateRequest.CustomerBVN = EmployeeInformation.BVN;
                customercreateRequest.IsBvnValidated = true;
                customercreateRequest.IsEmailValidated = true;
                customercreateRequest.IsPhoneValidated = true;
                customercreateRequest.IsPoliticallyExposed = true;
                customercreateRequest.CrmsLegalStatusId = 104;
                customercreateRequest.CrmsRelationshipTypeId = 152;
                customercreateRequest.TaxNumber = EmployeeInformation.NIN;
                customercreateRequest.Title = EmployeeInformation.Title.ToString();
                customercreateRequest.SubSectorId = int.Parse(EmployeeInformation.MobileNumber);
                customercreateRequest.BranchCode = EmployeeInformation.Branch.ToString();
                customercreateRequest.AccountDetails = acctdetails;
                customercreateRequest.contactPhone = phone;
                customercreateRequest.contactAddress = address;
                bool CreateCustomer = await _companyService.IndividualExiting(customercreateRequest);
                if (!CreateCustomer)
                {
                    Message = "Error Creating Customer";
                }


            }
            else
            {
                checkafford CustomersAffordabilityvm = new checkafford();
                CustomersAffordabilityvm.amountRequested = Convert.ToDecimal(initiateLoanDto.PrincipalAmount);
                CustomersAffordabilityvm.productId = Convert.ToInt32(initiateLoanDto.Product);
                CustomersAffordabilityvm.nhfAccount = loggedUserinfo.Employee.ToString();
                CustomersAffordabilityvm.proposedTenor = Convert.ToInt32(initiateLoanDto.Tenor);
                var AffordabilityCheck = await UpdateLoanAffordability(CustomersAffordabilityvm);
                result.affordableAmount = AffordabilityCheck.FirstOrDefault().affordableAmount.ToString();
                result.amountRequested = AffordabilityCheck.FirstOrDefault().amountRequested.ToString();
                result.monthlyRepayment = AffordabilityCheck.FirstOrDefault().amountRequested.ToString();
                result.rate = AffordabilityCheck.FirstOrDefault().rate;
                result.proposedTenor = AffordabilityCheck.FirstOrDefault().proposedTenor;


            }
            TData<AffordabilityResponseDto> obj = new TData<AffordabilityResponseDto>();
            obj.Data = result;
            obj.Tag = 1;
            return obj;


        }
        #endregion


        #region LoanApplicaiton
        public async Task<TData<LoanInitiationEntity>> LoanApplication(InitiateLoanDto initiateLoanDto)
        {
            var user = await Operator.Instance.Current();
            var employeedetails = await _iUnitOfWork.Employees.GetEntityByNhfNumber(user.EmployeeInfo.NHFNumber);
            LoanInitiationEntity entity = new LoanInitiationEntity();
            entity.Amount = initiateLoanDto.PrincipalAmount;
            entity.Rate = initiateLoanDto.InterestRate;
            entity.Tenor = initiateLoanDto.Tenor;
            entity.LoanProduct = initiateLoanDto.Product;
            entity.LoanPurpose = initiateLoanDto.Purpose;
            await _iUnitOfWork.LoanInitiations.SaveForm(entity);
            TData<LoanInitiationEntity> obj = new TData<LoanInitiationEntity>();
            obj.Data = entity;
            obj.Tag = 1;
            obj.Message = "Loan Initiated Successfully, Passed to Underwritting";
            return obj;
            //bool employeeExist = await _companyService.CustomerExist(loggedUserinfo.Employee.ToString());
            //string Message = string.Empty;
            //AffordabilityResponseDto result = new AffordabilityResponseDto();
            //if (!employeeExist)
            //{
            //    CreateCustomerRequestDTO customercreateRequest = new CreateCustomerRequestDTO();
            //    ContactAddress address = new ContactAddress()
            //    {
            //        contactAddress = EmployeeInformation.PostalAddress,
            //        NearestLandmark = EmployeeInformation.PostalAddress,
            //        MailingAddress = EmployeeInformation.EmailAddress,
            //        CityId = 3017,
            //        StateId = 1,
            //        AddressTypeId = 3,
            //        UtilityBillNo = "3456789"


            //    };
            //    NextofKin NOK = new NextofKin()
            //    {
            //        firstName = EmployeeInformation.NOKName,
            //        lastName = EmployeeInformation.NOKName,
            //        contactAddress = EmployeeInformation.NOKAddress,
            //        emailAddress = EmployeeInformation.NOKEmailAddress,
            //        mobilePhoneNo = EmployeeInformation.NOKNumber,
            //        nearestLandmark = "",
            //        cityId = 3017,
            //        relationship = EmployeeInformation.KinRelationship.ToString(),
            //        dateOfBirth = DateTime.MinValue,
            //        gender = EmployeeInformation.Gender.ToString(),

            //    };
            //    ContactPhone phone = new ContactPhone()
            //    {
            //        OfficeLandNo = EmployeeInformation.MobileNumber,
            //        MobilePhoneNo = EmployeeInformation.MobileNumber
            //    };
            //    AccountDetails acctdetails = new AccountDetails()
            //    {
            //        AccountStatusName = "Active",
            //        ProductAccountName = "NHF",
            //        AccountNumber = EmployeeInformation.Id.ToString(),
            //        DateOfEmployment = EmployeeInformation.DateOfEmployment,
            //        MonthlyIncome = EmployeeInformation.MonthlySalary.ToString(),
            //        OtherBankAccountNumber = EmployeeInformation.BankAccountNumber,
            //        OtherBankSortCode = "214"
            //    };

            //    customercreateRequest.FirstName = EmployeeInformation.FirstName;
            //    customercreateRequest.Gender = EmployeeInformation.FirstName;
            //    customercreateRequest.LastName = EmployeeInformation.LastName;
            //    customercreateRequest.MiddleName = EmployeeInformation.OtherName;
            //    customercreateRequest.Nationality = string.Empty;
            //    customercreateRequest.DateOfBirth = Convert.ToDateTime(EmployeeInformation.DateOfBirth);
            //    customercreateRequest.EmailAddress = EmployeeInformation.EmailAddress;
            //    customercreateRequest.CustomerBVN = EmployeeInformation.BVN;
            //    customercreateRequest.IsBvnValidated = true;
            //    customercreateRequest.IsEmailValidated = true;
            //    customercreateRequest.IsPhoneValidated = true;
            //    customercreateRequest.IsPoliticallyExposed = true;
            //    customercreateRequest.CrmsLegalStatusId = 104;
            //    customercreateRequest.CrmsRelationshipTypeId = 152;
            //    customercreateRequest.TaxNumber = EmployeeInformation.NIN;
            //    customercreateRequest.Title = EmployeeInformation.Title.ToString();
            //    customercreateRequest.SubSectorId = int.Parse(EmployeeInformation.MobileNumber);
            //    customercreateRequest.BranchCode = EmployeeInformation.Branch.ToString();
            //    customercreateRequest.AccountDetails = acctdetails;
            //    customercreateRequest.contactPhone = phone;
            //    customercreateRequest.contactAddress = address;
            //    bool CreateCustomer = await _companyService.IndividualExiting(customercreateRequest);
            //    if (!CreateCustomer)
            //    {
            //        Message = "Error Creating Customer";
            //    }


            //}
            //checkafford affordPayload = new checkafford();
            //affordPayload.amountRequested = Convert.ToDecimal(initiateLoanDto.PrincipalAmount);
            //affordPayload.proposedTenor = Convert.ToInt32(initiateLoanDto.Tenor);
            //affordPayload.productId = Convert.ToInt32(initiateLoanDto.Product);
            //affordPayload.nhfAccount = loggedUserinfo.Employee.ToString();
            //var affordabilitydetails = UpdateLoanAffordability(affordPayload);
            //LoanApplicationViewModel2 lad = new LoanApplicationViewModel2()
            //{

            //    proposedAmount = Convert.ToDecimal(initiateLoanDto.PrincipalAmount),
            //    proposedProductId = Convert.ToInt32(initiateLoanDto.Product),
            //    proposedTenor = Convert.ToInt32(initiateLoanDto.Tenor),
            //    loanPurpose = initiateLoanDto.Purpose,
            //    operatingAccountNo = Convert.ToString(loggedUserinfo.Employee),
            //    subSectorId = Convert.ToInt32(EmployeeInformation.StaffNumber),
            //    requestedAmount = initiateLoanDto.PrincipalAmount,
            //    repaymentDate = initiateLoanDto.repaymentDate,
            //};
            //AffordabilityDetails loanaff = new AffordabilityDetails()
            //{
            //    productId = affordabilitydetails.Result.FirstOrDefault().productId,
            //    yearsInService = affordabilitydetails.Result.FirstOrDefault().yearsInService,
            //    affordableAmount = affordabilitydetails.Result.FirstOrDefault().affordableAmount,
            //    age = affordabilitydetails.Result.FirstOrDefault().age,
            //    casaAccountId = affordabilitydetails.Result.FirstOrDefault().casaAccountId,
            //    amountRequested = affordabilitydetails.Result.FirstOrDefault().amountRequested,
            //    monthlyRepayment = affordabilitydetails.Result.FirstOrDefault().monthlyRepayment,
            //    presentValue = affordabilitydetails.Result.FirstOrDefault().presentValue,
            //    profitability = affordabilitydetails.Result.FirstOrDefault().profitability,
            //    repaymentPeriod = affordabilitydetails.Result.FirstOrDefault().repaymentPeriod,
            //    rate = affordabilitydetails.Result.FirstOrDefault().rate,
            //    tenorOverride = affordabilitydetails.Result.FirstOrDefault().tenorOverride

            //};
            //LoanApplicationRequestDTO loanDetails = new LoanApplicationRequestDTO();
            //loanDetails.customerCode = Convert.ToString(EmployeeInformation.Id);
            //loanDetails.loanApplicationDetail = lad;
            //loanDetails.affordabilityDetails = loanaff;
            //var message = UpdateLoanApplication(loanDetails);

        }

        #endregion
    }
}

