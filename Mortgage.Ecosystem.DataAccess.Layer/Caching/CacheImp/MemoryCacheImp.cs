using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Mortgage.Ecosystem.DataAccess.Layer.Caching.Interface;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;

namespace Mortgage.Ecosystem.DataAccess.Layer.Caching.CacheImp
{
    // Memory cache
    public class MemoryCacheImp : ICache
    {
        // Memory cache
        private IMemoryCache cache;

        // When Memory caches instances
        public MemoryCacheImp()
        {
            cache = GlobalContext.ServiceProvider.GetService<IMemoryCache>();
        }

        // Read cache
        // <typeparam name="T">Type</typeparam>
        // <param name="key">key</param>
        // <returns></returns>
        public T GetCache<T>(string key)
        {
            var value = cache.Get<T>(key);
            return value;
        }

        // Write to the cache
        // <typeparam name="T">Type</typeparam>
        // <param name="key">key</param>
        // <param name="value">value</param>
        // <param name="expireTime">Expire Time</param>
        // <returns></returns>
        public bool SetCache<T>(string key, T value, DateTime? expireTime = null)
        {
            try
            {
                if (expireTime == null) cache.Set(key, value);
                else cache.Set(key, value, expireTime.Value - DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return false;
        }

        // Delete the cache
        // <param name="key">type</param>
        // <returns></returns>
        public bool RemoveCache(string key)
        {
            cache?.Remove(key);
            return true;
        }

        #region Hash

        // Read cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <param name="fieldKey"></param>
        // <returns></returns>
        public T GetHashFieldCache<T>(string key, string fieldKey)
        {
            var dic = new Dictionary<string, T> { { fieldKey, default } };
            var dict = GetHashFieldCache(key, dic);
            return dict[fieldKey];
        }

        // read cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <returns></returns>
        public List<T> GetHashToListCache<T>(string key)
        {
            var list = new List<T>();
            var hashFields = cache.Get<Dictionary<string, T>>(key);
            foreach (var field in hashFields.Keys)
            {
                list.Add(hashFields[field]);
            }
            return list;
        }

        // Read cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <param name="dict"></param>
        // <returns></returns>
        public Dictionary<string, T> GetHashFieldCache<T>(string key, Dictionary<string, T> dict)
        {
            var hashFields = cache.Get<Dictionary<string, T>>(key);
            foreach (var keyValuePair in hashFields.Where(p => dict.Keys.Contains(p.Key)))
            {
                dict[keyValuePair.Key] = keyValuePair.Value;
            }
            return dict;
        }

        // Read cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <returns></returns>
        public Dictionary<string, T> GetHashCache<T>(string key)
        {
            var dict = new Dictionary<string, T>();
            var hashFields = cache.Get<Dictionary<string, T>>(key);
            foreach (var field in hashFields.Keys)
            {
                dict[field] = hashFields[field];
            }
            return dict;
        }

        // Write to the cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <param name="fieldKey"></param>
        // <param name="fieldValue"></param>
        // <returns></returns>
        public int SetHashFieldCache<T>(string key, string fieldKey, T fieldValue)
        {
            return SetHashFieldCache(key, new Dictionary<string, T> { { fieldKey, fieldValue } });
        }

        // Write to the cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <param name="dict"></param>
        // <returns></returns>
        public int SetHashFieldCache<T>(string key, Dictionary<string, T> dict)
        {
            var count = 0;
            foreach (string fieldKey in dict.Keys)
            {
                count += cache.Set(key, dict) != null ? 1 : 0;
            }
            return count;
        }

        // Delete cache        
        // <param name="key"></param>
        // <param name="fieldKey"></param>
        // <returns></returns>
        public bool RemoveHashFieldCache(string key, string fieldKey)
        {
            var dict = new Dictionary<string, bool> { { fieldKey, false } };
            dict = RemoveHashFieldCache(key, dict);
            return dict[fieldKey];
        }

        // Delete cache
        // <param name="key"></param>
        // <param name="dict"></param>
        // <returns></returns>
        public Dictionary<string, bool> RemoveHashFieldCache(string key, Dictionary<string, bool> dict)
        {
            var hashFields = cache.Get<Dictionary<string, object>>(key);
            foreach (var fieldKey in dict.Keys)
            {
                dict[fieldKey] = hashFields.Remove(fieldKey);
            }
            return dict;
        }

        #endregion
    }
}