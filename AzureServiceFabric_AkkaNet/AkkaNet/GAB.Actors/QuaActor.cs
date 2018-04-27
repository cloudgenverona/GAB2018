using Akka.Actor;
using GAB.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.Actors
{
  public  class QuaActor :NephewActor
    {
        public QuaActor(NephewUI ui) : base(ui)
        {

        }
    }
}
