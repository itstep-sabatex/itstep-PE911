using System;
using System.Threading;
using System.Threading.Tasks;

namespace LockDemo
{
    class Program
    {
        private static int DemoInt = 0;
        private static object locker = new object();

        static void IncDemoInt()
        {
            for (int i = 0; i < 10; i++)
            {
                //One Threadn
                lock(locker)
                {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}     DemoInt = {DemoInt++}");
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
