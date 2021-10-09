using System.Diagnostics;
using BenchmarkDotNet.Running;

namespace ListPerformance
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }


    }
}
