using System;
using System.Diagnostics;
using System.Threading;

namespace SystemUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new CpuUsage();
            while (true)
            {
                var metrics = client.ReadCpuUsage();

                Console.WriteLine("Core1: " + metrics.Core1Usage);
                Console.WriteLine("Core2: " + metrics.Core2Usage);
                Console.WriteLine("Core3: " + metrics.Core3Usage);
                Console.WriteLine("Core4: " + metrics.Core4Usage);
                Console.WriteLine("All Usage: " + metrics.CpuUsage);
                Thread.Sleep(1000);
            }
        }
    }
}
