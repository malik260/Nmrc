using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class ContributionFrequencyService : IContributionFrequencyService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ContributionFrequencyService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ContributionFrequencyEntity>>> GetList(ContributionFrequencyListParam param)
        {
            TData<List<ContributionFrequencyEntity>> obj = new TData<List<ContributionFrequencyEntity>>();
            obj.Data = await _iUnitOfWork.ContributionFrequencies.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ContributionFrequencyEntity>>> GetPageList(ContributionFrequencyListParam param, Pagination pagination)
        {
            TData<List<ContributionFrequencyEntity>> obj = new TData<List<ContributionFrequencyEntity>>();
            obj.Data = await _iUnitOfWork.ContributionFrequencies.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ContributionFrequencyEntity>> GetEntity(long id)
        {
            TData<ContributionFrequencyEntity> obj = new TData<ContributionFrequencyEntity>();
            obj.Data = await _iUnitOfWork.ContributionFrequencies.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ContributionFrequencyEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.ContributionFrequencies.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ContributionFrequencies.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
