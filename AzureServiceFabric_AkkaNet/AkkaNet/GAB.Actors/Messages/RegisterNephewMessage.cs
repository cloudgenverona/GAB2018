using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.ActorMessages
{
    public class RegisterNephewMessage
    {
        public IActorRef Nephew { get; private set; }

        public RegisterNephewMessage(IActorRef nephew)
        {
            Nephew = nephew;
        }
    }
}
