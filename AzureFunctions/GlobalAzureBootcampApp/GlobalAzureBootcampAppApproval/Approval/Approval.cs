using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Serilog;
using Serilog.Sinks.AzureWebJobsTraceWriter;

namespace GlobalAzureBootcampAppApproval.Pending
{
    public static class Approval
    {
        [FunctionName("Approval")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Approval")]HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient starter,
            TraceWriter traceWriter)
        {
            // Registrazione del tracewriter su Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.TraceWriter(traceWriter)
                .CreateLogger();


            var payload = await req.Content.ReadAsAsync<Payload>();
            await starter.RaiseEventAsync(payload.InstanceId, "Approve", payload.Result);

            
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        public class Payload
        {
            public string InstanceId { get; set; }
            public bool Result { get; set; }
        }
    }
}
