using System;
using System.Threading.Tasks;

namespace DeadLockDemo
{
    class Program
    {
        private static object lockerA = new object();
        private static object lockerB = new object();
        private static object lockerC = new object();
        private static object lockerD = new object();

        private static void MethodA()
        {
            Console.WriteLine("StART A");
            lock (lockerA)
            {
                Console.WriteLine("Thread A");
                MethodB();
            }
            Console.WriteLine("End A");
        }
        private static void MethodB()
        {
            Console.WriteLine("StART B");
            lock (lockerB)
            {
                Console.WriteLine("Thread B");
                MethodA();
            }
            Console.WriteLine("End B");
        }

        static void Main(string[] args)
        {
            Task.Run(() => MethodA());
            Task.Run(() => MethodA());
            Console.ReadKey();
        }
    }
}
