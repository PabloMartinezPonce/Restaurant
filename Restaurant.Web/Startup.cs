using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Restaurant.Repository.Config;
using Restaurant.Repository.Interfaces;
using Restaurant.Web.Extentions;
using Restaurante.Data.DAO;
using Restaurante.Data.DBModels;
using Restaurante.Model;
using System;
using System.Globalization;

namespace Restaurant.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //Scaffold-DbContext "server=db4free.net;database=las_paseras_db;port=3306;user id=a78b82_lpb;password=LPB2021.;SslMode=none" MySql.EntityFrameworkCore -OutputDir ..\Restaurante.Data\DBModels -f
        //Scaffold-DbContext "server=db4free.net;database=las_paseras_db;port=3306;user id=a78b82_lpb;password=LPB2021.;SslMode=none" MySql.EntityFrameworkCore -Context restauranteContext -DataAnnotations -verbose -force -OutputDir ..\Restaurante.Model\EFModel -f - ContextDir ..\Restaurante.Model\EFModel

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("es-MX") // Convierte cada string a CultureInfo
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("es-MX");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddControllersWithViews();

            //Automapper
            services.AddAutoMapper(typeof(AutoMapperConfig));

            //INYECCIONES
            services.AddScoped<ICajaChicaDAO, CajaChicaDAO>();
            services.AddScoped<IMesasDAO, MesasDAO>();
            services.AddScoped<IProductosDAO, ProductosDAO>();
            services.AddScoped<ICuentasDAO, CuentasDAO>();
            services.AddScoped<ICategoriasDAO, CategoriasDAO>();
            services.AddScoped<IUsuariosDAO, UsuarioDAO>();
            services.AddScoped<IComplementosDAO, ComplementosDAO>();
            services.AddScoped<IConfiguracionDAO, ConfiguracionDAO>();
            services.AddScoped<ICortesDAO, CortesDAO>();
            services.AddScoped<IVentasDAO, VentasDAO>();

            //var sqlConnectionConfiguration = new SqlConfiguration(Configuration.GetConnectionString("SqlConnection"));
            //ENTITY
            //services.AddDbContext<restauranteContext>(options => options.UseMySql(Configuration.GetConnectionString("restauranteContext"), null));
            //services.AddDbContext<restauranteContext>(options =>
            //{
            //    options.UseMySql(Configuration.GetConnectionString("SqlConnection"), new MySqlServerVersion(new Version(8, 0, 21)));
            //});

            //SESSION
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1000);
            });

            // PWA 
            services.AddMvc();
            services.AddProgressiveWebApp();

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

            app.UseRequestLocalization();
            app.ConfigureCustomExceptionMiddleware();
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
