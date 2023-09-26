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





        public async Task<TData<CreditAssessmentFactorIndexEntity>> GetEntity(long id)
        {
            TData<CreditAssessmentFactorIndexEntity> obj = new TData<CreditAssessmentFactorIndexEntity>();
            obj.Data = await _iUnitOfWork.CreditAssessmentFactorIndexes.GetEntity(id)
;
            obj.Tag = 1;
            return obj;
        }


        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(CreditAssessmentFactorIndexEntity entity)
        {
            TData<string> obj = new TData<string>();
            try
            {
                var autoIncreament = _iUnitOfWork.CreditAssessmentFactorIndexes.GetList(entity.FactorIndexId).Result.ToList();

                if (autoIncreament.Count == 0)
                {
                    entity.FactorIndexId = 1;
                }
                else
                {
                    var maxFactorIndexId = autoIncreament.Max(x => x.FactorIndexId);
                    entity.FactorIndexId = ++maxFactorIndexId;
                }

                await _iUnitOfWork.CreditAssessmentFactorIndexes.SaveForm(entity);
                obj.Data = entity.FactorIndexId.ToString();
                obj.Tag = 1;
                obj.Message = "Factor Index added successfully";
                return obj;
            }

            catch (Exception ex)
            {

            }

            // In case of an exception, return the error response
            obj.Tag = -1;
            obj.Message = "An error occurred while saving the form";
            return obj;
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