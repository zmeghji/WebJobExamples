using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureWebJobs.Controllers
{
    public class HomeController: Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var storageAccount = CloudStorageAccount.Parse(_configuration.GetSection("AzureWebJobsStorage").Value);
            var client = storageAccount.CreateCloudQueueClient();
            var queue = client.GetQueueReference("webappqueue");
            var message= await queue.GetMessageAsync();
            return View(message);
        }
    }
}
