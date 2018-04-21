using GlobalAzureBootcampAppDurable.OrdineCliente.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace GlobalAzureBootcampAppDurable.NotificaSms
{
    public static class NotificaSmsOrdineCliente
    {
        [FunctionName(Workflow.NotificaSmsOrdineCliente)]
        public static string Run(
           [ActivityTrigger] OrdiniAcquistoModel ordiniAcquisto,
           [TwilioSms(AccountSidSetting = "TwilioAccountSid",
                      AuthTokenSetting = "TwilioAuthToken",
                      From = "%TwilioPhoneNumber%")]
            out SMSMessage message)
        {
            string currentInstance = Guid.NewGuid().ToString("N");
            Log.Information($"NotificaSmsOrdineCliente : {currentInstance}");
            // Get a random number generator with a random seed (not time-based)

            Log.Information($"Invio ordine {ordiniAcquisto.IdOrdine} a {ordiniAcquisto.ClienteCorrente.NumeroTelefono}.");
            message = new SMSMessage
            {
                To = ordiniAcquisto.ClienteCorrente.NumeroTelefono,
                Body = $"L'ordine {ordiniAcquisto.IdOrdine} è preso in carico. Conferma la mail"
            };
            return currentInstance;
        }
    }
    
}
