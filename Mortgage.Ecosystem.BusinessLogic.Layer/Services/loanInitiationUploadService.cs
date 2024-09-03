using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class loanInitiationUploadService : ILoanInitiationUploadService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public loanInitiationUploadService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<LoanInitiationUploadEntity>>> GetList(long id)
        {
            TData<List<LoanInitiationUploadEntity>> obj = new TData<List<LoanInitiationUploadEntity>>();
            obj.Data = await _iUnitOfWork.LoanInitiationUploads.GetList(id);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LoanInitiationUploadEntity>>> GetPageList(LoanInitiationUploadListParam param, Pagination pagination)
        {
            TData<List<LoanInitiationUploadEntity>> obj = new TData<List<LoanInitiationUploadEntity>>();
            obj.Data = await _iUnitOfWork.LoanInitiationUploads.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<LoanInitiationUploadEntity>> GetEntity(long id)
        {
            TData<LoanInitiationUploadEntity> obj = new TData<LoanInitiationUploadEntity>();
            obj.Data = await _iUnitOfWork.LoanInitiationUploads.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(LoanInitiationUploadEntity entity)
        {
            TData<string> obj = new TData<string>();
            // Generate a random six-digit number
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);

            // Set the FileId with the generated random number
            entity.FileId = randomNumber;
            await _iUnitOfWork.LoanInitiationUploads.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.LoanInitiationUploads.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
