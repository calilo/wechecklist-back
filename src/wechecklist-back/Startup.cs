using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using wechecklist_back.ORM;
using MySQL.Data.EntityFrameworkCore.Extensions;
using wechecklist_back.DataAccess;
using wechecklist_back.Middleware;
using wechecklist_back.Tools;

namespace wechecklist_back
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddDbContext<ChecklistDBContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySQLConnection")));

            services.Configure<AppConfig>(Configuration.GetSection("App"));

            services.AddMvc();

            services.AddTransient<IUser, DefaultUser>();
            services.AddTransient<IChecklistSubscriber, DefaultChecklistSubscriber>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ChecklistDBContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMiddleware<SlidingExpiryMiddleware>();

            app.UseMvc(routes=> {
                routes.MapRoute("wechat", "wechat/{action:regex(^index|userinfo$)}", defaults: new { controller = "Wechat" });
                routes.MapRoute("api routes", "api/{controller:regex(^checklist$)}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(context);
        }
    }
}
