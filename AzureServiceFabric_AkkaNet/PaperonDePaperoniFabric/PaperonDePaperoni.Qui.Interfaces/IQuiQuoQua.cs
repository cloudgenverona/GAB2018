using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace PaperonDePaperoni.QuiQuoQua.Interfaces
{
    public interface IQui : IActor
    {
        /// <summary>
        /// Aggiorna l'ammontare dei soldi
        /// </summary>
        /// <param name="money"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateMoneyAsync(decimal money, CancellationToken cancellationToken);

        /// <summary>
        /// Restituisce i soldi in banca
        /// </summary>
        /// <returns></returns>
        Task<decimal> GetMoneyAsync();
    }

    public interface IQuo : IActor
    {
        /// <summary>
        /// Aggiorna l'ammontare dei soldi
        /// </summary>
        /// <param name="money"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateMoneyAsync(decimal money, CancellationToken cancellationToken);

        /// <summary>
        /// Restituisce i soldi in banca
        /// </summary>
        /// <returns></returns>
        Task<decimal> GetMoneyAsync();
    }

    public interface IQua : IActor
    {
        /// <summary>
        /// Aggiorna l'ammontare dei soldi
        /// </summary>
        /// <param name="money"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateMoneyAsync(decimal money, CancellationToken cancellationToken);

        /// <summary>
        /// Restituisce i soldi in banca
        /// </summary>
        /// <returns></returns>
        Task<decimal> GetMoneyAsync();
    }
}
