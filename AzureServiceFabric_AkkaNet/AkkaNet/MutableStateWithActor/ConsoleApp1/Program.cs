using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutableStateWithActor
{
    class Program
    {
        static IActorRef IncreaserActor1;
        static IActorRef IncreaserActor2;
        static void Main(string[] args)
        {
            ActorSystem actorSystem = ActorSystem.Create("AS");

            IActorRef CounterActor1 = actorSystem.ActorOf(Props.Create(() => new CounterActor()));
            IncreaserActor1 = actorSystem.ActorOf(Props.Create(() => new IncreaserActor(CounterActor1)), "T1");
            IncreaserActor2 = actorSystem.ActorOf(Props.Create(() => new IncreaserActor(CounterActor1)), "T2");

            IncreaserActor1.Tell(new CountForN(50000));
            IncreaserActor2.Tell(new CountForN(50000));

            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(5);
                CounterActor1.Tell(new ShowVariable());
            }
            Console.ReadLine();

        }

    }

    internal class IncreaserActor : ReceiveActor
    {
        IActorRef counterActor;
        public IncreaserActor(IActorRef counterActorRef)
        {
            counterActor = counterActorRef;
            Thread.CurrentThread.Name = Self.Path.Name;
            Receive<CountForN>(message =>
                 {
                     CountNTimes(counterActor, message.Times);
                 });
        }

        static void CountNTimes(IActorRef actor, int times)
        {
            Random random = new Random(Thread.CurrentThread.ManagedThreadId);
            for (int i = 0; i < times; i++)
            {
                for (int u = 0; u < random.Next(1, 300); u++)
                {
                }
                actor.Tell(new IncreaseVariable());
            }
        }
    }


    internal class CounterActor : ReceiveActor
    {
        int counter = 0;
        public CounterActor()
        {
            Receive<IncreaseVariable>(a =>
            {
                counter++;
            });

            Receive<ShowVariable>(a =>
            {
                Console.WriteLine($"The value of the counter: {counter}");
            });


        }
    }

    //Messages

    internal class IncreaseVariable
    {
    }
    internal class ShowVariable
    {
    }
    internal class CountForN
    {
        public int Times { get; private set; }

        public CountForN(int times)
        {
            Times = times;
        }

    }
}
