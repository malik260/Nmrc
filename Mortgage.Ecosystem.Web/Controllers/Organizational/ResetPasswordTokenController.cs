using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer;
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
    public class ResetPasswordTokenController : BaseController
    {
        private readonly IResetPasswordTokenService _iResetPasswordTokenService;
        private readonly IAuditTrailService _iAuditTrailService;
        private readonly IUserService _iUserService;
        public ResetPasswordTokenController(IUnitOfWork iUnitOfWork, IResetPasswordTokenService iResetPasswordTokenService, IAuditTrailService iAuditTrailService, IUserService userService) : base(iUnitOfWork)
        {
            _iResetPasswordTokenService = iResetPasswordTokenService;
            _iAuditTrailService = iAuditTrailService;
            _iUserService = userService;
        }

        #region View function
        [AuthorizeFilter("ResetPasswordToken:view")]
        public IActionResult ResetPasswordTokenIndex()
        {
            return View();
        }

        public IActionResult ResetPasswordTokenForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("ResetPasswordToken:search,user:search")]
        public async Task<IActionResult> GetListJson(ResetPasswordTokenListParam param)
        {
            TData<List<ResetPasswordTokenEntity>> obj = await _iResetPasswordTokenService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("ResetPasswordToken:search,user:search")]
        public async Task<IActionResult> GetChargeSetupPageListJson(ResetPasswordTokenListParam param, Pagination pagination)
        {
            TData<List<ResetPasswordTokenEntity>> obj = await _iResetPasswordTokenService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("ResetPasswordToken:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(ResetPasswordTokenListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iResetPasswordTokenService.GetZtreeResetPasswordTokenList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("ResetPasswordToken:view")]
        public async Task<IActionResult> GetUserTreeListJson(ResetPasswordTokenListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iResetPasswordTokenService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("ResetPasswordToken:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<ResetPasswordTokenEntity> obj = await _iResetPasswordTokenService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iResetPasswordTokenService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        //[AuthorizeFilter("ResetPasswordToken:add,company:edit")]
        public async Task<IActionResult> GenerateTokenJson(ResetPasswordTokenEntity entity)
        {
            try
            {
                var Employee = await _iUserService.GetEntityByEmail(entity.EmailAddress);
                TData<string> obj = await _iResetPasswordTokenService.GenerateToken(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.UserName = Employee.Data.Employee.ToString();
                auditInstance.Action = SystemOperationCode.GenerateTokenJson.ToString();
                auditInstance.ActionRoute = SystemOperationCode.ResetPassword.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckToken(ResetPasswordTokenListParam param)
        {
            try
            {
                var getUser = await _iUserService.GetEntityByEmail(param.EmailAddress);

                var result = await _iResetPasswordTokenService.GetTokenList(param);

                if (result.Data == null)
                {
                    return NotFound();
                }
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.CheckToken.ToString();
                auditInstance.ActionRoute = SystemOperationCode.ResetPassword.ToString();
                auditInstance.UserName = getUser.Data.Employee.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(result);
            }
            catch (Exception e)
            {

                throw;
            }
        }


        [HttpPost]
        [AuthorizeFilter("ResetPasswordToken:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iResetPasswordTokenService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}