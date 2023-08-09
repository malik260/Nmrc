using Microsoft.Extensions.Configuration;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers.Web
{
    // Config help
    public static class ConfigHelp
    {
        // Get the Config object
        // <returns></returns>
        public static IConfiguration GetObj()
        {
            return GlobalContext.Configuration;
        }

        // Read Config
        // <param name="sName">Name</param>
        // <returns>value</returns>
        public static T? Get<T>(string sName)
        {
            try
            {
                var config = GlobalContext.Configuration;
                if (string.IsNullOrEmpty(sName)) return default;
                var configValue = config.GetValue<T>(sName);
                if (configValue == null)
                {
                    var sectionValue = config.GetSection(sName);
                    configValue = (T)sectionValue.Get(typeof(T));
                }
                return configValue;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return default;
            }
        }

        // Read Config
        // <param name="sName">Name</param>
        // <returns>value</returns>
        public static string Get(string sName)
        {
            var configValue = Get<string>(sName);
            if (string.IsNullOrEmpty(configValue)) return "";
            return configValue;
        }
    }
}