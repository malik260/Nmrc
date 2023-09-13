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
        public async Task<TData<List<CreditAssessmentIndexTitleEntity>>> GetList(int factorIndexId)
        {
            TData<List<CreditAssessmentIndexTitleEntity>> obj = new TData<List<CreditAssessmentIndexTitleEntity>>();
            obj.Data = await _iUnitOfWork.CreditAssessmentIndexTitles.GetList(factorIndexId);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
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


        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(CreditAssessmentIndexTitleEntity entity)
        {

            var autoIncreament = _iUnitOfWork.CreditAssessmentIndexTitles.GetList(entity.IndexTitleId).Result.ToList();

            if (autoIncreament.Count == 0)
            {
                entity.IndexTitleId = 1;

            }
            else
            {
                var maxIndexTitleId = autoIncreament.Max(x => x.IndexTitleId);
                entity.IndexTitleId = ++maxIndexTitleId;

            }

            TData<string> obj = new TData<string>();
            await _iUnitOfWork.CreditAssessmentIndexTitles.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "Credit Assessment Index Title added successfully";
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.CreditAssessmentIndexTitles.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
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