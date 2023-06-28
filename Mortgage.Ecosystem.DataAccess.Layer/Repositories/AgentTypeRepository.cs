using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class AgentTypeRepository : DataRepository, IAgentTypeRepository
    {
        #region Retrieve data
        public async Task<List<AgentTypeEntity>> GetList(AgentTypeListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<AgentTypeEntity>> GetPageList(AgentTypeListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<AgentTypeEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<AgentTypeEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(AgentTypeEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<AgentTypeEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<AgentTypeEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<AgentTypeEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<AgentTypeEntity, bool>> ListFilter(AgentTypeListParam param)
        {
            var expression = ExtensionLinq.True<AgentTypeEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(second: t => t.Name.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}