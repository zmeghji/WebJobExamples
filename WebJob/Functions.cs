using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebJob
{
    public class Functions
    {
        
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, ILogger logger)
        {
            logger.LogInformation(message);

            var storageAccount = CloudStorageAccount.Parse(Program.Configuration.GetSection("AzureWebJobsStorage").Value);
            var client = storageAccount.CreateCloudQueueClient();
            var queue = client.GetQueueReference("webappqueue");
            queue.AddMessageAsync(new CloudQueueMessage(message));
        }
    }
}
