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
    public class RelationController : BaseController
    {
        private readonly IRelationService _iRelationService;

        public RelationController(IUnitOfWork iUnitOfWork, IRelationService iRelationService) : base(iUnitOfWork)
        {
            _iRelationService = iRelationService;
        }

        #region View function
        [AuthorizeFilter("relation:view")]
        public IActionResult RelationIndex()
        {
            return View();
        }

        [AuthorizeFilter("relation:view")]
        public IActionResult RelationForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("relation:search,user:search")]
        public async Task<IActionResult> GetListJson(RelationListParam param)
        {
            TData<List<RelationEntity>> obj = await _iRelationService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("relation:search,user:search")]
        public async Task<IActionResult> GetPageListJson(RelationListParam param, Pagination pagination)
        {
            TData<List<RelationEntity>> obj = await _iRelationService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("relation:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<RelationEntity> obj = await _iRelationService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("relation:view")]
        public async Task<IActionResult> GetRelationName(RelationListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iRelationService.GetList(param);
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
        [AuthorizeFilter("relation:add,relation:edit")]
        public async Task<IActionResult> SaveFormJson(RelationEntity entity)
        {
            TData<string> obj = await _iRelationService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("relation:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iRelationService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}