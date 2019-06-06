using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ContinuousMessageJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json");
            var configuration = configBuilder.Build();

            var storageAccount = CloudStorageAccount.Parse(configuration.GetSection("AzureWebJobsStorage").Value);
            var client = storageAccount.CreateCloudQueueClient();
            var queue = client.GetQueueReference("queue");
            while (true)
            {
                queue.AddMessageAsync(new CloudQueueMessage(DateTime.UtcNow.ToLongTimeString()));
                Thread.Sleep(60000);
            }
        }
    }
}
