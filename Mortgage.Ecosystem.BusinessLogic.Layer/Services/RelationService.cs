using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class RelationService : IRelationService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public RelationService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data

        public async Task<TData<List<RelationEntity>>> GetList(RelationListParam param)
        {
            TData<List<RelationEntity>> obj = new TData<List<RelationEntity>>();
            obj.Data = await _iUnitOfWork.Relations.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RelationEntity>>> GetPageList(RelationListParam param, Pagination pagination)
        {
            TData<List<RelationEntity>> obj = new TData<List<RelationEntity>>();
            obj.Data = await _iUnitOfWork.Relations.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<RelationEntity>> GetEntity(long id)
        {
            TData<RelationEntity> obj = new TData<RelationEntity>();
            obj.Data = await _iUnitOfWork.Relations.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        #endregion Retrieve data

        #region Submit data

        public async Task<TData<string>> SaveForm(RelationEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Relations.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Relations.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        #endregion Submit data
    }
}