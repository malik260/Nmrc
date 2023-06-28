using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class BankController : BaseController
    {
        private readonly IBankService _iBankService;

        public BankController(IUnitOfWork iUnitOfWork, IBankService iBankService) : base(iUnitOfWork)
        {
            _iBankService = iBankService;
        }

        #region View function
        [AuthorizeFilter("Bank:view")]
        public IActionResult BankIndex()
        {
            return View();
        }

        [AuthorizeFilter("Bank:view")]
        public IActionResult BankForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("Bank:search,user:search")]
        public async Task<IActionResult> GetListJson(BankListParam param)
        {
            TData<List<BankEntity>> obj = await _iBankService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("Bank:search,user:search")]
        public async Task<IActionResult> GetPageListJson(BankListParam param, Pagination pagination)
        {
            TData<List<BankEntity>> obj = await _iBankService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("Bank:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<BankEntity> obj = await _iBankService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("Bank:view")]
        public async Task<IActionResult> GetBankName(BankListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iBankService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.Name));
                obj.Tag = 1;
            }
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("Bank:add,Bank:edit")]
        public async Task<IActionResult> SaveFormJson(BankEntity entity)
        {
            TData<string> obj = await _iBankService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("Bank:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iBankService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}