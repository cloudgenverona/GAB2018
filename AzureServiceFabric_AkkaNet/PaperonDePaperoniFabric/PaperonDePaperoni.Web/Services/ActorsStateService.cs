using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using PaperonDePaperoni.BandaBassotti.Interfaces;
using PaperonDePaperoni.QuiQuoQua.Interfaces;
using PaperonDePaperoni.ZioPaperone.Interfaces;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading.Tasks;

namespace PaperonDePaperoni.Web.Services
{
    public class ActorsStateService : IActorsStateService
    {
        private StatelessServiceContext _statelessServiceContext;
        private readonly IZioPaperone _zioPaperone;
        private readonly IBandaBassotti _bandaBassotti;
        private readonly IQui _qui;
        private readonly IQuo _quo;
        private readonly IQua _qua;

        public ActorsStateService(StatelessServiceContext statelessServiceContext)
        {
            _statelessServiceContext = statelessServiceContext;
            string applicationName = _statelessServiceContext.CodePackageActivationContext.ApplicationName;
            _zioPaperone = ActorProxy.Create<IZioPaperone>(new ActorId("ZioPaperone"), applicationName);
            _bandaBassotti = ActorProxy.Create<IBandaBassotti>(new ActorId("BandaBassotti"), applicationName);
            _qui = ActorProxy.Create<IQui>(new ActorId("Qui"), applicationName);
            _quo = ActorProxy.Create<IQuo>(new ActorId("Quo"), applicationName);
            _qua = ActorProxy.Create<IQua>(new ActorId("Qua"), applicationName);
        }

        public async Task<CurrentActorState> GetActorsStatus()
        {
            string applicationName = _statelessServiceContext.CodePackageActivationContext.ApplicationName;
            //var result = new CurrentActorState
            //{
            //    ZioPaperone = await _zioPaperone.GetMoneyAsync(),
            //    BandaBassotti = await _bandaBassotti.GetMoneyAsync(),
            //    Qui = await _qui.GetMoneyAsync(),
            //    Quo = await _quo.GetMoneyAsync(),
            //    Qua = await _qua.GetMoneyAsync()
            //};

            var result = new CurrentActorState();
            result.ZioPaperone = await _zioPaperone.GetMoneyAsync();
            result.BandaBassotti = await _bandaBassotti.GetMoneyAsync();
            result.Qui = await _qui.GetMoneyAsync();
            result.Quo = await _quo.GetMoneyAsync();
            result.Qua = await _qua.GetMoneyAsync();
            return result;
        }
    }
}
