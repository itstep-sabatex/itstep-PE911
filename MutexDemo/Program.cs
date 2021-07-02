using System;
using System.Threading;
using System.Threading.Tasks;

namespace MutexDemo
{
    class Program
    {
        private static int DemoInt = 0;
        private static Mutex locker = new Mutex();

        static void IncDemoInt()
        {
            var d =Mutex.OpenExisting("DemoMutex");
            for (int i = 0; i < 10; i++)
            {
                //One Threadn
                if (locker.WaitOne())
                {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}     DemoInt = {DemoInt++}");
                }
                locker.ReleaseMutex();
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
