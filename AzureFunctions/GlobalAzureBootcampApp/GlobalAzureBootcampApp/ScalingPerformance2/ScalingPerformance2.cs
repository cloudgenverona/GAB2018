using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;

namespace GlobalAzureBootcampApp
{
    public static class ScalingPerformance2
    {
        public static async Task<HttpResponseMessage> Run(
            HttpRequestMessage req,
            IAsyncCollector<TableEntity> instanceIds,
            TraceWriter log)
        {
            var instanceId = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID", EnvironmentVariableTarget.Process);
            await instanceIds.AddAsync(new TableEntity(Guid.NewGuid().ToString(), instanceId));
            return req.CreateResponse(HttpStatusCode.OK, instanceId);
        }
    }
}
