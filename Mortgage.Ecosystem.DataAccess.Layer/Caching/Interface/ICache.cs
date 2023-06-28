namespace Mortgage.Ecosystem.DataAccess.Layer.Caching.Interface
{
    // Cache abstraction
    public interface ICache
    {
        // Read cache
        // <typeparam name="T">Type</typeparam>
        // <param name="key">key</param>
        // <returns></returns>
        T GetCache<T>(string key);

        // Write to the cache
        // <typeparam name="T">Type</typeparam>
        // <param name="key">key</param>
        // <param name="value">value</param>
        // <param name="expireTime">Expire Time</param>
        // <returns></returns>
        bool SetCache<T>(string key, T value, DateTime? expireTime = null);

        // Delete the cache
        // <param name="key">type</param>
        // <returns></returns>
        bool RemoveCache(string key);

        #region Hash

        // Read cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <param name="fieldKey"></param>
        // <returns></returns>
        T GetHashFieldCache<T>(string key, string fieldKey);

        // Read cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <returns></returns>
        List<T> GetHashToListCache<T>(string key);

        // Read cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <param name="dict"></param>
        // <returns></returns>
        Dictionary<string, T> GetHashFieldCache<T>(string key, Dictionary<string, T> dict);

        // Read cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <returns></returns>
        Dictionary<string, T> GetHashCache<T>(string key);

        // Write to the cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <param name="fieldKey"></param>
        // <param name="fieldValue"></param>
        // <returns></returns>
        int SetHashFieldCache<T>(string key, string fieldKey, T fieldValue);

        // Write to the cache
        // <typeparam name="T"></typeparam>
        // <param name="key"></param>
        // <param name="dict"></param>
        // <returns></returns>
        int SetHashFieldCache<T>(string key, Dictionary<string, T> dict);

        // Delete the cache
        // <param name="key"></param>
        // <param name="fieldKey"></param>
        // <returns></returns>
        bool RemoveHashFieldCache(string key, string fieldKey);

        // Delete the cache
        // <param name="key"></param>
        // <param name="dict"></param>
        // <returns></returns>
        Dictionary<string, bool> RemoveHashFieldCache(string key, Dictionary<string, bool> dict);

        #endregion
    }
}