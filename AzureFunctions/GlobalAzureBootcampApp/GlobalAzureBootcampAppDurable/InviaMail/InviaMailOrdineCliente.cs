using GlobalAzureBootcampAppDurable.OrdineCliente;
using GlobalAzureBootcampAppDurable.OrdineCliente.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SendGrid.Helpers.Mail;
using Serilog;
using System;
using System.Threading.Tasks;
using Twilio;

namespace GlobalAzureBootcampAppDurable.InviaMail
{
    public static class InviaMailFunction
    {
        [FunctionName(Workflow.InviaMailOrdineCliente)]
        public static string Run(
           [ActivityTrigger] OrdiniAcquistoModel ordiniAcquisto,
           [OrchestrationClient] DurableOrchestrationClient starter,
           [SendGrid(ApiKey = "SendGridApiKey")]
           out Mail message)
        {
            string currentInstance = Guid.NewGuid().ToString("N");
            Log.Information($"InviaMailOrdineCliente : {currentInstance}");

            string toMail = Utility.GetEnvironmentVariable("SendGridTo");
            string fromMail = Utility.GetEnvironmentVariable("SendGridFrom");
            Log.Information($"Invio ordine {ordiniAcquisto.IdOrdine} a {ordiniAcquisto.ClienteCorrente.NumeroTelefono}.");
            message = new Mail
            {
                Subject = $"GlobalAzureBootcamp 2018"                
            };
            var personalization = new Personalization();
            personalization.AddTo(new Email(toMail));
            Content content = new Content
            {
                Type = "text/html",
                Value = $@"L'ordine {ordiniAcquisto.IdOrdine} è stato preso in carico
                <br><a href='{Utility.GetEnvironmentVariable("PublicUrl")}/ApprovaOrdine?ordineId={ordiniAcquisto.IdOrdine}'>Conferma ordine</a>"
            };

            message.From = new Email(fromMail);
            message.AddContent(content);
            message.AddPersonalization(personalization);

            return currentInstance;
        }
    }
}
