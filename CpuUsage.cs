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
        public double Core1Usage { get; set; }
        public double Core2Usage { get; set; }
        public double Core3Usage { get; set; }
        public double Core4Usage { get; set; }
        public double CpuUsage { get; set; }
    }
    public class CpuUsage
    {
        public Cpu ReadCpuUsage()
        {
            var output = "";
            var cpuInfo = new ProcessStartInfo("top -1 -n 1");
            cpuInfo.FileName = "/bin/bash";
            cpuInfo.Arguments = "-c \"top -1 -n 1\"";
            cpuInfo.RedirectStandardOutput = true;

            using (var process = Process.Start(cpuInfo))
            {
                output = process.StandardOutput.ReadToEnd();
                //Console.WriteLine(output);
            }

            var cpuLine = output.Split("\n").ToList();
            var cpuLine2 = cpuLine[2].Split(",", StringSplitOptions.RemoveEmptyEntries);
            var cpuLine3 = cpuLine[3].Split(",", StringSplitOptions.RemoveEmptyEntries);

            double core1 = double.Parse(cpuLine2[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[2]) +
                    double.Parse(cpuLine2[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]) +
                    double.Parse(cpuLine2[2].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
            core1 = Math.Round(core1, 2);

            double core2 = double.Parse(cpuLine2[7].Split(" ", StringSplitOptions.RemoveEmptyEntries)[5]) +
                    double.Parse(cpuLine2[8].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]) +
                    double.Parse(cpuLine2[9].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
            core2 = Math.Round(core2, 2);

            double core3 = double.Parse(cpuLine3[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[2]) +
                    double.Parse(cpuLine3[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]) +
                    double.Parse(cpuLine3[2].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
            core3 = Math.Round(core3, 2);

            double core4 = double.Parse(cpuLine3[7].Split(" ", StringSplitOptions.RemoveEmptyEntries)[5]) +
                    double.Parse(cpuLine3[8].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]) +
                    double.Parse(cpuLine3[9].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
            core4 = Math.Round(core4, 2);

            var cpuUsage = Math.Round(((core1 + core2 + core3 + core4) / 4), 2);

            return new Cpu()
            {
                Core1Usage = core1,
                Core2Usage = core2,
                Core3Usage = core3,
                Core4Usage = core4,
                CpuUsage = cpuUsage
            };
        }
    }
}