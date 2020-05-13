using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Utilities;

namespace UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
 
            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Session
            services.AddSession();
            //Cookie
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false; //设置false可以打开cookie
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
         
            }
            MyServiceProvider.ServiceProvider = app.ApplicationServices;
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"name=\"{env.ApplicationName}\"");
                await context.Response.WriteAsync($"name=\"{env.ContentRootFileProvider}\"");
                await context.Response.WriteAsync($"name=\"{env.ContentRootPath}\"");
                await context.Response.WriteAsync($"name=\"{env.EnvironmentName}\"");
                await context.Response.WriteAsync($"name=\"{env.WebRootFileProvider}\"");   //不一一输出了  原理一样的
                //await context.Response.WriteAsync($"connectionString=\"{configuration["connectionString:defaultConnectionString"]}\"");
                //await context.Response.WriteAsync($"name=\"{configuration["name"]}\"");
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
