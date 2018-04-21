using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GlobalAzureBootcampAppDurable.OrdineCliente.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Serilog;

namespace GlobalAzureBootcampAppDurable
{
    public static class ApprovaOrdine
    {
        [FunctionName("ApprovaOrdine")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ApprovaOrdine")]HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient starter,
            TraceWriter log)
        {
            string ordineId = req.GetQueryNameValuePairs().First(x => x.Key == "ordineId").Value;
            Log.Information($"Approva Ordine {ordineId}");

            await starter.RaiseEventAsync(ordineId, Workflow.EventoApprova, true);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}
