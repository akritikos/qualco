using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EzPay.Web
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public Startup(IConfiguration configuration,
                                ILogger<Startup> logger)
        {
            Configuration = configuration;
            logger.LogCritical("{loglevel}", configuration ["Logging:LogLevel:Default"]);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //Login Redirection
            //app.Use(
            //    async (context, next) =>
            //        {
            //            if (context.Request.Path == "/Login")
            //            {
            //                await context.Response.WriteAsync("Login Page");
            //            }
            //            else
            //            {
            //                try
            //                {
            //                    await next();
            //                }
            //                catch (Exception ex)
            //                {

            //                }
            //            }
            //        });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
