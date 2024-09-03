using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;
using OfficeOpenXml;
using System.Data;
using System.Net.Mime;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{

    [ExceptionFilter]
    public class ContributionController : BaseController
    {
        private readonly IContributionService _iContributionService;
        private readonly IPaymentIntegrationService _ipaymentIntegrationService;
        private readonly IAuditTrailService _iAuditTrailService;

        public ContributionController(IUnitOfWork iUnitOfWork, IContributionService iContributionService, IPaymentIntegrationService paymentIntegrationService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iContributionService = iContributionService;
            _ipaymentIntegrationService = paymentIntegrationService;
            _iAuditTrailService = iAuditTrailService;
        }
        #region View function
        //[AuthorizeFilter("contribution:view")]
        public IActionResult ContributionIndex()
        {
            return View();
        }

        public IActionResult ContributionForm()
        {
            return View();
        }

       

        public IActionResult BatchContributionForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("contribution:search,user:search")]
        public async Task<IActionResult> GetListJson(ContributionListParam param, Pagination pagination)
        {
            TData<List<ContributionEntity>> obj = await _iContributionService.GetList(param, pagination);
            return Json(obj);
        }



        [HttpGet]
        //[AuthorizeFilter("contribution:search,user:search")]
        public async Task<IActionResult> GetEmployerListJson(ContributionListParam param, Pagination pagination)
        {
            TData<List<ContributionEntity>> obj = await _iContributionService.GetEmployerList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("contribution:search,user:search")]
        public async Task<IActionResult> GetListJson2(ContributionListParam param, Pagination pagination)
        {
            TData<List<ContributionEntity>> obj = await _iContributionService.GetList2(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("contribution:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(ContributionListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iContributionService.GetZtreeSingleContributionList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("contribution:view")]
        public async Task<IActionResult> GetUserTreeListJson(ContributionListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iContributionService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("contribution:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<ContributionEntity> obj = await _iContributionService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iContributionService.GetMaxSort();
            return Json(obj);
        }

        public async Task<IActionResult> GetEmployeeDetails()
        {
            try
            {
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetEmployeeDetails.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Contribution.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                TData obj = await _iContributionService.GetCustomerDetails();
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IActionResult> GetEmployerDetails()
        {
            try
            {
                
                TData obj = await _iContributionService.GetEmployerDetails();
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IActionResult> GetEmployeeList(long company)
        {
            
            TData<List<EmployeeEntity>> obj = await _iContributionService.GetEmployees(company);
            return Json(obj);
        }


        public async Task<FileResult> BacklogDownload()
        {
            //var transaction = db.Database.BeginTransaction();

            string fileName;
            //fileName = "MultipleUpload.xlsx";
            fileName = "Backlog_Payment.xlsx";


            var path = Path.Combine(
               Directory.GetCurrentDirectory(),
               "wwwroot\\", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.TemplateDownload2.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Contribution.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);


            //transaction.Commit();
            return File(memory, MediaTypeNames.Application.Octet, Path.GetFileName(path));
        }

        public async Task<FileResult> BatchDownload()
        {
            //var transaction = db.Database.BeginTransaction();

            string fileName;
            //fileName = "MultipleUpload.xlsx";
            fileName = "Contribution_Template.xlsx";


            var path = Path.Combine(
               Directory.GetCurrentDirectory(),
               "wwwroot\\", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.TemplateDownload.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Contribution.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);

            //transaction.Commit();
            return File(memory, MediaTypeNames.Application.Octet, Path.GetFileName(path));
        }

        [HttpPost]
        public IActionResult ValidateUploadedFile(string filePath)
        {
            // Define the path to your template file
            var templateFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Backlog_Payment.xlsx");

            // Check if the uploaded file matches the template file
            if (FileCompare(filePath, templateFilePath))
            {
                return Json(new { isValid = true });
            }
            else
            {
                return Json(new { isValid = false });
            }
        }
        // Helper method to compare two files
        private bool FileCompare(string file1, string file2)
        {
            using (var stream1 = new FileStream(file1, FileMode.Open))
            using (var stream2 = new FileStream(file2, FileMode.Open))
            {
                if (stream1.Length != stream2.Length)
                {
                    return false;
                }

                int byte1;
                int byte2;
                do
                {
                    byte1 = stream1.ReadByte();
                    byte2 = stream2.ReadByte();
                } while (byte1 == byte2 && byte1 != -1);

                return byte1 == byte2;
            }
        }





        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("singlecontribution:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(ContributionEntity entity)
        {
            TData<string> obj = await _iContributionService.SaveForm(entity);
            return Json(obj);
        }





        [HttpPost]
        [AuthorizeFilter("singlecontribution:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iContributionService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion

        #region single contribution
        public async Task<IActionResult> SingleContribution(ContributionEntity entity)
        {

            try
            {
                TData obj = await _iContributionService.SingleContribution(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.SingleContribution.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Contribution.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IActionResult> EmployerSingleContribution(ContributionEntity entity)
        {

            try
            {
                TData obj = await _iContributionService.EmployerSingleContribution(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.SingleContribution.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Contribution.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<JsonResult> BacklogSingleContribution(BacklogUploadVM entity)
        {
            var result = new List<BacklogSingleContributionResultVM>();
            var obj = await _iContributionService.BacklogSingleContribution(entity);
            if (obj.Tag == 0)
            {
                result.Add(obj.Data);
                return Json(new { success = false, message = obj.Data.ErrorLists });

            }
            else
            {
                return Json(new { success = true, message = "Backlog Contribution Successfull" });

            }
            //result.Add(obj);

        }

        [HttpPost]
        public async Task<IActionResult> BatchContribution(BatchUploadVM entity)
        {
            try
            {
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.BatchContribution.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Contribution.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                //var obj = await _iContributionService.BatchContribution(entity);
                //return Json(obj);
                var result = new List<BatchContributionResultVM>();
                var obj = await _iContributionService.BatchContribution(entity);
                if (obj.Tag == 0)
                {
                    result.Add(obj.Data);
                    return Json(new { success = false, message = obj.Data.ErrorLists });

                }
                else
                {
                    return Json(new { success = true, message = "Backlog Contribution Successfull" });

                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion


        #region Check RRR status 
        public async Task<IActionResult> CheckPaymentStatus(string RRR)
        {
            try
            {
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.CheckPaymentStatus.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Contribution.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                TData obj = await _ipaymentIntegrationService.CheckRRRStatus(RRR);
                return Json(obj);

            }
            catch (Exception ex)
            {

                throw;
            }        }
        #endregion
    }
}