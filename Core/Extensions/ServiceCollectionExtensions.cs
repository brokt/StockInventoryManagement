using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Configuration;
using Core.Utilities.Security.Encyption;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Routing;
using System.Text.Json.Serialization;
using Newtonsoft;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddDependencyResolvers(this IServiceCollection services, IConfiguration configuration, ICoreModule[] modules)
		{
			foreach (var module in modules)
			{
				module.Load(services, configuration);
			}
		}

        public static IServiceCollection AddCustomizedPages(this IServiceCollection services)
        {
            services.AddRazorPages()
                .AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
					o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AllowAnonymousToPage("/Auth");
                })
                .AddMvcOptions(options =>
                {
                    options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
                });

            services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.PropertyNamingPolicy = null;
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddSignalR();

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            return services;
        }

        public static IServiceCollection AddCustomizedAuthentication(this IServiceCollection services, IConfiguration configuration, string connectionString)
		{
			//var common = configuration.GetSection("Common");
			//var baseUrl = common.GetValue<string>("BaseUrl");
			//var audience = common.GetValue<string>("Audience");
			//var key = common.GetValue<string>("SecretKey");
			//var keyByteArray = Encoding.ASCII.GetBytes(key);
			//var signingKey = new SymmetricSecurityKey(keyByteArray);
			var tokenOptions = configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.Jwt.TokenOptions>();

			//services.AddIdentity<User, Role>(o =>
			//{
			//	o.Password.RequireDigit = false;
			//	o.Password.RequireLowercase = false;
			//	o.Password.RequireUppercase = false;
			//	o.Password.RequireNonAlphanumeric = false;
			//	o.Password.RequiredLength = 4;
			//})
			//	.AddEntityFrameworkStores<MaritzaDbContext>()
			//	.AddDefaultTokenProviders();

			services.AddAuthentication(options =>
			{
				options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			})
				.AddCookie(options =>
				{
					options.LoginPath = "/auth/login";
					options.Cookie.Name = "StockInventoryManagement";
					options.SlidingExpiration = true;
					options.ExpireTimeSpan = TimeSpan.FromDays(365);
				})
				.AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
						ValidAudience = tokenOptions.Audience,
						ValidIssuer = tokenOptions.Issuer,
						ValidateIssuerSigningKey = true,
						ValidateLifetime = true,
						ClockSkew = TimeSpan.FromDays(365),
						NameClaimType = "name",
						RoleClaimType = "role"
					};

					options.Events = new JwtBearerEvents
					{
						OnMessageReceived = (context) =>
						{
							Microsoft.Extensions.Primitives.StringValues token;
							if (context.Request.Path.Value.StartsWith("/broadcaster") && context.Request.Query.TryGetValue("token", out token))
							{
								context.Request.Headers.Add("Authorization", token);
								context.Token = token;
							}
							return Task.CompletedTask;
						},
						OnTokenValidated = (context) =>
						{

							try
							{
								//var username = context.Principal.FindFirstValue(ClaimTypes.Name);
								//var version = context.Principal.FindFirstValue(ClaimTypes.Version);

								//string userDeviceId = context.Principal.FindFirstValue("UserDeviceID");
								//string userId = context.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

								//if (userDeviceId != null)
								//{
								//	var deviceSql = "SELECT TOP 1 [Id] FROM [Auth].[UserDevice] WHERE [IsDeleted] = 0 AND [DeviceID] = '" + userDeviceId + "' AND [UserId] = " + userId;
								//	long? deviceId = deviceSql.ExecuteScalar<long>(connectionString);
								//	if (deviceId != null && deviceId > 0)
								//	{
								//		return Task.CompletedTask;
								//	}
								//}
								
							}
							catch (Exception) { }

							context.Fail(new UnauthorizedAccessException());
							return Task.CompletedTask;
						}
					};
				});

			services.Configure<SecurityStampValidatorOptions>(options =>
			{
				options.ValidationInterval = TimeSpan.Zero;
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("PanelRights", policy => policy.RequireClaim("Admin"));
				//options.AddPolicy("PanelRights", policy => policy.RequireRole("systemadmin", "admin", "editor"));
			});

			return services;
		}
	}
}