using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class PropertyUploadService : IPropertyUploadService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public PropertyUploadService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<PropertyUploadEntity>>> GetList(long id)
        {
            TData<List<PropertyUploadEntity>> obj = new TData<List<PropertyUploadEntity>>();
            obj.Data = await _iUnitOfWork.PropertyUploads.GetList(id);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<PropertyUploadEntity>>> GetPropertyList(PropertyUploadListParam param)
        {
            TData<List<PropertyUploadEntity>> obj = new TData<List<PropertyUploadEntity>>();
            obj.Data = await _iUnitOfWork.PropertyUploads.GetList2(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

       

        public async Task<TData<List<PropertyUploadEntity>>> GetPageList(PropertyUploadListParam param, Pagination pagination)
        {
            TData<List<PropertyUploadEntity>> obj = new TData<List<PropertyUploadEntity>>();
            obj.Data = await _iUnitOfWork.PropertyUploads.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<PropertyUploadEntity>> GetEntity(long id)
        {
            TData<PropertyUploadEntity> obj = new TData<PropertyUploadEntity>();
            obj.Data = await _iUnitOfWork.PropertyUploads.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(PropertyUploadEntity entity)
        {
            TData<string> obj = new TData<string>();
            // Generate a random six-digit number
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);

            // Set the ParselId with the generated random number
            entity.ParselId = randomNumber;
            await _iUnitOfWork.PropertyUploads.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.PropertyUploads.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
