using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaperonDePaperoni.Web.Services
{
    public interface IActorsStateService
    {
        Task<CurrentActorState> GetActorsStatus();
    }

    public class CurrentActorState
    {
        public decimal ZioPaperone { get; set; }
        public decimal BandaBassotti { get; set; }
        public decimal Qui { get; set; }
        public decimal Quo { get; set; }
        public decimal Qua { get; set; }
    }
}
