using Akka.Actor;
using GAB.ActorMessages;
using GAB.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GAB.Actors
{
   public class BeagleBoysActor : ReceiveActor
    {
        BeagleBoyCavernUI BeagleBoyUI;
        public BeagleBoysActor( ActorSelection Bank , BeagleBoyCavernUI beagleBoyCavernUI)
        {
            BeagleBoyUI = beagleBoyCavernUI;

            Receive<StealMessage>(message =>
            {
                Bank.Tell(new StealMessage());
            });


            Receive<RoberyResponseMessage>(message => 
            {
                BeagleBoyUI.RoberyAmount += message.AmountOfTheRobery;
            });
        }



    }
}
