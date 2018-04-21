using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;
using PaperonDePaperoni.BandaBassotti.Interfaces;
using PaperonDePaperoni.ZioPaperone.Interfaces;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace PaperonDePaperoni.Bank.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IBank : IActor
    {
        Task DepositToPaperonDePaperoniAsync(decimal money);

        Task StealFromPaperonDePaperoni(decimal money);
    }
}
