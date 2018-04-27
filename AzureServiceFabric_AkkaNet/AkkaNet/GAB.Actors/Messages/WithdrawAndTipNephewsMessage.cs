using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.ActorMessages
{
   public class WithdrawAmountMessage
    { 
        public string AccountID { get; private set; }
        public decimal Amount { get; private set; }


        public WithdrawAmountMessage(string accountID, decimal amount)
        {
            AccountID = accountID;
            Amount = amount;
        }
    }
}
