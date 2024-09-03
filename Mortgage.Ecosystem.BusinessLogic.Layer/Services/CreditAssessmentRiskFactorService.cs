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
using NPOI.Util;
using System.Collections.Generic;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class CreditAssessmentRiskFactorService : ICreditAssessmentRiskFactorService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CreditAssessmentRiskFactorService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }

        #region Retrieve data
        public async Task<List<CreditAssessmentRiskFactorEntity>> GetList(string productcode)
        {
            List<CreditAssessmentRiskFactorEntity> obj = new List<CreditAssessmentRiskFactorEntity>();
            var pdCode = _iUnitOfWork.CreditTypes.GetEntitybyName(productcode).Result.Code;
            obj = await _iUnitOfWork.CreditAssessmentRiskFactors.GetList(pdCode);

            return obj;
        }  
        
        public async Task <TData<List<CreditAssessmentRiskFactorEntity>>> Getrisks(string productcode)
        {
           TData<List<CreditAssessmentRiskFactorEntity>> obj = new TData<List<CreditAssessmentRiskFactorEntity>>();
            var pdCode = _iUnitOfWork.CreditTypes.GetEntity(productcode).Result.Code;
            obj.Data = await _iUnitOfWork.CreditAssessmentRiskFactors.GetList(pdCode);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }



        
        public async Task<TData<CreditAssessmentRiskFactorEntity>> GetEntity(long id)
        {
            TData<CreditAssessmentRiskFactorEntity> obj = new TData<CreditAssessmentRiskFactorEntity>();
            obj.Data = await _iUnitOfWork.CreditAssessmentRiskFactors.GetEntity(id)
;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<CreditAssessmentRiskFactorEntity>> GetEntities(int id)
        {
            TData<CreditAssessmentRiskFactorEntity> obj = new TData<CreditAssessmentRiskFactorEntity>();

            try
            {
                CreditAssessmentRiskFactorEntity entity;

                if (id == 0)
                {
                    // Initialize a new entity for adding
                    entity = new CreditAssessmentRiskFactorEntity();
                }
                else
                {
                    // Get entity by ID for editing
                    entity = await _iUnitOfWork.CreditAssessmentRiskFactors.GetEntities(id);

                    if (entity == null)
                    {
                        obj.Message = "Entity not found.";
                        obj.Tag = -1;
                        return obj;
                    }

                    // Retrieve and assign the ProductName based on ProductCode
                    var productEntity = await _iUnitOfWork.CreditTypes.GetEntityByProductCode(entity.ProductCode);
                    //if (productEntity == null)
                    //{
                    //    entity.ProductName = productEntity.Name;
                    //}
                    if (productEntity != null)
                    {
                        entity.ProductName = productEntity.Name;
                    }
                    else
                    {
                        entity.ProductName = "Unknown Product"; // Default value if product entity is not found
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

        public async Task<TData<List<CreditAssessmentRiskFactorEntity>>> GetPageList(CreditAssessmentRiskFactorListParam param, Pagination pagination)
        {
            TData<List<CreditAssessmentRiskFactorEntity>> obj = new TData<List<CreditAssessmentRiskFactorEntity>>();
            obj.Data = await _iUnitOfWork.CreditAssessmentRiskFactors.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<string>> SaveForm(CreditAssessmentRiskFactorEntity entity)
        {
            TData<string> obj = new TData<string>();
            try
            {
                CreditScoreListParam param = new CreditScoreListParam();
                param.CreditType = entity.ProductCode;
                var productscoreInfo = _iUnitOfWork.CreditScores.GetList(param).Result.Select(i => i.RangeMax).Max();
                if (Convert.ToDecimal(entity.Weight) > productscoreInfo)
                {
                    obj.Data = entity.Id.ToString();
                    obj.Tag = 0;
                    obj.Message = "Risk Weight cannot be greater than the Product Max weight: " + productscoreInfo;
                    return obj;
                }
                if (entity.Id == 0)
                {
                    var ExistRisk = _iUnitOfWork.CreditAssessmentRiskFactors.GetList(entity.ProductCode).Result.ToList().Select(i => i.Weight).Sum();
                    var NewWeight = entity.Weight + ExistRisk;
                    if (Convert.ToDecimal(NewWeight) > productscoreInfo)
                    {
                        obj.Data = entity.Id.ToString();
                        obj.Tag = 0;
                        obj.Message = "Total Risk Weight cannot be greater than the Product Max weight: " + productscoreInfo;
                        return obj;

                    }
                }
                if (entity.Id > 0)
                {
                    var getrisk = await _iUnitOfWork.CreditAssessmentRiskFactors.GetEntities(entity.Id);
                    getrisk.Weight = entity.Weight;
                    var ExistRisk = _iUnitOfWork.CreditAssessmentRiskFactors.GetList(entity.ProductCode).Result.Where(i => i.Id != getrisk.Id).ToList().Select(i => i.Weight).Sum();
                    var NewWeight = entity.Weight + ExistRisk;
                    if (Convert.ToDecimal(NewWeight) > productscoreInfo)
                    {
                        obj.Data = entity.Id.ToString();
                        obj.Tag = 0;
                        obj.Message = "Total Risk Weight cannot be greater than the Product Max weight : " + productscoreInfo;
                        return obj;

                    }

                }
                var autoIncreament = _iUnitOfWork.CreditAssessmentRiskFactors.GetList(null).Result.ToList();
                if (autoIncreament.Count == 0)
                {
                    entity.RiskFactorId = 1;

                }
                else
                {
                    var maxRiskId = autoIncreament.Max(x => x.RiskFactorId);
                    entity.RiskFactorId = ++maxRiskId;

                }
                bool isUpdate = entity.Id > 0;

                await _iUnitOfWork.CreditAssessmentRiskFactors.SaveForm(entity);
                obj.Data = entity.Id.ToString();
                obj.Tag = 1;
                obj.Message = isUpdate ? "Risk Factor Updated successfully" : "Risk Factor added successfully";

                return obj;
            }



            catch (Exception ex)
            {
                throw;
            }


        }


        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.CreditAssessmentRiskFactors.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> UpdateForm(CreditAssessmentRiskFactorEntity entity)
        {
            TData<string> obj = new TData<string>();           

            await _iUnitOfWork.CreditAssessmentRiskFactors.SaveForm(entity);

            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }



        #endregion
    }
}