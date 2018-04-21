using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace PaperonDePaperoni.BandaBassotti.Interfaces
{
    public interface IBandaBassotti : IActor
    {
        Task MoreMoneyAsync(decimal money);
        
        Task LessMoneyAsync(decimal money);

        /// <summary>
        /// Restituisce i soldi in banca
        /// </summary>
        /// <returns></returns>
        Task<decimal> GetMoneyAsync();
    }
}
