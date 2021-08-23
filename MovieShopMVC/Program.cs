using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace MovieShopMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.Console()
            //    .WriteTo.File(@"C:\Users\kitho\Desktop\MovieShop-master\LogFile.log", rollingInterval: RollingInterval.Day)
            //    .CreateLogger();

            //Log.Information("Hello, world!");

            //int a = 10, b = 0;
            //try
            //{
            //    Log.Debug("Dividing {A} by {B}", a, b);
            //    Console.WriteLine(a / b);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, "Something went wrong");
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
