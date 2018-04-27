using Akka.Actor;
using GAB.ActorMessages;
using GAB.Actors;
using GAB.Entities;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

namespace GAB.BlueTeamImplementation.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    { 
        ActorManagerService MyActors = SimpleIoc.Default.GetInstance<ActorManagerService>();
       
        public NephewUI QuiUI { get; private set; }
        public NephewUI QuoUI { get; }
        public NephewUI QuaUI { get; }
        public BankAccountUI PaperoneBankAccountUi { get; }
        public RelayCommand Withdraw { get; }
        public RelayCommand Deposit { get; }

        public IActorRef Paperone { get; }

        decimal amountToProcess;
        public decimal AmountToProcess
        {
            get
            {
                return amountToProcess;
            }
            set
            {
                Set(ref amountToProcess, value, "AmountToProcess");
            }
        }

        public MainViewModel()
        {
           
            IActorRef Bank = MyActors.ActorService.ActorOf(Props.Create(() => new BankActor()), "Bank");

            QuiUI = new NephewUI();
            QuoUI = new NephewUI();
            QuaUI = new NephewUI();
            PaperoneBankAccountUi = new BankAccountUI();
            // Creazione account bancario
            Paperone = MyActors.ActorService.ActorOf(Props.Create(() => new PaperoneActor(Bank)));

            Bank.Tell(new RegisterAccountMessage("PP11110", "Paperone Paperoni", Paperone, PaperoneBankAccountUi));
     
            IActorRef Qui = MyActors.ActorService.ActorOf(Props.Create(() => new QuiActor(QuiUI)));
            IActorRef Quo = MyActors.ActorService.ActorOf(Props.Create(() => new QuoActor(QuoUI)));
            IActorRef Qua = MyActors.ActorService.ActorOf(Props.Create(() => new QuaActor(QuaUI)));

            
            //Registrazione nipoti
            Paperone.Tell(new RegisterNephewMessage(Qui));
            Paperone.Tell(new RegisterNephewMessage(Quo));
            Paperone.Tell(new RegisterNephewMessage(Qua));

            Withdraw = new RelayCommand(() =>
           {
               Paperone.Tell(new WithdrawAmountMessage("PP11110", AmountToProcess));

               AmountToProcess = 0;

           });

            Deposit = new RelayCommand(() =>
            {
                Paperone.Tell(new DepositAmountMessage("PP11110", AmountToProcess));

                AmountToProcess = 0;
            });



            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}