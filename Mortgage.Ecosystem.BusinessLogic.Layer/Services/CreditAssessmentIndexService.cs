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
    public class CreditAssessmentIndexService : ICreditAssessmentIndexService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CreditAssessmentIndexService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }

        #region Retrieve data
        public async Task<List<CreditAssessmentIndexEntity>> GetList(int indexTitleId)
        {
            List<CreditAssessmentIndexEntity> obj = new List<CreditAssessmentIndexEntity>();
            obj = await _iUnitOfWork.CreditAssessmentIndexes.GetList(indexTitleId);

            return obj;
        }

        public async Task<TData<List<CreditAssessmentIndexEntity>>> GetPageList(CreditAssessmentIndexListParam param, Pagination pagination)
        {
            TData<List<CreditAssessmentIndexEntity>> obj = new TData<List<CreditAssessmentIndexEntity>>();
            obj.Data = await _iUnitOfWork.CreditAssessmentIndexes.GetPageList(param, pagination);
            foreach (var item in obj.Data)
            {
                item.IndexTitle =  _iUnitOfWork.CreditAssessmentIndexTitles.GetEntityByIndextitleId(item.Indextitleid).Result.IndexTitleDescription;
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<CreditAssessmentIndexEntity>> GetEntity(long id)
        {
            TData<CreditAssessmentIndexEntity> obj = new TData<CreditAssessmentIndexEntity>();
            obj.Data = await _iUnitOfWork.CreditAssessmentIndexes.GetEntity(id)
;
            obj.Tag = 1;
            return obj;
        }


        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(CreditAssessmentIndexEntity entity)

        {
            var autoIncreament = _iUnitOfWork.CreditAssessmentIndexes.GetList(entity.Indexid).Result.ToList();

            if (autoIncreament.Count == 0)
            {
                entity.Indexid = 1;

            }
            else
            {
                var maxIndexId = autoIncreament.Max(x => x.Indexid);
                entity.Indexid = ++maxIndexId;

            }


            TData<string> obj = new TData<string>();
            await _iUnitOfWork.CreditAssessmentIndexes.SaveForm(entity);
            obj.Data = entity.Indexid.ParseToString();
            obj.Tag = 1;
            obj.Message = "Credit Assessment Index Title added successfully";
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.CreditAssessmentIndexes.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> UpdateForm(CreditAssessmentIndexEntity entity)
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
            await _iUnitOfWork.CreditAssessmentIndexes.SaveForm(entity);

            obj.Data = existingEntity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }



        #endregion
    }
}