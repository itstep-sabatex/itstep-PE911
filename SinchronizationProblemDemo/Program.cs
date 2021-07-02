using System;
using System.Threading;
using System.Threading.Tasks;

namespace SinchronizationProblemDemo
{
    class Program
    {
        private static int DemoInt=0;

        static void IncDemoInt()
        {
            for (int i = 0; i < 10; i++)
            {
//

                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}     DemoInt = {Interlocked.Increment(ref DemoInt)}");
//
            }
        }
        static void Main(string[] args)
        {
            Task.Run(() => IncDemoInt());
            Task.Run(() => IncDemoInt());
            Task.Run(() => IncDemoInt());
            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }
}
