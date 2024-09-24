using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class AgentTypeService : IAgentTypeService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public AgentTypeService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<AgentTypeEntity>>> GetList(AgentTypeListParam param)
        {
            TData<List<AgentTypeEntity>> obj = new TData<List<AgentTypeEntity>>();
            obj.Data = await _iUnitOfWork.AgentTypes.GetList(param);
            var Lenders = obj.Data.Where(x => x.Id == GlobalConstant.SIX).FirstOrDefault();
            obj.Data.RemoveAll(x => x.Id == GlobalConstant.SIX);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AgentTypeEntity>>> GetPageList(AgentTypeListParam param, Pagination pagination)
        {
            TData<List<AgentTypeEntity>> obj = new TData<List<AgentTypeEntity>>();
            obj.Data = await _iUnitOfWork.AgentTypes.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<AgentTypeEntity>> GetEntity(long id)
        {
            TData<AgentTypeEntity> obj = new TData<AgentTypeEntity>();
            obj.Data = await _iUnitOfWork.AgentTypes.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(AgentTypeEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.AgentTypes.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.AgentTypes.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
