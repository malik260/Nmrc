using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers.Web
{
    // Session help
    public static class SessionHelper
    {
        // Get the Session object
        // <returns></returns>
        public static ISession? GetObj()
        {
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            return hca?.HttpContext?.Session;
        }

        // Write to Session
        // <typeparam name="T">Session key value type</typeparam>
        // <param name="key">Session key name</param>
        // <param name="value">Session key value</param>
        // <returns>Status</returns>
        public static bool Set<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key)) return false;
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            hca?.HttpContext?.Session.SetString(key, JsonSerializer.Serialize(value));
            return true;
        }

        // Write to Session
        // <param name="key">Session key name</param>
        // <param name="value">Session key value</param>
        // <returns>Status</returns>
        public static bool Set(string key, string value)
        {
            return Set<string>(key, value);
        }

        // Read Session
        // <param name="key">Session key name</param>
        // <returns>value</returns>
        public static T? Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key)) return default;
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            var sessionStr = hca?.HttpContext?.Session.GetString(key);
            if (string.IsNullOrEmpty(sessionStr)) return default;
            return JsonSerializer.Deserialize<T>(sessionStr);
        }

        // Read Session
        // <param name="key">Session key name</param>
        // <returns>value</returns>
        public static string? Get(string key)
        {
            return Get<string>(key);
        }

        // delete the session
        // <param name="key">Session key name</param>
        // <returns>Status</returns>
        public static bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            hca?.HttpContext?.Session.Remove(key);
            return true;
        }

        // Clear the Session
        // <returns>Status</returns>
        public static bool Clear()
        {
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            hca?.HttpContext?.Session.Clear();
            return true;
        }
    }
}