using Mortgage.Ecosystem.BusinessLogic.Layer.Cache;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public RoleService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Get data
        public async Task<TData<List<RoleEntity>>> GetList(RoleListParam param)
        {
            TData<List<RoleEntity>> obj = new TData<List<RoleEntity>>();
            obj.Data = await _iUnitOfWork.Roles.GetList(param);
            foreach (var item in obj.Data)
            {
                var companyInfo = await  _iUnitOfWork.Companies.GetById(item.Company);
                if (companyInfo != null)
                {
                    item.CompanyName = companyInfo.Name;

                }
                
            }
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RoleEntity>>> GetPageList(RoleListParam param, Pagination pagination)
        {
            TData<List<RoleEntity>> obj = new TData<List<RoleEntity>>();
            obj.Data = await _iUnitOfWork.Roles.GetPageList(param, pagination);
            if (obj.Data != null)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                foreach (RoleEntity role in obj.Data)
                {
                    //role.CompanyName = companyList.Where(p => p.Id == role.Company).Select(p => p.Name).FirstOrDefault();
                    role.CompanyName =  _iUnitOfWork.Companies.GetEntity(role.Company).Result.Name;
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeRoleList(RoleListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RoleEntity> roleList = await _iUnitOfWork.Roles.GetList(param);
            foreach (RoleEntity role in roleList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = role.Id,
                    name = role.RoleName
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<RoleEntity>> GetEntity(long id)
        {
            TData<RoleEntity> obj = new TData<RoleEntity>();
            RoleEntity roleEntity = await _iUnitOfWork.Roles.GetEntity(id);
            List<MenuAuthorizeEntity> menuAuthorizeList = await _iUnitOfWork.MenuAuthorizes.GetList(new MenuAuthorizeEntity
            {
                AuthorizeId = id,
                AuthorizeType = AuthorizeTypeEnum.Role.ToInt()
            });
            // Get the permissions corresponding to the role
            roleEntity.MenuIds = string.Join(",", menuAuthorizeList.Select(p => p.MenuId));

            obj.Data = roleEntity;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.Roles.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(RoleEntity entity)
        {
            TData<string> obj = new TData<string>();

            if (_iUnitOfWork.Roles.ExistRoleName(entity))
            {
                obj.Message = "Role name already exists!";
                return obj;
            }

            await _iUnitOfWork.Roles.SaveForm(entity);

            // Clear the permission data in the cache
            new MenuAuthorizeCache(_iUnitOfWork).Remove();

            obj.Data = entity.Id.ToStr();
            obj.Tag = 1;

            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();

            await _iUnitOfWork.Roles.DeleteForm(ids);

            // Clear the permission data in the cache
            new MenuAuthorizeCache(_iUnitOfWork).Remove();

            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}