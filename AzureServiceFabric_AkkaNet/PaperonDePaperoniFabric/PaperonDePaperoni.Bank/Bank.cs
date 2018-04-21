using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using PaperonDePaperoni.Bank.Interfaces;
using PaperonDePaperoni.BandaBassotti.Interfaces;
using PaperonDePaperoni.ZioPaperone.Interfaces;

namespace PaperonDePaperoni.Bank
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class Bank : Actor, IBank
    {
        private static ActorId ZioPaperoneActorId = new ActorId("ZioPaperone");
        private static ActorId BandaBassottiActorId = new ActorId("BandaBassotti");
        private IZioPaperone _zioPaperone;
        private IBandaBassotti _bandaBassotti;
        /// <summary>
        /// Initializes a new instance of Bank
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public Bank(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
        }

        protected override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _zioPaperone = ActorProxy.Create<IZioPaperone>(ZioPaperoneActorId,
               $"{ActorService.Context.CodePackageActivationContext.ApplicationName}");

            _bandaBassotti = ActorProxy.Create<IBandaBassotti>(BandaBassottiActorId,
                $"{ActorService.Context.CodePackageActivationContext.ApplicationName}");
        }

        public async Task DepositToPaperonDePaperoniAsync(decimal money)
        {
            await _zioPaperone.MoreMoneyAsync(money);
            await _bandaBassotti.LessMoneyAsync(money);
        }
        
        public async Task StealFromPaperonDePaperoni(decimal money)
        {
            await _zioPaperone.LessMoneyAsync(money);
            await _bandaBassotti.MoreMoneyAsync(money);
        }
    }
}
