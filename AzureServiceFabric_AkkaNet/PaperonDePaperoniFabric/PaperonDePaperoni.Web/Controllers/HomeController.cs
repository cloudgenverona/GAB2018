using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using PaperonDePaperoni.BandaBassotti.Interfaces;
using PaperonDePaperoni.Bank.Interfaces;
using PaperonDePaperoni.QuiQuoQua.Interfaces;
using PaperonDePaperoni.Web.Models;
using PaperonDePaperoni.Web.Services;
using PaperonDePaperoni.ZioPaperone.Interfaces;

namespace PaperonDePaperoni.Web.Controllers
{
    public class HomeController : Controller
    {
        private static ActorId BankActorId = new ActorId("Bank");
        private StatelessServiceContext _statelessServiceContext;
        private readonly IBank _bank;
        private readonly IActorsStateService _actorService;

        public HomeController(StatelessServiceContext statelessServiceContext, IActorsStateService actorStateService)
        {
            _statelessServiceContext = statelessServiceContext ?? throw new ArgumentNullException(nameof(statelessServiceContext));
            _actorService = actorStateService ?? throw new ArgumentNullException(nameof(actorStateService));
            string applicationName = _statelessServiceContext.CodePackageActivationContext.ApplicationName;
            _bank = ActorProxy.Create<IBank>(BankActorId, applicationName);
        }

        public async Task<IActionResult> Index()
        {

            return View(new BankChangesViewModel()
            {
                CurrenteActorState = await GetStatusAsync(),
                Deposit = 0,
                Withdraw = 0
            });
        }

        public async Task<IActionResult> Transactions(BankChangesViewModel changesViewModel)
        {
            if (ModelState.IsValid && changesViewModel.Deposit >= 0 && changesViewModel.Withdraw >= 0)
            {
                if(changesViewModel.Deposit > 0)
                    await _bank.DepositToPaperonDePaperoniAsync(changesViewModel.Deposit);
                if(changesViewModel.Withdraw > 0)
                    await _bank.StealFromPaperonDePaperoni(changesViewModel.Withdraw);
                changesViewModel.CurrenteActorState = await GetStatusAsync();
                changesViewModel.Deposit = 0;
                changesViewModel.Withdraw = 0;
                return View("Index", changesViewModel);
            }
            changesViewModel.CurrenteActorState = await GetStatusAsync();
            return View("Index", changesViewModel);
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<CurrentActorStateViewModel> GetStatusAsync()
        {
            var status = await _actorService.GetActorsStatus();
            CurrentActorStateViewModel model = new CurrentActorStateViewModel
            {
                BandaBassotti = status.BandaBassotti,
                Qua = status.Qua,
                Qui = status.Qui,
                Quo = status.Quo,
                ZioPaperone = status.ZioPaperone,
                Bank = 10m
            };
            return model;
        }
    }
}
