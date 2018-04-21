using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GlobalAzureBootcampAppDurable.OrdineCliente.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Serilog;
using Serilog.Sinks.AzureWebJobsTraceWriter;

namespace GlobalAzureBootcampAppDurable.OrdineCliente
{
    public static class OrdineCliente
    {
        [FunctionName("OrdineCliente")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, methods: "post", Route = "OrdineCliente")] HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient starter,
            TraceWriter traceWriter)
        {
            // Registrazione del tracewriter su Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.TraceWriter(traceWriter)
                .CreateLogger();

            OrdiniAcquistoModel ordiniAcquisto = await req.Content.ReadAsAsync<OrdiniAcquistoModel>();
            string instanceId = await starter.StartNewAsync(Workflow.OrdineClienteManager, ordiniAcquisto);

            // Verifica completamento lavoro...
            Log.Information($"Inizio Orchestratore con ID = '{instanceId}'.");
            var res = starter.CreateCheckStatusResponse(req, instanceId);
            res.Headers.RetryAfter = new RetryConditionHeaderValue(TimeSpan.FromMinutes(10));
            return res;
        }
    }
}
