using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;

namespace WebJob
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json");
            Configuration = configBuilder.Build();

            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddAzureStorage();
            });
            builder.ConfigureLogging((hostBuilder, logBuilder) =>
            {
                logBuilder.AddConsole();
            });

            var host = builder.Build();
            using (host)
            {
                host.Run();
            }
        }
    }
}
