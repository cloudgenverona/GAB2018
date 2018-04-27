using Akka.Actor;
using GAB.ActorMessages;
using GAB.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.Actors
{
    public class NephewActor : ReceiveActor
    {
        public decimal Wallet { get; private set; }
        private NephewUI UI;
        public NephewActor( NephewUI nephewUI)
        {
            Wallet = 0;
                UI = nephewUI;
            Receive<TipMessage>(message =>
            {
                Wallet += message.Tip;
                UI.Amount = Wallet;
            });
        }
    }
}
