using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
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
using Mortgage.Ecosystem.DataAccess.Layer.Request;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using static Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos.LoanApplicationDTO;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class UnderwritingService : IUnderwritingService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public UnderwritingService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<UnderwritingEntity>>> GetList(UnderwritingListParam param)
        {
            TData<List<UnderwritingEntity>> obj = new TData<List<UnderwritingEntity>>();
            obj.Data = await _iUnitOfWork.Underwritings.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<UnderwritingEntity>>> GetApprovalPageList()
        {
            TData<List<UnderwritingEntity>> obj = new TData<List<UnderwritingEntity>>();
            obj.Data = await _iUnitOfWork.Underwritings.GetApprovalPageList();
            foreach (UnderwritingEntity under in obj.Data)
            {
                under.ProductName = _iUnitOfWork.CreditTypes.GetEntitybiId(Convert.ToInt32(under.ProductName)).Result.Name;

            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<UnderwritingEntity>>> GetBatchedLoans(long id)
        {
            Pagination pagination = new Pagination();
            TData<List<UnderwritingEntity>> obj = new TData<List<UnderwritingEntity>>();
            var batchRef = _iUnitOfWork.Underwritings.GetEntity(id).Result.BatchRefNo;
            obj.Data = _iUnitOfWork.Underwritings.GetLoanBatches(batchRef, pagination).Result.Where(i => i.isBatched == 1).ToList();
            foreach (UnderwritingEntity under in obj.Data)
            {
                under.ProductName = _iUnitOfWork.CreditTypes.GetEntitybiId(Convert.ToInt32(under.ProductName)).Result.Name;

            }
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<UnderwritingEntity>>> GetLoanForReview()
        {
            var context = new ApplicationDbContext();
            TData<List<UnderwritingEntity>> obj = new TData<List<UnderwritingEntity>>();
            //obj.Data = await _iUnitOfWork.Underwritings.GetLoanForReview();
            var user = await Operator.Instance.Current();
            var userinfo = await _iUnitOfWork.Employees.GetEntity(user.Employee);
            var companyinfo = await _iUnitOfWork.Pmbs.GetEntity(user.Company);

            var query = from t1 in context.UnderwritingEntity
                        join t2 in context.ApprovalSetupEntity on 660881219264712704 equals t2.MenuId
                        where t1.CheckList == "1" && t1.Rated == 1 && t2.Authority == user.Employee && t1.isBatched == 0
                        && t1.LoanAmount > 0 && t1.Reviewed != 1 && t1.Approved != 1 && t1.Approved != 2 && (t1.NextStafffLevel == userinfo.NHFNumber.ToString() || t1.NextStafffLevel == companyinfo.NHFNumber)
                        select t1;
            //obj.Data = await _iUnitOfWork.Underwritings.GetLoanForUnderwriting();
            obj.Data = query.ToList().DistinctBy(i => i.Id).ToList();
            foreach (UnderwritingEntity under in obj.Data)
            {
                var riskRating = await _iUnitOfWork.RiskAssessmentProcedure.GetEntity(under.NHFNumber);
                var checklist = await _iUnitOfWork.ChecklistsProcedure.GetEntity(under.NHFNumber);
                var customerinfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(under.NHFNumber));
                under.DateofEmployment = customerinfo.DateOfEmployment;
                under.DOB = customerinfo.DateOfBirth;
                under.MonthlyIncome = customerinfo.MonthlySalary;
                under.Bvn = customerinfo.BVN;
                under.Name = customerinfo.FirstName + " " + customerinfo.LastName;
                //under.ProductName = _iUnitOfWork.CreditTypes.GetEntitybiId(Convert.ToInt32(under.ProductName)).Result.Name;
                under.ProductName = "NHF Mortgage Loan";
                under.RiskScore = riskRating.AverageScore;
                under.Rating = riskRating.Rating;
                under.CheckList = checklist.Checklist;
                under.Scheme = _iUnitOfWork.Schemes.GetEntitybiId(under.SchemeType).Result.SchemeName;

            }
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<UnderwritingEntity>>> GetLoanForBatching()
        {
            try
            {
                var context = new ApplicationDbContext();
                var user = await Operator.Instance.Current();
                var userinfo = await _iUnitOfWork.Employees.GetEntity(user.Employee);
                var companyinfo = await _iUnitOfWork.Pmbs.GetEntity(user.Company);
                var query = (from t1 in context.UnderwritingEntity
                             join t2 in context.ApprovalSetupEntity on 664553002530508800 equals t2.MenuId
                             where t1.CheckList == "1" && t1.Rated == 1 && t1.Reviewed == 1 && t2.Authority == user.Employee && t1.isBatched == 0 && t1.Approved != 1 && t1.Approved != 2 && (t1.NextStafffLevel == userinfo.NHFNumber.ToString() || t1.NextStafffLevel == companyinfo.NHFNumber) && t1.SchemeType == 1
                             select t1).Distinct();
                TData<List<UnderwritingEntity>> obj = new TData<List<UnderwritingEntity>>();
                //obj.Data = await _iUnitOfWork.Underwritings.GetLoanForBatching();
                obj.Data = query.ToList();
                foreach (var under in obj.Data)
                {
                    var customerinfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(under.NHFNumber));
                    under.DateofEmployment = customerinfo.DateOfEmployment;
                    under.DOB = customerinfo.DateOfBirth;
                    under.MonthlyIncome = customerinfo.MonthlySalary;
                    under.Bvn = customerinfo.BVN;
                    under.Name = customerinfo.FirstName + " " + customerinfo.LastName;
                    under.ProductName = "NHF Mortgage Loan";

                }
                obj.Total = obj.Data.Count;
                obj.Tag = 1;
                return obj;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<TData<List<UnderwritingEntity>>> GetLoanForDisbursment()
        {
            try
            {
                var context = new ApplicationDbContext();
                var user = await Operator.Instance.Current();
                var userinfo = await _iUnitOfWork.Employees.GetEntity(user.Employee);
                var companyinfo = await _iUnitOfWork.Pmbs.GetEntity(user.Company);
                var query = (from t1 in context.UnderwritingEntity
                             join t2 in context.ApprovalSetupEntity on 5898777445214440000 equals t2.MenuId
                             where t1.CheckList == "1" && t1.Rated == 1 && t1.Reviewed == 1 && t2.Authority == user.Employee && t1.isBatched == 0 && t1.Approved != 1 && t1.Approved != 2 && (t1.NextStafffLevel == userinfo.NHFNumber.ToString() || t1.NextStafffLevel == companyinfo.NHFNumber) && t1.SchemeType == 2
                             select t1).Distinct();
                TData<List<UnderwritingEntity>> obj = new TData<List<UnderwritingEntity>>();
                //obj.Data = await _iUnitOfWork.Underwritings.GetLoanForBatching();
                obj.Data = query.ToList();
                foreach (var under in obj.Data)
                {
                    var productInfo = await _iUnitOfWork.CreditTypes.GetEntityByProductCode(under.ProductName);
                    var customerinfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(under.NHFNumber));
                    under.DateofEmployment = customerinfo.DateOfEmployment;
                    under.DOB = customerinfo.DateOfBirth;
                    under.MonthlyIncome = customerinfo.MonthlySalary;
                    under.Bvn = customerinfo.BVN;
                    under.Name = customerinfo.FirstName + " " + customerinfo.LastName;
                    under.creditName = productInfo.Name;
                    under.Scheme = _iUnitOfWork.Schemes.GetEntitybiId(under.SchemeType).Result.SchemeName;

                }
                obj.Total = obj.Data.Count;
                obj.Tag = 1;
                return obj;
            }
            catch (Exception ex)
            {

                throw;
            }
        }





        public async Task<TData<List<UnderwritingEntity>>> GetBatched()
        {
            decimal amount = 0;
            var context = new ApplicationDbContext();
            TData<List<UnderwritingEntity>> obj = new TData<List<UnderwritingEntity>>();
            //obj.Data =  _iUnitOfWork.Underwritings.GetBatchedLoan().Result.DistinctBy(x => x.BatchRefNo).ToList();
            var user = await Operator.Instance.Current();
            var userinfo = await _iUnitOfWork.Employees.GetEntity(user.Employee);
            var companyinfo = await _iUnitOfWork.Pmbs.GetEntity(user.Company);
            var query = from t1 in context.UnderwritingEntity
                        join t2 in context.ApprovalSetupEntity on 664553002530508800 equals t2.MenuId
                        where t1.CheckList == "1" && t1.Rated == 1 && t1.Reviewed == 1 && t2.Authority == user.Employee && t1.isBatched == 1 && t1.Approved != 1 && t1.Approved != 2 && (t1.NextStafffLevel == userinfo.NHFNumber.ToString() || t1.NextStafffLevel == companyinfo.NHFNumber)
                        select t1;
            //obj.Data = await _iUnitOfWork.Underwritings.GetLoanForBatching();
            obj.Data = query.ToList().DistinctBy(i => i.BatchRefNo).ToList();
            foreach (UnderwritingEntity under in obj.Data)
            {
                amount = amount + under.LoanAmount;
                under.pmb = _iUnitOfWork.Pmbs.GetEntitybyNhf(under.NextStafffLevel).Result.Name;
                //under.ProductName = _iUnitOfWork.CreditTypes.GetEntitybiId(Convert.ToInt32(under.ProductName)).Result.Name;
                under.ProductName = "NHF Mortgage Loan";

            }
            //foreach (var item in obj.Data)
            //{
            //    item.totalAmount = amount;

            //}
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<List<UnderwritingEntity>>> GetLoanForUnderwriting()
        {
            try
            {
                var context = new ApplicationDbContext();
                TData<List<UnderwritingEntity>> obj = new TData<List<UnderwritingEntity>>();
                var user = await Operator.Instance.Current();
                var userinfo = await _iUnitOfWork.Employees.GetEntity(user.Employee);
                var companyinfo = await _iUnitOfWork.Pmbs.GetEntity(user.Company);
                var query = from t1 in context.UnderwritingEntity
                            join t2 in context.ApprovalSetupEntity on 563327185478225920 equals t2.MenuId
                            where t1.CheckList != "1"
                                  && t1.Rated != 1
                                  && t2.Authority == user.Employee
                                  && t1.isBatched == 0 
                                  && t1.LoanAmount > 0
                                  && t1.Reviewed != 1
                                  && t1.Approved != 1
                                  && t1.Approved != 2
                                  && (t1.NextStafffLevel == userinfo.NHFNumber.ToString() || t1.NextStafffLevel == companyinfo.NHFNumber)
                            select t1;

                //obj.Data = await _iUnitOfWork.Underwritings.GetLoanForUnderwriting();
                obj.Data = query.ToList();
                foreach (UnderwritingEntity under in obj.Data)
                {
                    var schemeInfo = await _iUnitOfWork.Schemes.GetEntitybiId(under.SchemeType);
                    under.Scheme = schemeInfo.SchemeName;
                    var customerinfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(under.NHFNumber));
                    var ChecklisInfo = await _iUnitOfWork.ChecklistsProcedure.GetEntity(under.NHFNumber);
                    if (ChecklisInfo != null)
                    {
                        under.CheckList = "1";
                    }
                    var ChecklisIn = await _iUnitOfWork.RiskAssessmentProcedure.GetEntity(under.NHFNumber);
                    if (ChecklisIn != null)
                    {
                        under.Rated = 1;
                    }
                    under.DateofEmployment = customerinfo.DateOfEmployment;
                    under.DOB = customerinfo.DateOfBirth;
                    under.MonthlyIncome = customerinfo.MonthlySalary;
                    under.Bvn = customerinfo.BVN;
                    under.Name = customerinfo.FirstName + " " + customerinfo.LastName;
                    under.ProductName = _iUnitOfWork.CreditTypes.GetEntityByProductCode(under.ProductName).Result.Name;

                }
                obj.Total = obj.Data.Count;
                obj.Tag = 1;
                return obj;

            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<TData<string>> ApproveUnderwriting(long id)
        {
            var context = new ApplicationDbContext();
            TData<string> obj = new TData<string>();
            var Item = await _iUnitOfWork.Underwritings.GetEntity(id);
            var CheckListInfo = await _iUnitOfWork.ChecklistsProcedure.GetEntity(Item.NHFNumber);
            var RiskRating = await _iUnitOfWork.RiskAssessmentProcedure.GetEntity(Item.NHFNumber);
            if (CheckListInfo == null || RiskRating == null)
            {
                obj.Message = "Please complete Underwriting Process before Approval";
                obj.Tag = 0;
                return obj;

            }
            Item.CheckList = "1";
            Item.Rated = 1;
            await _iUnitOfWork.Underwritings.SaveForm(Item);
            var Employeeinfo = context.EmployeeEntity.Where(i => i.NHFNumber == long.Parse(Item.NHFNumber)).DefaultIfEmpty().FirstOrDefault();
            if (Employeeinfo != null)
            {
                string message;
                MailParameter mailParameter = new()
                {
                    UserEmail = Employeeinfo.EmailAddress,
                    RealName = Employeeinfo.LastName + " " + Employeeinfo.FirstName,
                    COmpanyMail = "fmbn@gov.ng",
                    UserCompany = "Federal Mortgage Bank of Nigeria"


                };

                var sendMail = EmailHelper.IsLoanReviewApprovalMailSent(mailParameter, out message);

            }

            obj.Message = "Approved Successfully";
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> DisapproveUnderwriting(long id)
        {
            var context = new ApplicationDbContext();
            TData<string> obj = new TData<string>();
            var Item = await _iUnitOfWork.Underwritings.GetEntity(id);
            Item.Approved = 2;
            await _iUnitOfWork.Underwritings.SaveForm(Item);
            obj.Message = "DisApproved Successfully";
            obj.Tag = 1;
            return obj;
        }





        public async Task<TData<List<UnderwritingEntity>>> GetPageList(UnderwritingListParam param, Pagination pagination)
        {
            var user = await Operator.Instance.Current();
            TData<List<UnderwritingEntity>> obj = new TData<List<UnderwritingEntity>>();
            param.pmb = Convert.ToString(user.EmployeeInfo.NHFNumber);
            obj.Data = await _iUnitOfWork.Underwritings.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        //public async Task<TData<string>> ProceedLoan(string NHFNumber)
        //{
        //    var user = await Operator.Instance.Current();
        //    var obj = new TData<string>();
        //    var employeeInfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(Convert.ToInt64(NHFNumber));
        //    var loanInfo = await _iUnitOfWork.LoanInitiations.GetEntity(Convert.ToString(employeeInfo.NHFNumber));
        //    var creditScore = await _iUnitOfWork.RiskAssessmentProcedure.GetEntity(Convert.ToString(employeeInfo.NHFNumber));
        //    var pmbExist = await CustomerExist(Convert.ToString(user.CompanyInfo.Id));
        //    var pmbInfo = await _iUnitOfWork.Pmbs.GetEntity(user.CompanyInfo.Id);
        //    if (!pmbExist)
        //    {
        //        await IndividualExisting(pmbInfo);

        //    }
        //    var employeeExist = await CustomerExist(Convert.ToString(employeeInfo.Id));
        //    var productId = _iUnitOfWork.CreditTypes.GetEntity(loanInfo.LoanProduct).Result.ProductId;
        //    if (!employeeExist)
        //    {
        //        await IndividualExisting(employeeInfo, pmbInfo.NHFNumber);

        //    }
        //    checkafford CustomersAffordabilityvm = new checkafford();
        //    CustomersAffordabilityvm.amountRequested = loanInfo.Principal;
        //    CustomersAffordabilityvm.nhfAccount = Convert.ToString(employeeInfo.NHFNumber);
        //    CustomersAffordabilityvm.productId = productId;
        //    CustomersAffordabilityvm.proposedTenor = loanInfo.Tenor;
        //    var affordabilityDetails = await Performaffordability(CustomersAffordabilityvm);
        //    LoanApplicationViewModel2 lad = new LoanApplicationViewModel2()
        //    {

        //        proposedAmount = loanInfo.Principal,
        //        proposedProductId = productId,
        //        proposedTenor = loanInfo.Tenor,
        //        loanPurpose = loanInfo.LoanPurpose,
        //        operatingAccountNo = Convert.ToString(pmbInfo.NHFNumber),
        //        subSectorId = 1,
        //        requestedAmount = loanInfo.Principal,
        //        repaymentDate = loanInfo.RepaymentPattern,
        //        creditScore = Convert.ToDecimal(creditScore.AverageScore),
        //        creditRating = creditScore.Rating

        //    };
        //    AffordabilityDetails loanaff = new AffordabilityDetails()
        //    {
        //        productId = affordabilityDetails.Data.FirstOrDefault().productId,
        //        yearsInService = affordabilityDetails.Data.FirstOrDefault().yearsInService,
        //        affordableAmount = affordabilityDetails.Data.FirstOrDefault().affordableAmount,
        //        age = affordabilityDetails.Data.FirstOrDefault().age,
        //        casaAccountId = affordabilityDetails.Data.FirstOrDefault().casaAccountId,
        //        amountRequested = affordabilityDetails.Data.FirstOrDefault().amountRequested,
        //        monthlyRepayment = affordabilityDetails.Data.FirstOrDefault().monthlyRepayment,
        //        presentValue = affordabilityDetails.Data.FirstOrDefault().presentValue,
        //        profitability = affordabilityDetails.Data.FirstOrDefault().profitability,
        //        repaymentPeriod = affordabilityDetails.Data.FirstOrDefault().repaymentPeriod,
        //        rate = affordabilityDetails.Data.FirstOrDefault().rate,
        //        tenorOverride = affordabilityDetails.Data.FirstOrDefault().tenorOverride

        //    };
        //    LoanApplicationRequestDTO loanDetails = new LoanApplicationRequestDTO();
        //    loanDetails.customerCode = Convert.ToString(employeeInfo.Id);
        //    loanDetails.loanApplicationSourceId = 3;
        //    loanDetails.loanApplicationDetail = lad;
        //    loanDetails.affordabilityDetails = loanaff;
        //    var LoanApp = await UpdateLoanApplication(loanDetails);

        //    var db = new ApplicationDbContext();
        //    var underwriting = db.UnderwritingEntity.Where(i => i.LoanId == Convert.ToString(loanInfo.Id)).DefaultIfEmpty().FirstOrDefault();
        //    underwriting.Comments = "Approved";
        //    underwriting.Approved = 1;
        //    underwriting.LoanRefNo = LoanApp.Data;
        //    db.SaveChanges();

        //    var loan = db.LoanInitiationEntity.Where(i => i.NHFNumber == NHFNumber).DefaultIfEmpty().FirstOrDefault();
        //    loan.ApplicationReferenceNo = LoanApp.Data;
        //    loan.Status = "1";
        //    db.SaveChanges();
        //    obj.Data = LoanApp.Data;
        //    obj.Message = "Loan Application successfull with Ref No:" + " " + LoanApp.Data;
        //    obj.Tag = 1;
        //    return obj;

        //}

        private async Task<TData<string>> UpdateLoanApplication(LoanApplicationsVM loanApplicationRequest)
        {

            try
            {
                var client = new HttpClient();
                //var jsonPayload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(loanApplicationRequest), Encoding.UTF8, ApiResource.ApplicationJson);
                var serializeObject = JsonConvert.SerializeObject(loanApplicationRequest);

                StringContent content1 = new StringContent(serializeObject, Encoding.UTF8, "application/json");


                var response = await client.PostAsync("https://testcorebanking.fmbn.gov.ng:4448/api/v1/loan/loan-application-multiple", content1);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoanApplicationUpdateDTO>(responseContent);

                if (response.IsSuccessStatusCode && result.success == true)
                {
                    TData<string> obj = new TData<string>();

                    obj.Data = result.result.applicationReferenceNumber;
                    obj.Tag = 1;
                    return obj;
                }
                else
                {
                    responseContent = await response.Content.ReadAsStringAsync();
                    var result1 = JsonConvert.DeserializeObject<LoanApplicationUpdateDTOFailed>(responseContent);

                    TData<string> obj = new TData<string>();

                    obj.Data = result1.message;
                    obj.Tag = 0;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        private async Task<bool> CustomerExist(string customerCode)
        {
            try
            {
                var _client = new HttpClient();
                var response = await _client.GetAsync(ApiResource.customerExist + customerCode);
                string response1 = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomerExistResponse>(response1);

                if (result.Status == true)
                {
                    // Customer exists
                    return true;
                }

                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<TData> IndividualExisting(EmployeeEntity customercreateRequest, string pmbNhf)
        {
            TData<string> obj = new TData<string>();
            try
            {
                var NextOfKinDetails = _iUnitOfWork.NextOfKins.GetEntity(customercreateRequest.Id).Result;
                ContactAddress address = new ContactAddress()
                {
                    contactAddress = customercreateRequest.PostalAddress.ToString(),
                    NearestLandmark = customercreateRequest.PostalAddress,
                    MailingAddress = customercreateRequest.EmailAddress,
                    CityId = 3017,
                    StateId = 1,
                    AddressTypeId = 3,
                    UtilityBillNo = "3456789"

                };
                NextofKin NOK = new NextofKin()
                {
                    firstName = NextOfKinDetails.FirstName,
                    lastName = NextOfKinDetails.LastName,
                    contactAddress = NextOfKinDetails.Address,
                    emailAddress = NextOfKinDetails.EmailAddress,
                    mobilePhoneNo = NextOfKinDetails.MobileNumber,
                    nearestLandmark = "",
                    cityId = 3017,
                    relationship = NextOfKinDetails.Relationship.ToString(),
                    dateOfBirth = DateTime.MinValue,
                    gender = "1",

                };
                ContactPhone phone = new ContactPhone()
                {
                    OfficeLandNo = customercreateRequest.MobileNumber,
                    MobilePhoneNo = customercreateRequest.MobileNumber
                };
                AccountDetails acctdetails = new AccountDetails()
                {
                    AccountStatusName = "Active",
                    ProductAccountName = "NHF",
                    AccountNumber = Convert.ToString(customercreateRequest.NHFNumber),
                    DateOfEmployment = customercreateRequest.DateOfEmployment,
                    MonthlyIncome = customercreateRequest.MonthlySalary.ToString(),
                    OtherBankAccountNumber = customercreateRequest.BankAccountNumber,
                    OtherBankSortCode = "214",
                    pmbNhfAccount = pmbNhf,

                };
                CreateCustomerRequestDTO customer = new CreateCustomerRequestDTO();
                customer.FirstName = customercreateRequest.FirstName;
                customer.Gender = Convert.ToString(customercreateRequest.Gender);
                customer.LastName = customercreateRequest.LastName;
                customer.MiddleName = customercreateRequest.OtherName;
                customer.CustomerCode = Convert.ToString(customercreateRequest.EmployeeCode);
                customer.customerTypeId = 1;
                customer.employerNumber = Convert.ToString(customercreateRequest.Company);
                customer.profileSourceId = 3;
                customer.Nationality = "Nigerian";
                customer.DateOfBirth = Convert.ToDateTime(customercreateRequest.DateOfBirth);
                customer.EmailAddress = customercreateRequest.EmailAddress;
                customer.CustomerBVN = customercreateRequest.BVN;
                customer.IsBvnValidated = true;
                customer.IsEmailValidated = true;
                customer.IsPhoneValidated = true;
                customer.IsPoliticallyExposed = true;
                customer.CrmsLegalStatusId = 104;
                customer.CrmsRelationshipTypeId = 152;
                customer.TaxNumber = customercreateRequest.NIN;
                customer.Title = customercreateRequest.Title.ToString();
                customer.SubSectorId = 1;
                customer.BranchCode = "21";
                customer.AccountDetails = acctdetails;
                customer.contactPhone = phone;
                customer.contactAddress = address;

                var client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://testcorebanking.fmbn.gov.ng:4448/api/v1/customer/individual-existing", content);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CustomerExistResponse>(apiResponse);

                    obj.Tag = 1;
                    obj.Message = "Employee Created";
                    obj.Data = null;

                }
                else
                {
                    obj.Tag = 0;
                    obj.Data = null;
                }
                return obj;

            }
            catch (Exception ex)
            {

                throw;
            }





        }

        private async Task<TData> IndividualExisting(LenderInstitutionsEntity customercreateRequest)
        {
            TData<string> obj = new TData<string>();
            try
            {
                ContactAddress address = new ContactAddress()
                {
                    contactAddress = customercreateRequest.Address.ToString(),
                    NearestLandmark = customercreateRequest.Address,
                    MailingAddress = customercreateRequest.EmailAddress,
                    CityId = 3017,
                    StateId = 1,
                    AddressTypeId = 3,
                    UtilityBillNo = "3456789"

                };

                ContactPhone phone = new ContactPhone()
                {
                    OfficeLandNo = customercreateRequest.MobileNumber,
                    MobilePhoneNo = customercreateRequest.MobileNumber
                };
                AccountDetails acctdetails = new AccountDetails()
                {
                    AccountStatusName = "Active",
                    ProductAccountName = "NHF",
                    AccountNumber = customercreateRequest.NHFNumber,
                    DateOfEmployment = customercreateRequest.DateOfIncorporation,
                    MonthlyIncome = "0",
                    OtherBankAccountNumber = "4545455",
                    OtherBankSortCode = "214",
                    pmbNhfAccount = "",

                };
                CreateCustomerRequestDTO customer = new CreateCustomerRequestDTO();
                customer.FirstName = customercreateRequest.Name;
                customer.customerTypeId = 2;
                customer.Gender = "";
                customer.CustomerCode = Convert.ToString(customercreateRequest.PmbCode);
                customer.LastName = customercreateRequest.Name;
                customer.MiddleName = "";
                customer.employerNumber = customercreateRequest.NHFNumber;
                customer.profileSourceId = 3;
                customer.Nationality = "Nigerian";
                customer.DateOfBirth = customercreateRequest.DateOfIncorporation;
                customer.EmailAddress = customercreateRequest.EmailAddress;
                customer.CustomerBVN = "234566";
                customer.IsBvnValidated = false;
                customer.IsEmailValidated = false;
                customer.IsPhoneValidated = false;
                customer.IsPoliticallyExposed = false;
                customer.CrmsLegalStatusId = 104;
                customer.CrmsRelationshipTypeId = 152;
                customer.TaxNumber = customercreateRequest.RCNumber;
                customer.Title = "";
                customer.SubSectorId = 5;
                customer.BranchCode = "21";
                customer.AccountDetails = acctdetails;
                customer.contactPhone = phone;
                customer.contactAddress = address;

                var client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://testcorebanking.fmbn.gov.ng:4438/api/v1/customer/individual-existing", content);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CustomerExistResponse>(apiResponse);

                    obj.Tag = 1;
                    obj.Message = "Employee Created";
                    obj.Data = null;

                }
                else
                {
                    obj.Tag = 0;
                    obj.Data = null;
                }
                return obj;

            }
            catch (Exception ex)
            {

                throw;
            }





        }

        private async Task<TData<List<messages>>> Performaffordability(checkafford CustomersAffordabilityvm)
        {

            string Message = string.Empty;
            AffordabilityResponseDto result = new AffordabilityResponseDto();
            CustomersAffordabilityvm.amountRequested = Convert.ToDecimal(CustomersAffordabilityvm.amountRequested);
            CustomersAffordabilityvm.productId = Convert.ToInt32(CustomersAffordabilityvm.productId);
            CustomersAffordabilityvm.nhfAccount = CustomersAffordabilityvm.nhfAccount;
            CustomersAffordabilityvm.proposedTenor = CustomersAffordabilityvm.proposedTenor;
            var AffordabilityCheck = UpdateLoanAffordability(CustomersAffordabilityvm).Result;
            result.affordableAmount = AffordabilityCheck.FirstOrDefault().affordableAmount.ToString();
            result.amountRequested = AffordabilityCheck.FirstOrDefault().amountRequested.ToString();
            result.monthlyRepayment = AffordabilityCheck.FirstOrDefault().monthlyRepayment.ToString();
            result.rate = AffordabilityCheck.FirstOrDefault().rate;
            result.proposedTenor = AffordabilityCheck.FirstOrDefault().proposedTenor;

            TData<List<messages>> obj = new TData<List<messages>>();
            obj.Data = AffordabilityCheck;
            obj.Tag = 1;
            return obj;


        }

        [HttpPost]
        private async Task<List<messages>> UpdateLoanAffordability(checkafford affordabilityVM)
        {
            var obj = new List<messages>();
            try
            {
                var _client = new HttpClient();
                var jsonPayload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(affordabilityVM), Encoding.UTF8, ApiResource.ApplicationJson);

                var response = await _client.PostAsync(ApiResource.loanAffordability, jsonPayload);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AffordabilityVM>(responseContent);
                if (response.IsSuccessStatusCode && result.success == true)
                {
                    obj.AddRange(result.result);
                    return obj;
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUnderwritingList(UnderwritingListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<UnderwritingEntity> underwritingList = await _iUnitOfWork.Underwritings.GetList(param);
            foreach (UnderwritingEntity underwriting in underwritingList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = underwriting.Id,
                    name = underwriting.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(UnderwritingListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<UnderwritingEntity> underwritingList = await _iUnitOfWork.Underwritings.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (UnderwritingEntity underwriting in underwritingList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = underwriting.Id,
                    name = underwriting.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == underwriting.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<UnderwritingEntity>> GetEntity(long id)
        {
            TData<UnderwritingEntity> obj = new TData<UnderwritingEntity>();
            obj.Data = await _iUnitOfWork.Underwritings.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.PropertySubscriptions.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(UnderwritingEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Underwritings.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> batchLoan(string lists)
        {
            TData<string> obj = new TData<string>();
            var batchData = new List<UnderwritingEntity>();
            var batchNo = RandomHelper.RandomLongGenerator(2000005, 99999999);
            batchData = JsonConvert.DeserializeObject<List<UnderwritingEntity>>(JsonConvert.DeserializeObject(lists).ToString());
            var pmb = await _iUnitOfWork.Pmbs.GetEntitybyNhf(batchData?.FirstOrDefault()?.NextStafffLevel);
            var batchRef = pmb?.Name.ParseToString() + "/FMBN" + "-" + batchNo;

            foreach (UnderwritingEntity item in batchData)
            {
                try
                {
                    var loanid = await _iUnitOfWork.CreditTypes.GetEntitybyName(item.ProductName);
                    item.isBatched = 1;
                    item.BatchRefNo = batchRef;
                    item.ProductName = loanid.Code;

                    await _iUnitOfWork.Underwritings.SaveForm(item);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            obj.Message = "Loan(s) Batched sucessfully";
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<string>> UnbatchLoan(string lists)
        {
            TData<string> obj = new TData<string>();
            var batchData = new List<UnderwritingEntity>();
            var batchNo = RandomHelper.RandomLongGenerator(2000005, 99999999);
            batchData = JsonConvert.DeserializeObject<List<UnderwritingEntity>>(JsonConvert.DeserializeObject(lists).ToString());
            var pmb = await _iUnitOfWork.Pmbs.GetEntitybyNhf(batchData?.FirstOrDefault()?.NextStafffLevel);
            var batchRef = pmb?.Name.ParseToString() + "-" + batchNo;

            foreach (UnderwritingEntity item in batchData)
            {
                var loanid = await _iUnitOfWork.CreditTypes.GetEntitybyName(item.ProductName);
                item.isBatched = 0;
                item.BatchRefNo = string.Empty;
                item.ProductName = loanid.Code;

                await _iUnitOfWork.Underwritings.SaveForm(item);

            }
            obj.Message = "Loan Unbatched from List successfully";
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<string>> ApproveBatchedLoan(string lists)
        {
            var Db = new ApplicationDbContext();
            TData<string> obj = new TData<string>();
            var batchData = new List<UnderwritingEntity>();
            batchData = JsonConvert.DeserializeObject<List<UnderwritingEntity>>(JsonConvert.DeserializeObject(lists).ToString());
            var pmb = await _iUnitOfWork.Pmbs.GetEntitybyNhf(batchData?.FirstOrDefault()?.NextStafffLevel);
            UnderwritingListParam Ent = new UnderwritingListParam();
            //Ent.BatchRefNo = batchData.FirstOrDefault().BatchRefNo;
            var Loans = _iUnitOfWork.Underwritings.GetList(Ent).Result.Where(i => i.BatchRefNo == batchData.FirstOrDefault().BatchRefNo).ToList();
            var Employees = new List<EmployeeEntity>();
            var param = new EmployeeListParam();
            foreach (var item in Loans)
            {
                var employeeInfo = _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(item.NHFNumber)).Result;
                Employees.Add(employeeInfo);

            }
            var AffordabilityList = new List<messages>();

            var pmbExist = await CustomerExist(Convert.ToString(pmb.PmbCode));
            var pmbInfo = await _iUnitOfWork.Pmbs.GetEntity(pmb.Id);
            if (!pmbExist)
            {
                await IndividualExisting(pmbInfo);

            }
            var AllLoanDetails = new List<LoanApplicationDetailss>();
            foreach (var employee in Employees)
            {
                var loanAppDetails = new LoanApplicationDetailss();
                var loanAffDetails = new AffordabilityDetailss();
                var employeeExist = await CustomerExist(Convert.ToString(employee.EmployeeCode));
                if (!employeeExist)
                {
                    await IndividualExisting(employee, pmbInfo.NHFNumber);

                }
                var CreditRating = await _iUnitOfWork.RiskAssessmentProcedure.GetEntity(Convert.ToString(employee.NHFNumber));
                var LoanInfo = Loans.Where(i => i.NHFNumber == employee.NHFNumber.ToString()).DefaultIfEmpty().FirstOrDefault();
                var EmployeeLoanInfo = await _iUnitOfWork.LoanInitiations.GetEntityById(long.Parse(LoanInfo.LoanId));
                var product = _iUnitOfWork.CreditTypes.GetEntity(EmployeeLoanInfo.LoanProduct).Result.ProductId;
                checkafford CustomersAffordabilityvm = new checkafford();
                CustomersAffordabilityvm.amountRequested = EmployeeLoanInfo.Principal;
                CustomersAffordabilityvm.nhfAccount = Convert.ToString(employee.NHFNumber);
                CustomersAffordabilityvm.productId = product;
                CustomersAffordabilityvm.proposedTenor = EmployeeLoanInfo.Tenor;
                var affordabilityDetails = await Performaffordability(CustomersAffordabilityvm);
                loanAffDetails.productId = affordabilityDetails.Data.FirstOrDefault().productId;
                loanAffDetails.yearsInService = affordabilityDetails.Data.FirstOrDefault().yearsInService;
                loanAffDetails.affordableAmount = affordabilityDetails.Data.FirstOrDefault().affordableAmount;
                loanAffDetails.age = affordabilityDetails.Data.FirstOrDefault().age;
                loanAffDetails.casaAccountId = affordabilityDetails.Data.FirstOrDefault().casaAccountId;
                loanAffDetails.amountRequested = affordabilityDetails.Data.FirstOrDefault().amountRequested;
                loanAffDetails.monthlyRepayment = affordabilityDetails.Data.FirstOrDefault().monthlyRepayment;
                loanAffDetails.presentValue = affordabilityDetails.Data.FirstOrDefault().presentValue;
                loanAffDetails.profitability = affordabilityDetails.Data.FirstOrDefault().profitability;
                loanAffDetails.repaymentPeriod = affordabilityDetails.Data.FirstOrDefault().repaymentPeriod;
                loanAffDetails.rate = affordabilityDetails.Data.FirstOrDefault().rate;
                loanAffDetails.tenorOverride = affordabilityDetails.Data.FirstOrDefault().tenorOverride;

                loanAppDetails.affordabilityDetails = loanAffDetails;
                loanAppDetails.customerCode = employee.EmployeeCode;
                loanAppDetails.proposedProductId = product;
                loanAppDetails.proposedTenor = affordabilityDetails.Data.FirstOrDefault().repaymentPeriod;
                loanAppDetails.proposedAmount = affordabilityDetails.Data.FirstOrDefault().affordableAmount;
                loanAppDetails.subSectorId = 1;
                loanAppDetails.loanPurpose = EmployeeLoanInfo.LoanPurpose;
                loanAppDetails.operatingAccountNo = pmbInfo.NHFNumber;
                loanAppDetails.requestedAmount = EmployeeLoanInfo.Principal;
                loanAppDetails.repaymentDate = "Monthly";
                loanAppDetails.creditRating = CreditRating.Rating;
                loanAppDetails.creditScore = Convert.ToInt32(CreditRating.AverageScore);

                AllLoanDetails.Add(loanAppDetails);

            }

            LoanApplicationsVM loanDetails = new LoanApplicationsVM();
            loanDetails.customerCode = pmbInfo.PmbCode;
            loanDetails.loanApplicationSourceId = 3;
            loanDetails.loanApplicationDetails = AllLoanDetails;
            var LoanApp = await UpdateLoanApplication(loanDetails);
            if (LoanApp.Tag == 0)
            {
                var context = new ApplicationDbContext();
                obj.Message = LoanApp.Data;
                obj.Tag = 0;
                var errorlog = new ErrorLogEntity();
                errorlog.ErrorCode = "405";
                errorlog.IpAddress = NetHelper.Ip;
                errorlog.Level = "Error";
                errorlog.Message = obj.Message;
                errorlog.LoggedOnDate = DateTime.Now;
                errorlog.OriginatingProcess = "https://testcorebanking.fmbn.gov.ng:4448/api/v1/loan/loan-application-multiple";
                errorlog.StackTrace = "Update Loan Application, Underwriting Controller";
                errorlog.Username = pmb.EmailAddress;
                errorlog.Callsite = "https://testcorebanking.fmbn.gov.ng:4448/api/v1/loan/loan-application-multiple";
                context.ErrorLogEntity.Add(errorlog);
                context.SaveChanges();
                return obj;

            }

            var LoanToUpdate = Db.UnderwritingEntity.Where(i => i.BatchRefNo == batchData.FirstOrDefault().BatchRefNo).ToList();
            foreach (var item in LoanToUpdate)
            {
                item.Approved = 1;
                item.LoanRefNo = LoanApp.Data;
            }
            foreach (var loan in LoanToUpdate)
            {
                var LoanTo = Db.LoanInitiationEntity.Where(i => i.Id == long.Parse(loan.LoanId)).FirstOrDefault();
                LoanTo.Status = "1";
                LoanTo.ApplicationReferenceNo = LoanApp.Data;

            }
            Db.SaveChanges();

            foreach (var employee in Employees)
            {
                string message;
                MailParameter mailParameter = new()
                {
                    UserEmail = employee.EmailAddress,
                    RealName = employee.LastName + " " + employee.FirstName,
                    COmpanyMail = "fmbn@gov.ng",
                    UserCompany = "Federal Mortgage Bank of Nigeria"


                };

                var sendMail = EmailHelper.IsLoanpprovalToCreditMailSent(mailParameter, out message);

            }

            obj.Message = "Loan Application Successful with loan Refrence Number " + LoanApp.Data;
            obj.Tag = 1;
            return obj;
        }















        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Underwritings.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AffordabilityDetails>>> PerformAffordability(string NHFNumber)
        {
            TData<List<AffordabilityDetails>> obj = new TData<List<AffordabilityDetails>>();
            var result = new List<AffordabilityDetails>();
            var user = await Operator.Instance.Current();
            var employeeInfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(Convert.ToInt64(NHFNumber));
            var loanInfo = await _iUnitOfWork.LoanInitiations.GetEntity(Convert.ToString(employeeInfo.NHFNumber));
            var creditScore = await _iUnitOfWork.RiskAssessmentProcedure.GetEntity(Convert.ToString(employeeInfo.NHFNumber));
            var pmbExist = await CustomerExist(Convert.ToString(user.CompanyInfo.Id));
            var pmbInfo = await _iUnitOfWork.Pmbs.GetEntity(user.CompanyInfo.Id);
            if (!pmbExist)
            {
                await IndividualExisting(pmbInfo);

            }
            var employeeExist = await CustomerExist(Convert.ToString(employeeInfo.Id));
            var productId = _iUnitOfWork.CreditTypes.GetEntity(loanInfo.LoanProduct).Result.ProductId;
            if (!employeeExist)
            {
                await IndividualExisting(employeeInfo, pmbInfo.NHFNumber);

            }
            checkafford CustomersAffordabilityvm = new checkafford();
            CustomersAffordabilityvm.amountRequested = loanInfo.Principal;
            CustomersAffordabilityvm.nhfAccount = Convert.ToString(employeeInfo.NHFNumber);
            CustomersAffordabilityvm.productId = productId;
            CustomersAffordabilityvm.proposedTenor = loanInfo.Tenor;
            var affordabilityDetails = await Performaffordability(CustomersAffordabilityvm);

            AffordabilityDetails loanaff = new AffordabilityDetails()
            {
                productId = affordabilityDetails.Data.FirstOrDefault().productId,
                yearsInService = affordabilityDetails.Data.FirstOrDefault().yearsInService,
                affordableAmount = affordabilityDetails.Data.FirstOrDefault().affordableAmount,
                age = affordabilityDetails.Data.FirstOrDefault().age,
                casaAccountId = affordabilityDetails.Data.FirstOrDefault().casaAccountId,
                amountRequested = affordabilityDetails.Data.FirstOrDefault().amountRequested,
                monthlyRepayment = affordabilityDetails.Data.FirstOrDefault().monthlyRepayment,
                presentValue = affordabilityDetails.Data.FirstOrDefault().presentValue,
                profitability = affordabilityDetails.Data.FirstOrDefault().profitability,
                repaymentPeriod = affordabilityDetails.Data.FirstOrDefault().repaymentPeriod,
                rate = affordabilityDetails.Data.FirstOrDefault().rate,
                tenorOverride = affordabilityDetails.Data.FirstOrDefault().tenorOverride

            };
            result.Add(loanaff);
            obj.Data = result;
            obj.Message = "Loan Affordability successful";
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<string>> DisburseNonNhfLoan(long Id)
        {
            var db = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            var obj = new TData<string>();
            var underwriting = db.UnderwritingEntity.Where(i => i.Id == Id).DefaultIfEmpty().FirstOrDefault();
            var pmbinfo = db.PmbEntity.Where(i => i.NHFNumber == underwriting.NextStafffLevel).DefaultIfEmpty().FirstOrDefault();

            var employeeInfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(underwriting.NHFNumber));

            //var underwriting = db.UnderwritingEntity.Where(i => i.Id == Id).DefaultIfEmpty().FirstOrDefault();
            underwriting.Comments = "Approved";
            underwriting.Approved = 1;
            underwriting.Reviewed = 1;
            underwriting.Disbursed = 1;

            var loaninfo =  db.LoanInitiationEntity.Where(i=> i.Id == long.Parse(underwriting.LoanId)).DefaultIfEmpty().FirstOrDefault();
            loaninfo.Status = "Loan Disbursed";
            var batchNo = RandomHelper.RandomLongGenerator(2000005, 99999999);

            loaninfo.ApplicationReferenceNo = "Non-NHF-" + batchNo;

            var Disbursement = new LoanDisbursementEntity
            {
                ProductCode = underwriting.ProductName,
                Tenor = Convert.ToInt32(underwriting.Tenor),
                Amount = underwriting.LoanAmount,
                Rate = Convert.ToInt32(underwriting.InterestRate),
                CustomerNhf = underwriting.NHFNumber,
                CustomerName = employeeInfo.FirstName + " " + employeeInfo.LastName,
                RepaymentStatus = 1,
                DisbursementDate = DateTime.Now,
                PmbId = pmbinfo.Id,
                LoanId = underwriting.LoanId,

            };
            await _iUnitOfWork.LoanDisbursement.SaveForm(Disbursement);


            if (employeeInfo != null)
            {
                string message;
                MailParameter mailParameter = new()
                {
                    UserEmail = employeeInfo.EmailAddress,
                    RealName = employeeInfo.LastName + " " + employeeInfo.FirstName,
                    COmpanyMail = "fmbn@gov.ng",
                    UserCompany = "Federal Mortgage Bank of Nigeria"


                };

                var sendMail = EmailHelper.IsLoanReviewApprovalMailSent(mailParameter, out message);

            }



            db.SaveChanges();
            obj.Message = "Loan Disbursed Successfully";
            obj.Tag = 1;
            return obj;
        }





        public async Task<TData<string>> ApproveLoanReview(long Id)
        {
            var db = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            var obj = new TData<string>();
            var loanInfo = await _iUnitOfWork.Underwritings.GetEntity(Id);
            var employeeInfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(loanInfo.NHFNumber));

            var underwriting = db.UnderwritingEntity.Where(i => i.Id == Id).DefaultIfEmpty().FirstOrDefault();
            underwriting.Comments = "Approved";
            //underwriting.Remark = "1";
            underwriting.Reviewed = 1;
            var Employeeinfo = db.EmployeeEntity.Where(i => i.NHFNumber == long.Parse(underwriting.NHFNumber)).DefaultIfEmpty().FirstOrDefault();
            if (Employeeinfo != null)
            {
                string message;
                MailParameter mailParameter = new()
                {
                    UserEmail = Employeeinfo.EmailAddress,
                    RealName = Employeeinfo.LastName + " " + Employeeinfo.FirstName,
                    COmpanyMail = "fmbn@gov.ng",
                    UserCompany = "Federal Mortgage Bank of Nigeria"


                };

                var sendMail = EmailHelper.IsLoanReviewApprovalMailSent(mailParameter, out message);

            }
            db.SaveChanges();
            obj.Message = "Approved Successfully";
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> RejectLoanReview(long Id, string remark)
        {
            var db = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            //var employeeDetails = await _iUnitOfWork.Employees.GetEntity(user.Id);
            var obj = new TData<string>();
            var loanUnderwritingInfo = await _iUnitOfWork.Underwritings.GetEntity(Id);

            var underwriting = db.UnderwritingEntity.Where(i => i.Id == Id).DefaultIfEmpty().FirstOrDefault();
            underwriting.Remark = "Disapproved";
            //underwriting.Remark = "1";
            //underwriting.Reviewed = 2;
            underwriting.Approved = 2;
            var Loan = db.LoanInitiationEntity.Where(i => i.Id == long.Parse(loanUnderwritingInfo.LoanId)).DefaultIfEmpty().FirstOrDefault();
            Loan.Status = "DisApproved";

            var riskAssesment = db.RiskAssessmentProcedureEntity.Where(i => i.NHFNo == underwriting.NHFNumber.ToString()).ToList();
            foreach (var item in riskAssesment)
            {
                db.RiskAssessmentProcedureEntity.Remove(item);

            }

            var checklists = db.ChecklistProcedureEntity.Where(i => i.NHFNo == underwriting.NHFNumber.ToString()).ToList();
            foreach (var checklist in checklists)
            {
                db.ChecklistProcedureEntity.Remove(checklist);

            }
            var Employeeinfo = db.EmployeeEntity.Where(i => i.NHFNumber == long.Parse(underwriting.NHFNumber)).DefaultIfEmpty().FirstOrDefault();
            if (Employeeinfo != null)
            {
                string message;
                MailParameter mailParameter = new()
                {
                    UserEmail = Employeeinfo.EmailAddress,
                    RealName = Employeeinfo.LastName + " " + Employeeinfo.FirstName,
                    COmpanyMail = "fmbn@gov.ng",
                    UserCompany = "Federal Mortgage Bank of Nigeria",
                    Remark = remark


                };

                var sendMail = EmailHelper.IsLoanRejecttionMailSent(mailParameter, out message);

            }

            db.SaveChanges();
            obj.Message = "DisApproved Successfully";
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<string>> RejectLoanUnderwriting(long Id, string remark)
        {
            try
            {
                var db = new ApplicationDbContext();
                var user = await Operator.Instance.Current();
                var obj = new TData<string>();
                //var employeeDetails = await _iUnitOfWork.Employees.GetEntity(user.Id);
                var loanUnderwritingInfo = await _iUnitOfWork.Underwritings.GetEntity(Id);

                var underwriting = db.UnderwritingEntity.Where(i => i.Id == Id).DefaultIfEmpty().FirstOrDefault();
                underwriting.Remark = "DisApproved";
                //underwriting.Remark = "1";
                underwriting.Approved = 2;
                var Loan = db.LoanInitiationEntity.Where(i => i.Id == long.Parse(loanUnderwritingInfo.LoanId)).DefaultIfEmpty().FirstOrDefault();
                Loan.Status = "DisApproved";

                var riskAssesment = db.RiskAssessmentProcedureEntity.Where(i => i.NHFNo == underwriting.NHFNumber.ToString()).DefaultIfEmpty().ToList();
                foreach (var item in riskAssesment)
                {
                    db.RiskAssessmentProcedureEntity.Remove(item);

                }

                var checklists = db.ChecklistProcedureEntity.Where(i => i.NHFNo == underwriting.NHFNumber.ToString()).DefaultIfEmpty().ToList();
                foreach (var checklist in checklists)
                {
                    db.ChecklistProcedureEntity.Remove(checklist);

                }

                var Employeeinfo = db.EmployeeEntity.Where(i => i.NHFNumber == long.Parse(underwriting.NHFNumber)).DefaultIfEmpty().FirstOrDefault();
                if (Employeeinfo != null)
                {
                    string message;
                    MailParameter mailParameter = new()
                    {
                        UserEmail = Employeeinfo.EmailAddress,
                        RealName = Employeeinfo.LastName + " " + Employeeinfo.FirstName,
                        COmpanyMail = "fmbn@gov.ng",
                        UserCompany = "Federal Mortgage Bank of Nigeria",
                        Remark = remark


                    };

                    var sendMail = EmailHelper.IsLoanRejecttionMailSent(mailParameter, out message);

                }
                db.SaveChanges();
                obj.Message = "DisApproved Successfully";
                obj.Tag = 1;
                return obj;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion
    }
}
