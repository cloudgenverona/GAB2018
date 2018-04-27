using System;

namespace GAB.ActorMessages
{
    public class DepositAmountMessage
    {
        public string AccountID { get; private set; }
        public decimal Amount { get; private set; }

        public DepositAmountMessage(string accountID, decimal amount)
        {
            AccountID = accountID;
            Amount = amount;
        }
    }
}
