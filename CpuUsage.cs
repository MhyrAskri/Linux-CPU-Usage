using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SystemUsage
{
    public class Cpu
    {
        public double CpuUsage { get; set; }
    }
    public class CpuUsage
    {
        public Cpu ReadCpuUsage()
        {
            var output = "";
            var cpuInfo = new ProcessStartInfo("top -b -n 1");
            cpuInfo.FileName = "/bin/bash";
            cpuInfo.Arguments = "-c \"top -b -n 1\"";
            cpuInfo.RedirectStandardOutput = true;

            using (var process = Process.Start(cpuInfo))
            {
                output = process.StandardOutput.ReadToEnd();
                //Console.WriteLine(output);
            }

            var cpuLine = output.Split("\n").ToList();
            var cpuLine2 = cpuLine[2].Split(",", StringSplitOptions.RemoveEmptyEntries);
            var firstPart = cpuLine2[0].Split(":", StringSplitOptions.RemoveEmptyEntries);
            var secondPart = cpuLine2[1].Split("s", StringSplitOptions.RemoveEmptyEntries);
            var thirdPart = cpuLine2[2].Split("n", StringSplitOptions.RemoveEmptyEntries);

            double cpuUsage = double.Parse(firstPart[1].Split("u", StringSplitOptions.RemoveEmptyEntries)[0]) +
                    double.Parse(secondPart[0]) +
                    double.Parse(thirdPart[0]);

            cpuUsage = Math.Round(cpuUsage, 2);

            return new Cpu()
            {
                CpuUsage = cpuUsage
            };
        }
    }
}