using Mortgage.Ecosystem.BusinessLogic.Layer.Cache;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class MenuAuthorizeService : IMenuAuthorizeService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public MenuAuthorizeService()
        {

        }

        public MenuAuthorizeService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Get data
        public async Task<TData<List<MenuAuthorizeInfo>>> GetAuthorizeList(OperatorInfo user)
        {
            var db = new ApplicationDbContext();
            TData<List<MenuAuthorizeInfo>> obj = new TData<List<MenuAuthorizeInfo>>();
            obj.Data = new List<MenuAuthorizeInfo>();

            List<MenuAuthorizeEntity> authorizeList = new List<MenuAuthorizeEntity>();
            List<MenuAuthorizeEntity>? userAuthorizeList = null;
            List<MenuAuthorizeEntity>? roleAuthorizeList = null;

            var menuAuthorizeCacheList = await new MenuAuthorizeCache(_iUnitOfWork).GetList();
            var menuList = await new MenuCache(_iUnitOfWork).GetList();
            var enableMenuIdList = menuList.Where(p => p.MenuStatus == (int)StatusEnum.Yes).Select(p => p.Id).ToList();
            menuAuthorizeCacheList = menuAuthorizeCacheList.Where(p => enableMenuIdList.Contains(p.MenuId)).ToList();
            userAuthorizeList = db.MenuAuthorizeEntity.Where(i => i.AuthorizeId == user.Employee).ToList();
            if (userAuthorizeList.Count == 0)
            {
                userAuthorizeList = db.MenuAuthorizeEntity.Where(i => i.AuthorizeId == user.Company).ToList();

            }
            var PosMenu = db.MenuEntity.Where(i => i.MenuStatus == (int)StatusEnum.Yes).Select(i => i.Id).ToList();
            userAuthorizeList = userAuthorizeList.Where(p => PosMenu.Contains(p.MenuId)).ToList();

            // user
            //userAuthorizeList = menuAuthorizeCacheList.Where(p => p.AuthorizeId == user.EmployeeInfo.Id && p.AuthorizeType == AuthorizeTypeEnum.User.ToInt()).ToList();
            //userAuthorizeList = menuAuthorizeCacheList.Where(p => (p.AuthorizeId == user.Employee) && p.AuthorizeType == AuthorizeTypeEnum.User.ToInt()).ToList();


            // Role
            // confirm user type before entering this 
            if (!string.IsNullOrEmpty(user.RoleIds))
            {
                List<long> roleIdList = user.RoleIds.Split(',').Select(p => long.Parse(p)).ToList();
                roleAuthorizeList = menuAuthorizeCacheList.Where(p => roleIdList.Contains(p.AuthorizeId) && p.AuthorizeType == AuthorizeTypeEnum.Role.ToInt()).ToList();
            }

            // exclude duplicate records
            if (userAuthorizeList.Count > 0)
            {
                authorizeList.AddRange(userAuthorizeList);
                if (roleAuthorizeList != null && roleAuthorizeList.Count > 0)
                {
                    roleAuthorizeList = roleAuthorizeList.Where(p => !userAuthorizeList.Select(u => u.AuthorizeId).Contains(p.AuthorizeId)).ToList();
                }
            }
            if (roleAuthorizeList != null && roleAuthorizeList.Count > 0)
            {
                authorizeList.AddRange(roleAuthorizeList);
            }

            foreach (MenuAuthorizeEntity authorize in authorizeList)
            {
                obj.Data.Add(new MenuAuthorizeInfo
                {
                    MenuId = authorize.MenuId,
                    AuthorizeId = authorize.AuthorizeId,
                    AuthorizeType = authorize.AuthorizeType,
                    Authorize = menuList.Where(t => t.Id == authorize.MenuId).Select(t => t.Authorize).FirstOrDefault()
                });
            }
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}