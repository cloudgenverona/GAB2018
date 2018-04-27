using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.ActorMessages
{
   public class RoberyResponseMessage
    {
        public decimal AmountOfTheRobery { get; private set; }
        public RoberyResponseMessage(decimal amountOfTheRobery )
        {
            AmountOfTheRobery = amountOfTheRobery;
        }
    }
}
