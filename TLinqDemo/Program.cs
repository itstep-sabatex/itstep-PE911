using System;
using System.Linq;

namespace TLinqDemo
{
    class Program
    {
        static double Factorial(int x)
        {
            double result = 1;
            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            string s = "";
            for (int i = 1; i <= 27000; i++)
            {
                s = $"{s} {i}";
            }


            return result;
        }

        static void Main(string[] args)
        {
            int[] numbers = new int[] { 10000, 20000, 30000, 40000, 50000, 60000, 70000, 80000, 90000, 100000, 120000, 130000, 140000, 150000 };
            var dstart = DateTime.Now;
            var factorial = from n in numbers select Factorial(n);
            factorial.ToArray();
            var dend = DateTime.Now;
            Console.WriteLine($"single - {dend - dstart}");
            dstart = DateTime.Now;
            factorial = from n in numbers.AsParallel() select Factorial(n);
            factorial.ToArray();
            dend = DateTime.Now;
            Console.WriteLine($"parallel - {dend - dstart}");
        }



    }
}
