using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers.Web
{
    // Cookie help
    public static class CookieHelper
    {
        // Get the Cookie object
        // <returns></returns>
        public static IResponseCookies? GetObj()
        {
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            return hca?.HttpContext?.Response.Cookies;
        }

        // Write cookie
        // <param name="sName">Name</param>
        // <param name="sValue">value</param>
        // <param name="httpOnly">Can the front-end script get the cookie</param>
        // <returns>Status</returns>
        public static bool Set(string sName, string sValue, bool httpOnly = true)
        {
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            var option = new CookieOptions
            {
                Expires = DateTime.MaxValue,
                HttpOnly = httpOnly,
            };
            hca?.HttpContext?.Response.Cookies.Append(sName, sValue, option);
            return true;
        }

        // Write cookie
        // <param name="sName">Name</param>
        // <param name="sValue">value</param>
        // <param name="expires">Expiration time (minutes)</param>
        // <param name="httpOnly">Can the front-end script get the cookie</param>
        // <returns>Status</returns>
        public static bool Set(string sName, string sValue, int expires, bool httpOnly = true)
        {
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(expires),
                HttpOnly = httpOnly,
            };
            hca?.HttpContext?.Response.Cookies.Append(sName, sValue, option);
            return true;
        }

        // Write a temporary cookie
        // <param name="sName">Name</param>
        // <param name="sValue">value</param>
        // <param name="httpOnly">Can the front-end script get the cookie</param>
        // <returns>Status</returns>
        public static bool SetTemp(string sName, string sValue, bool httpOnly = true)
        {
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            var option = new CookieOptions
            {
                HttpOnly = httpOnly,
            };
            hca?.HttpContext?.Response.Cookies.Append(sName, sValue, option);
            return true;
        }

        // Read cookies
        // <param name="sName">Name</param>
        // <returns>value</returns>
        public static string? Get(string sName)
        {
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            return hca?.HttpContext?.Request.Cookies[sName];
        }

        // Delete cookies
        // <param name="sName">Cookie object name</param>
        // <returns>Status</returns>
        public static bool Remove(string sName)
        {
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            hca?.HttpContext?.Response.Cookies.Delete(sName);
            return true;
        }

        // Clear cookies
        // <returns>Status</returns>
        public static bool Clear()
        {
            var hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            var request = hca?.HttpContext?.Request; //ask            
            var response = hca?.HttpContext?.Response; //response
            //loop through all cookies
            foreach (var cookie in request.Cookies)
            {
                var key = cookie.Key;
                //var value = cookie.Value;
                response.Cookies.Delete(key);
            }
            return true;
        }
    }
}