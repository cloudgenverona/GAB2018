using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GlobalAzureBootcampAppDurable.OrdineCliente.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Serilog;

namespace GlobalAzureBootcampAppDurable.InviaMail
{
    public static class AttendiOrdineCliente
    {
        [FunctionName(Workflow.AttendiOrdineCliente)]
        [return: Table("ApprovaOrdineCliente")]
        public static async Task<ApprovaOrdineTable> Run(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            if (!context.IsReplaying) Log.Verbose("Starting up!");
            Log.Information($"Instance: {context.InstanceId}");
            string ordinePendingId = context.GetInput<string>();
            Log.Information($"Pending Order {ordinePendingId}");
            string status = "";
            using (var timeoutCts = new CancellationTokenSource())
            {
                DateTime dueTime = context.CurrentUtcDateTime.AddMinutes(15);
                Task durableTimeout = context.CreateTimer(dueTime, timeoutCts.Token);

                Task<bool> approvalTask = context.WaitForExternalEvent<bool>(Workflow.EventoApprova);
                //Attendo un evento o un timer
                if (approvalTask == await Task.WhenAny(approvalTask, durableTimeout))
                {
                    timeoutCts.Cancel();
                    if (await approvalTask)
                    {
                        status = "Approvato";
                    }
                }
                else
                {
                    status = "TempoScaduto";
                }
                Log.Information(status);
            }
            if (!context.IsReplaying) Log.Verbose("Done!");
            return new ApprovaOrdineTable
            {
                PartitionKey = "PendingApproval",
                RowKey = context.InstanceId,
                OrdineId = ordinePendingId,
                Status = status
            };
        }

        public class ApprovaOrdineTable
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public string OrdineId { get; set; }
            public string Status { get; set; }
        }
        
    }
}
