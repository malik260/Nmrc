using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileProviders;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Caching;
using Mortgage.Ecosystem.DataAccess.Layer.Configurations;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Settings;
using Mortgage.Ecosystem.Web.Authorization;
using Mortgage.Ecosystem.Web.Filter;
using NLog.Web;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Mortgage.Ecosystem.Web
{
    // Program entry
    public static class Program
    {
        // Program entry
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Application Settings
            SystemConfig.ApplicationPortal = builder.Configuration.GetSection("ApplicationPortal").Get<ApplicationPortal>();
            SystemConfig.WebHost = builder.Configuration.GetSection("WebHost").Get<WebHost>();
            SystemConfig.Database = builder.Configuration.GetSection("Database").Get<Database>();
            SystemConfig.FileProvider = builder.Configuration.GetSection("FileProvider").Get<FileProvider>();
            SystemConfig.Google = builder.Configuration.GetSection("Google").Get<GoogleConfig>();
            SystemConfig.Jwt = builder.Configuration.GetSection("Jwt").Get<Jwt>();
            SystemConfig.CacheConfiguration = builder.Configuration.GetSection("CacheConfiguration").Get<CacheConfiguration>();
            SystemConfig.AzureAd = builder.Configuration.GetSection("AzureAd").Get<AzureAd>();
            SystemConfig.Origins = builder.Configuration.GetSection("Origins").Get<string>();
            SystemConfig.DefaultScheme = builder.Configuration.GetSection("DefaultScheme").Get<string>();
            SystemConfig.SecurityHeaders = builder.Configuration.GetSection("SecurityHeaders").Get<SecurityHeaders>();
            GlobalContext.SystemConfig = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();

            //Service
            builder.Services.ConfigureServices();

            //Injection
            //builder.Services.AddInjection();

            //App
            var app = builder.Build();
            app.Configure();

            //Run
            app.Run();
        }

        // This method is called by the runtime
        // Use this method to add the service to the container
        // <param name="services">Services</param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddPortalSettings();

            //Add Razor runtime compilation
            services.AddRazorPages().AddRazorRuntimeCompilation();

            //log component
            services.AddLogging(logging =>
            {
                logging.AddNLog("nlog.config");
            });

            //Add Memory cache function
            services.AddMemoryCache();

            //Start Session
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AspNetCore.Session";
                //options.IdleTimeout = TimeSpan.FromDays(7); //Set the expiration time of the Session
                options.IdleTimeout = TimeSpan.FromSeconds(60); //Set the expiration time of the Session
                options.Cookie.HttpOnly = true; //Set the value of the cookie that cannot be obtained by js in the browser
                options.Cookie.IsEssential = true; //Enable cookies
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Home/Login";
                        options.AccessDeniedPath = "/Home/NoPermission";
                        options.LogoutPath = "/Home/Login?l=1";
                        options.SlidingExpiration = true;
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                    });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether a given request requires user consent for non-essential cookies
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Add Options
            services.AddOptions();

            //add MVC
            var mvcBuilder = services.AddMvc();

            //Add HttpContext accessor
            services.AddHttpContextAccessor();

            //add filter controller
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.ModelMetadataDetailsProviders.Add(new ModelBindingMetadataProvider());
            });

            //return the first letter of the data
            mvcBuilder.AddJsonOptions(options =>
            {
                //PropertyNamingPolicy = null does not change by default
                //PropertyNamingPolicy = JsonNamingPolicy.CamelCase default lowercase
                //https://docs.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions.propertynamingpolicy?view=net-6.0
                // return the first word of the data unchanged
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                // format time
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                //Long to string
                options.JsonSerializerOptions.Converters.Add(new LongJsonConverter());
                //Cancel Unicode encoding
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                //null value is not returned to the front end
                //options.JsonSerializerOptions.IgnoreNullValues ​​= true;
                // allow extra symbols
                //options.JsonSerializerOptions.AllowTrailingCommas = true;
                //Whether the property name uses case-insensitive comparison during deserialization
                //options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
            });

            GlobalContext.Services = services;
        }

        // This method is called by the runtime
        // Use this method to configure the HTTP request pipeline
        // <param name="app">Apply</param>
        public static void Configure(this WebApplication app)
        {
            //config object
            GlobalContext.Configuration = app.Configuration;

            //service provider
            GlobalContext.ServiceProvider = app.Services;

            //host environment
            GlobalContext.HostingEnvironment = app.Environment;

            // Determine the operating mode
            if (app.Environment.IsDevelopment())
            {
                //The development environment displays the error stack page
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Official environment custom error page
                app.UseExceptionHandler("/Help/Error");

                //Capture the global request
                app.Use(async (context, next) =>
                {
                    await next();
                    //401 error
                    if (context.Response.StatusCode == 401)
                    {
                        context.Request.Path = "/Admin/Index";
                        await next();
                    }
                    //404 error
                    if (context.Response.StatusCode == 404)
                    {
                        context.Request.Path = "/Help/Error";
                        await next();
                    }
                    //500 error
                    if (context.Response.StatusCode == 500)
                    {
                        context.Request.Path = "/Help/Error";
                        await next();
                    }
                });
            }

            if (!string.IsNullOrEmpty(GlobalContext.SystemConfig?.VirtualDirectory))
            {
                //Let the Pathbase middleware be the first middleware to process the request in order to simulate the virtual path correctly
                app.UsePathBase(new PathString(GlobalContext.SystemConfig.VirtualDirectory));
            }

            //static directory
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = GlobalContext.SetCacheControl
            });

            //custom static directory
            string resource = Path.Combine(app.Environment.ContentRootPath, "Resource");
            FileHelper.CreateDirectory(resource);
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/Resource",
                FileProvider = new PhysicalFileProvider(resource),
                OnPrepareResponse = GlobalContext.SetCacheControl
            });

            // user route
            app.UseRouting();

            //User authentication
            app.UseAuthorization();

            //User Session
            app.UseSession();

            //User default route
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Dependency injection
        // <param name="services">Services</param>
        public static void AddInjection(this IServiceCollection services)
        {
            //database context
            services.AddDbContext<ApplicationDbContext>();
        }
    }
}
