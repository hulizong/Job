using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq; 

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathToContentRoot = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
             .SetBasePath(pathToContentRoot)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables();
  
            ConfigModel.Configurations.CreateAcquire(builder.Build());
            HandleStart();
            var webHostArgs = args.Where(arg => arg != "--console").ToArray();

            var host = WebHost.CreateDefaultBuilder(webHostArgs)
                .UseStartup<Startup>()
                .UseContentRoot(pathToContentRoot)
                .UseKestrel(options => {
                    options.Limits.MinRequestBodyDataRate = null;
                })
                .UseUrls(ConfigModel.Configurations.Acquire.Setting.Host)
                .Build();
            host.Run();
        }
        static void HandleStart()
            {
            try
            {
                new Job.Scheduler().Start().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Common.LogHelper.Error(ex);
            }

        }
    }
}
