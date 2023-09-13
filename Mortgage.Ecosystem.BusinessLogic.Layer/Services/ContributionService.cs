using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
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
        public async Task<TData<List<ContributionEntity>>> GetList(ContributionListParam param)
        {
            TData<List<ContributionEntity>> obj = new TData<List<ContributionEntity>>();
            obj.Data = await _iUnitOfWork.Contributions.GetList(param);
            obj.Total = obj.Data.Count;
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
            var employeedetails = await _iUnitOfWork.Employees.GetEntityByNhfNumber(user.EmployeeInfo.NHFNumber);


            var custDetails = new EmployeeDetailsVM
            {
                Nhfno = employeedetails.NHFNumber.ToString(),
                EmployerNo = employeedetails.Company.ToString(),
                Name = employeedetails.FirstName + " " + employeedetails.LastName,
                EmployerName = employeedetails.CompanyName,

            };

            obj.Data = custDetails;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<RemitaPaymentDetailsEntity>> SingleContribution(ContributionEntity entity)
        {
            string message = "";
            TData<RemitaPaymentDetailsEntity> obj = new TData<RemitaPaymentDetailsEntity>();
            var loggedUser = await Operator.Instance.Current();
            var employeedetails = await _iUnitOfWork.Employees.GetEntityByNhfNumber(loggedUser.EmployeeInfo.NHFNumber);
            RemitaPaymentDTO PaymentDetails = new RemitaPaymentDTO();
            PaymentDetails.amount = entity.contributionAmount;
            PaymentDetails.description = entity.remarks;
            PaymentDetails.payerEmail = entity.Email;
            PaymentDetails.payerPhone = entity.phoneNumber;
            PaymentDetails.payerName = loggedUser.UserName;

            var Rrrgenerator = await paymentIntegrationService.Generate(PaymentDetails);
            if (Rrrgenerator.Data == null)
            {
                message = "Failed to Generate RRR";
                obj.Message = message;
                obj.Tag = 0;
                return obj;
            }
            RemitaPaymentDetailsEntity remitaPaymentDetailsEntity = new RemitaPaymentDetailsEntity();
            remitaPaymentDetailsEntity.TransactionId = Rrrgenerator.Data.TransactionId;
            remitaPaymentDetailsEntity.TransactionDate = DateTime.Now;
            remitaPaymentDetailsEntity.Status = 1;
            remitaPaymentDetailsEntity.Rrr = Rrrgenerator.Data.RRR;
            remitaPaymentDetailsEntity.Amount = PaymentDetails.amount.ToStr();
            remitaPaymentDetailsEntity.EmployeeNumber = Convert.ToString(loggedUser.EmployeeInfo.NHFNumber.ToStr());
            remitaPaymentDetailsEntity.EmployerNumber = employeedetails.Company.ToStr();

            ContributionEntity nhfcontributionbtch = new ContributionEntity(); 
            nhfcontributionbtch.NhfNo = Convert.ToString(loggedUser.EmployeeInfo.NHFNumber.ToStr()); ;
            nhfcontributionbtch.EmployeeName = employeedetails.FirstName +" "+ employeedetails.LastName+" "+ employeedetails.OtherName;
            nhfcontributionbtch.contributionAmount = entity.contributionAmount;
            nhfcontributionbtch.TotalAmount = entity.TotalAmount;
            nhfcontributionbtch.PaymentDate = DateTime.Now;
            nhfcontributionbtch.naration = entity.naration;
            nhfcontributionbtch.ContributionDate = DateTime.Now;
            nhfcontributionbtch.paymentOption = entity.paymentOption;
            nhfcontributionbtch.month = entity.month;
            nhfcontributionbtch.year = entity.year;
            nhfcontributionbtch.accountName = entity.accountName;
            nhfcontributionbtch.TransactionId = remitaPaymentDetailsEntity.Rrr;
            nhfcontributionbtch.TransactionDate = DateTime.Now;
            nhfcontributionbtch.ContributionType = 1;

            FinanceCounterpartyTransactionEntity CPT = new FinanceCounterpartyTransactionEntity();
            CPT.Ref = loggedUser.EmployeeInfo.NHFNumber.ToStr();
            CPT.Approved = 1;
            CPT.Branch = employeedetails.Branch.ToStr();
            CPT.TransactionType = "70";
            CPT.PostDate = DateTime.Now;
            CPT.TransactionDate = DateTime.Now;
            CPT.Description = entity.naration;
            CPT.CreditAmount = entity.contributionAmount;
            CPT.TransactionId = remitaPaymentDetailsEntity.Rrr;
            CPT.DebitAmount = 0;


            FinanceTransactionEntity FTTCredit = new FinanceTransactionEntity();
            FTTCredit.DebitAmt = 0;
            FTTCredit.Approved = 1;
            FTTCredit.CreditAmt = entity.contributionAmount;
            FTTCredit.DestinationBranch = employeedetails.Branch.ToStr();
            FTTCredit.TransactionDate = DateTime.Now;
            FTTCredit.TransactionType = 70;
            FTTCredit.ValueDate = DateTime.Now;
            FTTCredit.Ref = loggedUser.EmployeeInfo.NHFNumber.ToStr();
            FTTCredit.SourceBranch = employeedetails.Branch.ToStr();
            FTTCredit.TransactionId = remitaPaymentDetailsEntity.Rrr;

            FinanceTransactionEntity FTTDebit = new FinanceTransactionEntity();
            FTTDebit.DebitAmt = 0;
            FTTDebit.Approved = 1;
            FTTDebit.CreditAmt = entity.contributionAmount;
            FTTDebit.DestinationBranch = employeedetails.Branch.ToStr();
            FTTDebit.TransactionDate = DateTime.Now;
            FTTDebit.TransactionType = 70;
            FTTDebit.ValueDate = DateTime.Now;
            FTTDebit.Ref = loggedUser.EmployeeInfo.NHFNumber.ToStr();
            FTTDebit.SourceBranch = employeedetails.Branch.ToStr();
            FTTDebit.TransactionId = remitaPaymentDetailsEntity.Rrr;

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
            TData<BatchContributionResultVM> obj = new TData<BatchContributionResultVM>();
            var Result = new BatchContributionResultVM();
            string MessageError = "";
            List<ErrorList> ErrorMessages = new List<ErrorList>();
            var loggedUser = await Operator.Instance.Current();
            var employeedetails = await _iUnitOfWork.Employees.GetEntityByNhfNumber(loggedUser.EmployeeInfo.NHFNumber);
            //var employernumber = _context.Nhfemployer.Where(e => e.Emailaddress == User.Identity.Name).FirstOrDefault();
            //string connectionString = "TNS_ADMIN=C:\\\\\\\\instantclient_19_13\\\\\\\\network\\\\\\\\admin;USER ID=FINTRAKBANKING;PASSWORD=Fintrak1;DATA SOURCE=FCB;PERSIST SECURITY INFO=True";
            // OracleHelper oracleHelper = new OracleHelper(connectionString);



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
                            for (int row = 2; row <= rowCount; row++)
                            {

                                NhfEmployerBatchContributionViewModel nhfemployee = new NhfEmployerBatchContributionViewModel();
                                string NhfNo = Convert.ToString(worksheet.Cells[row, 4].Value);
                                string NhfNoLength = Convert.ToString(worksheet.Cells[row, 4].Value);
                                string Lastname = Convert.ToString(worksheet.Cells[row, 1].Value);
                                string Firstname = Convert.ToString(worksheet.Cells[row, 2].Value);
                                string MiddleName = Convert.ToString(worksheet.Cells[row, 3].Value);
                                string Amount = Convert.ToString(worksheet.Cells[row, 5].Value);
                                string ContributionYear = Convert.ToString(worksheet.Cells[row, 8].Value);
                                string ContributionMonth = Convert.ToString(worksheet.Cells[row, 7].Value);
                                string StaffNo = Convert.ToString(worksheet.Cells[row, 6].Value);

                                if (Lastname == "" && Firstname == "" && MiddleName == "" && NhfNo == "" && Amount == "" && ContributionYear == "" && ContributionMonth == "")
                                    continue;
                                nhfemployee.NhfNo = NhfNo;
                                nhfemployee.Firstname = Firstname;
                                nhfemployee.Lastname = Lastname;
                                nhfemployee.MiddleName = MiddleName;
                                nhfemployee.Amount = Amount;
                                nhfemployee.Contributionmonth = ContributionMonth;
                                nhfemployee.Contributionyear = ContributionYear;

                                nhfemployee.Accountname = entity.accountName;
                                nhfemployee.Paymentoption = entity.paymentOptionBatch;
                                ;
                                //UnvalidatedList.Add(nhfemployee);
                                employeesData.Add(row, nhfemployee);
                            }
                            foreach (var item in employeesData)
                            {
                                MessageError = string.Empty;
                                //VALIDATIONS for required fields
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
                                if (string.IsNullOrEmpty(item.Value.Contributionyear))
                                {
                                    MessageError = $"Contribution Year is required at row {item.Key}" + " in excel.";
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
                                //string[] addrecord = { NhfNo };

                                //var duplicate = addrecord.Where(item => !listexcelrecord.Add(item)).Distinct().ToList();
                                if (ValidatedEmployeeList.ToString().Contains(item.Value.NhfNo))
                                {
                                    MessageError = $"A record NHF number ({item.Value.NhfNo}) already exists at row {item.Key} ";
                                    ErrorMessages.Add(new ErrorList { Error = MessageError });
                                }
                                var exist = query.Where(i => i.NhfNumber.Contains(item.Value.NhfNo) && i.EmployerNumber.Contains(item.Value.EmployerNo));
                                if (exist == null)
                                {
                                    MessageError = $"This NHF Number: " + item.Value.NhfNo + " does not exist or belong to this employer";
                                    ErrorMessages.Add(new ErrorList { Error = MessageError });

                                }
                                var exists = query.Where(i => i.NhfNumber.Contains(item.Value.NhfNo));
                                if (exists == null)
                                {
                                    MessageError = $"This NHF Number: " + item.Value.NhfNo + " does not exist";
                                    ErrorMessages.Add(new ErrorList { Error = MessageError });

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
                            Result.ErrorLists = ErrorMessages;
                            obj.Data = Result;
                            obj.Tag = 0;
                            return obj;
                        }

                        RemitaPaymentDTO PaymentDetails = new RemitaPaymentDTO();
                        PaymentDetails.amount = Convert.ToDecimal(totalAmount);
                        PaymentDetails.description = entity.narration;
                        PaymentDetails.payerEmail = entity.emailAddress;
                        PaymentDetails.payerPhone = entity.phoneNumber;
                        PaymentDetails.payerName = entity.accountName;


                        var Rrrgenerator = await paymentIntegrationService.Generate(PaymentDetails);
                        if (Rrrgenerator.Data == null)
                        {
                            string message = "Failed to Generate RRR";
                            obj.Message = message;
                            obj.Tag = 0;
                            return obj;
                        }
                        RemitaPaymentDetailsEntity remitaPaymentDetailsEntity = new RemitaPaymentDetailsEntity();
                        remitaPaymentDetailsEntity.TransactionId = Rrrgenerator.Data.TransactionId;
                        remitaPaymentDetailsEntity.TransactionDate = DateTime.Now;
                        remitaPaymentDetailsEntity.Status = 1;
                        remitaPaymentDetailsEntity.Rrr = Rrrgenerator.Data.RRR;
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
                                ContributionEntity nhfcontributionbtch = new ContributionEntity();
                                nhfcontributionbtch.NhfNo = item.NhfNo;
                                nhfcontributionbtch.EmployeeName = item.Lastname +" " + item.Firstname +" "+ item.MiddleName;
                                nhfcontributionbtch.contributionAmount = Convert.ToDecimal(item.Amount);
                                nhfcontributionbtch.TotalAmount = Convert.ToDecimal(totalAmount);
                                nhfcontributionbtch.PaymentDate = DateTime.Now;
                                nhfcontributionbtch.naration = item.Narration;
                                nhfcontributionbtch.ContributionDate = DateTime.Now;
                                nhfcontributionbtch.paymentOption = item.Paymentoption;
                                nhfcontributionbtch.month = item.Contributionmonth;
                                nhfcontributionbtch.year = item.Contributionyear;
                                nhfcontributionbtch.accountName = item.Accountname;
                                nhfcontributionbtch.Email = item.EmailAddress;
                                nhfcontributionbtch.TransactionId = remitaPaymentDetailsEntity.Rrr;
                                nhfcontributionbtch.TransactionDate = DateTime.Now;
                                nhfcontributionbtch.ContributionType = 2;

                                contributions.Add(nhfcontributionbtch);

                                var cpttable = new FinanceCounterpartyTransactionEntity();
                                {

                                    cpttable.TransactionDate = DateTime.Now;
                                    cpttable.Ref = item.NhfNo;

                                    cpttable.CreditAmount = Convert.ToDecimal(item.Amount);

                                    cpttable.CustCode = item.EmployeeNumber;

                                    cpttable.PostDate = DateTime.Now;

                                    cpttable.Description = item.Narration;

                                    cpttable.Approved = 0;

                                    cpttable.DebitAmount = 0;

                                    cpttable.TransactionId = remitaPaymentDetailsEntity.Rrr;

                                    cpttable.TransactionType = "70";

                                    cpttable.Show = 1;

                                    cpttable.Coy = query.FirstOrDefault().EmployerNumber;




                                };
                                financeCounterpartyTransactions.Add(cpttable);

                            }


                            var ftTableCredit = new FinanceTransactionEntity()
                            {

                                TransactionDate = DateTime.Now,
                                ValueDate = DateTime.Now,
                                Approved = 0,
                                ApprovedBy = "SYS",
                                ApplicationId = "FBMN APPLICATION",
                                //Destinationbranch = employernumber.Branchcode,
                                CreditAmt = Convert.ToDecimal(totalAmount),
                                // Description = entity.,
                                DebitAmt = 0,
                                Isreversed = 0,

                                PostedBy = "IBANK",
                                Saved = 1,
                                Deleted = 0,
                                PostingTime = Convert.ToString(DateTime.Now),
                                Ref = remitaPaymentDetailsEntity.Rrr,
                                //SourceBranch = employernumber.Branchcode,
                                TransactionType = 70

                            };

                            financeTransactions.Add(ftTableCredit);

                            var ftTableDebit = new FinanceTransactionEntity()
                            {

                                TransactionDate = DateTime.Now,
                                Ref = remitaPaymentDetailsEntity.Rrr,
                                ValueDate = DateTime.Now,
                                //ApplicationId = "FBMN APPLICATION",
                                Saved = 1,
                                //DestinationBranch = employernumber.Branchcode,
                                Isreversed = 0,
                                Deleted = 0,
                                Approved = 0,
                                ApprovedBy = "SYS",
                                CreditAmt = 0,
                                //AccountId = glAccountId.Ledgerdr,
                                //LegType = glAccountId.Ledgerdr,
                                PostedBy = "IBANK",
                                PostingTime = Convert.ToString(DateTime.Now),
                                DebitAmt = Convert.ToDecimal(totalAmount),
                                //Description = uploads.Narration,
                                //SourceBranch = employernumber.Branchcode,
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
            var employeedetails = await _iUnitOfWork.Employees.GetEntityByNhfNumber(loggedUser.EmployeeInfo.NHFNumber);

            //var employernumber = _context.Nhfemployer.Where(e => e.Emailaddress == User.Identity.Name).FirstOrDefault();
            //string connectionString = "TNS_ADMIN=C:\\\\\\\\instantclient_19_13\\\\\\\\network\\\\\\\\admin;USER ID=FINTRAKBANKING;PASSWORD=Fintrak1;DATA SOURCE=FCB;PERSIST SECURITY INFO=True";
            // OracleHelper oracleHelper = new OracleHelper(connectionString);



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
                                string NhfNo = Convert.ToString(worksheet.Cells[row, 1].Value);                                
                                string Amount = Convert.ToString(worksheet.Cells[row, 2].Value);
                                string ContributionYear = Convert.ToString(worksheet.Cells[row, 4].Value);
                                string ContributionMonth = Convert.ToString(worksheet.Cells[row, 3].Value);
                               

                                if (NhfNo == "" && Amount == "" && ContributionYear == "" && ContributionMonth == "")
                                    continue;
                                nhfemployee.NhfNo = NhfNo;                               
                                nhfemployee.Amount = Amount;
                                nhfemployee.Contributionmonth = ContributionMonth;
                                nhfemployee.Contributionyear = ContributionYear;

                                nhfemployee.Accountname = entity.AccountName;
                                nhfemployee.Paymentoption = entity.BacklogPaymentOption;
                                nhfemployee.EmailAddress = entity.EmailAddress;
                                //nhfemployee.EmailAddress = entity.EmailAddress;
                                ;
                                //UnvalidatedList.Add(nhfemployee);
                                employeesData.Add(row, nhfemployee);
                            }
                            foreach (var item in employeesData)
                            {
                                MessageError = string.Empty;
                                //VALIDATIONS for required fields
                                if (string.IsNullOrEmpty(item.Value.NhfNo))
                                {
                                    MessageError = $"NHF Number is required at row {item.Key} " + " in excel.";
                                    ErrorMessages.Add(new ErrorListBacklog { Error = MessageError });
                                }
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
                                //string[] addrecord = { NhfNo };

                                //var duplicate = addrecord.Where(item => !listexcelrecord.Add(item)).Distinct().ToList();

                                var exist = query.Where(i => i.NhfNumber.Contains(item.Value.NhfNo) && i.EmployerNumber.Contains(item.Value.EmployerNo));
                                if (exist == null)
                                {
                                    MessageError = $"This NHF Number: " + item.Value.NhfNo + " does not exist or belong to this employer";
                                    ErrorMessages.Add(new ErrorListBacklog { Error = MessageError });

                                }
                                var exists = query.Where(i => i.NhfNumber.Contains(item.Value.NhfNo));
                                if (exists == null)
                                {
                                    MessageError = $"This NHF Number: " + item.Value.NhfNo + " does not exist";
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
                            //return Json(new { success = false, message = ErrorMessages });

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


                        var Rrrgenerator = await paymentIntegrationService.Generate(PaymentDetails);
                        if (Rrrgenerator.Data == null)
                        {
                            string message = "Failed to Generate RRR";
                            obj.Message = message;
                            obj.Tag = 0;
                            return obj;
                        }
                        RemitaPaymentDetailsEntity remitaPaymentDetailsEntity = new RemitaPaymentDetailsEntity();
                        remitaPaymentDetailsEntity.TransactionId = Rrrgenerator.Data.TransactionId;
                        remitaPaymentDetailsEntity.TransactionDate = DateTime.Now;
                        remitaPaymentDetailsEntity.Status = 1;
                        remitaPaymentDetailsEntity.Rrr = Rrrgenerator.Data.RRR;
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
                                ContributionEntity nhfcontributionbtch = new ContributionEntity();
                                nhfcontributionbtch.NhfNo = item.NhfNo;
                                //nhfcontributionbtch.EmployeeName = item.Lastname + " " + item.Firstname + " " + item.MiddleName;
                                nhfcontributionbtch.EmployeeName = employeedetails.FirstName + " " + employeedetails.LastName + " " + employeedetails.OtherName;

                                nhfcontributionbtch.contributionAmount = Convert.ToDecimal(item.Amount);
                                nhfcontributionbtch.TotalAmount = Convert.ToDecimal(totalAmount);
                                nhfcontributionbtch.PaymentDate = DateTime.Now;
                                nhfcontributionbtch.naration = item.Narration;
                                nhfcontributionbtch.ContributionDate = DateTime.Now;
                                nhfcontributionbtch.paymentOption = item.Paymentoption;
                                nhfcontributionbtch.month = item.Contributionmonth;
                                nhfcontributionbtch.year = item.Contributionyear;
                                nhfcontributionbtch.accountName = item.Accountname;
                                nhfcontributionbtch.Email = item.EmailAddress;
                                nhfcontributionbtch.TransactionId = remitaPaymentDetailsEntity.Rrr;
                                nhfcontributionbtch.TransactionDate = DateTime.Now;
                                nhfcontributionbtch.ContributionType = 2;
                               

                                contributions.Add(nhfcontributionbtch);

                                var cpttable = new FinanceCounterpartyTransactionEntity();
                                {

                                    cpttable.TransactionDate = DateTime.Now;
                                    cpttable.Ref = item.NhfNo;

                                    cpttable.CreditAmount = Convert.ToDecimal(item.Amount);

                                    cpttable.CustCode = item.EmployeeNumber;

                                    cpttable.PostDate = DateTime.Now;

                                    cpttable.Description = item.Narration;

                                    cpttable.Approved = 0;

                                    cpttable.DebitAmount = 0;

                                    cpttable.TransactionId = remitaPaymentDetailsEntity.Rrr;

                                    cpttable.TransactionType = "70";

                                    cpttable.Show = 1;

                                    cpttable.Coy = query.FirstOrDefault().EmployerNumber;




                                };
                                financeCounterpartyTransactions.Add(cpttable);

                            }


                            var ftTableCredit = new FinanceTransactionEntity()
                            {

                                TransactionDate = DateTime.Now,
                                ValueDate = DateTime.Now,
                                Approved = 0,
                                ApprovedBy = "SYS",
                                ApplicationId = "FBMN APPLICATION",
                                //Destinationbranch = employernumber.Branchcode,
                                CreditAmt = Convert.ToDecimal(totalAmount),
                                // Description = entity.,
                                DebitAmt = 0,
                                Isreversed = 0,

                                PostedBy = "IBANK",
                                Saved = 1,
                                Deleted = 0,
                                PostingTime = Convert.ToString(DateTime.Now),
                                Ref = remitaPaymentDetailsEntity.Rrr,
                                //SourceBranch = employernumber.Branchcode,
                                TransactionType = 70

                            };

                            financeTransactions.Add(ftTableCredit);

                            var ftTableDebit = new FinanceTransactionEntity()
                            {

                                TransactionDate = DateTime.Now,
                                Ref = remitaPaymentDetailsEntity.Rrr,
                                ValueDate = DateTime.Now,
                                //ApplicationId = "FBMN APPLICATION",
                                Saved = 1,
                                //DestinationBranch = employernumber.Branchcode,
                                Isreversed = 0,
                                Deleted = 0,
                                Approved = 0,
                                ApprovedBy = "SYS",
                                CreditAmt = 0,
                                //AccountId = glAccountId.Ledgerdr,
                                //LegType = glAccountId.Ledgerdr,
                                PostedBy = "IBANK",
                                PostingTime = Convert.ToString(DateTime.Now),
                                DebitAmt = Convert.ToDecimal(totalAmount),
                                //Description = uploads.Narration,
                                //SourceBranch = employernumber.Branchcode,
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
    }
}


