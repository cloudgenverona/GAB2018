using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.Entities
{

  public class ActorManagerService
    {
        public ActorSystem ActorService { get; private set; }
        public ActorManagerService(string actorSystemName)
        {
            ActorService = ActorSystem.Create(actorSystemName); 

        }

        public ActorManagerService(string actorSystemName, Akka.Configuration.Config config)
        {
            ActorService = ActorSystem.Create(actorSystemName, config);

        }
    }
}
