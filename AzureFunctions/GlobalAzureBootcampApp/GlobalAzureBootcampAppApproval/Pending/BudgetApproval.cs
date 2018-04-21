using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Serilog;

namespace GlobalAzureBootcampAppApproval.Pending
{
    public static class BudgetApproval
    {
        [FunctionName("BudgetApproval")]
        [return: Table("ApproveTable")]
        public static async Task<ApproveTable> Run(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            Log.Information($"Instance: {context.InstanceId}");
            string status = "";
            using (var timeoutCts = new CancellationTokenSource())
            {
                DateTime dueTime = context.CurrentUtcDateTime.AddMinutes(1);
                Task durableTimeout = context.CreateTimer(dueTime, timeoutCts.Token);

                Task<bool> approvalEvent = context.WaitForExternalEvent<bool>("Approve");
                //Attendo un evento o un timer
                if (approvalEvent == await Task.WhenAny(approvalEvent, durableTimeout))
                {
                    timeoutCts.Cancel();
                    if(await approvalEvent)
                    {
                        status = "Approvato";
                    }
                    else
                    {
                        status = "NonApprovato";
                    }
                }
                else
                {
                    status = "TempoScaduto";
                }
                Log.Information(status);
            }
            return new ApproveTable
            {
                PartitionKey = "PendingApproval",
                RowKey = context.InstanceId,
                Status = status
            };
        }

        public class ApproveTable
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public string Status { get; set; }
        }
    }
}
