using Akka.Actor;
using GAB.Actors.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.Actors
{
    public class SimplePubActor : ReceiveActor
    {
        public int RequestCounter { get; private set; }
        public SimplePubActor()
        {
            RequestCounter = 0;
            Receive<SimpleRequestMessage>(
                message =>
                {
                    RequestCounter += 1;
                    Sender.Tell(new SimpleResponseMessage(RequestCounter));
                    Console.WriteLine($"{DateTime.Now} - Il nuovo valore è: {RequestCounter}");
                });
        }
    }
}
