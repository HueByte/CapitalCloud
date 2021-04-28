using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            SayHi();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .WriteTo.File(new CompactJsonFormatter(), "./logs/systemlog.json", rollingInterval: RollingInterval.Day)
                .WriteTo.Seq("http://localhost:5341")
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("Starting server...");
            try
            {
                var host = CreateHostBuilder(args).Build();
                // TODO - seed data
                host.Run();
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "Fatal Error Occured during start!");
            }
            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();

        public static void SayHi()
        {
            Console.WriteLine(@"
##     ##  #######     ##      ##     #####                 ##      ##   #####   ########     ##   ########  
##     ## ##     ##  ####    ####    ##   ##                ##  ##  ##  ##   ##  ##     ##  ####   ##     ## 
##     ##        ##    ##      ##   ##     ##               ##  ##  ## ##     ## ##     ##    ##   ##     ## 
#########  #######     ##      ##   ##     ##               ##  ##  ## ##     ## ########     ##   ##     ## 
##     ##        ##    ##      ##   ##     ##               ##  ##  ## ##     ## ##   ##      ##   ##     ## 
##     ## ##     ##    ##      ##    ##   ##                ##  ##  ##  ##   ##  ##    ##     ##   ##     ## 
##     ##  #######   ######  ######   #####                  ###  ###    #####   ##     ##  ###### ########  
            ");
        }
    }
}
