    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _4_Roman
{
    class Program
    {
        static void FindFib()
        {
            List<int> fibonacciNums = new List<int>();
            Parallel.For(0, 25, i => {
                fibonacciNums.Add(Fib(i));
            });
            fibonacciNums.Sort();
            string[] lines = fibonacciNums.Select(i => i.ToString()).ToArray();
            File.WriteAllLinesAsync("fib.txt", lines);
        }
        static void FindPrime()
        {
            List<int> primeNums = new List<int>();
            for(int i = 0; i < 500; i++)
            {
                if (isPrime(i))
                    primeNums.Add(i);
            }
            primeNums.Sort();
            string[] lines = primeNums.Select(i => i.ToString()).ToArray();
            File.WriteAllLinesAsync("prime.txt", lines);
        } 
        static int Fib(int n) => (n == 1 || n == 0) ? n : Fib(n - 1) + Fib(n - 2);
        static bool isPrime(int number)
        {
            bool CalculatePrime(int value)
            {
                var possibleFactors = Math.Sqrt(number);
                for(var factor = 2; factor <= possibleFactors; factor++)
                {
                    if (value % factor == 0) return false;
                }
                return true;
            }
            return number > 1 && CalculatePrime(number);
        }
        static void Main(string[] args)
        {
            Thread A = new Thread(FindFib) { Name = "Fibonacci Worker" };
            Thread B = new Thread(FindPrime) { Name = "Prime nums worker" };
            A.Start(); B.Start();
            string[] fibNums = File.ReadAllLines("fib.txt");
            string[] primeNums = File.ReadAllLines("prime.txt");
            Console.WriteLine($"Prime numbers count - {primeNums.Length}: ");
            foreach(var line in primeNums) Console.Write($"{line} ");
            Console.WriteLine($"\nFibo numbers count - {fibNums.Length}: ");
            foreach (var line in fibNums) Console.Write($"{line} ");
        }
    }
}
