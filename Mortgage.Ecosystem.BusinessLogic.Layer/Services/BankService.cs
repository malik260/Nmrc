using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class BankService : IBankService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public BankService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<BankEntity>>> GetList(BankListParam param)
        {
            TData<List<BankEntity>> obj = new TData<List<BankEntity>>();
            obj.Data = await _iUnitOfWork.Banks.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<BankEntity>>> GetPageList(BankListParam param, Pagination pagination)
        {
            TData<List<BankEntity>> obj = new TData<List<BankEntity>>();
            obj.Data = await _iUnitOfWork.Banks.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<BankEntity>> GetEntity(long id)
        {
            TData<BankEntity> obj = new TData<BankEntity>();
            obj.Data = await _iUnitOfWork.Banks.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(BankEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Banks.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Banks.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
