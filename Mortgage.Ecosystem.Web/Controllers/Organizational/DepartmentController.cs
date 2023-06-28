using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _iDepartmentService;

        public DepartmentController(IUnitOfWork iUnitOfWork, IDepartmentService iDepartmentService) : base(iUnitOfWork)
        {
            _iDepartmentService = iDepartmentService;
        }

        #region View function
        [AuthorizeFilter("department:view")]
        public IActionResult DepartmentIndex()
        {
            return View();
        }
        public IActionResult DepartmentForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("department:search,user:search")]
        public async Task<IActionResult> GetListJson(DepartmentListParam param)
        {
            TData<List<DepartmentEntity>> obj = await _iDepartmentService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("department:search,user:search")]
        public async Task<IActionResult> GetDepartmentTreeListJson(DepartmentListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iDepartmentService.GetZtreeDepartmentList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("department:view")]
        public async Task<IActionResult> GetUserTreeListJson(DepartmentListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iDepartmentService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("department:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<DepartmentEntity> obj = await _iDepartmentService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iDepartmentService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("department:add,department:edit")]
        public async Task<IActionResult> SaveFormJson(DepartmentEntity entity)
        {
            TData<string> obj = await _iDepartmentService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("department:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iDepartmentService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}