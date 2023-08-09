using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class PropertyGalleryService : IPropertyGalleryService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public PropertyGalleryService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<PropertyGalleryEntity>>> GetList(PropertyGalleryListParam param)
        {
            TData<List<PropertyGalleryEntity>> obj = new TData<List<PropertyGalleryEntity>>();
            obj.Data = await _iUnitOfWork.PropertyGalleries.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<PropertyGalleryEntity>>> GetPageList(PropertyGalleryListParam param, Pagination pagination)
        {
            TData<List<PropertyGalleryEntity>> obj = new TData<List<PropertyGalleryEntity>>();
            obj.Data = await _iUnitOfWork.PropertyGalleries.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreePropertyGalleryList(PropertyGalleryListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<PropertyGalleryEntity> propertyGalleryList = await _iUnitOfWork.PropertyGalleries.GetList(param);
            foreach (PropertyGalleryEntity propertyGallery in propertyGalleryList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = propertyGallery.Id,
                    name = propertyGallery.Title
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(PropertyGalleryListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<PropertyGalleryEntity> propertyGalleryList = await _iUnitOfWork.PropertyGalleries.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (PropertyGalleryEntity propertyGallery in propertyGalleryList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = propertyGallery.Id,
                    name = propertyGallery.Title
                });
                List<long> userIdList = userList.Where(t => t.Company == propertyGallery.Id).Select(t => t.Employee).ToList();
                foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
                {
                    obj.Data.Add(new ZtreeInfo
                    {
                        id = user.Id,
                        name = user.RealName
                    });
                }
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<PropertyGalleryEntity>> GetEntity(long id)
        {
            TData<PropertyGalleryEntity> obj = new TData<PropertyGalleryEntity>();
            obj.Data = await  _iUnitOfWork.PropertyGalleries.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.PropertyGalleries.GetMaxSort();
            obj.Tag = 1;
            return obj;
        
        }
        [HttpGet]
        public async Task<TData<List<PropertyGalleryEntity>>> GetAllCards(PropertyGalleryListParam param, Pagination pagination)
        {
            TData<List<PropertyGalleryEntity>> obj = new TData<List<PropertyGalleryEntity>>();
            obj.Data = await _iUnitOfWork.PropertyGalleries.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(PropertyGalleryEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.PropertyGalleries.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.PropertyGalleries.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
