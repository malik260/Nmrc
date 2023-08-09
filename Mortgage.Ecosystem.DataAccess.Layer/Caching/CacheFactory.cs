using Mortgage.Ecosystem.DataAccess.Layer.Caching.CacheImp;
using Mortgage.Ecosystem.DataAccess.Layer.Caching.Interface;

namespace Mortgage.Ecosystem.DataAccess.Layer.Caching
{
    // Cache factory
    public class CacheFactory
    {
        // ICache
        private static ICache? cache = null;

        // Lock
        private static readonly object lockHelper = new();

        // ICache
        public static ICache Cache
        {
            get
            {
                if (cache != null) return cache;
                lock (lockHelper)
                {
                    if (cache != null) return cache;
                    var cacheProvider = GlobalContext.SystemConfig?.CacheProvider;
                    return cache = cacheProvider?.ToLower() switch
                    {
                        "redis" => new RedisCacheImp(),
                        _ => new MemoryCacheImp(),
                    };
                }
            }
        }
    }
}