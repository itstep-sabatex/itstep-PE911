using System;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEventDemo
{
    class Program
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);

        private static double Temperature = 0;
        private static void setTemperature()
        {
            Thread.Sleep(2000);
            Temperature = 23.45;
            mre.Set();

        }


        private static void ThreadProc()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"{id} старт потоку і очікування ");
            mre.WaitOne();


            Console.WriteLine($"{id} end temperature = {Temperature}");

        }


        static void Main(string[] args)
        {
            Task.Run(() => ThreadProc());
            Task.Run(() => ThreadProc());
            Task.Run(() => ThreadProc());
            Task.Run(() => setTemperature());
            Console.ReadKey();
            //mre.Set();
            Console.ReadKey();


        }
    }
}
