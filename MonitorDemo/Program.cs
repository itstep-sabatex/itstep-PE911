using System;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorDemo
{
    class Program
    {
        private static int DemoInt = 0;
        private static object locker = new object();

        static void IncDemoInt()
        {
            for (int i = 0; i < 10; i++)
            {
                bool lockTaken = false;
                //One Threadn
                Monitor.TryEnter(locker,500,ref lockTaken);
                if (lockTaken)
                {
                    try
                    {
                        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}     DemoInt = {DemoInt++}");
                    }
                    finally
                    {
                        Monitor.Exit(locker);
                    }
                }
                else
                {
                    // не вдалося ввійти в критичну секцію
                }
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
