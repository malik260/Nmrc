using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mortgage.Ecosystem.DataAccess.Layer
{
    public class GlobalContext
    {
        // All registered service and class instance container. Which are used for dependency injection.
        public static IServiceCollection? Services { get; set; }

        // Configured service provider.
        public static IServiceProvider? ServiceProvider { get; set; }

        public static IConfiguration? Configuration { get; set; }

        public static IWebHostEnvironment? HostingEnvironment { get; set; }

        public static SystemConfig? SystemConfig { get; set; }

        public static string GetVersion()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            return version.Major + "." + version.Minor;
        }

        // Set cache control
        // <param name="context"></param>
        public static void SetCacheControl(StaticFileResponseContext context)
        {
            int second = 365 * 24 * 60 * 60;
            context.Context.Response.Headers.Add("Cache-Control", new[] { "public,max-age=" + second });
            context.Context.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") }); // Format RFC1123
        }
    }
}
