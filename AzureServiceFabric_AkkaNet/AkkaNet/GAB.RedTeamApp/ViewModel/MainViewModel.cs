using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using GAB.ActorMessages;
using GAB.Actors;
using GAB.Entities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

namespace GAB.RedTeamApp.ViewModel
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
        public IActorRef BeagleBoy { get; }
        public BeagleBoyCavernUI BeagleBoyUI {get;}
        public RelayCommand Steal { get; }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ActorManagerService MyActors = SimpleIoc.Default.GetInstance<ActorManagerService>();

            ActorSelection R = MyActors.ActorService.ActorSelection("akka.tcp://BlueTeam@localhost:8082/user/Bank");

            BeagleBoyUI = new BeagleBoyCavernUI();

            BeagleBoy = MyActors.ActorService.ActorOf(Props.Create(() => new BeagleBoysActor(R, BeagleBoyUI)));


            Steal = new RelayCommand(() =>
            {
                BeagleBoy.Tell(new StealMessage());
            });
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
              
            }
            else
            {
           



            
                // Code runs "for real"
            }
        }




    }
}