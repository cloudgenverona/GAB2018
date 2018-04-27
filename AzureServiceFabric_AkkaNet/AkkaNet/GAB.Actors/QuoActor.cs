using Akka.Actor;
using GAB.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.Actors
{
   public class QuoActor : NephewActor
    {
        public QuoActor(NephewUI ui) : base(ui)
        {

        }
    }
}
