using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Restaurant.Web.Common;
using System;
using WebEssentials.AspNetCore.Pwa;

namespace Restaurant.Web
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

            //services.AddControllers().AddNewtonsoftJson(x =>
            //    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);

            //var sqlConnectionConfiguration = new SqlConfiguration(Configuration.GetConnectionString("SqlConnection"));
            //ENTITY
            //services.AddDbContext<restauranteContext>(options => options.UseMySql(Configuration.GetConnectionString("restauranteContext"), null));

            //SESSION
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(90);
            });

            // PWA 
            services.AddMvc();
            services.AddProgressiveWebApp();
            //services.AddProgressiveWebApp(new PwaOptions
            //{
            //    CacheId = "Worker 1.1",
            //    Strategy = ServiceWorkerStrategy.CacheFirst,
            //    RoutesToPreCache = "/Login/Dashboard, /Home/About",
            //    OfflineRoute = "/Shared/Offline",
            //});


            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton(sqlConnectionConfiguration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            app.UseSession();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");
            });
        }
    }
}
