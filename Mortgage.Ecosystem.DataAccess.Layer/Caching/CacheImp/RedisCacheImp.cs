﻿using Mortgage.Ecosystem.DataAccess.Layer.Caching.Interface;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Mortgage.Ecosystem.DataAccess.Layer.Caching.CacheImp
{
    // Redis cache
    public class RedisCacheImp : ICache
    {
        // Redis cache
        private ConnectionMultiplexer connection;

        // When Redis caches an instance
        public RedisCacheImp()
        {
            if (connection == null)
            {
                var redisConnectionString = GlobalContext.SystemConfig?.RedisConnectionString;
                var configuration = ConfigurationOptions.Parse(redisConnectionString, true);
                configuration.ResolveDns = true;
                connection = ConnectionMultiplexer.Connect(configuration);
            }
        }

        // Read cache
        // <typeparam name="T">Type</typeparam>
        // <param name="key">key</param>
        // <returns></returns>
        public T GetCache<T>(string key)
        {
            var t = default(T);
            try
            {
                var cache = connection.GetDatabase();
                var value = cache.StringGet(key);
                if (string.IsNullOrEmpty(value)) return t;
                //
                t = JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return t;
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
                var cache = connection.GetDatabase();
                var jsonOption = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var strValue = JsonConvert.SerializeObject(value, jsonOption);
                if (string.IsNullOrEmpty(strValue)) return false;
                //
                if (expireTime == null) return cache.StringSet(key, strValue);
                else return cache.StringSet(key, strValue, expireTime.Value - DateTime.Now);
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
            var cache = connection.GetDatabase();
            return cache.KeyDelete(key);
        }

        #region Hash

        // Read cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <param name="fieldKey"></param>
        // <returns></returns>
        public T GetHashFieldCache<T>(string key, string fieldKey)
        {
            var dict = GetHashFieldCache(key, new Dictionary<string, T> { { fieldKey, default } });
            return dict[fieldKey];
        }

        // Read cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <returns></returns>
        public List<T> GetHashToListCache<T>(string key)
        {
            var list = new List<T>();
            var cache = connection.GetDatabase();
            var hashFields = cache.HashGetAll(key);
            foreach (var field in hashFields)
            {
                list.Add(JsonConvert.DeserializeObject<T>(field.Value));
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
            var cache = connection.GetDatabase();
            foreach (var fieldKey in dict.Keys)
            {
                var fieldValue = cache.HashGet(key, fieldKey);
                dict[fieldKey] = JsonConvert.DeserializeObject<T>(fieldValue);
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
            var cache = connection.GetDatabase();
            var hashFields = cache.HashGetAll(key);
            foreach (var field in hashFields)
            {
                dict[field.Name] = JsonConvert.DeserializeObject<T>(field.Value);
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
            var cache = connection.GetDatabase();
            var jsonOption = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            foreach (var fieldKey in dict.Keys)
            {
                var fieldValue = JsonConvert.SerializeObject(dict[fieldKey], jsonOption);
                count += cache.HashSet(key, fieldKey, fieldValue) ? 1 : 0;
            }
            return count;
        }

        // Delete the cache
        // <param name="key"></param>
        // <param name="fieldKey"></param>
        // <returns></returns>
        public bool RemoveHashFieldCache(string key, string fieldKey)
        {
            var dict = new Dictionary<string, bool> { { fieldKey, false } };
            dict = RemoveHashFieldCache(key, dict);
            return dict[fieldKey];
        }

        // Delete the cache
        // <param name="key"></param>
        // <param name="dict"></param>
        // <returns></returns>
        public Dictionary<string, bool> RemoveHashFieldCache(string key, Dictionary<string, bool> dict)
        {
            var cache = connection.GetDatabase();
            foreach (var fieldKey in dict.Keys)
            {
                dict[fieldKey] = cache.HashDelete(key, fieldKey);
            }
            return dict;
        }

        #endregion

        // Release the object
        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
            }
            GC.SuppressFinalize(this);
        }
    }
}