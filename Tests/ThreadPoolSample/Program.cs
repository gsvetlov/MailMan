using System;
using System.Numerics;
using System.Threading;

namespace ThreadPoolSample
{
    class Program
    {
        static void Main(string[] args)
        {
            NumbersFun numbers = new();
            Console.WriteLine("Введите целое положительное число");            
            while (true)
            {
                var input = Console.ReadLine();
                bool cancel = string.IsNullOrWhiteSpace(input);
                if (cancel) break;
                int.TryParse(input, out int value);
                ThreadPool.QueueUserWorkItem(numbers.GetFactorial, value);
                ThreadPool.QueueUserWorkItem(numbers.GetSum, value);
            }
            Console.WriteLine();
        }


    }

    class NumbersFun
    {
        public void GetFactorial(object obj)
        {
            int number = (int)obj;
            if (number < 0) throw new ArgumentOutOfRangeException(nameof(number), "Отрицательные числа недопустимы");
            BigInteger result = 1;
            if (number > 1)
                for (int i = 1; i <= number; i++)
                    result *= i;
            Console.WriteLine($"{number}! = {result}");
        }

        public void GetSum(object obj)
        {
            int number = (int)obj;
            if (number < 0) throw new ArgumentOutOfRangeException(nameof(number), "Отрицательные числа недопустимы");
            long result = number * (number + 1) / 2;
            Console.WriteLine($"Сумма чисел от 1 до {number} равна {result}");
        }

    }
}
