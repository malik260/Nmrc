using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class BrokerController : BaseController
    {
        private readonly IBrokerService _iBrokerService;

        public BrokerController(IUnitOfWork iUnitOfWork, IBrokerService iBrokerService) : base(iUnitOfWork)
        {
            _iBrokerService = iBrokerService;
            // _iPmbService = iPmbService;
        }

        [AuthorizeFilter("broker:view")]
        public IActionResult BrokerIndex()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        #region View function
        [AuthorizeFilter("brokerapproval:view")]
        public IActionResult BrokerApprovalIndex()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeFilter("broker:add,pmb:edit")]
        public async Task<IActionResult> ApproveFormJson(BrokerEntity entity)
        {
            TData obj = await _iBrokerService.ApproveForm(entity);
            return Json(obj);
        }



        [HttpGet]
        //[AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetApprovalPageListJson(BrokerListParam param, Pagination pagination)
        {
            TData<List<BrokerEntity>> obj = await _iBrokerService.GetApprovalPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        //[AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetPageListJson(BrokerListParam param, Pagination pagination)
        {
            TData<List<BrokerEntity>> obj = await _iBrokerService.GetPageList(param, pagination);
            return Json(obj);
        }
        #endregion
    }
}
