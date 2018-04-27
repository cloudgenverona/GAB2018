using Akka.Actor;
using GAB.ActorMessages;
using System;

namespace GAB.Entities
{
    public class BankAccount
    {
        public readonly IActorRef AccountActor;
        public readonly string ID;
        public readonly string Owner;
        public  bool IsActive { get; private set; }
        public decimal Balance { get; private set; }
        
        public BankAccountUI AccountUi { get; private set; }

        public void ResetBalance ()
        {
            Balance = 0;
            AccountUi.Balance = Balance;
            
        }
        public bool DepositMoney(decimal amount)
        {
            if (amount<= 0)
            {
                throw new ArgumentException($"{amount} can only be positive");
            }
            if (IsActive )
            {
                Balance += amount;
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public bool WithdrawMoney(decimal amount)
        {
            if (Balance - amount <= 0)
            {
                throw new ArgumentException($"{amount} is not available in the account");
            }
            if (IsActive)
            {
                Balance -= amount;
                AccountActor.Tell(new GiveMoneyMessage(amount));
                return true;
            }
            else
            {
                return false;
            }

        }
        public BankAccount(string id, string owner, IActorRef actorRef, BankAccountUI accountUI)
        {
            ID = id;
            Owner = owner;
            Balance = 0;
            IsActive = true;
            AccountActor = actorRef;
            AccountUi = accountUI;

        }


    }
}