using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.ActorMessages
{
  public class GiveMoneyMessage
    {
        public decimal Amount { get; }
        public GiveMoneyMessage(decimal amount)
        {
            Amount = amount;
        }
    }
}
