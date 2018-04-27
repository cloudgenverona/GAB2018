using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedMutableState
{
    class Program
    {
     
         static void Main(string[] args)
        {
            NonStaticProgram nonStaticProgram = new NonStaticProgram();
            Console.ReadLine();
        }

        

    }

    public class NonStaticProgram
    {
         UInt32 counter { get; set; } = 0;
        public NonStaticProgram()
        {

            for (int i = 0; i < 10; i++)
            {
                Thread T1 = new Thread(IncrementCounter);
                T1.Name = "T1";
                Thread T2 = new Thread(IncrementCounter);
                T2.Name = "T2";
                T1.Start();
                T2.Start();
                T1.Join();
                T2.Join();

                Console.WriteLine($"{Thread.CurrentThread.Name} -> Counter = {counter} ");

            }
            Console.ReadLine();

        }

        void IncrementCounter()
        {
            Random random = new Random(Thread.CurrentThread.ManagedThreadId);
      
            for (int i = 0; i < 50000; i++)
            {
               
                counter++;

                for (int u = 0; u < random.Next(1, 600); u++)
                {
                }



            }
        }
    }
}
