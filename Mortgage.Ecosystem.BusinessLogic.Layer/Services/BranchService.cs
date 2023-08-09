using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public BranchService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<BranchEntity>>> GetList(BranchListParam param)
        {
            TData<List<BranchEntity>> obj = new TData<List<BranchEntity>>();
            obj.Data = await _iUnitOfWork.Branches.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<BranchEntity>>> GetPageList(BranchListParam param, Pagination pagination)
        {
            TData<List<BranchEntity>> obj = new TData<List<BranchEntity>>();
            obj.Data = await _iUnitOfWork.Branches.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<BranchEntity>> GetEntity(long id)
        {
            TData<BranchEntity> obj = new TData<BranchEntity>();
            obj.Data = await _iUnitOfWork.Branches.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(BranchEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Branches.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Branches.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
