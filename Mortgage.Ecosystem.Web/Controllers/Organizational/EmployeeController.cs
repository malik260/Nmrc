using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    [ExceptionFilter]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;
        private readonly IAuditTrailService _iAuditTrailService;

        public EmployeeController(IUnitOfWork iUnitOfWork, IEmployeeService employeeService, IUserService userService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _employeeService = employeeService;
            _userService = userService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("employee:view")]
        public IActionResult EmployeeIndex()
        {
            return View();
        }

        public IActionResult EmployeeForm()
        {
            return View();
        }

        [AuthorizeFilter("nhfregusers:view")]
        public IActionResult NHFRegUsersIndex()
        {
            return View();
        }

        public IActionResult NHFRegUserForm()
        {
            return View();
        }

        #endregion View function

        #region Get data
        //[HttpGet]
        //[AuthorizeFilter("employee:search,user:search")]
        //public async Task<IActionResult> GetListJson(EmployeeListParam param)
        //{
        //    TData<List<EmployeeEntity>> obj = await _employeeService.GetList(param);
        //    return Json(obj);
        //}

        public async Task<IActionResult> GetListJson2(EmployeeListParam param)
        {
            TData<List<EmployeeEntity>> obj = await _employeeService.GetList2(param);
            return Json(obj);
        }


        public async Task<IActionResult> GetListJson(EmployeeListParam param)
        {
            TData<List<EmployeeEntity>> obj = await _employeeService.GetList(param);
            return Json(obj);
        }
        [HttpGet]
        [AuthorizeFilter("employee:search,user:search")]
        public async Task<IActionResult> GetPageListJson(EmployeeListParam param, Pagination pagination)
        {
            TData<List<EmployeeEntity>> obj = await _employeeService.GetPageList(param, pagination);

            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("employee:search,user:search")]
        public async Task<IActionResult> GetApprovalPageListJson(EmployeeListParam param, Pagination pagination)
        {
            TData<List<EmployeeEntity>> obj = await _employeeService.GetApprovalPageList(param, pagination);

            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("employee:search,user:search")]
        public async Task<IActionResult> GetEmployeeTreeListJson(EmployeeListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _employeeService.GetZtreeEmployeeList(param);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetEmployeeTreeListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Employee.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("employee:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<EmployeeEntity> obj = await _employeeService.GetEntity(id);
            return Json(obj);
        }


        #endregion Get data

        #region Submit data
        [HttpPost]
        //[AuthorizeFilter("employee:add,employee:edit")]
        public async Task<IActionResult> SaveFormJson(EmployeeEntity entity)
        {
            try
            {
                TData<string> obj = await _employeeService.SaveForm(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.RegisterEmployee.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Employee.ToString();
                auditInstance.UserName = entity.Id.ToString();
                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        //[AuthorizeFilter("employee:add,employee:edit")]
        public async Task<IActionResult> SaveFormsJson(EmployeeEntity entity)
        {
            try
            {
                TData<string> obj = await _employeeService.SaveForms(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.RegisterEmployee.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Employee.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        [AuthorizeFilter("employee:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _employeeService.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveFormJson(EmployeeEntity entity)
        {
            try
            {
                TData obj = await _employeeService.ApproveForm(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.ApproveEmployee.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Employee.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> RejectFormJson(EmployeeEntity entity, string Remark)
        {
            try
            {
                TData obj = await _employeeService.RejectForm(entity, Remark);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.RejectEmployee.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Employee.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        #endregion Submit data
    }
}