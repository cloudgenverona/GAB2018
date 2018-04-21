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
    public static class PendingApprove
    {
        [FunctionName("PendingApprove")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Pending")]HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient starter,
            TraceWriter traceWriter)
        {
            // Registrazione del tracewriter su Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.TraceWriter(traceWriter)
                .CreateLogger();

            string instanceId = await starter.StartNewAsync("BudgetApproval", null);
            // Verifica completamento lavoro...
            Log.Information($"Inizio Orchestratore con ID = '{instanceId}'.");
            var res = starter.CreateCheckStatusResponse(req, instanceId);
            return res;
        }
    }
}
