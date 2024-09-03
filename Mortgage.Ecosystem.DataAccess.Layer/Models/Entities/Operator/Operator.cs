using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mortgage.Ecosystem.DataAccess.Layer.Caching;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers.Web;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator
{
    public class Operator
    {
        public static Operator Instance
        {
            get { return new Operator(); }
        }

        private string LoginProvider = GlobalContext.SystemConfig.LoginProvider;
        private string TokenName = "UserToken"; //cookie name or session name

        public async Task AddCurrent(string token)
        {
            switch (LoginProvider)
            {
                case "Cookie":
                    CookieHelper.Set(TokenName, token);
                    break;

                case "Session":
                    SessionHelper.Set(TokenName, token);
                    break;

                case "WebApi":
                    OperatorInfo user = await new DataRepository().GetUserByToken(token);
                    if (user != null)
                    {
                        CacheFactory.Cache.SetCache(token, user);
                    }
                    break;

                default:
                    throw new Exception("LoginProvider configuration not found");
            }
        }

        // Api interface needs to pass in apiToken
        // <param name="apiToken"></param>
        public void RemoveCurrent(string apiToken = "")
        {
            switch (LoginProvider)
            {
                case "Cookie":
                    CookieHelper.Remove(TokenName);
                    break;

                case "Session":
                    SessionHelper.Remove(TokenName);
                    break;

                case "WebApi":
                    CacheFactory.Cache.RemoveCache(apiToken);
                    break;

                default:
                    throw new Exception("LoginProvider configuration not found");
            }
        }

        // Api interface needs to pass in apiToken
        // <param name="apiToken"></param>
        // <returns></returns>
        public async Task<OperatorInfo> Current(string apiToken = "")
        {
            IHttpContextAccessor? hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            OperatorInfo? user = null;
            string? token = string.Empty;
            switch (LoginProvider)
            {
                case "Cookie":
                    if (hca.HttpContext != null)
                    {
                        token = CookieHelper.Get(TokenName);
                    }
                    break;

                case "Session":
                    if (hca.HttpContext != null)
                    {
                        token = SessionHelper.Get(TokenName);
                    }
                    break;

                case "WebApi":
                    token = apiToken;
                    break;
            }
            if (string.IsNullOrEmpty(token))
            {
                return user;
            }
            token = token.Trim('"');
            //user = CacheFactory.Cache.GetCache<OperatorInfo>(token);
            if (user == null)
            {
                user = await new DataRepository().GetUserByToken(token);
                if (user != null)
                {
                    CacheFactory.Cache.SetCache(token, user);
                }
            }
            return user;
        }
    }
}