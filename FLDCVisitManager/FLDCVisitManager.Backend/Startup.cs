using FLDCVisitManagerBackend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using FLDCVisitManager.CMSDataLayar;
using DBManager;
using FLDCVisitManagerBackend.BL;
using System.IO;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using FLDCVisitManager.Backend.Hubs;

namespace FLDCVisitManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var dom = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .Build(); ;
            Configuration = dom;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IConfigurationSection cmsSettings = Configuration.GetSection("CMSOptions");
            services.Configure<AppOptionsConfiguration>(cmsSettings);
            services.Configure<DatabaseOptions>(Configuration.GetSection("ConnectionStrings"));
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<ICMSDataHelper, SquidexHelper>();
            services.AddTransient<IDBManager, SQLHelper>();
            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "backendclient/build";
            });
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ClientHub>("/clientHub");
            });

/*            app.UseHttpsRedirection();*/
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Join(env.ContentRootPath, "backendclient");

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
