using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Helpers
{
    internal class ThreadSafeMemoryCacheHelper<TItem>
    {
        private readonly MemoryCache _cache = new(new MemoryCacheOptions());
        private readonly ConcurrentDictionary<object, SemaphoreSlim> _locks = new();

        public async Task<TItem?> Get(object key)
        {
            if (!_cache.TryGetValue(key, out TItem cacheEntry))
            {
                return default;
            }
            return cacheEntry;
        }

        public async Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem, TimeSpan? cacheEntryLifespan = null)
        {
            if (!cacheEntryLifespan.HasValue)
            {
                // Cache entries for 8 hours by default
                cacheEntryLifespan = TimeSpan.FromHours(8);
            }

            if (!_cache.TryGetValue(key, out TItem cacheEntry))// Look for cache key.
            {
                SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

                await mylock.WaitAsync();
                try
                {
                    if (!_cache.TryGetValue(key, out cacheEntry))
                    {
                        // Key not in cache, so get data.
                        cacheEntry = await createItem();
                        if (cacheEntry != null)
                        {
                            _cache.Set(key, cacheEntry, cacheEntryLifespan.Value);
                        }
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }
            return cacheEntry;
        }
    }
}
