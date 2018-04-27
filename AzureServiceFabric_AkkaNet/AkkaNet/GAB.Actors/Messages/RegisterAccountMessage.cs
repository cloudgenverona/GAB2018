using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using GAB.Entities;

namespace GAB.ActorMessages
{
    public class RegisterAccountMessage
    {
        public IActorRef AccountActor { get; }
        public string AccountID { get; }
        public string AccountOwner { get; }

        public BankAccountUI AccountUI { get; }  

        public RegisterAccountMessage( string accountNumber, string accountOwner, IActorRef accountActor, BankAccountUI accountUI)
        {
            AccountID = accountNumber;
            AccountOwner = accountOwner;
            AccountActor = accountActor;
            AccountUI = accountUI;
        }
    }
}
