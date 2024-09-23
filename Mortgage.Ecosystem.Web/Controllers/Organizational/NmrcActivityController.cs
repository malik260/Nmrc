using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class NmrcActivityController : BaseController
    {
        private readonly INmrcActivityService _nmrcActivityService;

        public NmrcActivityController(IUnitOfWork iUnitOfWork, INmrcActivityService inmrcActivityService) : base(iUnitOfWork)
        {
            _nmrcActivityService = inmrcActivityService;
        }

        #region View function
        [AuthorizeFilter("nmrcunderwriting:view")]
        public IActionResult NmrcUnderwritingIndex()
        {
            return View();
        }

        [AuthorizeFilter("nmrcdisbursement:view")]
        public IActionResult NmrcLoanDisbursement()
        {
            return View();
        }

        public IActionResult NmrcPmbChecklistForm()
        {
            return View();
        }

        public IActionResult NmrcObligorChecklistForm()
        {
            return View();
        }

        [AuthorizeFilter("nmrcreview:view")]
        public IActionResult NmrcLoanReviewIndex()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("gender:search,user:search")]
        public async Task<IActionResult> GetListJson(RefinancingEntity param)
        {
            TData<List<RefinancingEntity>> obj = await _nmrcActivityService.GetList(param);
            return Json(obj);
        }



        public async Task<IActionResult> ApproveUnderwriting(long id)
        {

            try
            {
                TData<String> obj = await _nmrcActivityService.ApproveUnderwriting(id);
                
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IActionResult> ApproveReview(long id)
        {

            try
            {
                TData<String> obj = await _nmrcActivityService.ApproveLoanReview(id);
                
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IActionResult> DisApproveReview(long id)
        {

            try
            {
                TData<String> obj = await _nmrcActivityService.RejectLoanReview(id);

                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<IActionResult> GetLoanForDis(EmployeeListParam param, Pagination pagination)
        {
            try
            {
                TData<List<NmrcRefinancingEntity>> obj = await _nmrcActivityService.GetLoanForDisbursment();

                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IActionResult> DisburseNonNhfLoan(long id)
        {

            try
            {
                TData<String> obj = await _nmrcActivityService.DisburseNonNhfLoan(id);

                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IActionResult> GetPMBChecklists(long Id)
        {
            try
            {
                TData<List<SecondaryLenderChecklistProcedureEntity>> obj = await _nmrcActivityService.GetPmbChecklist(Id);

                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }





        public async Task<IActionResult> GetLoanForReview(EmployeeListParam param, Pagination pagination)
        {
            try
            {
                TData<List<NmrcRefinancingEntity>> obj = await _nmrcActivityService.GetLoanForReview();
                
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }





        public async Task<IActionResult> GetListById(string id)
        {
            var param = new RefinancingEntity();
            param.RefinanceNumber = id;
            TData<List<RefinancingEntity>> obj = await _nmrcActivityService.GetListByBatchId(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("refinancing:search,user:search")]
        public async Task<IActionResult> GetPageListJson(NmrcRefinancingEntity param, Pagination pagination)
        {
            TData<List<NmrcRefinancingEntity>> obj = await _nmrcActivityService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("refinancing:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<RefinancingEntity> obj = await _nmrcActivityService.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("refinancing:add,refinancing:edit")]
        public async Task<IActionResult> SaveFormJson(RefinancingEntity entity)
        {
            TData<string> obj = await _nmrcActivityService.SaveForm(entity);
            return Json(obj);
        }

     
        #endregion

    }
}
