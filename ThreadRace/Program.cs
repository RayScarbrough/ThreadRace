using System;
using System.Threading;

namespace threadRace
{
    internal class Program
    {
        private static readonly object lockObject = new object(); 
        private static bool raceWon = false; 

        static void Main(string[] args)
        {
            Console.WriteLine("Ready, Set, Go...");
            int t1Location = 0, t2Location = 0, t3Location = 0, t4Location = 0, t5Location = 0;

           
            Thread t1 = new Thread(() => RunRace(ref t1Location)) { Name = "Speedy Gonzales" };
            Thread t2 = new Thread(() => RunRace(ref t2Location)) { Name = "Road Runner" };
            Thread t3 = new Thread(() => RunRace(ref t3Location)) { Name = "Flash" };
            Thread t4 = new Thread(() => RunRace(ref t4Location)) { Name = "Sonic" };
            Thread t5 = new Thread(() => RunRace(ref t5Location)) { Name = "Quick Silver" };

            
            t3.Priority = ThreadPriority.AboveNormal; 

            
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();

           
            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();

            Console.WriteLine("Race has ended");
        }

        static void RunRace(ref int location)
        {
            while (location < 100)
            {
                MoveIt(ref location);
                Thread.Sleep(10); 
            }
        }

        static void MoveIt(ref int location)
        {
            lock (lockObject)
            {
                if (!raceWon && location < 100) 
                {
                    location++;
                    Console.WriteLine($"{Thread.CurrentThread.Name} location={location}");
                    if (location >= 100)
                    {
                        raceWon = true; 
                        Console.WriteLine($"{Thread.CurrentThread.Name} is the winner!");
                    }
                }
            }
        }
    }
}
