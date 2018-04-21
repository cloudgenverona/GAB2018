using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using PaperonDePaperoni.BandaBassotti.Interfaces;
using PaperonDePaperoni.Models;
using System.Transactions;

namespace PaperonDePaperoni.BandaBassotti
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
    internal class BandaBassotti : Actor, IBandaBassotti
    {
        private const string CurrentMoney = nameof(CurrentMoney);
        private const string AccountBalance = nameof(AccountBalance);

        /// <summary>
        /// Initializes a new instance of BandaBassotti
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public BandaBassotti(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
        }

        protected override async Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");
            await StateManager.TryAddStateAsync(CurrentMoney, 0m);
            await StateManager.TryAddStateAsync(AccountBalance, new AccountBalance());
        }

        public async Task LessMoneyAsync(decimal money)
        {
            if(money < 0)
                throw new InvalidOperationException("Importo negativo non gestito. Inserire un valore positivo");

            await AddMoneyAsync(-money);
        }

        public async Task MoreMoneyAsync(decimal money)
        {
            if (money < 0)
                throw new InvalidOperationException("Importo negativo non gestito. Inserire un valore positivo");
            await AddMoneyAsync(money);
        }

        public async Task<decimal> GetMoneyAsync()
        {
            return await StateManager.GetStateAsync<decimal>(CurrentMoney);
        }

        public async Task<AccountBalance> GetAccountBalanceAsync()
        {
            return await StateManager.GetStateAsync<AccountBalance>(AccountBalance);
        }

        private async Task AddMoneyAsync(decimal money)
        {
            AccountBalance accountBalance = await StateManager.GetStateAsync<AccountBalance>(AccountBalance);
            decimal currentBalance = accountBalance.Records.Sum(x => x.CurrentMoney);
            await StateManager.SetStateAsync<decimal>(CurrentMoney, currentBalance + money);
         
            accountBalance.Records.Add(new BankRecords
            {
                CurrentMoney = money,
                Date = DateTime.UtcNow
            });
        }
    }
}
