using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Program
    {
        record MatrixParams(double[,] a, double[,] b, double[,] result, int i, int j);
        const int size = 1000;
        static double MultipleOneElement(double [,] a, double [,] b,int i,int j)
        {
            double result = 0;
            for (int i1 = 0; i1 < size; i1++)
                result = result + a[i, i1] * b[i1, j];
            return result;
        }

        static double [,] GenerateMatrix()
        {
            double[,] result = new double[size, size];
            var r = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j=0;j<size;j++)
                {
                    result[i, j] = r.NextDouble();
                }
            }
            return result;
        }
        static double[] GenerateMatrixOneDimension()
        {
            double[] result = new double[size*size];
            var r = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i*j + j] = r.NextDouble();
                }
            }
            return result;
        }

        static double [,] MatrixMultibleOneThread(double[,] a,double[,] b)
        {
            var result = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = MultipleOneElement(a, b, i, j);
                }
            }
            return result;
        }

        static void ThreadMultiple(double[,] a, double[,] b, int i, int j,ref double result)
        {
            result = MultipleOneElement(a, b, i, j);
        }

        static void ThreadPoolMultiple(object obj)
        {
            MatrixParams matrixParams = obj as MatrixParams;
            matrixParams.result[matrixParams.i, matrixParams.j] = MultipleOneElement(matrixParams.a, matrixParams.b, matrixParams.i, matrixParams.j);
        }


        static double[,] MatrixMultibleThread(double[,] a, double[,] b)
        {
            var tr = new List<Thread>();
            var result = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                   if (tr.Count >= 8)
                   {
                        tr[0].Join();
                        tr = tr.Where(s => s.IsAlive).ToList();
                   }
                     
                    var ti = i;
                    var tj = j;

                    var ts = new Thread(new ThreadStart(() => ThreadMultiple(a,b,ti,tj,ref result[ti, tj])));
                    ts.Start();
                    tr.Add(ts);

                }
            }
 
  
            return result;
        }
        static double[,] MatrixMultiblePoolThread(double[,] a, double[,] b)
        {
            var result = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var matrixParams = new MatrixParams(a, b, result, i, j);
                    ThreadPool.QueueUserWorkItem(ThreadPoolMultiple,matrixParams);
                }
            }

            while (ThreadPool.PendingWorkItemCount > 0) Thread.Sleep(100);

            return result;
        }

        static double[,] MatrixMultibleTask(double[,] a, double[,] b)
        {
            var tasks = new Task<double>[size, size];

            for (int i = 0; i < size; i++)
            {

                for (int j = 0; j < size; j++)
                {
                    var it = i;
                    var jt = j;
                    tasks[i,j] = Task.Run(()=>MultipleOneElement(a,b,it,jt));
                }
            }
            foreach (var t in tasks) t.Wait();

            var result = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++) result[i, j] = tasks[i, j].Result;
            }
            return result;
        }
        static double[,] MatrixMultibleTaskHalf(double[,] a, double[,] b)
        {
            var tasks = new Task<double>[size];
            var result = new double[size, size];

            for (int i = 0; i < size; i++)
            {

                for (int j = 0; j < size; j++)
                {
                    var it = i;
                    var jt = j;
                    tasks[j] = Task.Run(() => MultipleOneElement(a, b, it, jt));
                }
                Task.WaitAll(tasks);
                for (int j = 0; j < size; j++) result[i, j] = tasks[j].Result;
            }
            return result;
        }


        public static void ThreadProc(string threadName)
        {
            double a = 2;
            for (int i = 0; i < 10; i++)
            {
                a = a * i + 5;
                
                Console.WriteLine($"{threadName} count {i}");
                Thread.Sleep(200);
            }
        }
        public delegate void DemoThread(string name);


        static void DoSomethingDemo(object state)
        {
            Console.WriteLine($"Pool parametr {(int)state}");
            Thread.Sleep(500);

            //for (int i = 0; i < 3; i++)
            //{
            //    //Console.WriteLine($"Виконуємо  трід з пула {Thread.CurrentThread.ManagedThreadId} на етапі {i}");
            //    Thread.Sleep(500);
            //}
        }

        
        static void Main(string[] args)
        {
            //ThreadPool.GetMinThreads(out int wokerThreads, out int completationPortThreads);
            //ThreadPool.GetMaxThreads(out int maxWokerThreads, out int maxCompletationPortThreads);

            //Console.WriteLine($"Мінімальна кількість потоків: {wokerThreads}");
            //Console.WriteLine($"Мінімальна кількість потоків вводу-виводу: {completationPortThreads}");
            //Console.WriteLine($"Максимальна кількість потоків: {maxWokerThreads}");
            //Console.WriteLine($"Максимальна кількість потоків вводу-виводу: {maxCompletationPortThreads}");



            //for (int i = 0; i < 100; i++)
            //{
            //    ThreadPool.QueueUserWorkItem(DoSomethingDemo,i);
            //}




            //Console.ReadKey();
            //var a = GenerateMatrix();
            //var b = GenerateMatrix();
            var m = GenerateMatrixOneDimension();

            var startD = DateTime.Now;
            var r = m.Select(s => (s * s) + (s * s)).ToList();
            //MatrixMultibleTaskHalf(a, b);
            //MatrixMultibleOneThread(a, b);
            //MatrixMultibleOneThread(a, b);
            //MatrixMultibleOneThread(a, b);
            //MatrixMultibleOneThread(a, b);
            var endD = DateTime.Now;
            Console.WriteLine($"Elapsed one thread {endD - startD}");




            ////ThreadPool.QueueUserWorkItem(() => MatrixMultibleOneThread(a, b));

            startD = DateTime.Now;
            var par = m.AsParallel().Select(s => (s * s)+(s*s)).ToList();
            //MatrixMultiblePoolThread(a, b);
            ////c = MatrixMultibleThread(a, b);
            //var t1 = new Thread(new ThreadStart(() => MatrixMultibleOneThread(a, b)));
            //var t2 = new Thread(new ThreadStart(() => MatrixMultibleOneThread(a, b)));
            //var t3 = new Thread(new ThreadStart(() => MatrixMultibleOneThread(a, b)));
            //var t4 = new Thread(new ThreadStart(() => MatrixMultibleOneThread(a, b)));
            //t1.Start();
            //t2.Start();
            //t3.Start();
            //t4.Start();
            //t4.Join();
            //t3.Join();
            //t2.Join();
            //t1.Join();

            endD = DateTime.Now;
            Console.WriteLine($"Elapsed one thread {endD - startD}");

            ////var t = new Thread(new ThreadStart(()=> MultipleOneElement(a,b, int i, int j)));
            //DemoThread dt = ThreadProc;
            //Thread t1 = new Thread(()=>dt.Invoke("Поток 1"));
            //t1.Priority = ThreadPriority.;
            //Thread  t2 = new Thread(() => dt.Invoke("Поток 2"));
            //t2.IsBackground = true;
            //Thread t3 = new Thread(() => dt.Invoke("Поток 3"));
            //t3.IsBackground = true;
            //t1.Start();
            //t2.Start();
            //t3.Start();
            //Thread.Sleep(1000);
            //Console.WriteLine("Hello World!");
        }
    }
}
