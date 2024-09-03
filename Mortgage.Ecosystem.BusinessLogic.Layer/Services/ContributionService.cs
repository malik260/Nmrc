using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class ContributionService : IContributionService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IPaymentIntegrationService paymentIntegrationService;
        public ContributionService(IUnitOfWork iUnitOfWork, IPaymentIntegrationService _ipaymentintegrationservice)
        {
            _iUnitOfWork = iUnitOfWork;
            paymentIntegrationService = _ipaymentintegrationservice;
        }

        #region Retrieve data
        public async Task<TData<List<ContributionEntity>>> GetList(ContributionListParam param,Pagination pagination)
        {
            TData<List<ContributionEntity>> obj = new TData<List<ContributionEntity>>();
            DateTime minDate = DateTime.MinValue;
            if (param.StartTime == minDate || param.EndTime == minDate)
            {
                obj.Total = 0;
                obj.Tag = 1;
                return obj;

            }
            var LoggedUser = await Operator.Instance.Current();
            var loginInfo = await _iUnitOfWork.Employees.GetById(LoggedUser.Employee);
            param.NHFNumber = loginInfo.NHFNumber.ToString();
            var startdate = param.StartTime.AddDays(-1);
            var enddate = param.EndTime.AddDays(1);
            param.StartTime = startdate;
            param.EndTime = enddate;
            obj.Data = await _iUnitOfWork.Contributions.GetPageList(param, pagination);
            foreach (var item in obj.Data)
            {
                if (item.ContributionType == 1)
                {

                    item.month = item?.month?.GetDescriptionByEnum<ContributionMonthsEnum>().ToString();

                }

            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<List<ContributionEntity>>> GetEmployerList(ContributionListParam param, Pagination pagination)
        {
            TData<List<ContributionEntity>> obj = new TData<List<ContributionEntity>>();
            DateTime minDate = DateTime.MinValue;
            if (param.StartTime == minDate || param.EndTime == minDate)
            {
                obj.Total = 0;
                obj.Tag = 1;
                return obj;

            }
            var LoggedUser = await Operator.Instance.Current();
            var loginInfo = await _iUnitOfWork.Companies.GetEntity(LoggedUser.Company);
            param.NHFNumber = loginInfo.EmployerNhfNumber;
            var startdate = param.StartTime.AddDays(-1);
            var enddate = param.EndTime.AddDays(1);
            param.StartTime = startdate;
            param.EndTime = enddate;
            obj.Data = await _iUnitOfWork.Contributions.GetEmployerPageList(param, pagination);
            foreach (var item in obj.Data)
            {
                if (item.ContributionType == 1)
                {

                    item.month = item?.month?.GetDescriptionByEnum<ContributionMonthsEnum>().ToString();

                }

            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<List<ContributionEntity>>> GetList2(ContributionListParam param, Pagination pagination)
        {
            TData<List<ContributionEntity>> obj = new TData<List<ContributionEntity>>();           
            var LoggedUser = await Operator.Instance.Current();
            var loginInfo = await _iUnitOfWork.Employees.GetById(LoggedUser.Employee);
            param.NHFNumber = loginInfo.NHFNumber.ToString();
           
            obj.Data = await _iUnitOfWork.Contributions.GetPageList(param, pagination);
            foreach (var item in obj.Data)
            {
                if (item.ContributionType == 1)
                {

                    item.month = item?.month?.GetDescriptionByEnum<ContributionMonthsEnum>().ToString();

                }

            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<List<ContributionEntity>>> GetPageList(ContributionListParam param, Pagination pagination)
        {
            TData<List<ContributionEntity>> obj = new TData<List<ContributionEntity>>();
            obj.Data = await _iUnitOfWork.Contributions.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeSingleContributionList(ContributionListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ContributionEntity> contributionList = await _iUnitOfWork.Contributions.GetList(param);
            foreach (ContributionEntity contribution in contributionList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = contribution.Id,
                    name = contribution.EmployeeName
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ContributionListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ContributionEntity> contributionList = await _iUnitOfWork.Contributions.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ContributionEntity contribution in contributionList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = contribution.Id,
                    name = contribution.employerName
                });
                List<long> userIdList = userList.Where(t => t.Company == contribution.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<ContributionEntity>> GetEntity(long id)
        {
            TData<ContributionEntity> obj = new TData<ContributionEntity>();
            obj.Data = await _iUnitOfWork.Contributions.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.Contributions.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ContributionEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Contributions.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Contributions.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
        public async Task<TData<EmployeeDetailsVM>> GetCustomerDetails()
        {
            TData<EmployeeDetailsVM> obj = new TData<EmployeeDetailsVM>();
            var user = await Operator.Instance.Current();
            var empInfo = await _iUnitOfWork.Employees.GetById(user.Employee);
            var employeedetails = await _iUnitOfWork.Employees.GetEntityByNhfNumber(empInfo.NHFNumber);
            var employerdetail = await _iUnitOfWork.Companies.GetEntity(employeedetails.Company);

            var custDetails = new EmployeeDetailsVM
            {
                Nhfno = employeedetails.NHFNumber.ToString(),
                EmployerNo = employeedetails.Company.ToString(),
                Name = employeedetails.FirstName + " " + employeedetails.LastName,
                EmployerName = employerdetail.Name,

            };

            obj.Data = custDetails;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<EmployeeDetailsVM>> GetEmployerDetails()
        {
            TData<EmployeeDetailsVM> obj = new TData<EmployeeDetailsVM>();
            var user = await Operator.Instance.Current();
            var empInfo = await _iUnitOfWork.Employees.GetEntity(user.Employee);
            var employerdetail = await _iUnitOfWork.Companies.GetEntity(empInfo.Company);

            var custDetails = new EmployeeDetailsVM
            {
                EmployerNo = employerdetail.Id.ToString(),
                EmployerName = employerdetail.Name,

            };

            obj.Data = custDetails;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<EmployeeEntity>>> GetEmployees(long companyId)
        {
            TData<List<EmployeeEntity>> obj = new TData<List<EmployeeEntity>>();
            EmployeeListParam param = new EmployeeListParam();
            param.Company = companyId;
            obj.Data = await _iUnitOfWork.Employees.GetListByCompany(param);
            obj.Data = obj.Data.Where(i => i.UserType != 1 && i.Status == 1 && i.Company == companyId).ToList();
            foreach (var item in obj.Data)
            {
                item.FullName = item.LastName + " " +item.FirstName;
            }
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }


        private int ObtenerNumeroMes(string NombreMes)
        {

            int NumeroMes;

            switch (NombreMes.ToUpper())
            {

                case ("JANUARY"):
                    NumeroMes = 01;
                    return NumeroMes;

                case ("FEBRUARY"):
                    NumeroMes = 02;
                    return NumeroMes;

                case ("FEBUARY"):
                    NumeroMes = 02;
                    return NumeroMes;

                case ("MARCH"):
                    NumeroMes = 03;
                    return NumeroMes;

                case ("APRIL"):
                    NumeroMes = 04;
                    return NumeroMes;

                case ("MAY"):
                    NumeroMes = 05;
                    return NumeroMes;

                case ("JUNE"):
                    NumeroMes = 06;
                    return NumeroMes;

                case ("JULY"):
                    NumeroMes = 07;
                    return NumeroMes;

                case ("AUGUST"):
                    NumeroMes = 08;
                    return NumeroMes;

                case ("SEPTEMBER"):
                    NumeroMes = 09;
                    return NumeroMes;

                case ("OCTOBER"):
                    NumeroMes = 10;
                    return NumeroMes;

                case ("NOVEMBER"):
                    NumeroMes = 11;
                    return NumeroMes;

                case ("DECEMBER"):
                    NumeroMes = 12;
                    return NumeroMes;

                default:
                    Console.WriteLine("Error");
                    return 00;

            }

        }


        [HttpPost]
        private async Task<ContributionResponseVM> IntegrateEmployeeContribution(CustomerContributionViewwModel Contributions)
        {
            try
            {
                var _client = new HttpClient();
                var serializeObject = JsonConvert.SerializeObject(Contributions);

                var jsonPayload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(Contributions), Encoding.UTF8, ApiResource.ApplicationJson);

                var response = await _client.PostAsync("https://testcorebanking.fmbn.gov.ng:5000/api/v2/Customer/AddCustomerContribution", jsonPayload);


                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ContributionResponseVM>(responseContent);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<TData<RemitaPaymentDetailsEntity>> EmployerSingleContribution(ContributionEntity entity)
        {
            string message = "";
            TData<RemitaPaymentDetailsEntity> obj = new TData<RemitaPaymentDetailsEntity>();
            var loggedUser = await Operator.Instance.Current();
            var info = await _iUnitOfWork.Employees.GetEntity(loggedUser.Employee);
            var employeedetails = await _iUnitOfWork.Employees.GetEntity(long.Parse(entity.employeeNumber));
            //RemitaPaymentDTO PaymentDetails = new RemitaPaymentDTO();
            //PaymentDetails.amount = entity.contributionAmount;
            //PaymentDetails.description = entity.remarks;
            //PaymentDetails.payerEmail = entity.Email;
            //PaymentDetails.payerPhone = entity.phoneNumber;
            //PaymentDetails.payerName = loggedUser.UserName;

            //var Rrrgenerator = await paymentIntegrationService.Generate(PaymentDetails);
            //if (Rrrgenerator.Data == null)
            //{
            //    message = "Failed to Generate RRR";
            //    obj.Message = message;
            //    obj.Tag = 0;
            //    return obj;
            //}

            string rrr = _iUnitOfWork.Employees.GenerateNHFNumber().ToString();

            RemitaPaymentDetailsEntity remitaPaymentDetailsEntity = new RemitaPaymentDetailsEntity();
            //remitaPaymentDetailsEntity.TransactionId = Rrrgenerator.Data.TransactionId;    
            remitaPaymentDetailsEntity.TransactionId = rrr;
            remitaPaymentDetailsEntity.TransactionDate = DateTime.Now;
            remitaPaymentDetailsEntity.Status = 1;
            remitaPaymentDetailsEntity.Rrr = rrr;
            remitaPaymentDetailsEntity.Amount = entity.contributionAmount.ToStr();
            remitaPaymentDetailsEntity.EmployeeNumber = Convert.ToString(employeedetails.NHFNumber.ToStr());
            remitaPaymentDetailsEntity.EmployerNumber = employeedetails.Company.ToStr();

            var monthtouse = ObtenerNumeroMes(entity.month);
            var datetouse = new DateTime(Convert.ToInt32(entity.year), monthtouse, DateTime.DaysInMonth(Convert.ToInt32(entity.year), monthtouse));
            var employerName = await _iUnitOfWork.Companies.GetEntity(employeedetails.Company);

            CustomerContributionViewwModel ContributionInteg = new CustomerContributionViewwModel();
            ContributionInteg.EmployeeNhfNumber = employeedetails.NHFNumber.ToString();
            ContributionInteg.EmployeeName = employeedetails.FirstName + " " + employeedetails.LastName;
            ContributionInteg.AmountContributed = entity.contributionAmount;
            ContributionInteg.ValueDate = DateTime.Now;
            ContributionInteg.PostDate = DateTime.Now;
            ContributionInteg.RRR = rrr;
            ContributionInteg.EmployerNhfNumber = employerName.EmployerNhfNumber;
            ContributionInteg.Month = entity.month;
            ContributionInteg.Year = entity.year;
            ContributionInteg.Narration = entity.remarks;
            var IntegrateContribution = await IntegrateEmployeeContribution(ContributionInteg);
            if (IntegrateContribution.responseCode != 00)
            {
                obj.Data = null;
                obj.Message = "Payment Failed";
                obj.Tag = 0;
                return obj;

            }
            var Employer = await _iUnitOfWork.Companies.GetEntity(long.Parse(entity.employerNumber));

            ContributionEntity nhfcontributionbtch = new ContributionEntity();
            nhfcontributionbtch.NhfNo = Convert.ToString(employeedetails.NHFNumber.ToStr()); ;
            nhfcontributionbtch.EmployeeName = employeedetails.FirstName + " " + employeedetails.LastName + " " + employeedetails.OtherName;
            nhfcontributionbtch.contributionAmount = entity.contributionAmount;
            nhfcontributionbtch.TotalAmount = entity.TotalAmount;
            nhfcontributionbtch.PaymentDate = DateTime.Now;
            nhfcontributionbtch.naration = entity.remarks;
            nhfcontributionbtch.ContributionDate = DateTime.Now;
            nhfcontributionbtch.paymentOption = entity.paymentOption;
            nhfcontributionbtch.month = entity.month;
            nhfcontributionbtch.year = entity.year;
            //nhfcontributionbtch.accountName = entity.accountName;
            nhfcontributionbtch.TransactionId = rrr;
            nhfcontributionbtch.TransactionDate = DateTime.Now;
            nhfcontributionbtch.ContributionType = 2;
            nhfcontributionbtch.Status = "1";
            nhfcontributionbtch.Email = entity.Email;
            nhfcontributionbtch.employerNumber = Employer.EmployerNhfNumber;
            nhfcontributionbtch.employerName = employerName.Name;
            nhfcontributionbtch.phoneNumber = entity.phoneNumber;


            FinanceCounterpartyTransactionEntity CPT = new FinanceCounterpartyTransactionEntity();
            CPT.Ref = employeedetails.NHFNumber.ToStr();
            CPT.Approved = 1;
            CPT.Branch = employeedetails.Branch.ToStr();
            CPT.TransactionType = "70";
            CPT.PostDate = DateTime.Now;
            CPT.TransactionDate = datetouse;
            CPT.Description = entity.naration;
            CPT.CreditAmount = entity.contributionAmount;
            CPT.TransactionId = rrr;
            CPT.DebitAmount = 0;
            CPT.Description = entity.remarks;

            FinanceTransactionEntity FTTCredit = new FinanceTransactionEntity();
            FTTCredit.DebitAmt = 0;
            FTTCredit.Approved = 1;
            FTTCredit.CreditAmt = entity.contributionAmount;
            FTTCredit.DestinationBranch = employeedetails.Branch.ToStr();
            FTTCredit.TransactionDate = DateTime.Now;
            FTTCredit.TransactionType = 70;
            FTTCredit.ValueDate = datetouse;
            FTTCredit.Ref = employeedetails.NHFNumber.ToStr();
            FTTCredit.SourceBranch = employeedetails.Branch.ToStr();
            FTTCredit.Description = entity.remarks;
            FTTCredit.TransactionId = rrr;

            FinanceTransactionEntity FTTDebit = new FinanceTransactionEntity();
            FTTDebit.DebitAmt = 0;
            FTTDebit.Approved = 1;
            FTTDebit.CreditAmt = entity.contributionAmount;
            FTTDebit.DestinationBranch = employeedetails.Branch.ToStr();
            FTTDebit.TransactionDate = DateTime.Now;
            FTTDebit.TransactionType = 70;
            FTTDebit.ValueDate = datetouse;
            FTTDebit.Ref = loggedUser.EmployeeInfo.NHFNumber.ToStr();
            FTTDebit.SourceBranch = employeedetails.Branch.ToStr();
            FTTDebit.TransactionId = rrr;
            FTTDebit.Description = entity.remarks;

            await _iUnitOfWork.Contributions.SaveForm(nhfcontributionbtch);
            await _iUnitOfWork.RemitaPaymentDetails.SaveForm(remitaPaymentDetailsEntity);
            await _iUnitOfWork.FinanceCounterpartyTransactions.SaveForm(CPT);
            await _iUnitOfWork.FinanceTransactions.SaveForm(FTTDebit);
            await _iUnitOfWork.FinanceTransactions.SaveForm(FTTCredit);

            obj.Data = remitaPaymentDetailsEntity;
            obj.Message = "Payment Initiated Successfully";
            obj.Tag = 1;
            return obj;

        }





        public async Task<TData<RemitaPaymentDetailsEntity>> SingleContribution(ContributionEntity entity)
        {
            string message = "";
            TData<RemitaPaymentDetailsEntity> obj = new TData<RemitaPaymentDetailsEntity>();
            var loggedUser = await Operator.Instance.Current();
            var info = await _iUnitOfWork.Employees.GetById(loggedUser.Employee);
            var employeedetails = await _iUnitOfWork.Employees.GetEntityByNhfNumber(info.NHFNumber);
            //RemitaPaymentDTO PaymentDetails = new RemitaPaymentDTO();
            //PaymentDetails.amount = entity.contributionAmount;
            //PaymentDetails.description = entity.remarks;
            //PaymentDetails.payerEmail = entity.Email;
            //PaymentDetails.payerPhone = entity.phoneNumber;
            //PaymentDetails.payerName = loggedUser.UserName;

            //var Rrrgenerator = await paymentIntegrationService.Generate(PaymentDetails);
            //if (Rrrgenerator.Data == null)
            //{
            //    message = "Failed to Generate RRR";
            //    obj.Message = message;
            //    obj.Tag = 0;
            //    return obj;
            //}

            string rrr = _iUnitOfWork.Employees.GenerateNHFNumber().ToString();

            RemitaPaymentDetailsEntity remitaPaymentDetailsEntity = new RemitaPaymentDetailsEntity();
            //remitaPaymentDetailsEntity.TransactionId = Rrrgenerator.Data.TransactionId;    
            remitaPaymentDetailsEntity.TransactionId = rrr;
            remitaPaymentDetailsEntity.TransactionDate = DateTime.Now;
            remitaPaymentDetailsEntity.Status = 1;
            remitaPaymentDetailsEntity.Rrr = rrr;
            remitaPaymentDetailsEntity.Amount = entity.contributionAmount.ToStr();
            remitaPaymentDetailsEntity.EmployeeNumber = Convert.ToString(employeedetails.NHFNumber.ToStr());
            remitaPaymentDetailsEntity.EmployerNumber = employeedetails.Company.ToStr();

            var monthtouse = ObtenerNumeroMes(entity.month);
            var datetouse = new DateTime(Convert.ToInt32(entity.year), monthtouse, DateTime.DaysInMonth(Convert.ToInt32(entity.year), monthtouse));
            var employerName = await _iUnitOfWork.Companies.GetEntity(employeedetails.Company);

            CustomerContributionViewwModel ContributionInteg = new CustomerContributionViewwModel();
            ContributionInteg.EmployeeNhfNumber = employeedetails.NHFNumber.ToString();
            ContributionInteg.EmployeeName = employeedetails.FirstName + " " + employeedetails.LastName;
            ContributionInteg.AmountContributed = entity.contributionAmount;
            ContributionInteg.ValueDate = DateTime.Now;
            ContributionInteg.PostDate = DateTime.Now;
            ContributionInteg.RRR = rrr;
            ContributionInteg.EmployerNhfNumber = employerName.EmployerNhfNumber;
            ContributionInteg.Month = entity.month;
            ContributionInteg.Year = entity.year;
            ContributionInteg.Narration = entity.remarks;
            var IntegrateContribution = await IntegrateEmployeeContribution(ContributionInteg);
            if (IntegrateContribution.responseCode != 00)
            {
                obj.Data = null;
                obj.Message = "Payment Failed";
                obj.Tag = 0;
                return obj;

            }

            ContributionEntity nhfcontributionbtch = new ContributionEntity();
            nhfcontributionbtch.NhfNo = Convert.ToString(employeedetails.NHFNumber.ToStr()); ;
            nhfcontributionbtch.EmployeeName = employeedetails.FirstName + " " + employeedetails.LastName + " " + employeedetails.OtherName;
            nhfcontributionbtch.contributionAmount = entity.contributionAmount;
            nhfcontributionbtch.TotalAmount = entity.TotalAmount;
            nhfcontributionbtch.PaymentDate = DateTime.Now;
            nhfcontributionbtch.naration = entity.remarks;
            nhfcontributionbtch.ContributionDate = DateTime.Now;
            nhfcontributionbtch.paymentOption = entity.paymentOption;
            nhfcontributionbtch.month = entity.month;
            nhfcontributionbtch.year = entity.year;
            //nhfcontributionbtch.accountName = entity.accountName;
            nhfcontributionbtch.TransactionId = rrr;
            nhfcontributionbtch.TransactionDate = DateTime.Now;
            nhfcontributionbtch.ContributionType = 1;
            nhfcontributionbtch.Status = "1";
            nhfcontributionbtch.Email = entity.Email;
            nhfcontributionbtch.employerNumber = entity.employerNumber;
            nhfcontributionbtch.employerName = employerName.Name;
            nhfcontributionbtch.phoneNumber = entity.phoneNumber;


            FinanceCounterpartyTransactionEntity CPT = new FinanceCounterpartyTransactionEntity();
            CPT.Ref = employeedetails.NHFNumber.ToStr();
            CPT.Approved = 1;
            CPT.Branch = employeedetails.Branch.ToStr();
            CPT.TransactionType = "70";
            CPT.PostDate = DateTime.Now;
            CPT.TransactionDate = datetouse;
            CPT.Description = entity.naration;
            CPT.CreditAmount = entity.contributionAmount;
            CPT.TransactionId = rrr;
            CPT.DebitAmount = 0;
            CPT.Description = entity.remarks;

            FinanceTransactionEntity FTTCredit = new FinanceTransactionEntity();
            FTTCredit.DebitAmt = 0;
            FTTCredit.Approved = 1;
            FTTCredit.CreditAmt = entity.contributionAmount;
            FTTCredit.DestinationBranch = employeedetails.Branch.ToStr();
            FTTCredit.TransactionDate = DateTime.Now;
            FTTCredit.TransactionType = 70;
            FTTCredit.ValueDate = datetouse;
            FTTCredit.Ref = employeedetails.NHFNumber.ToStr();
            FTTCredit.SourceBranch = employeedetails.Branch.ToStr();
            FTTCredit.Description = entity.remarks;
            FTTCredit.TransactionId = rrr;

            FinanceTransactionEntity FTTDebit = new FinanceTransactionEntity();
            FTTDebit.DebitAmt = 0;
            FTTDebit.Approved = 1;
            FTTDebit.CreditAmt = entity.contributionAmount;
            FTTDebit.DestinationBranch = employeedetails.Branch.ToStr();
            FTTDebit.TransactionDate = DateTime.Now;
            FTTDebit.TransactionType = 70;
            FTTDebit.ValueDate = datetouse;
            FTTDebit.Ref = loggedUser.EmployeeInfo.NHFNumber.ToStr();
            FTTDebit.SourceBranch = employeedetails.Branch.ToStr();
            FTTDebit.TransactionId = rrr;
            FTTDebit.Description = entity.remarks;

            await _iUnitOfWork.Contributions.SaveForm(nhfcontributionbtch);
            await _iUnitOfWork.RemitaPaymentDetails.SaveForm(remitaPaymentDetailsEntity);
            await _iUnitOfWork.FinanceCounterpartyTransactions.SaveForm(CPT);
            await _iUnitOfWork.FinanceTransactions.SaveForm(FTTDebit);
            await _iUnitOfWork.FinanceTransactions.SaveForm(FTTCredit);

            obj.Data = remitaPaymentDetailsEntity;
            obj.Message = "Payment Initiated Successfully";
            obj.Tag = 1;
            return obj;

        }

        public async Task<TData<BatchContributionResultVM>> BatchContribution(BatchUploadVM entity)
        {
            var context = new ApplicationDbContext();
            TData<BatchContributionResultVM> obj = new TData<BatchContributionResultVM>();
            var Result = new BatchContributionResultVM();
            string MessageError = "";
            List<ErrorList> ErrorMessages = new List<ErrorList>();
            var loggedUser = await Operator.Instance.Current();
            var employeedetails = await _iUnitOfWork.Employees.GetEntity(loggedUser.Employee);
            var companyDetails = await _iUnitOfWork.Companies.GetEntity(employeedetails.Company);

            try
            {


                Stopwatch stopwatch = new Stopwatch();
                IFormFile formFile = entity.ContributionTemplate;
                var query = from employeeQuery in _iUnitOfWork.Employees.GetLists().Result.Select(i => new EmployeeCheckDto
                {
                    NhfNumber = Convert.ToString(i.NHFNumber),
                    EmployerNumber = i.Company.ToStr()
                })
                            select employeeQuery;
                List<NhfEmployerBatchContributionViewModel> UnvalidatedList = new List<NhfEmployerBatchContributionViewModel>();
                // List<EmployeeCheckDto> nhfemployees = query.ToList();
                //var extension = "." + formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
                var defaultFileName = Path.GetFileNameWithoutExtension(formFile.FileName);
                var extension = Path.GetExtension(formFile.FileName);

                string fileName = DateTime.Now.Ticks + extension;

                //var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload/files");


                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                //var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", fileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload/files/" + fileName);


                using (var stream = new FileStream(path, FileMode.Create))
                {
                        await formFile.CopyToAsync(stream);

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {

                        List<NhfEmployerBatchContributionViewModel> EmprList = new List<NhfEmployerBatchContributionViewModel>();
                        List<NhfEmployerBatchContributionViewModel> ValidatedEmployeeList = new List<NhfEmployerBatchContributionViewModel>();
                        var listexcelrecord = new HashSet<string>();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var columnCount = worksheet.Dimension.Columns;
                        if (worksheet == null)
                        {

                            MessageError = $"ExcelSheet Cannot Be Empty";
                            ErrorMessages.Add(new ErrorList { Error = MessageError });
                            Result.ErrorLists = ErrorMessages;
                            obj.Data = Result;
                            obj.Tag = 0;
                            return obj;

                        }

                        var rowCount = worksheet.Dimension.Rows;
                        if (rowCount < 2)
                        {
                            MessageError = $"ExcelSheet Cannot Be Empty";
                            ErrorMessages.Add(new ErrorList { Error = MessageError });
                            Result.ErrorLists = ErrorMessages;
                            obj.Data = Result;
                            obj.Tag = 0;
                            return obj;

                        }
                        Double totalAmount = 0;
                        try
                        {
                            Dictionary<int, NhfEmployerBatchContributionViewModel> employeesData = new Dictionary<int, NhfEmployerBatchContributionViewModel>();
                            HashSet<string> encounteredNhfNumbers = new HashSet<string>();
                            List<string> duplicateNhfNumbers = new List<string>();

                            for (int row = 2; row <= rowCount; row++)
                            {

                                NhfEmployerBatchContributionViewModel nhfemployee = new NhfEmployerBatchContributionViewModel();
                                string NhfNo = Convert.ToString(worksheet.Cells[row, 4].Value);
                                string NhfNoLength = Convert.ToString(worksheet.Cells[row, 4].Value);
                                string Lastname = Convert.ToString(worksheet.Cells[row, 1].Value);
                                string Firstname = Convert.ToString(worksheet.Cells[row, 2].Value);
                                string MiddleName = Convert.ToString(worksheet.Cells[row, 3].Value);
                                string Amount = Convert.ToString(worksheet.Cells[row, 5].Value);
                               
                                if (Lastname == "" && Firstname == "" && MiddleName == "" && NhfNo == "" && Amount == "")
                                    continue;
                                nhfemployee.NhfNo = NhfNo;
                                nhfemployee.Firstname = Firstname;
                                nhfemployee.Lastname = Lastname;
                                nhfemployee.MiddleName = MiddleName;
                                nhfemployee.Amount = Amount;
                                nhfemployee.Contributionmonth = entity.Month;
                                nhfemployee.Contributionyear = entity.Year;
                                nhfemployee.EmailAddress = entity.BatchEmailAddress;
                                nhfemployee.Accountname = entity.accountName;
                                nhfemployee.Paymentoption = entity.paymentOptionBatch;

                                if (encounteredNhfNumbers.Contains(NhfNo))
                                {
                                    duplicateNhfNumbers.Add(NhfNo);
                                    MessageError = $"Duplicate NHF number ({NhfNo}) found at row {row}.";
                                    ErrorMessages.Add(new ErrorList { Error = MessageError });
                                }
                                else
                                {
                                    encounteredNhfNumbers.Add(NhfNo);
                                }

                                //UnvalidatedList.Add(nhfemployee);
                                employeesData.Add(row, nhfemployee);

                            }
                            foreach (var item in employeesData)
                            {
                                MessageError = string.Empty;
                                if (string.IsNullOrEmpty(item.Value.NhfNo))
                                {
                                    MessageError = $"NHF Number is required at row {item.Key} " + " in excel.";
                                    ErrorMessages.Add(new ErrorList { Error = MessageError });
                                }
                                if (string.IsNullOrEmpty(item.Value.Contributionmonth))
                                {
                                    MessageError = $"Contribution Month is required at row {item.Key} " + " in excel.";
                                    ErrorMessages.Add(new ErrorList { Error = MessageError });
                                }
                                if (string.IsNullOrEmpty(item.Value.Firstname))
                                {
                                    MessageError = $"Firstname is required at row {item.Key} " + " in excel.";
                                    ErrorMessages.Add(new ErrorList { Error = MessageError });
                                }
                                
                                if (string.IsNullOrEmpty(item.Value.Lastname))
                                {
                                    MessageError = $" Lastname is required at row {item.Key}" + " in excel. ";
                                    ErrorMessages.Add(new ErrorList { Error = MessageError });
                                }

                                if (string.IsNullOrEmpty(item.Value.Amount))
                                {
                                    MessageError = $"Amount is required at row {item.Key} " + " in excel.";
                                    ErrorMessages.Add(new ErrorList { Error = MessageError });
                                }

                                bool containsAlphabet = Regex.IsMatch(item.Value.Amount, "[a-zA-Z]");
                                if (containsAlphabet)
                                {
                                    MessageError = $"Amount at row {item.Key} " + " in excel is not valid.";
                                    ErrorMessages.Add(new ErrorList { Error = MessageError });
                                }

                                var exist = context.EmployeeEntity.Where(i => i.NHFNumber == long.Parse(item.Value.NhfNo) && i.Company == loggedUser.Company).DefaultIfEmpty().FirstOrDefault() ;
                                if (exist == null)
                                {
                                    MessageError = $"This NHF Number: " + item.Value.NhfNo + " does not exist or belong to this employer";
                                    ErrorMessages.Add(new ErrorList { Error = MessageError });

                                }

                                if (exist != null)
                                {
                                    if (exist.FirstName.Trim().ToLower() != item.Value.Firstname.Trim().ToLower() || exist.LastName.Trim().ToLower() != item.Value.Lastname.Trim().ToLower())
                                    {
                                        MessageError = $"First or Last name at row {item.Key} " + " in excel is not associated with the NHF No";
                                        ErrorMessages.Add(new ErrorList { Error = MessageError });

                                    }

                                }


                                

                                totalAmount += double.Parse(item.Value.Amount);

                                if (string.IsNullOrEmpty(MessageError))
                                {
                                    ValidatedEmployeeList.Add(item.Value);
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }
                        if (ErrorMessages.Count > 0)
                        {
                            //return Json(new { success = false, message = ErrorMessages });

                            ErrorMessages.Add(new ErrorList { Error = MessageError });
                            Result.ErrorLists = ErrorMessages.DistinctBy(i=> i.Error).ToList();
                            obj.Data = Result;
                            obj.Tag = 0;
                            return obj;
                        }

                        //RemitaPaymentDTO PaymentDetails = new RemitaPaymentDTO();
                        //PaymentDetails.amount = Convert.ToDecimal(totalAmount);
                        //PaymentDetails.description = entity.narration;
                        //PaymentDetails.payerEmail = entity.BatchEmailAddress;
                        //PaymentDetails.payerPhone = entity.phoneNumber;
                        //PaymentDetails.payerName = entity.accountName;


                        //var Rrrgenerator = await paymentIntegrationService.Generate(PaymentDetails);
                        //if (Rrrgenerator.Data == null)
                        //{
                        //    string message = "Failed to Generate RRR";
                        //    obj.Message = message;
                        //    obj.Tag = 0;
                        //    return obj;
                        //}

                        string rrr = _iUnitOfWork.Employees.GenerateNHFNumber().ToString();
                        RemitaPaymentDetailsEntity remitaPaymentDetailsEntity = new RemitaPaymentDetailsEntity();
                        remitaPaymentDetailsEntity.TransactionId = rrr;
                        remitaPaymentDetailsEntity.TransactionDate = DateTime.Now;
                        remitaPaymentDetailsEntity.Status = 1;
                        remitaPaymentDetailsEntity.Rrr = rrr;
                        remitaPaymentDetailsEntity.Amount = totalAmount.ToString();
                        remitaPaymentDetailsEntity.EmployeeNumber = "";
                        remitaPaymentDetailsEntity.EmployerNumber = companyDetails.EmployerNhfNumber;


                        try
                        {

                            List<ContributionEntity> contributions = new List<ContributionEntity>();
                            List<FinanceCounterpartyTransactionEntity> financeCounterpartyTransactions = new List<FinanceCounterpartyTransactionEntity>();
                            List<FinanceTransactionEntity> financeTransactions = new List<FinanceTransactionEntity>();
                            foreach (var item in ValidatedEmployeeList)
                            {
                                CustomerContributionViewwModel ContributionInteg = new CustomerContributionViewwModel();
                                ContributionInteg.EmployeeNhfNumber = employeedetails.NHFNumber.ToString();
                                ContributionInteg.EmployeeName = employeedetails.FirstName + " " + employeedetails.LastName;
                                ContributionInteg.AmountContributed = Convert.ToDecimal(item.Amount);
                                ContributionInteg.ValueDate = DateTime.Now;
                                ContributionInteg.PostDate = DateTime.Now;
                                ContributionInteg.RRR = rrr;
                                ContributionInteg.EmployerNhfNumber = employeedetails.EmployerNhfNumber;
                                ContributionInteg.Month = item.Contributionmonth;
                                ContributionInteg.Year = item.Contributionyear;
                                ContributionInteg.Narration = entity.narration;
                                var IntegrateContribution = await IntegrateEmployeeContribution(ContributionInteg);


                                ContributionEntity nhfcontributionbtch = new ContributionEntity();
                                nhfcontributionbtch.NhfNo = item.NhfNo;
                                nhfcontributionbtch.EmployeeName = item.Lastname + " " + item.Firstname + " " + item.MiddleName;
                                nhfcontributionbtch.contributionAmount = Convert.ToDecimal(item.Amount);
                                nhfcontributionbtch.TotalAmount = Convert.ToDecimal(totalAmount);
                                nhfcontributionbtch.PaymentDate = DateTime.Now;
                                nhfcontributionbtch.naration = entity.narration;
                                nhfcontributionbtch.ContributionDate = DateTime.Now;
                                nhfcontributionbtch.paymentOption = item.Paymentoption;
                                nhfcontributionbtch.month = entity.Month;
                                nhfcontributionbtch.year = entity.Year;
                                nhfcontributionbtch.accountName = item.Accountname;
                                nhfcontributionbtch.Email = item.EmailAddress;
                                nhfcontributionbtch.Status = "1";
                                nhfcontributionbtch.TransactionId = rrr;
                                nhfcontributionbtch.TransactionDate = DateTime.Now;
                                nhfcontributionbtch.ContributionType = 2;
                                nhfcontributionbtch.employerNumber = companyDetails.EmployerNhfNumber;
                                nhfcontributionbtch.employerName = companyDetails.Name;
                                nhfcontributionbtch.phoneNumber = companyDetails.MobileNumber;

                                contributions.Add(nhfcontributionbtch);

                                var cpttable = new FinanceCounterpartyTransactionEntity();
                                {

                                    cpttable.TransactionDate = DateTime.Now;
                                    cpttable.Ref = item.NhfNo;
                                    cpttable.CreditAmount = Convert.ToDecimal(item.Amount);
                                    cpttable.CustCode = item.EmployeeNumber;
                                    cpttable.PostDate = DateTime.Now;
                                    cpttable.Description = item.Narration;
                                    cpttable.Approved = 1;
                                    cpttable.DebitAmount = 0;
                                    cpttable.TransactionId = rrr;
                                    cpttable.TransactionType = "70";

                                    cpttable.Show = 1;

                                    cpttable.Coy = companyDetails.EmployerNhfNumber;




                                };
                                financeCounterpartyTransactions.Add(cpttable);

                            }


                            var ftTableCredit = new FinanceTransactionEntity()
                            {

                                TransactionDate = DateTime.Now,
                                ValueDate = DateTime.Now,
                                Approved =  1,
                                ApprovedBy = "SYS",
                                ApplicationId = "FBMN APPLICATION",
                                CreditAmt = Convert.ToDecimal(totalAmount),
                                DebitAmt = 0,
                                Isreversed = 0,
                                PostedBy = "IBANK",
                                Saved = 1,
                                Deleted = 0,
                                PostingTime = Convert.ToString(DateTime.Now),
                                Ref = rrr,
                                TransactionType = 70

                            };

                            financeTransactions.Add(ftTableCredit);

                            var ftTableDebit = new FinanceTransactionEntity()
                            {

                                TransactionDate = DateTime.Now,
                                Ref = rrr,
                                ValueDate = DateTime.Now,
                                Description = entity.narration,
                                Saved = 1,
                                //DestinationBranch = employernumber.Branchcode,
                                Isreversed = 0,
                                Deleted = 0,
                                Approved = 1,
                                ApprovedBy = "SYS",
                                CreditAmt = 0,
                                //LegType = glAccountId.Ledgerdr,
                                PostedBy = "IBANK",
                                PostingTime = Convert.ToString(DateTime.Now),
                                DebitAmt = Convert.ToDecimal(totalAmount),
                                TransactionType = 70

                            };

                            if (ErrorMessages.Count > 0)
                            {
                                string message = "Failed to Generate RRR";
                                obj.Message = message;
                                obj.Tag = 0;
                                return obj;
                            }
                            await _iUnitOfWork.RemitaPaymentDetails.SaveForm(remitaPaymentDetailsEntity);
                            await _iUnitOfWork.Contributions.SaveForms(contributions);
                            await _iUnitOfWork.FinanceCounterpartyTransactions.SaveForms(financeCounterpartyTransactions);
                            await _iUnitOfWork.FinanceTransactions.SaveForms(financeTransactions);
                            Result.Amount = remitaPaymentDetailsEntity.Amount;
                            Result.TransactionId = remitaPaymentDetailsEntity.TransactionId;
                            Result.Rrr = remitaPaymentDetailsEntity.Rrr;
                            Result.TransactionDate = remitaPaymentDetailsEntity.TransactionDate;
                            obj.Data = Result;
                            obj.Tag = 1;
                            return obj;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }




                    }


                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TData<BacklogSingleContributionResultVM>> BacklogSingleContribution(BacklogUploadVM entity)
        {
            TData<BacklogSingleContributionResultVM> obj = new TData<BacklogSingleContributionResultVM>();
            var Result = new BacklogSingleContributionResultVM();
            string MessageError = "";
            List<ErrorListBacklog> ErrorMessages = new List<ErrorListBacklog>();
            var loggedUser = await Operator.Instance.Current();
            var employeedetails = await _iUnitOfWork.Employees.GetEntity(loggedUser.Employee);



            try
            {


                Stopwatch stopwatch = new Stopwatch();
                IFormFile formFile = entity.BacklogTemplate;
                var query = from employeeQuery in _iUnitOfWork.Employees.GetLists().Result.Select(i => new EmployeeCheckDto
                {
                    NhfNumber = Convert.ToString(i.NHFNumber),
                    EmployerNumber = i.Company.ToStr()
                })
                            select employeeQuery;
                List<NhfEmployerBatchContributionViewModel> UnvalidatedList = new List<NhfEmployerBatchContributionViewModel>();
                // List<EmployeeCheckDto> nhfemployees = query.ToList();
                //var extension = "." + formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
                var defaultFileName = Path.GetFileNameWithoutExtension(formFile.FileName);
                var extension = Path.GetExtension(formFile.FileName);

                string fileName = DateTime.Now.Ticks + extension;
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload/files");


                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                //var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", fileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload/files/" + fileName);


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {

                        List<NhfEmployerBatchContributionViewModel> EmprList = new List<NhfEmployerBatchContributionViewModel>();
                        List<NhfEmployerBatchContributionViewModel> ValidatedEmployeeList = new List<NhfEmployerBatchContributionViewModel>();
                        var listexcelrecord = new HashSet<string>();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var columnCount = worksheet.Dimension.Columns;
                        if (worksheet == null)
                        {

                            MessageError = $"ExcelSheet Cannot Be Empty";
                            ErrorMessages.Add(new ErrorListBacklog { Error = MessageError });
                            Result.ErrorLists = ErrorMessages;
                            obj.Data = Result;
                            obj.Tag = 0;
                            return obj;





                        }

                        var rowCount = worksheet.Dimension.Rows;
                        if (rowCount < 2)
                        {
                            MessageError = $"ExcelSheet Cannot Be Empty";
                            ErrorMessages.Add(new ErrorListBacklog { Error = MessageError });
                            Result.ErrorLists = ErrorMessages;
                            obj.Data = Result;
                            obj.Tag = 0;
                            return obj;

                        }
                        Double totalAmount = 0;
                        try
                        {
                            Dictionary<int, NhfEmployerBatchContributionViewModel> employeesData = new Dictionary<int, NhfEmployerBatchContributionViewModel>();
                            for (int row = 2; row <= rowCount; row++)
                            {

                                NhfEmployerBatchContributionViewModel nhfemployee = new NhfEmployerBatchContributionViewModel();
                                string Amount = Convert.ToString(worksheet.Cells[row, 1].Value);
                                string ContributionMonth = Convert.ToString(worksheet.Cells[row, 2].Value);
                                string ContributionYear = Convert.ToString(worksheet.Cells[row, 3].Value);


                                if (Amount == "" && ContributionYear == "" && ContributionMonth == "")
                                    continue;
                                nhfemployee.Amount = Amount;
                                nhfemployee.Contributionmonth = ContributionMonth;
                                nhfemployee.Contributionyear = ContributionYear;
                                nhfemployee.Accountname = entity.AccountName;
                                nhfemployee.Paymentoption = entity.BacklogPaymentOption;
                                nhfemployee.EmailAddress = entity.EmailAddress;
                               
                                employeesData.Add(row, nhfemployee);
                            }
                            foreach (var item in employeesData)
                            {
                                MessageError = string.Empty;

                                if (string.IsNullOrEmpty(item.Value.Contributionmonth))
                                {
                                    MessageError = $"Contribution Month is required at row {item.Key} " + " in excel.";
                                    ErrorMessages.Add(new ErrorListBacklog { Error = MessageError });
                                }


                                if (string.IsNullOrEmpty(item.Value.Contributionyear))
                                {
                                    MessageError = $"Contribution Year is required at row {item.Key}" + " in excel.";
                                    ErrorMessages.Add(new ErrorListBacklog { Error = MessageError });
                                }


                                if (string.IsNullOrEmpty(item.Value.Amount))
                                {
                                    MessageError = $"Amount is required at row {item.Key} " + " in excel.";
                                    ErrorMessages.Add(new ErrorListBacklog { Error = MessageError });
                                }

                                totalAmount += double.Parse(item.Value.Amount);

                                if (string.IsNullOrEmpty(MessageError))
                                {
                                    ValidatedEmployeeList.Add(item.Value);
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }
                        if (ErrorMessages.Count > 0)
                        {

                            ErrorMessages.Add(new ErrorListBacklog { Error = MessageError });
                            Result.ErrorLists = ErrorMessages;
                            obj.Data = Result;
                            obj.Tag = 0;
                            return obj;
                        }

                        RemitaPaymentDTO PaymentDetails = new RemitaPaymentDTO();
                        PaymentDetails.amount = Convert.ToDecimal(totalAmount);
                        PaymentDetails.description = entity.BacklogNarration;
                        PaymentDetails.payerEmail = entity.EmailAddress;
                        PaymentDetails.payerPhone = entity.PhoneNumber;
                        PaymentDetails.payerName = entity.AccountName;

                        //var Rrrgenerator = await paymentIntegrationService.Generate(PaymentDetails);
                        Random random = new Random();
                        long tenDigitNumber = random.Next(1000000000, int.MaxValue); 

                        string Rrrgenerator = _iUnitOfWork.Employees.GenerateNHFNumber().ToString();
                        string Tid = tenDigitNumber.ToString();
                        if (Rrrgenerator == null)
                        {
                            string message = "Failed to Generate RRR";
                            obj.Message = message;
                            obj.Tag = 0;
                            return obj;
                        }
                        RemitaPaymentDetailsEntity remitaPaymentDetailsEntity = new RemitaPaymentDetailsEntity();
                        remitaPaymentDetailsEntity.TransactionId = Rrrgenerator;
                        remitaPaymentDetailsEntity.TransactionDate = DateTime.Now;
                        remitaPaymentDetailsEntity.Status = 1;
                        remitaPaymentDetailsEntity.Rrr = Rrrgenerator;
                        remitaPaymentDetailsEntity.Amount = PaymentDetails.amount.ToStr();
                        remitaPaymentDetailsEntity.EmployeeNumber = "";
                        remitaPaymentDetailsEntity.EmployerNumber = query.FirstOrDefault().EmployerNumber;


                        try
                        {

                            List<ContributionEntity> contributions = new List<ContributionEntity>();
                            List<FinanceCounterpartyTransactionEntity> financeCounterpartyTransactions = new List<FinanceCounterpartyTransactionEntity>();
                            List<FinanceTransactionEntity> financeTransactions = new List<FinanceTransactionEntity>();
                            foreach (var item in ValidatedEmployeeList)
                            {

                                CustomerContributionViewwModel ContributionInteg = new CustomerContributionViewwModel();
                                ContributionInteg.EmployeeNhfNumber = employeedetails.NHFNumber.ToString();
                                ContributionInteg.EmployeeName = employeedetails.FirstName + " " + employeedetails.LastName;
                                ContributionInteg.AmountContributed = Convert.ToDecimal(item.Amount);
                                ContributionInteg.ValueDate = DateTime.Now;
                                ContributionInteg.PostDate = DateTime.Now;
                                ContributionInteg.RRR = Rrrgenerator;
                                ContributionInteg.EmployerNhfNumber = employeedetails.EmployerNhfNumber;
                                ContributionInteg.Month = item.Contributionmonth;
                                ContributionInteg.Year = item.Contributionyear;
                                ContributionInteg.Narration = entity.BacklogNarration;
                                var IntegrateContribution = await IntegrateEmployeeContribution(ContributionInteg);

                                ContributionEntity nhfcontributionbtch = new ContributionEntity();
                                nhfcontributionbtch.NhfNo = employeedetails.NHFNumber.ToString();
                                nhfcontributionbtch.EmployeeName = employeedetails.FirstName + " " + employeedetails.LastName + " " + employeedetails.OtherName;
                                nhfcontributionbtch.contributionAmount = Convert.ToDecimal(item.Amount);
                                nhfcontributionbtch.TotalAmount = Convert.ToDecimal(totalAmount);
                                nhfcontributionbtch.PaymentDate = DateTime.Now;
                                nhfcontributionbtch.naration = entity.BacklogNarration;
                                nhfcontributionbtch.ContributionDate = DateTime.Now;
                                nhfcontributionbtch.paymentOption = item.Paymentoption;
                                nhfcontributionbtch.month = item.Contributionmonth;
                                nhfcontributionbtch.year = item.Contributionyear;
                                nhfcontributionbtch.accountName = item.Accountname;
                                nhfcontributionbtch.Email = item.EmailAddress;
                                nhfcontributionbtch.TransactionId = remitaPaymentDetailsEntity.Rrr;
                                nhfcontributionbtch.TransactionDate = DateTime.Now;
                                nhfcontributionbtch.ContributionType = 2;
                                nhfcontributionbtch.Status = "1";


                                contributions.Add(nhfcontributionbtch);

                                var cpttable = new FinanceCounterpartyTransactionEntity();
                                {

                                    cpttable.TransactionDate = DateTime.Now;
                                    cpttable.Ref = employeedetails.NHFNumber.ToString();
                                    cpttable.CreditAmount = Convert.ToDecimal(item.Amount);
                                    cpttable.CustCode = item.EmployeeNumber;
                                    cpttable.PostDate = DateTime.Now;
                                    cpttable.Description = entity.BacklogNarration;
                                    cpttable.Approved = 1;
                                    cpttable.DebitAmount = 0;
                                    cpttable.TransactionId = remitaPaymentDetailsEntity.Rrr;
                                    cpttable.TransactionType = "70";
                                    cpttable.Show = 1;
                                    cpttable.Coy = employeedetails.EmployerNhfNumber;
                                    cpttable.BatchRef = Rrrgenerator;



                                };
                                financeCounterpartyTransactions.Add(cpttable);

                            }


                            var ftTableCredit = new FinanceTransactionEntity()
                            {

                                TransactionDate = DateTime.Now,
                                ValueDate = DateTime.Now,
                                Approved = 1,
                                ApprovedBy = "SYS",
                                ApplicationId = "FBMN APPLICATION",
                                CreditAmt = Convert.ToDecimal(totalAmount),
                                DebitAmt = 0,
                                Isreversed = 0,
                                PostedBy = "IBANK",
                                Saved = 1,
                                Deleted = 0,
                               Description = entity.BacklogNarration,
                                PostingTime = Convert.ToString(DateTime.Now),
                                Ref = remitaPaymentDetailsEntity.Rrr,
                                TransactionType = 70

                            };

                            financeTransactions.Add(ftTableCredit);

                            var ftTableDebit = new FinanceTransactionEntity()
                            {

                                TransactionDate = DateTime.Now,
                                Ref = remitaPaymentDetailsEntity.Rrr,
                                ValueDate = DateTime.Now,
                                Saved = 1,
                                Isreversed = 0,
                                Deleted = 0,
                                Approved = 1,
                                ApprovedBy = "SYS",
                                CreditAmt = 0,
                                PostedBy = "IBANK",
                                PostingTime = Convert.ToString(DateTime.Now),
                                DebitAmt = Convert.ToDecimal(totalAmount),
                                TransactionType = 70,
                                Description = entity.BacklogNarration,


                            };

                            if (ErrorMessages.Count > 0)
                            {
                                string message = "Failed to Generate RRR";
                                obj.Message = message;
                                obj.Tag = 0;
                                return obj;
                            }
                            await _iUnitOfWork.RemitaPaymentDetails.SaveForm(remitaPaymentDetailsEntity);
                            await _iUnitOfWork.Contributions.SaveForms(contributions);
                            await _iUnitOfWork.FinanceCounterpartyTransactions.SaveForms(financeCounterpartyTransactions);
                            await _iUnitOfWork.FinanceTransactions.SaveForms(financeTransactions);

                            Result.Amount = remitaPaymentDetailsEntity.Amount;
                            Result.TransactionId = remitaPaymentDetailsEntity.TransactionId;
                            Result.Rrr = remitaPaymentDetailsEntity.Rrr;
                            Result.TransactionDate = remitaPaymentDetailsEntity.TransactionDate;
                            obj.Data = Result;
                            obj.Tag = 1;
                            return obj;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}


