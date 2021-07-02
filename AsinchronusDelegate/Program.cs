using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsinchronusDelegate
{
    class Program
    {
        public static double ThreadProc(double a,double b)
        {
            return a * b;
        }
        public delegate double DemoThread(double a, double b);

        static void Main(string[] args)
        {
            DemoThread dt = ThreadProc;
            IAsyncResult asyncResult = dt.BeginInvoke(23.34,12.34, CallBack, null);
            //while (!asyncResult.IsCompleted)
            //{
            //    Thread.Sleep(100);

            //}
            //var result = dt.EndInvoke(asyncResult);
            dt.BeginInvoke(25.34, 12.34, CallBack, null);
            Console.ReadKey();
        }

        private static void CallBack(IAsyncResult ar)
        {
            AsyncResult result = (AsyncResult)ar;
            var caller = result.AsyncDelegate as DemoThread;
            var valueResult = caller.EndInvoke(ar);
            Console.WriteLine($"Результат {valueResult}");
        }
    }
}
