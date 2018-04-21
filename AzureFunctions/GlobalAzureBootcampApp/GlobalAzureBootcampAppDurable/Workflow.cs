using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalAzureBootcampAppDurable
{
    public static class Workflow
    {
        public const string OrdineClienteManager = "OrdineClienteManager";
        public const string NotificaSmsOrdineCliente = "NotificaSmsOrdineCliente";
        public const string InviaMailOrdineCliente = "InviaMailOrdineCliente";
        public const string AttendiOrdineCliente = "AttendiOrdineCliente";

        public const string EventoApprova = "EventoApprova";
    }
}
