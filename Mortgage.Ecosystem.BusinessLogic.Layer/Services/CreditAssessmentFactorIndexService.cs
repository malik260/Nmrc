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
    public class CreditAssessmentFactorIndexService : ICreditAssessmentFactorIndexService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CreditAssessmentFactorIndexService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }

        #region Retrieve data
        public async Task<List<CreditAssessmentFactorIndexEntity>> GetList(int riskFactorId)
        {
            List<CreditAssessmentFactorIndexEntity> obj = new List<CreditAssessmentFactorIndexEntity>();
            obj = await _iUnitOfWork.CreditAssessmentFactorIndexes.GetList(riskFactorId);

            return obj;
        }

        public async Task<TData<List<CreditAssessmentFactorIndexEntity>>> GetFactorIndex(int riskFactorId)
        {
            TData<List<CreditAssessmentFactorIndexEntity>> obj = new TData<List<CreditAssessmentFactorIndexEntity>>();
            obj.Data = await _iUnitOfWork.CreditAssessmentFactorIndexes.GetList(riskFactorId);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CreditAssessmentFactorIndexEntity>>> GetPageList(CreditAssessmentFactorIndexListParam param, Pagination pagination)
        {
            TData<List<CreditAssessmentFactorIndexEntity>> obj = new TData<List<CreditAssessmentFactorIndexEntity>>();
            obj.Data = await _iUnitOfWork.CreditAssessmentFactorIndexes.GetPageList(param, pagination);
            foreach (var item in obj.Data)
            {
                item.RiskFactorDesc = _iUnitOfWork.CreditAssessmentRiskFactors.GetEntitiesByRiskId(item.RiskFactorId).Result?.RiskFactorsDescription;
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<CreditAssessmentFactorIndexEntity>> GetEntity(long id)
        {
            TData<CreditAssessmentFactorIndexEntity> obj = new TData<CreditAssessmentFactorIndexEntity>();
            obj.Data = await _iUnitOfWork.CreditAssessmentFactorIndexes.GetEntity(id)
;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<CreditAssessmentFactorIndexEntity>> GetEntities(int id)
        {
            TData<CreditAssessmentFactorIndexEntity> obj = new TData<CreditAssessmentFactorIndexEntity>();

            try
            {
                CreditAssessmentFactorIndexEntity entity;

                if (id == 0)
                {
                    // Initialize a new entity for adding
                    entity = new CreditAssessmentFactorIndexEntity();
                }
                else
                {
                    // Get entity by ID for editing
                    entity = await _iUnitOfWork.CreditAssessmentFactorIndexes.GetEntities(id);

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
        public async Task<TData<string>> SaveForm(CreditAssessmentFactorIndexEntity entity)
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
                    obj.Message = "Factor Weight cannot be greater than the Product Max weight: " + productscoreInfo;
                    return obj;
                }
                if (entity.Id == 0)
                {

                    var getFactorIndexWeiht = _iUnitOfWork.CreditAssessmentFactorIndexes.GetListbyProductCode(entity.ProductCode).Result.ToList().Select(i => i.Weight).Sum(); ;
                    
                    if ((Convert.ToDecimal(entity.Weight) + getFactorIndexWeiht) > productscoreInfo)
                    {
                        obj.Data = entity.Id.ToString();
                        obj.Tag = 0;
                        obj.Message = " Weight cannot be greater than total risk factor weight " + productscoreInfo;
                        return obj;

                    }
                }
                if (entity.Id > 0)
                {
                    var getFactorIndexWeiht = _iUnitOfWork.CreditAssessmentFactorIndexes.GetListbyProductCode(entity.ProductCode).Result.Where(i=> i.Id != entity.Id).ToList().Select(i => i.Weight).Sum(); ;
                    var NewWeight = entity.Weight + getFactorIndexWeiht;
                    if (Convert.ToDecimal(NewWeight) > productscoreInfo)
                    {
                        obj.Data = entity.Id.ToString();
                        obj.Tag = 0;
                        obj.Message = "  Weight cannot be greater than the Product Risk weight : " + productscoreInfo;
                        return obj;

                    }

                }
                var autoIncreament = _iUnitOfWork.CreditAssessmentFactorIndexes.GetListbyProductCode(null).Result.ToList();

                if (autoIncreament.Count == 0)
                {
                    entity.FactorIndexId = 1;
                }
                else
                {
                    var maxFactorIndexId = autoIncreament.Max(x => x.FactorIndexId);
                    entity.FactorIndexId = ++maxFactorIndexId;
                }

                bool isUpdate = entity.Id > 0;
                await _iUnitOfWork.CreditAssessmentFactorIndexes.SaveForm(entity);
                obj.Data = entity.FactorIndexId.ToString();
                obj.Tag = 1;

                obj.Message = isUpdate ? "Factor Index Updated successfully" : "Factor Index Added successfully";

                return obj;
            }

            catch (Exception ex)
            {
                obj.Tag = -1;
                obj.Message = "An error occurred while saving the form";
                return obj;

            }

            // In case of an exception, return the error response
           
        }


        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.CreditAssessmentFactorIndexes.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<string>> UpdateForm(CreditAssessmentFactorIndexEntity entity)
        {
            TData<string> obj = new TData<string>();

            await _iUnitOfWork.CreditAssessmentFactorIndexes.SaveForm(entity);

            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        #endregion
    }
}