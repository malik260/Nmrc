using Mortgage.Ecosystem.DataAccess.Layer.Caching;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Cache
{
    public class MenuAuthorizeCache : BaseBusinessCache<MenuAuthorizeEntity>
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public MenuAuthorizeCache(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public override string CacheKey => this.GetType().Name;

        public override async Task<List<MenuAuthorizeEntity>> GetList()
        {
            var cacheList = CacheFactory.Cache.GetCache<List<MenuAuthorizeEntity>>(CacheKey);
            if (cacheList == null)
            {
                var list = await _iUnitOfWork.MenuAuthorizes.GetList(null);
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