using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
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



        public async Task<TData<CustomerDetailsViewModel>> GetCustomerDetails2()
        {
            var _db = new ApplicationDbContext();
            TData<CustomerDetailsViewModel> obj = new TData<CustomerDetailsViewModel>();
            var cust = new CustomerDetailsViewModel();
            decimal monthlysal = 0;
            var user = await Operator.Instance.Current();
            var employeeInfo = await _iUnitOfWork.Employees.GetEntity(user.Employee);
            monthlysal = employeeInfo.MonthlySalary;
            cust.MonthlyIncome = monthlysal;            
            cust.LoanRepayment = "Monthly";
            obj.Data = cust;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<CustomerDetailsViewModel>> GetCustomerDetails()
        {
            var _db = new ApplicationDbContext();
            TData<CustomerDetailsViewModel> obj = new TData<CustomerDetailsViewModel>();
            var cust = new CustomerDetailsViewModel();
            decimal monthlysal = 0;
            var user = await Operator.Instance.Current();
            var employeeInfo = await _iUnitOfWork.Employees.GetById(user.Employee);
            var customerDetails = await _iUnitOfWork.Employees.GetEntityByNhfNumber(employeeInfo.NHFNumber);
            var selectedPropertyInfo = await _iUnitOfWork.PropertySubscriptions.GetSubcribedProperties(Convert.ToString(customerDetails.NHFNumber));
            if (selectedPropertyInfo == null)
            {
                obj.Message = "Please subscribe to a property before applying for a loan";
                obj.Tag = 0;
                return obj;
            }
            var result = from a in _db.PmbEntity
                         join b in _db.PropertySubscriptionEntity
                         on a.NHFNumber equals b.Developer
                         where b.Subscriber == Convert.ToString(employeeInfo.NHFNumber) && a.Status == 1
                         select a.Name;
            var ecosytem = result.FirstOrDefault();
            if (ecosytem == null)
            {
                obj.Message = "Selected Pmb has been deactivated";
                obj.Tag = 0;
                return obj;
            }
            var pmbName = await _iUnitOfWork.Pmbs?.GetEntitybyNhf(selectedPropertyInfo?.Developer);
           
            monthlysal = customerDetails.MonthlySalary;
            cust.MonthlyIncome = monthlysal;
            cust.PmbNo = selectedPropertyInfo?.Developer;
            cust.PmbName = ecosytem;
            cust.LoanRepayment = "Monthly";
            obj.Data = cust;
            obj.Tag = 1;
            return obj;
        }


        #region Retrieve data
        public async Task<TData<List<LoanInitiationEntity>>> GetList(LoanInitiationListParam param)
        {
            var user = await Operator.Instance.Current();
            var employeeInfo = await _iUnitOfWork.Employees.GetById(user.Employee);
            param.NHFNumber = Convert.ToString(employeeInfo.NHFNumber);
            TData<List<LoanInitiationEntity>> obj = new TData<List<LoanInitiationEntity>>();
            obj.Data = await _iUnitOfWork.LoanInitiations.GetList(param);
            foreach (var item in obj.Data)
            {
                item.Scheme =  _iUnitOfWork.Schemes.GetEntitybiId(Convert.ToInt32(item.LoanScheme)).Result.SchemeName;
                item.LoanProduct = _iUnitOfWork.CreditTypes.GetEntityByProductCode(item.LoanProduct).Result.Name;

            }
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LoanInitiationEntity>>> GetPageList(LoanInitiationListParam param, Pagination pagination)
        {
            TData<List<LoanInitiationEntity>> obj = new TData<List<LoanInitiationEntity>>();
            var user = await Operator.Instance.Current();
            param.NHFNumber = user.EmployeeInfo.NHFNumber.ToString();
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
        public async Task<TData<LoanInitiationEntity>> GetEntity()
        {
            var user = await Operator.Instance.Current();
            TData<LoanInitiationEntity> obj = new TData<LoanInitiationEntity>();
            obj.Data = await _iUnitOfWork.LoanInitiations.GetEntity(Convert.ToString(user.EmployeeInfo.NHFNumber));
            obj.Data.LoanProduct = _iUnitOfWork.CreditTypes.GetEntitybiId(Convert.ToInt32(obj.Data.LoanProduct)).Result.Name;
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

        public class res
        {
            public bool success { get; set; }
            public string message { get; set; }
        }

        [HttpPost]
        public async Task<List<messages>> UpdateLoanAffordability(checkafford affordabilityVM)
        {

            try
            {
                var jsonPayload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(affordabilityVM), Encoding.UTF8, ApiResource.ApplicationJson);
                var load = JsonConvert.SerializeObject(affordabilityVM);
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
                    var results = JsonConvert.DeserializeObject<res>(responseContent);

                    List<messages> obj = new List<messages>();
                    messages mm = new messages();
                    string mess = results.message;
                    mm.applicationUrl = mess;
                    obj.Add(mm);

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
        public async Task<AffordabilityResponseDto> Performaffordability(InitiateLoanDto initiateLoanDto)
        {
            var loggedUserinfo = await Operator.Instance.Current();
            var employeeInfo = await _iUnitOfWork.Employees.GetById(loggedUserinfo.Employee);
            var EmployeeInformation = await _iUnitOfWork.Employees.GetEntityByNhfNumber(employeeInfo.NHFNumber);
            var employerinfo = await _iUnitOfWork.Companies.GetEntity(EmployeeInformation.Company);
            var KinInfo = await _iUnitOfWork.NextOfKins.GetEntity(EmployeeInformation.Id);
            bool employeeExist = await _companyService.CustomerExist(EmployeeInformation.EmployeeCode);
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
                    firstName = KinInfo.FirstName ?? "Operator",
                    lastName = KinInfo.LastName ?? "Developer",
                    contactAddress = KinInfo.Address ?? "Lagos",
                    emailAddress = KinInfo.EmailAddress ?? "KinInfo@gmail.com",
                    mobilePhoneNo = KinInfo.MobileNumber ?? "08054112111",
                    nearestLandmark = "",
                    cityId = 3017,
                    relationship = "Family",
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
                    AccountNumber = EmployeeInformation.NHFNumber.ToString(),
                    DateOfEmployment = EmployeeInformation.DateOfEmployment,
                    MonthlyIncome = EmployeeInformation.MonthlySalary.ToString(),
                    OtherBankAccountNumber = EmployeeInformation.BankAccountNumber,
                    OtherBankSortCode = "214"
                };
                customercreateRequest.CustomerCode = EmployeeInformation.EmployeeCode;
                customercreateRequest.customerTypeId = 1;
                customercreateRequest.FirstName = EmployeeInformation.FirstName;
                customercreateRequest.Gender = EmployeeInformation.Gender.ToString();
                customercreateRequest.LastName = EmployeeInformation.LastName;
                customercreateRequest.MiddleName = EmployeeInformation.OtherName;
                customercreateRequest.PlaceOfBirth = "Nigeria";
                customercreateRequest.Nationality = "Nigerian";
                customercreateRequest.DateOfBirth = Convert.ToDateTime(EmployeeInformation.DateOfBirth);
                customercreateRequest.EmailAddress = EmployeeInformation.EmailAddress;
                //customercreateRequest.CustomerBVN = EmployeeInformation.BVN;
                customercreateRequest.IsBvnValidated = true;
                customercreateRequest.IsEmailValidated = true;
                customercreateRequest.IsPhoneValidated = true;
                customercreateRequest.IsPoliticallyExposed = true;
                customercreateRequest.CrmsLegalStatusId = 104;
                customercreateRequest.CrmsRelationshipTypeId = 152;
                customercreateRequest.TaxNumber = EmployeeInformation.NIN;
                customercreateRequest.Title = EmployeeInformation.Title.ToString();
                customercreateRequest.SubSectorId = 1;
                customercreateRequest.CustomerBVN = EmployeeInformation.BVN;
                customercreateRequest.BranchCode = "101";
                customercreateRequest.employerNumber = EmployeeInformation.EmployerNhfNumber;
                customercreateRequest.AccountDetails = acctdetails;
                customercreateRequest.contactPhone = phone;
                customercreateRequest.contactAddress = address;
                customercreateRequest.nextOfKin = NOK;
                bool CreateCustomer = await _companyService.IndividualExiting(customercreateRequest);
                if (!CreateCustomer)
                {
                    Message = "Error Creating Customer";
                }


            }

            var product = await _iUnitOfWork.CreditTypes.GetEntitybyName(initiateLoanDto.LoanProduct);
            checkafford CustomersAffordabilityvm = new checkafford();
            CustomersAffordabilityvm.amountRequested = Convert.ToDecimal(initiateLoanDto.PrincipalAmount);
            CustomersAffordabilityvm.productId = product.ProductId;
            CustomersAffordabilityvm.nhfAccount = EmployeeInformation.NHFNumber.ToString();
            CustomersAffordabilityvm.proposedTenor = 0;
            var AffordabilityCheck = await UpdateLoanAffordability(CustomersAffordabilityvm);
            if (AffordabilityCheck.FirstOrDefault().applicationUrl != null)
            {
                var context = new ApplicationDbContext();
                result.message = AffordabilityCheck.FirstOrDefault().applicationUrl;
                var errorlog = new ErrorLogEntity();
                errorlog.ErrorCode = "500";
                errorlog.Device = Environment.MachineName;
                errorlog.InnerException = result.message;
                errorlog.AdditionalInfo = result.message;
                errorlog.Type = "API Exception";
                errorlog.IpAddress = NetHelper.Ip;
                errorlog.Level = "Error";
                errorlog.Message = result.message;
                errorlog.LoggedOnDate = DateTime.Now;
                errorlog.OriginatingProcess = "https://testcorebanking.fmbn.gov.ng:4448/api/v1/loan/loan-affordability-check";
                errorlog.StackTrace = "Update Loan Application, Underwriting Controller";
                errorlog.Username = EmployeeInformation.EmailAddress;
                errorlog.Callsite = "https://testcorebanking.fmbn.gov.ng:4448/api/v1/loan/loan-affordability-check";
                context.ErrorLogEntity.Add(errorlog);
                context.SaveChanges();
            }
            result.affordableAmount = AffordabilityCheck.FirstOrDefault().affordableAmount.ToString();
            result.amountRequested = AffordabilityCheck.FirstOrDefault().amountRequested.ToString();
            result.monthlyRepayment = AffordabilityCheck.FirstOrDefault().monthlyRepayment.ToString();
            result.rate = AffordabilityCheck.FirstOrDefault().rate;
            result.proposedTenor = AffordabilityCheck.FirstOrDefault().repaymentPeriod;



            AffordabilityResponseDto obj = new AffordabilityResponseDto();
            obj = result;
            return obj;


        }
        #endregion


        #region LoanApplicaiton
        public async Task<TData<LoanInitiationEntity>> LoanApplication(InitiateLoanDto initiateLoanDto)
        {
            var context = new ApplicationDbContext();
            TData<LoanInitiationEntity> obj = new TData<LoanInitiationEntity>();
            EmployeeListParam employeeListParam = new EmployeeListParam();
            var user = await Operator.Instance.Current();
            var employeedetails = await _iUnitOfWork.Employees.GetById(user.Employee);
            var loanss = context.UnderwritingEntity.Where(x=> x.NHFNumber == employeedetails.NHFNumber.ToString()).ToList();
            
            if (loanss.Count >= 1)
            {
                var UnderApproved = loanss.Where(i => i.Approved == 0).ToList();
                if (UnderApproved.Count != 0)
                {
                    obj.Message = "You currently have an ongoing loans process in progress.";
                    obj.Tag = 0;
                    return obj;
                }
            }

            
            var noOfLoans = context.LoanInitiationEntity.Where(x=> x.NHFNumber == employeedetails.NHFNumber.ToString()).ToList();
            
            if (noOfLoans.Count >= 1)
            {
                var UnderApproved = noOfLoans.Where(i => i.Status == "0" || i.Status == null || i.Status == "").ToList();

                if (UnderApproved.Count >=1)
                {
                    obj.Message = "You currently have an ongoing loans process in progress.";
                    obj.Tag = 0;
                    return obj;
                }
               
            }

            var affordablityDetails = await Performaffordability(initiateLoanDto);
            if (affordablityDetails.proposedTenor == 0 && affordablityDetails.affordableAmount == "0")
            {
                obj.Tag = 0;
                obj.Message = affordablityDetails.message;
                return obj;
            }
            if (initiateLoanDto.PrincipalAmount > Convert.ToDecimal(affordablityDetails.affordableAmount))
            {
                obj.Tag = 0;
                obj.Message = "Principal Amount cannot be greater than Affordable Amount";
                return obj;

            }
            //if (initiateLoanDto.Tenor > Convert.ToDecimal(affordablityDetails.proposedTenor))
            //{
            //    obj.Tag = 0;
            //    obj.Message = "Tenor cannot be greater than Proposed Tenor " + affordablityDetails.proposedTenor;
            //    return obj;

            //}

            FinanceCounterpartyTransactionListParam param = new FinanceCounterpartyTransactionListParam();
            param.Ref = employeedetails.NHFNumber.ToString();
            var contribution = await _iUnitOfWork.FinanceCounterpartyTransactions.GetList(param);
            var startDate = DateTime.Now.AddMonths(-6);
            var endDate  = DateTime.Now;
            var NoofContribution = contribution.Where(i=> i.TransactionDate <= endDate && i.TransactionDate >= startDate).Select(i=>i.CreditAmount).Count();
            if (NoofContribution < 6)
            {
                obj.Tag = 0;
                obj.Message = "Cannot apply for loan at the moment as your contribution is not up to date";
                return obj;
            }

          
            var productName = _iUnitOfWork.CreditTypes.GetEntitybyName(initiateLoanDto.LoanProduct).Result.ProductId;
            var pmbinfo = await _iUnitOfWork.Pmbs.GetEntitybyNhf(initiateLoanDto.PMB);
            LoanInitiationEntity entity = new LoanInitiationEntity();
            
            entity.Principal = initiateLoanDto.PrincipalAmount;
            entity.Rate =  initiateLoanDto.InterestRate;
            entity.Tenor = Convert.ToInt32(affordablityDetails.proposedTenor);
            entity.LoanProduct = Convert.ToString(productName);
            entity.LoanPurpose = initiateLoanDto.Purpose;
            entity.Status = "Undergoing Approval";
            entity.NHFNumber = employeedetails.NHFNumber.ToStr();
            entity.PMB = pmbinfo.NHFNumber;
            entity.RepaymentPattern = "Monthly";
            entity.file = initiateLoanDto.file;
            entity.LoanScheme = 1;
            var saveform = _iUnitOfWork.LoanInitiations.SaveForm(entity);



            var underwriting = new UnderwritingEntity
            {
                LoanAmount = initiateLoanDto.PrincipalAmount,
                InterestRate = initiateLoanDto.InterestRate,
                Tenor = affordablityDetails.proposedTenor.ToStr(),
                Name = employeedetails.FirstName + " " + employeedetails.LastName,
                ProductName = Convert.ToString(productName),
                NHFNumber = employeedetails.NHFNumber.ToStr(),
                NextStafffLevel = pmbinfo.NHFNumber,
                LoanId = Convert.ToString(entity.Id),
                Rated = 0,
                CheckList = "0",
                Reviewed = 0,
                Company = pmbinfo.Id,
                BaseProcessMenu = 563327185478225920,
                SchemeType = 1
            };
            await _iUnitOfWork.Underwritings.SaveForm(underwriting);

            var customerInfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(underwriting.NHFNumber));
            var pmbInfo = await _iUnitOfWork.Pmbs.GetEntitybyNhf(underwriting.NextStafffLevel.ToString());
            employeeListParam.Company = pmbInfo.Id;
            var contactPerson = _iUnitOfWork.Employees.GetListByCompany(employeeListParam).Result.Where(i => i.EmployerType == 0).FirstOrDefault();
            var message = string.Empty;
            MailParameter mailParameter = new()
            {
                ContactPerson = contactPerson.FirstName + ' ' + contactPerson.LastName,
                ContactPersonEmail = pmbInfo.EmailAddress,
                UserName = customerInfo.FirstName + ' ' + customerInfo.LastName,
                PmbName = pmbinfo.Name,

            };
            bool sendemail = EmailHelper.IsContactPmbSent(mailParameter, out message);

            MailParameter LoanmailParameter = new()
            {
                UserCompany = "Federal Mortgage Of Nigeria",
                UserEmail = employeedetails.EmailAddress,
                RealName = employeedetails.LastName + " " + employeedetails.FirstName,

            };
            bool sendLoanemail = EmailHelper.IsSuccessfulLoanEmail(LoanmailParameter, out message);


            obj.Data = entity;
            obj.Tag = 1;
            obj.Message = "Loan Initiated Successfully, Passed to PMB Underwritting";
            return obj;
        }



        public async Task<TData<LoanInitiationEntity>> NonMortgageLoanApplication(InitiateLoanDto initiateLoanDto)
        {
            var context = new ApplicationDbContext();
            TData<LoanInitiationEntity> obj = new TData<LoanInitiationEntity>();
            EmployeeListParam employeeListParam = new EmployeeListParam();
            var user = await Operator.Instance.Current();
            var employeedetails = await _iUnitOfWork.Employees.GetById(user.Employee);
           
            var noOfLoans = context.LoanInitiationEntity.Where(x => x.NHFNumber == employeedetails.NHFNumber.ToString()).ToList();

            if (noOfLoans.Count >= 1)
            {
                var UnderApproved = noOfLoans.Where(i => i.Status == "0" || i.Status == null || i.Status == "").ToList();

                if (UnderApproved.Count >= 1)
                {
                    obj.Message = "You currently have an ongoing loans process in progress.";
                    obj.Tag = 0;
                    return obj;
                }

            }

            var productDetails = await _iUnitOfWork.CreditTypes.GetEntityByProductCode(initiateLoanDto.LoanProduct);
            var pmbinfo = await _iUnitOfWork.Pmbs.GetEntity(initiateLoanDto.Lender);
            LoanInitiationEntity entity = new LoanInitiationEntity();

            entity.Principal = initiateLoanDto.PrincipalAmount;
            entity.Rate = initiateLoanDto.InterestRate;
            entity.Tenor = initiateLoanDto.Tenor;
            entity.LoanProduct = productDetails.Code;
            entity.LoanPurpose = initiateLoanDto.Purpose;
            //entity.Status = "Undergoing Approval";
            entity.NHFNumber = employeedetails.NHFNumber.ToStr();
            entity.PMB = pmbinfo.NHFNumber;
            entity.RepaymentPattern = "Monthly";
            entity.file = initiateLoanDto.file;
            entity.LoanScheme = 2;
            var saveform = _iUnitOfWork.LoanInitiations.SaveForm(entity);



            var underwriting = new UnderwritingEntity
            {
                LoanAmount = initiateLoanDto.PrincipalAmount,
                InterestRate = initiateLoanDto.InterestRate,
                Tenor = initiateLoanDto.Tenor.ToString(),
                Name = employeedetails.FirstName + " " + employeedetails.LastName,
                ProductName = productDetails.Code,
                NHFNumber = employeedetails.NHFNumber.ToStr(),
                NextStafffLevel = pmbinfo.NHFNumber,
                LoanId = Convert.ToString(entity.Id),
                Rated = 0,
                CheckList = "0",
                Reviewed = 0,
                Company = pmbinfo.Id,
                BaseProcessMenu = 563327185478225920,
                SchemeType = 2
            };
            await _iUnitOfWork.Underwritings.SaveForm(underwriting);

            var customerInfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(underwriting.NHFNumber));
            var pmbInfo = await _iUnitOfWork.Pmbs.GetEntitybyNhf(underwriting.NextStafffLevel.ToString());
            employeeListParam.Company = pmbInfo.Id;
            var contactPerson = _iUnitOfWork.Employees.GetListByCompany(employeeListParam).Result.Where(i => i.EmployerType == 0).FirstOrDefault();
            var message = string.Empty;
            MailParameter mailParameter = new()
            {
                ContactPerson = contactPerson.FirstName + ' ' + contactPerson.LastName,
                ContactPersonEmail = pmbInfo.EmailAddress,
                UserName = customerInfo.FirstName + ' ' + customerInfo.LastName,
                PmbName = pmbinfo.Name,

            };
            bool sendemail = EmailHelper.IsContactPmbSent(mailParameter, out message);

            MailParameter LoanmailParameter = new()
            {
                UserCompany = "Federal Mortgage Of Nigeria",
                UserEmail = employeedetails.EmailAddress,
                RealName = employeedetails.LastName + " " + employeedetails.FirstName,

            };
            bool sendLoanemail = EmailHelper.IsSuccessfulLoanEmail(LoanmailParameter, out message);


            obj.Data = entity;
            obj.Tag = 1;
            obj.Message = "Loan Initiated Successfully, Passed to Lender Underwritting";
            return obj;
        }



        #endregion
    }
}