using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using API.Configuration;
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
             });



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
                endpoints.MapGet("/api/MyResult", async context =>
                {

                    // TODO - remove
                    
                    context.Response.ContentType = "text/plain";

                    // Host info
                    var name = Dns.GetHostName(); // get container id
                    var ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                    Console.WriteLine($"Host Name: { Environment.MachineName} \t {name}\t {ip}");
                    await context.Response.WriteAsync($"Host Name: {Environment.MachineName}{Environment.NewLine}");
                    await context.Response.WriteAsync(Environment.NewLine);

                    // Request method, scheme, and path
                    await context.Response.WriteAsync($"Request Method: {context.Request.Method}{Environment.NewLine}");
                    await context.Response.WriteAsync($"Request Scheme: {context.Request.Scheme}{Environment.NewLine}");
                    await context.Response.WriteAsync($"Request URL: {context.Request.GetDisplayUrl()}{Environment.NewLine}");
                    await context.Response.WriteAsync($"Request Path: {context.Request.Path}{Environment.NewLine}");

                    // Headers
                    await context.Response.WriteAsync($"Request Headers:{Environment.NewLine}");
                    foreach (var (key, value) in context.Request.Headers)
                    {
                        await context.Response.WriteAsync($"\t {key}: {value}{Environment.NewLine}");
                    }
                    await context.Response.WriteAsync(Environment.NewLine);

                    // Connection: RemoteIp
                    await context.Response.WriteAsync($"Request Remote IP: {context.Connection.RemoteIpAddress}");

                    await context.Response.WriteAsync(Environment.NewLine);
                    await context.Response.WriteAsync("H3110 W0r1d!");
                });
            });
        }
    }
}
