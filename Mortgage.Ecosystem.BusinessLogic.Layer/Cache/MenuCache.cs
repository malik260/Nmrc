using Mortgage.Ecosystem.DataAccess.Layer.Caching;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Cache
{
    public class MenuCache : BaseBusinessCache<MenuEntity>
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public MenuCache(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public override string CacheKey => this.GetType().Name;

        public override async Task<List<MenuEntity>> GetList()
        {
            var cacheList = CacheFactory.Cache.GetCache<List<MenuEntity>>(CacheKey);
            if (cacheList == null)
            {
                var list = await _iUnitOfWork.Menus.GetList(null);
                CacheFactory.Cache.SetCache(CacheKey, list);
                return list;
            }
            else
            {
                return cacheList;
            }
        }
    }
}
