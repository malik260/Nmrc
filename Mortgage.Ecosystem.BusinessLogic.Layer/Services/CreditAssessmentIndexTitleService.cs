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
    public class CreditAssessmentIndexTitleService : ICreditAssessmentIndexTitleService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CreditAssessmentIndexTitleService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }

        #region Retrieve data
        //public async Task<TData<List<CreditAssessmentIndexTitleEntity>>> GetList(int factorIndexId)
        //{
        //    TData<List<CreditAssessmentIndexTitleEntity>> obj = new TData<List<CreditAssessmentIndexTitleEntity>>();
        //    obj.Data = await _iUnitOfWork.CreditAssessmentIndexTitles.GetList(factorIndexId);
        //    obj.Total = obj.Data.Count;
        //    obj.Tag = 1;
        //    return obj;
        //}

        public async Task<List<CreditAssessmentIndexTitleEntity>> GetList(int factorIndexId)
        {
            List<CreditAssessmentIndexTitleEntity> obj = new List<CreditAssessmentIndexTitleEntity>();
            obj = await _iUnitOfWork.CreditAssessmentIndexTitles.GetList(factorIndexId);

            return obj;
        }

        public async Task<TData<CreditAssessmentIndexTitleEntity>> GetEntity(long id)
        {
            TData<CreditAssessmentIndexTitleEntity> obj = new TData<CreditAssessmentIndexTitleEntity>();
            obj.Data = await _iUnitOfWork.CreditAssessmentIndexTitles.GetEntity(id)
;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<CreditAssessmentIndexTitleEntity>> GetEntities(int id)
        {
            TData<CreditAssessmentIndexTitleEntity> obj = new TData<CreditAssessmentIndexTitleEntity>();

            try
            {
                CreditAssessmentIndexTitleEntity entity;

                if (id == 0)
                {
                    // Initialize a new entity for adding
                    entity = new CreditAssessmentIndexTitleEntity();
                }
                else
                {
                    // Get entity by ID for editing
                    entity = await _iUnitOfWork.CreditAssessmentIndexTitles.GetEntities(id);

                    if (entity == null)
                    {
                        obj.Message = "Entity not found.";
                        obj.Tag = -1;
                        return obj;
                    }

                    // Retrieve and assign the ProductName based on ProductCode
                    var productEntity = await _iUnitOfWork.CreditTypes.GetEntityByProductCode(entity.ProductCode);
                    var indextitle = await _iUnitOfWork.CreditAssessmentIndexTitles.GetEntities(id);
                    var factorindex = await _iUnitOfWork.CreditAssessmentFactorIndexes.GetEntitiesbyfactorIndexid(indextitle.FactorIndexId);
                    var riskfactor = await _iUnitOfWork.CreditAssessmentRiskFactors.GetEntitiesByRiskId(factorindex.RiskFactorId);
                    //entity.RiskFactor = _iUnitOfWork.CreditAssessmentRiskFactors.GetEntities(entity.RiskFactor).Result.RiskFactorsDescription;
                    if (productEntity != null)
                    {
                        entity.ProductName = productEntity.Name;
                        entity.FactorIndex = factorindex.FactorIndexDescription;
                        entity.RiskFactor = riskfactor.RiskFactorsDescription;
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
        public async Task<TData<string>> SaveForm(CreditAssessmentIndexTitleEntity entity)
        {
            TData<string> obj = new TData<string>();
            CreditScoreListParam param = new CreditScoreListParam();
            param.CreditType = entity.ProductCode;
            var productscoreInfo = _iUnitOfWork.CreditScores.GetList(param).Result.Select(i => i.RangeMax).Max();
            if (Convert.ToDecimal(entity.Weight) > productscoreInfo)
            {
                obj.Data = entity.Id.ToString();
                obj.Tag = 0;
                obj.Message = " Weight cannot be greater than the Product Max weight: " + productscoreInfo;
                return obj;
            }
            if (entity.Id == 0)
            {

                var getFactorIndexWeiht = _iUnitOfWork.CreditAssessmentIndexTitles.GetListbyProduct(entity.ProductCode).Result.ToList().Select(i => i.Weight).Sum(); 
                if ((Convert.ToDecimal(entity.Weight) + Convert.ToDecimal(getFactorIndexWeiht)) > productscoreInfo)
                {
                    obj.Data = entity.Id.ToString();
                    obj.Tag = 0;
                    obj.Message = " Weight cannot be greater than total Product weight: " + productscoreInfo;
                    return obj;

                }
                           }
            if (entity.Id > 0)
            {
                var getFactorIndexWeiht = _iUnitOfWork.CreditAssessmentIndexTitles.GetListbyProduct(entity.ProductCode).Result.Where(i=> i.Id != entity.Id).ToList().Select(i => i.Weight).Sum();
                var NewWeight = entity.Weight + getFactorIndexWeiht;
                if (Convert.ToDecimal(NewWeight) > productscoreInfo)
                {
                    obj.Data = entity.Id.ToString();
                    obj.Tag = 0;
                    obj.Message = "  Weight cannot be greater than the Product Risk weight : " + productscoreInfo;
                    return obj;

                }

            }

            var autoIncreament = _iUnitOfWork.CreditAssessmentIndexTitles.GetListbyProduct(null).Result.ToList();

            if (autoIncreament.Count == 0)
            {
                entity.IndexTitleId = 1;

            }
            else
            {
                var maxIndexTitleId = autoIncreament.Max(x => x.IndexTitleId);
                entity.IndexTitleId = ++maxIndexTitleId;

            }
            bool isUpdate = entity.Id > 0;
            await _iUnitOfWork.CreditAssessmentIndexTitles.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = isUpdate ? "Credit Assessment Index Title Updated successfully" : "Credit Assessment Index Title Added successfully";

            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.CreditAssessmentIndexTitles.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CreditAssessmentIndexTitleEntity>>> GetIndexTitle(int FactorIndexId)
        {
            TData<List<CreditAssessmentIndexTitleEntity>> obj = new TData<List<CreditAssessmentIndexTitleEntity>>();
            obj.Data = await _iUnitOfWork.CreditAssessmentIndexTitles.GetList(FactorIndexId);
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<CreditAssessmentIndexTitleEntity>>> GetPageList(CreditAssessmentIndexTitleListParam param, Pagination pagination)
        {
            try
            {
                TData<List<CreditAssessmentIndexTitleEntity>> obj = new TData<List<CreditAssessmentIndexTitleEntity>>();
                obj.Data = await _iUnitOfWork.CreditAssessmentIndexTitles.GetPageList(param, pagination);
                foreach (var item in obj.Data)
                {
                    item.FactorIndex = _iUnitOfWork.CreditAssessmentFactorIndexes.GetEntitiesbyfactorIndexid(item.FactorIndexId).Result?.FactorIndexDescription;

                }
                obj.Total = pagination.TotalCount;
                obj.Tag = 1;
                return obj;
            }catch(Exception ex)
            {
                throw;
            }
        }



        public async Task<TData<string>> UpdateForm(CreditAssessmentIndexTitleEntity entity)
        {
            TData<string> obj = new TData<string>();

            // Check if the entity exists in the database
            EmployeeEntity existingEntity = await _iUnitOfWork.Employees.GetEntity(entity.Id);
            if (existingEntity == null)
            {
                obj.Message = "Entity not found";
                obj.Tag = -1;
                return obj;
            }



            // Save the changes to the database
            await _iUnitOfWork.Employees.SaveForm(existingEntity);
            await _iUnitOfWork.CreditAssessmentIndexTitles.SaveForm(entity);

            obj.Data = existingEntity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }



        #endregion
    }
}