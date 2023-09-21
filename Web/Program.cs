using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Refit;
using Web.Filters;
using Web.Infrastructure.Handlers;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCustomizedPages();
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(MyCustomExceptionFilter));
            });
          
            builder.Services.AddFlashes();
            builder.Services.AddCustomizedAuthentication(builder.Configuration, "");

            builder.Services.AddKendo();
          
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddTransient<AuthHeaderHandler>();
            builder.Services
                .AddRefitClient<IStockInventoryMangement>()

                .AddHttpMessageHandler<AuthHeaderHandler>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://localhost:5001");
                    c.Timeout = TimeSpan.FromMinutes(1);


                });


            var app = builder.Build();
            ServiceTool.ServiceProvider = app.Services;
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePagesWithRedirects("~/error/{0}");

            app.MapRazorPages();

            app.Run();
        }
    }
}