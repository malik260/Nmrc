using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    [ExceptionFilter]
    public class UserController : BaseController
    {
        private readonly IUserService _iUserService;
        private readonly IMenuAuthorizeService _iMenuAuthorizeService;
        private readonly IAuditTrailService _iAuditTrailService;
        private readonly IEmployeeService _employeeService;
        public UserController(IUnitOfWork iUnitOfWork, IUserService iUserService, IMenuAuthorizeService iMenuAuthorizeService, IAuditTrailService AuditTrailService, IEmployeeService employeeService) : base(iUnitOfWork)
        {
            _iUserService = iUserService;
            _iMenuAuthorizeService = iMenuAuthorizeService;
            _iAuditTrailService = AuditTrailService;
            _employeeService = employeeService;
        }

        #region View function

        //[AuthorizeFilter("user:view")]
        public IActionResult UserIndex()
        {
            return View();
        }

        public IActionResult UserForm()
        {
            return View();
        }

        public IActionResult UserDetail()
        {
           
            ViewBag.Ip = NetHelper.Ip;
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }


        public IActionResult ForgotPasswordForm()
        {
            return View();
        }
        public async Task<IActionResult> ChangePassword()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }

        public IActionResult ChangeUser()
        {
            return View();
        }

        public async Task<IActionResult> UserPortrait()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }

        public IActionResult UserImport()
        {
            return View();
        }

        #endregion view function

        #region Get data

        [HttpGet]
        [AuthorizeFilter("user:search")]
        public async Task<IActionResult> GetListJson(UserListParam param)
        {
            TData<List<UserEntity>> obj = await _iUserService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("user:search")]
        public async Task<IActionResult> GetPageListJson(UserListParam param, Pagination pagination)
        {
            TData<List<UserEntity>> obj = await _iUserService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("user:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<EmployeeEntity> obj = await _iUserService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("user:view")]
        public async Task<IActionResult> GetUserAuthorizeJson()
        {
            TData<UserAuthorizeInfo> obj = new TData<UserAuthorizeInfo>();
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            TData<List<MenuAuthorizeInfo>> objMenuAuthorizeInfo = await _iMenuAuthorizeService.GetAuthorizeList(operatorInfo);
            obj.Data = new UserAuthorizeInfo();
            obj.Data.IsSystem = operatorInfo.IsSystem;
            if (objMenuAuthorizeInfo.Tag == 1)
            {
                obj.Data.MenuAuthorize = objMenuAuthorizeInfo.Data;
            }
            obj.Tag = 1;
            return Json(obj);
        }

        #endregion get data

        #region Submit data

        [HttpPost]
        [AuthorizeFilter("user:add,user:edit")]
        public async Task<IActionResult> SaveFormJson(UserEntity entity)
        {
            TData<string> obj = await _iUserService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("user:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iUserService.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("user:resetpassword")]
        public async Task<IActionResult> ResetPasswordJson(UserEntity entity)
        {
            TData<long> obj = await _iUserService.ResetPassword(entity);
            return Json(obj);
        }

        [HttpPost]
        //[AuthorizeFilter("user:edit")]
        public async Task<IActionResult> ChangePasswordJson(ChangePasswordParam entity)
        {
            try
            {
                var getUser = await _iUserService.GetEntityByEmail(entity.Username);

                TData<long> obj = await _iUserService.ChangePassword(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.ChangePasswordJson.ToString();
                auditInstance.ActionRoute = SystemOperationCode.User.ToString();
                auditInstance.UserName = getUser.Data.Employee.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        ////[AuthorizeFilter("user:edit")]
        public async Task<IActionResult> ForgotPasswordJson(ChangePasswordParam entity)
        {
            try
            {
                TData<long> obj = await _iUserService.ForgotPassword(entity);
                
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        [AuthorizeFilter("user:edit")]
        public async Task<IActionResult> ChangeUserJson(UserEntity entity)
        {
            TData<long> obj = await _iUserService.ChangeUser(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("user:edit")]
        public async Task<IActionResult> ImportUserJson(ImportParam param)
        {
            List<UserEntity> list = new ExcelHelper<UserEntity>().ImportFromExcel(param.FilePath);
            TData obj = await _iUserService.ImportUser(param, list);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("user:edit")]
        public async Task<IActionResult> ExportUserJson(UserListParam param)
        {
            try
            {
                TData<string> obj = new TData<string>();
                TData<List<UserEntity>> userObj = await _iUserService.GetList(param);
                if (userObj.Tag == 1)
                {
                    string file = new ExcelHelper<UserEntity>().ExportToExcel("User List.xls",
                                                                              "user list",
                                                                              userObj.Data,
                                                                              new string[] { "UserName", "RealName", "Gender", "Mobile", "Email" });
                    obj.Data = file;
                    obj.Tag = 1;
                }
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.ExportEmployee.ToString();
                auditInstance.ActionRoute = SystemOperationCode.User.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);

            }
            catch (Exception e)
            {

                throw;
            }        }

        #endregion Submit data
    }
}