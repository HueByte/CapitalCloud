using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using API.Configuration;
using API.Hubs;
using Core.Entities;
using Core.RepositoriesInterfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddRazorPages();
            services.AddControllersWithViews().AddNewtonsoftJson();
            ModuleConfiguration moduleConfiguration = new ModuleConfiguration(services, Configuration);
            moduleConfiguration.ConfigureDataBase();
            moduleConfiguration.ConfigureSecurity();
            moduleConfiguration.ConfigureSwagger();
            moduleConfiguration.ConfigureServices();
            moduleConfiguration.ConfigureCors();
            moduleConfiguration.ConfigureForwardedHeaders();
            moduleConfiguration.ConfigureSmtpClient();
            moduleConfiguration.ConfigureSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v0.2A/swagger.json", "Roulette");
                 //  c.RoutePrefix = "/api";
             });
            app.UseSerilogRequestLogging();



            app.UseCors();
            // app.UseHttpsRedirection();
            // app.UseHsts();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapHub<TestHub>("/api/TestingHub");
                endpoints.MapGet("/api", async context =>
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(@"
##     ##  #######     ##      ##     #####                 ##      ##   #####   ########     ##   ########  
##     ## ##     ##  ####    ####    ##   ##                ##  ##  ##  ##   ##  ##     ##  ####   ##     ## 
##     ##        ##    ##      ##   ##     ##               ##  ##  ## ##     ## ##     ##    ##   ##     ## 
#########  #######     ##      ##   ##     ##               ##  ##  ## ##     ## ########     ##   ##     ## 
##     ##        ##    ##      ##   ##     ##               ##  ##  ## ##     ## ##   ##      ##   ##     ## 
##     ## ##     ##    ##      ##    ##   ##                ##  ##  ##  ##   ##  ##    ##     ##   ##     ## 
##     ##  #######   ######  ######   #####                  ###  ###    #####   ##     ##  ###### ########  
                    ");
                });
            });
        }
    }
}
