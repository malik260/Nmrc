﻿using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers.Web;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers
{
    [ExceptionFilter]
    public class HomeController : BaseController
    {
        private readonly IMenuService _iMenuService;
        private readonly IUserService _iUserService;
        private readonly ILogLoginService _iLogLoginService;
        private readonly IMenuAuthorizeService _iMenuAuthorizeService;
        private readonly IAuditTrailService _iAuditTrailService;
        public HomeController(IUnitOfWork iUnitOfWork, IMenuService iMenuService, IUserService iUserService, ILogLoginService iLogLoginService, IMenuAuthorizeService iMenuAuthorizeService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iMenuService = iMenuService;
            _iUserService = iUserService;
            _iLogLoginService = iLogLoginService;
            _iMenuAuthorizeService = iMenuAuthorizeService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [HttpGet]
        [AuthorizeFilter]
        public async Task<IActionResult> Index()
        {
            string companyName = string.Empty;
            string name = string.Empty;
            var context = new ApplicationDbContext();
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            var employeeInfo = context.EmployeeEntity.Where(i => i.Id == operatorInfo.Employee).FirstOrDefault();
            var companyInfo = context.CompanyEntity.Where(i => i.Id == employeeInfo.Company).FirstOrDefault();
            TData<List<MenuEntity>> objMenu = await _iMenuService.GetList(null);
            List<MenuEntity> menuList = objMenu.Data;
            menuList = menuList.Where(p => p.MenuStatus == StatusEnum.Yes.ToInt() && p.BaseVersion == 1).ToList();

            if (operatorInfo.IsSystem != 1)
            {
                TData<List<MenuAuthorizeInfo>> objMenuAuthorize = await _iMenuAuthorizeService.GetAuthorizeList(operatorInfo);
                List<long?> authorizeMenuIdList = objMenuAuthorize.Data.Select(p => p.MenuId).ToList();
                menuList = context.MenuEntity.ToList().Where(p => authorizeMenuIdList.Contains(p.Id)).ToList();
            }

            ViewBag.MenuList = menuList;
            ViewBag.OperatorInfo = operatorInfo;
            ViewBag.name = employeeInfo.FirstName + ' ' + employeeInfo.LastName;
            ViewBag.company = companyInfo.Name;
            return View();
        }

        [HttpGet]
        public IActionResult Welcome()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            OperatorInfo user = await Operator.Instance.Current();
            if (user != null)
            {
                #region Exit the system
                // Do not set IsOnline to 0 to allow multiple users to log in simultaneously
                await _iUserService.UpdateUser(new UserEntity { Id = user.Id, Company = user.Company, Employee = user.Employee, LoginCount = user.LoginCount, UserStatus = user.UserStatus, IsSystem = user.IsSystem, IsOnline = 1 });

                // Log out
                await _iLogLoginService.SaveForm(new LogLoginEntity
                {
                    LogStatus = OperateStatusEnum.Success.ToInt(),
                    Remark = "Session Timeout",
                    IpAddress = NetHelper.Ip,
                    IpLocation = string.Empty,
                    Browser = NetHelper.Browser,
                    OS = NetHelper.GetOSVersion(),
                    ExtraRemark = NetHelper.UserAgent,
                    BaseCreatorId = user.Employee
                });

                Operator.Instance.RemoveCurrent();
                CookieHelper.Remove("RememberMe");
                #endregion
            }

            if (GlobalConstant.IsDevelopment)
            {
                ViewBag.UserName = "admin";
                ViewBag.Password = "123456";
            }



            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginOffJson()
        {

            OperatorInfo user = await Operator.Instance.Current();

            if (user != null)
            {
                #region Exit the system
                await _iUserService.UpdateUser(new UserEntity { Id = user.Id, Company = user.Company, Employee = user.Employee, LoginCount = user.LoginCount, UserStatus = user.UserStatus, IsSystem = user.IsSystem, IsOnline = 0, Pmb = user.Pmb, Developer = user.Developer });

                // log out
                await _iLogLoginService.SaveForm(new LogLoginEntity
                {
                    LogStatus = OperateStatusEnum.Success.ToInt(),
                    Remark = "Exit system",
                    IpAddress = NetHelper.Ip,
                    IpLocation = string.Empty,
                    Browser = NetHelper.Browser,
                    OS = NetHelper.GetOSVersion(),
                    ExtraRemark = NetHelper.UserAgent,
                    BaseCreatorId = user.Employee,
                    Company = user.Company
                });

                Operator.Instance.RemoveCurrent();
                CookieHelper.Remove("RememberMe");

                return Json(new TData { Tag = 1 });
                #endregion
            }
            else
            {
                throw new Exception("Illegal request");
            }

        }

        [HttpGet]
        public IActionResult NoPermission()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpGet]
        public IActionResult Skin()
        {
            return View();
        }
        #endregion

        #region Get data
        public IActionResult GetCaptchaImage()
        {
            var captchaCode = CaptchaHelper.GetCaptchaCode();
            var bytes = CaptchaHelper.CreateCaptchaImage(captchaCode.Item1);
            SessionHelper.Set("CaptchaCode", captchaCode.Item2);
            return File(bytes, @"image/jpeg");

        }
        #endregion

        #region Submit data
        [HttpPost]
        public async Task<IActionResult> LoginJson(string userName, string password, string captchaCode)
        {
            try
            {

                TData obj = new();
                if (string.IsNullOrEmpty(captchaCode))
                {
                    obj.Message = "<span style='color: black;'>Verification code cannot be empty</span>";
                    obj.Tag = -1;
                    return Json(obj);
                }
                if (captchaCode != SessionHelper.Get("CaptchaCode").ToStr())
                {
                    obj.Message = "<span style='color: black;'>Incorrect verification code, please re-enter</span>";
                    obj.Tag = -1;

                    return Json(obj);
                }
                TData<UserEntity> userObj = await _iUserService.CheckLogin(userName, password, (int)PlatformEnum.Web);
                if (userObj.Tag == 1)
                {
                    await _iUserService.UpdateUser(userObj.Data);
                    await Operator.Instance.AddCurrent(userObj.Data.WebToken);
                }

                string ip = NetHelper.Ip;
                string machineName = IpLocationHelper.GetMachineNameUsingIPAddress(ip);
                //string publicIp = NetHelper.GetPublicIPAddress();
                //string location = IpLocationHelper.GetLocation(publicIp);
                string browser = NetHelper.Browser;
                string os = NetHelper.GetOSVersion();
                string userAgent = NetHelper.UserAgent;

                Action taskAction = async () =>
                {
                    LogLoginEntity logLoginEntity = new LogLoginEntity()
                    {
                        Company = userObj.Data != null ? userObj.Data.Company : 0,
                        LogStatus = userObj.Tag == 1 ? OperateStatusEnum.Success.ToInt() : OperateStatusEnum.Fail.ToInt(),
                        Remark = userObj.Message,
                        IpAddress = ip,
                        IpLocation = IpLocationHelper.GetIpLocation(ip),
                        Browser = browser,
                        OS = os,
                        ExtraRemark = userAgent,
                        BaseCreatorId = userObj.Data != null ? userObj.Data.Employee : 0,
                        //UserName = userObj.Data.UserName
                    };

                    // Let the bottom layer not need to get HttpContext
                    logLoginEntity.BaseCreatorId = logLoginEntity.BaseCreatorId;

                    if (userObj.Tag == 1)
                    {
                        await _iLogLoginService.SaveForm(logLoginEntity);
                    }
                };
                AsyncTaskHelper.StartTask(taskAction);

                obj.Tag = userObj.Tag;
                obj.Message = userObj.Message;
                return Json(obj);

            }
            catch (Exception e)
            {

                throw;
            }
        }
        #endregion
    }
}