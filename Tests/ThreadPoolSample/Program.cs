using System;
using System.Diagnostics;
using System.Linq;

namespace ThreadPoolSample
{
    class Program
    {
        private static Random random = new();
        static void Main(string[] args)
        {
            var m1 = Matrix.GetRandomMatrix(100, 100, 0, 10, random.Next);
            var m2 = Matrix.GetRandomMatrix(100, 100, 0, 10, random.Next);

            Stopwatch sw = new();
            Console.WriteLine("простой цикл");
            sw.Start();
            var result = m1.Mul(m2);
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine($"сумма элементов для проверки: {result.Sum()}");

            Console.WriteLine("параллельный цикл");
            sw.Restart();
            result = m1.MulParallel(m2);
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine($"сумма элементов для проверки: {result.Sum()}");

            Console.WriteLine("range в параллельном цикле");
            sw.Restart();
            result = m1.MulParallelWithRange(m2);
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine($"сумма элементов для проверки: {result.Sum()}");

            Console.WriteLine("zip в параллельном цикле");
            sw.Restart();
            result = m1.MulParallelWithZip(m2);
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine($"сумма элементов для проверки: {result.Sum()}");


            Console.ReadLine();

        }
    }
}
