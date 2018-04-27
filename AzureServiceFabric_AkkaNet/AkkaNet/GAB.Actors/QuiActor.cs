using Akka.Actor;
using GAB.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.Actors
{
   public class QuiActor : NephewActor
    {
        public QuiActor(NephewUI ui):base(ui)
        {
            
        }
    }
}
