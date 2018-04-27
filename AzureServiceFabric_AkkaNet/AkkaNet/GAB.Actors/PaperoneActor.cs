using Akka.Actor;
using GAB.ActorMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.Actors
{
   public class PaperoneActor : ReceiveActor
    {
        public IActorRef BankActor;
        public List<IActorRef> NephewActors;
        public PaperoneActor(IActorRef bankActor)
        {
            NephewActors = new List<IActorRef>();
            BankActor = bankActor;
            
            Receive<RegisterNephewMessage>(message =>
            {
                NephewActors.Add(message.Nephew);
            });

            Receive<DepositAmountMessage>(message =>
            {
                BankActor.Tell(message); 
            });

            Receive<WithdrawAmountMessage>(message =>
            {
                BankActor.Tell(message);
            });

            Receive<GiveMoneyMessage>(message =>
            {
                decimal amountPerNephew = message.Amount/3;

                foreach (var nephew in NephewActors)
                {
                    nephew.Tell(new TipMessage(amountPerNephew));
                }

            });
        }
 

    }
}
