using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class NmrcEligibilityService : INmrcEligibilityService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public NmrcEligibilityService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }

        #region Retrieve data
        public async Task<TData<List<NmrcEligibilityEntity>>> GetList(NmrcEligibilityListParam param)
        {
            TData<List<NmrcEligibilityEntity>> obj = new TData<List<NmrcEligibilityEntity>>();
            obj.Data = await _iUnitOfWork.NmrcEligibilities.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<NmrcEligibilityEntity>>> GetPmbList(NmrcEligibilityListParam param)
        {
            TData<List<NmrcEligibilityEntity>> obj = new TData<List<NmrcEligibilityEntity>>();
            obj.Data = await _iUnitOfWork.NmrcEligibilities.GetList(param);
            obj.Data = obj.Data.Where(i => i.Category == 1).ToList();
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }
         public async Task<TData<List<NmrcEligibilityEntity>>> GetObligorList(NmrcEligibilityListParam param)
        {
            TData<List<NmrcEligibilityEntity>> obj = new TData<List<NmrcEligibilityEntity>>();
            obj.Data = await _iUnitOfWork.NmrcEligibilities.GetList(param);
            obj.Data = obj.Data.Where(i => i.Category == 2).ToList();
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<NmrcEligibilityEntity>>> GetCategory(string categoryId)
        {
            TData<List<NmrcEligibilityEntity>> obj = new TData<List<NmrcEligibilityEntity>>();
            obj.Data = await _iUnitOfWork.NmrcEligibilities.GetListbyCategory(categoryId);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<NmrcEligibilityEntity>>> GetPageList(NmrcEligibilityListParam param, Pagination pagination)
        {
            TData<List<NmrcEligibilityEntity>> obj = new TData<List<NmrcEligibilityEntity>>();
            obj.Data = await _iUnitOfWork.NmrcEligibilities.GetPageList(param, pagination);
            if ((obj.Data.Count) > 0 && (!string.IsNullOrEmpty(param.Item)))
            {
                obj.Data = obj.Data.Where(i => i.Item.Trim().ToLower() == param.Item.Trim().ToLower()).DefaultIfEmpty().ToList();
                if (obj.Data[0] == null)
                {
                    obj.Data = new List<NmrcEligibilityEntity>();
                    obj.Total = 0;
                    obj.Tag = 1;
                    return obj;
                }
            }
            if (obj.Data.Count > 0)
            {
                List<NmrcCategoryEntity> nmrcCategoryList = await _iUnitOfWork.NmrcCategories.GetList(new NmrcCategoryListParam { Ids = obj.Data.Select(p => p.Category).ToList() });
                foreach (NmrcEligibilityEntity nmrcEligibility in obj.Data)
                {
                    nmrcEligibility.CategoryName = nmrcCategoryList.Where(p => p.Id == nmrcEligibility.Category).Select(p => p.Name).FirstOrDefault();
                }
            }
                    obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<NmrcEligibilityEntity>> GetEntity(int id)
        {
            TData<NmrcEligibilityEntity> obj = new TData<NmrcEligibilityEntity>();
            obj.Data = await _iUnitOfWork.NmrcEligibilities.GetEntity(id)
;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<NmrcEligibilityEntity>> GetEntities(int id)
        {
            TData<NmrcEligibilityEntity> obj = new TData<NmrcEligibilityEntity>();

            try
            {
                NmrcEligibilityEntity entity;

                if (id == 0)
                {
                    // Initialize a new entity for adding
                    entity = new NmrcEligibilityEntity();
                }
                else
                {
                    // Get entity by ID for editing
                    entity = await _iUnitOfWork.NmrcEligibilities.GetEntities(id);

                    if (entity == null)
                    {
                        obj.Message = "Entity not found.";
                        obj.Tag = -1;
                        return obj;
                    }

                    // Retrieve and assign the ProductName based on ProductCode
                    var categoryEntity = await _iUnitOfWork.NmrcCategories.GetEntityByCategoryId(entity.Id);
                    //if (productEntity == null)
                    //{
                    //    entity.ProductName = productEntity.Name;
                    //}
                    if (categoryEntity != null)
                    {
                        entity.Category = categoryEntity.Id;
                    }
                    else
                    {
                        entity.Item = "Unknown Product"; // Default value if product entity is not found
                    }
                }

                obj.Data = entity; // Assign the retrieved or new entity to obj.Data
                obj.Tag = 1; // Set tag to indicate success
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message; // Set error message
                obj.Tag = -1; // Set tag to indicate failure
            }

            return obj;
        }




        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(NmrcEligibilityEntity entity)
        {
            TData<string> obj = new TData<string>();

            bool isUpdate = entity.Id > 0;
            if (!isUpdate)
            {
                var NameExist = await _iUnitOfWork.NmrcEligibilities.GetEntitybyName(entity.Item);
                if (NameExist != null)
                {
                    obj.Message = "Criteria already exists!";
                    obj.Tag = 0;
                    return obj;
                }
            }

            await _iUnitOfWork.NmrcEligibilities.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = isUpdate ? "Eligibility Criteria updated successfully" : "Eligibility Criteria added successfully";

            return obj;
        }


        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.NmrcEligibilities.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<string>> UpdateForm(NmrcEligibilityEntity entity)
        {
            TData<string> obj = new TData<string>();

            await _iUnitOfWork.NmrcEligibilities.SaveForm(entity);

            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        #endregion
    }
}