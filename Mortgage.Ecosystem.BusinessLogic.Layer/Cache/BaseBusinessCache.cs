using Mortgage.Ecosystem.DataAccess.Layer.Caching;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Cache
{
    public abstract class BaseBusinessCache<T>
    {
        public abstract string CacheKey { get; }

        public virtual bool Remove()
        {
            return CacheFactory.Cache.RemoveCache(CacheKey);
        }

        public virtual Task<List<T>> GetList()
        {
            throw new Exception("Please implement in subclass");
        }
    }
}