using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;
//using OfficeOpenXml;
using System.Data;
using System.Net.Mime;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{


    public class ContributionController : BaseController
    {
        private readonly IContributionService _iContributionService;
        private readonly IPaymentIntegrationService _ipaymentIntegrationService;

        public ContributionController(IUnitOfWork iUnitOfWork, IContributionService iContributionService, IPaymentIntegrationService paymentIntegrationService) : base(iUnitOfWork)
        {
            _iContributionService = iContributionService;
            _ipaymentIntegrationService = paymentIntegrationService;
        }
        #region View function
        [AuthorizeFilter("contribution:view")]
        public IActionResult ContributionIndex()
        {
            return View();
        }

        public IActionResult ContributionForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("contribution:search,user:search")]
        public async Task<IActionResult> GetListJson(ContributionListParam param)
        {
            TData<List<ContributionEntity>> obj = await _iContributionService.GetList(param);
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

        [HttpGet]
        [AuthorizeFilter("contribution:search,user:search")]
        public async Task<IActionResult> GetContributionPageListJson(ContributionListParam param, Pagination pagination)
        {
            TData<List<ContributionEntity>> obj = await _iContributionService.GetPageList(param, pagination);
            return Json(obj);
        }


        public async Task<IActionResult> GetEmployeeDetails()
        {
            TData obj = await _iContributionService.GetCustomerDetails();
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
            TData obj = await _iContributionService.SingleContribution(entity);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> BatchContribution(BatchUploadVM entity)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Check RRR status 
        public async Task<IActionResult> CheckPaymentStatus(string RRR)
        {
            TData obj = await _ipaymentIntegrationService.CheckRRRStatus(RRR);
            return Json(obj);
        }
        #endregion
    }
}
