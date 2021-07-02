using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
    class Program
    {
        static int a;
        static int Summ(params int[] values)
        {
            return values.Sum();
        }

        static async Task<string> Summ(params double[] values)
        {
            
            var s = await File.ReadAllTextAsync("");
            //return await Task.FromResult(values.Sum());
            return s;
        }


        static  void Main(string[] args)
        {

            var t = new Task(() =>
            {
                for (int i = 0; i < 10; i++)
                    Console.WriteLine($"Message from task, LOOP = {i}");

            });
            t.Start();
            //var r = await Summ(12, 23.5, 23, 45, 67);


            //Task.Run(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //        Console.WriteLine($"Message from task, LOOP = {i}");

            //});



            //var t=Task.Run(() => Summ(10, 23, 45, 5));



            //t.Wait();

            //var result = t.Result;

            //Console.WriteLine($"Result calc  = {t.Result}");

            Parallel.For(0, 100, i => 
            {
                Console.WriteLine($"For Demo Thread={Thread.CurrentThread.ManagedThreadId} i={i} ");
                Thread.Sleep(1000);
            });

            var values = new double[] { 23, 45, 6, 20, 67, 98, 22 };

            var result = values.AsParallel().Select(s => s * 23).ToArray();

            Parallel.ForEach(values, i =>
            {
                Console.WriteLine($"Foreach Demo Thread={Thread.CurrentThread.ManagedThreadId} i={i} ");
                Thread.Sleep(100);
            });
            Parallel.Invoke(
            () =>
            {
                Console.WriteLine($"Thread={Thread.CurrentThread.ManagedThreadId} invoke 1 ");
                Thread.Sleep(100);
            },
            () =>
            {
                Console.WriteLine($"Thread={Thread.CurrentThread.ManagedThreadId} invoke 2 ");
                Thread.Sleep(100);
            },
            () =>
            {
                Console.WriteLine($"Thread={Thread.CurrentThread.ManagedThreadId} invoke 3 ");
                Thread.Sleep(100);
            }


                );

        }
    }
}
