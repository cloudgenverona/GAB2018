using Akka.Actor;
using Akka.Persistence;
using GAB.ActorMessages;
using GAB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GAB.Actors
{
    public class BankActor : ReceiveActor
    {
        List<BankAccount> Accounts = new List<BankAccount>();
        public BankActor()
        {
            Receive<StealMessage>(o =>
            {
                decimal RoberyTotalMoney = 0;
                foreach (var item in Accounts)
                {
                    RoberyTotalMoney += item.Balance;
                    item.ResetBalance();
                }
                Sender.Tell(new RoberyResponseMessage(RoberyTotalMoney));
            });
     
            Receive<DepositAmountMessage>(deposit =>
            {
               var account = Accounts.FirstOrDefault(o => o.ID == deposit.AccountID);
                if (account != null)
                {
                    //try
                    //{
                    account.DepositMoney(deposit.Amount);
                    account.AccountUi.Balance = account.Balance;
                    //}
                    //catch (Exception e)
                    //{

                    //}
                }
            });

            Receive<WithdrawAmountMessage>(withdraw =>
            {
                var account = Accounts.FirstOrDefault(o => o.ID == withdraw.AccountID);
                if (account != null)
                {
                    account.WithdrawMoney(withdraw.Amount);
                    account.AccountUi.Balance = account.Balance;


                }
            });

            Receive<RegisterAccountMessage>(Account =>
            {
                BankAccount account = new BankAccount(Account.AccountID, Account.AccountOwner, Account.AccountActor, Account.AccountUI);
                Accounts.Add(account);
            });

           
        }
        public override void AroundPreRestart(Exception cause, object message)
        {
            base.AroundPreRestart(cause, message);
        }

        
    }
}
