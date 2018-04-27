using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.ActorMessages
{
    public class TipMessage
    {
       public decimal Tip { get; }

        public TipMessage(decimal tip)
        {
            Tip = tip;
        }
    }
}
