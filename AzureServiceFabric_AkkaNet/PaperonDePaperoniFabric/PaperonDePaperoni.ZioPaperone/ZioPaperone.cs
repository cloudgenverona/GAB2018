using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using PaperonDePaperoni.ZioPaperone.Interfaces;
using PaperonDePaperoni.QuiQuoQua.Interfaces;
using PaperonDePaperoni.Models;

namespace PaperonDePaperoni.ZioPaperone
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class ZioPaperone : Actor, IZioPaperone
    {
        private static ActorId QuiActorId = new ActorId("Qui");
        private static ActorId QuoActorId = new ActorId("Quo");
        private static ActorId QuaActorId = new ActorId("Qua");
        private const string CurrentMoney = nameof(CurrentMoney);
        private const string AccountBalance = nameof(AccountBalance);

        public ZioPaperone(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
        }

        protected override async Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, $"Actor {ActorService.Context.ServiceName} activated.");
            await StateManager.TryAddStateAsync(AccountBalance, new AccountBalance());
            await StateManager.TryAddStateAsync(CurrentMoney, 0m);
        }

        public async Task LessMoneyAsync(decimal money)
        {
            await AddMoneyAsync(-money);
            await UpdateQuiQuoQuaAsync(-money / 3);
        }

        public async Task MoreMoneyAsync(decimal money)
        {
            await AddMoneyAsync(money);
            await UpdateQuiQuoQuaAsync(money / 3);
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

        private async Task UpdateQuiQuoQuaAsync(decimal moneyToQuiQuoQua)
        {
            IQui qui = ActorProxy.Create<IQui>(QuiActorId,
              $"{ActorService.Context.CodePackageActivationContext.ApplicationName}");

            IQuo quo = ActorProxy.Create<IQuo>(QuoActorId,
               $"{ActorService.Context.CodePackageActivationContext.ApplicationName}");

            IQua qua = ActorProxy.Create<IQua>(QuaActorId,
               $"{ActorService.Context.CodePackageActivationContext.ApplicationName}");
            
            CancellationToken c = CancellationToken.None;
            await qui.UpdateMoneyAsync(moneyToQuiQuoQua, c);
            await quo.UpdateMoneyAsync(moneyToQuiQuoQua, c);
            await qua.UpdateMoneyAsync(moneyToQuiQuoQua, c);
        }
    }
}
