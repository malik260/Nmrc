namespace Mortgage.Ecosystem.Web.Authorization
{
    public static class IdentityServiceExtensions
    {
        //public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        //{
        //    services
        //        // If using Kestrel:
        //        .Configure<KestrelServerOptions>(options =>
        //        {
        //            options.AllowSynchronousIO = true;
        //            options.Limits.MaxRequestBodySize = int.MaxValue;
        //        })
        //        // If using IIS:
        //        .Configure<IISServerOptions>(options =>
        //        {
        //            options.AllowSynchronousIO = true;
        //            options.MaxRequestBodySize = int.MaxValue;
        //        })
        //        .AddIdentity<ApplicationUser, IdentityRole>()
        //        .AddEntityFrameworkStores<ApplicationDbContext>()
        //        .AddDefaultTokenProviders();

        //    services.AddAuthorization(options =>
        //    {
        //        // inline policies
        //        options.AddPolicy(UserType.User, policy =>
        //        {
        //            policy.RequireClaim(ApplicationClaimTypes.User, "Register");
        //            policy.RequireRole("MMBS");
        //        });
        //    });

        //    services.AddIdentityServer(options =>
        //    {
        //        options.Events.RaiseErrorEvents = true;
        //        options.Events.RaiseInformationEvents = true;
        //        options.Events.RaiseFailureEvents = true;
        //        options.Events.RaiseSuccessEvents = true;
        //        options.UserInteraction.LoginUrl = SystemConfig.AzureAd?.CallbackPath;
        //        options.UserInteraction.LogoutUrl = SystemConfig.AzureAd?.SignedOutCallbackPath;

        //        options.Authentication = new AuthenticationOptions()
        //        {
        //            CookieLifetime = new TimeSpan(5, 0, 0),
        //            CookieSlidingExpiration = false
        //        };
        //    });

        //    services.ConfigureApplicationCookie(o =>
        //    {
        //        o.Cookie.SameSite = SameSiteMode.None;
        //        o.ExpireTimeSpan = TimeSpan.FromHours(1);
        //        o.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        //    });

        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultScheme = SystemConfig.DefaultScheme;
        //        options.DefaultChallengeScheme = "oidc";
        //        options.DefaultAuthenticateScheme = "oidc";
        //    })
        //    .AddCookie("Main.Cookies", options =>
        //    {
        //        options.SlidingExpiration = false;
        //        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        //    })
        //    //.AddCookie("Main.Cookies", options => { options.AccessDeniedPath = "/account/accessdenied"; "https://localhost";})
        //    .AddOpenIdConnect("oidc", options =>
        //    {
        //        options.Authority = SystemConfig.AzureAd?.Authority;
        //        options.RequireHttpsMetadata = true;
        //        options.ClientId = SystemConfig.AzureAd?.ClientId;
        //        options.SignInScheme = SystemConfig.DefaultScheme;
        //        options.ResponseType = "code id_token";
        //        options.Scope.Clear();
        //        options.Scope.Add("openid");
        //        options.Scope.Add("profile");
        //        options.Scope.Add("email");
        //        options.Scope.Add("roles");
        //        options.GetClaimsFromUserInfoEndpoint = true;
        //        options.SaveTokens = true;
        //        options.ClientSecret = SystemConfig.AzureAd?.ClientSecret;
        //        options.SignOutScheme = SystemConfig.DefaultScheme;
        //        options.Events.OnSignedOutCallbackRedirect += context =>
        //        {
        //            context.Response.Redirect(context.Options.SignedOutRedirectUri);
        //            context.HandleResponse();
        //            return Task.CompletedTask;
        //        };

        //        options.Events = new OpenIdConnectEvents()
        //        {
        //            OnTokenValidated = tokenValidatedContext =>
        //            {
        //                return Task.FromResult(0);
        //            },
        //            OnUserInformationReceived = (context) =>
        //            {
        //                ClaimsIdentity claimsId = (ClaimsIdentity)context.Principal.Identity;
        //                try
        //                {
        //                    dynamic userClaim = JObject.Parse(context.User.ToString());
        //                    var roles = userClaim.role;
        //                    foreach (string role in roles)
        //                    {
        //                        claimsId.AddClaim(new Claim("role", role));
        //                    }
        //                }
        //                catch (Exception)
        //                {
        //                    //Users does not have roles
        //                }
        //                return Task.FromResult(0);
        //            }
        //        };

        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            NameClaimType = JwtClaimTypes.Name,
        //            RoleClaimType = JwtClaimTypes.Role
        //        };
        //    });

        //    return services;
        //}
    }
}
