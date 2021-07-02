using System;
using System.Threading;
using System.Threading.Tasks;

namespace SemaforeDemo
{
    class Program
    {
        private static int DemoInt = 0;
        private static Mutex locker = new Mutex();
        private static Semaphore semaphore = new Semaphore(2,2);

        static void IncDemoInt()
        {
            if (semaphore.WaitOne())
            {
                for (int i = 0; i < 10; i++)
                {
                    var mt = Mutex.OpenExisting("TestMutex");
                    //One Threadn
                    if (locker.WaitOne())
                    {
                        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}     DemoInt = {DemoInt++}");
                    }
                    locker.ReleaseMutex();
                    //
                }
            }
            semaphore.Release();
        }
        static void Main(string[] args)
        {
            Task.Run(() => IncDemoInt());
            Task.Run(() => IncDemoInt());
            Task.Run(() => IncDemoInt());
            Task.Run(() => IncDemoInt());
            Task.Run(() => IncDemoInt());

            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }
}
