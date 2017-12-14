using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2017
{
    class BScottDay13 : BScottSolution
    {
        public override string Name => "Day 13: Packet Scanners";

        public override void Run()
        {
            string[] exampleInput = new[] {
                "0: 3",
                "1: 2",
                "4: 4",
                "6: 4"
            };

            Console.WriteLine($"Part 1 Example Answer: {PacketScannerPart1(exampleInput)}");
            Console.WriteLine($"Part 2 Example Answer: {PacketScannerPart2(exampleInput)}");

            string[] input = File.ReadAllLines("BScottDay13.txt");

            Console.WriteLine($"Part 1 Answer: {PacketScannerPart1(input)}");
            Console.WriteLine($"Part 2 Answer: {PacketScannerPart2(input)}");

        }

        private int PacketScannerPart1(string[] input)
        {
            Dictionary<int, Layer> firewallList = new Dictionary<int, Layer>();

            foreach (var layer in input)
            {
                int[] args = Array.ConvertAll(layer.Split(':'), s => int.Parse(s.Trim()));
                firewallList.Add(args[0], new Layer(args[0], args[1]));
            }

            int totalSeverity = 0;
            foreach (int key in firewallList.Keys)
            {
                if (key % (2 * firewallList[key].Range - 2) == 0)
                    totalSeverity += firewallList[key].Severity;
            }

            return totalSeverity;
        }

        private int PacketScannerPart2(string[] input)
        {
            Dictionary<int, Layer> firewallList = new Dictionary<int, Layer>();

            foreach (var layer in input)
            {
                int[] args = Array.ConvertAll(layer.Split(':'), s => int.Parse(s.Trim()));
                firewallList.Add(args[0], new Layer(args[0], args[1]));
            }

            for (int delay = 10; delay < int.MaxValue; delay++)
            {
                bool isCaught = false;
                foreach (int key in firewallList.Keys)
                {
                    if ((key + delay) % (2 * firewallList[key].Range - 2) == 0)
                    {
                        isCaught = true;
                        break;
                    }
                }

                if (!isCaught)
                    return delay;
            }

            return -1;
        }

        class Layer
        {
            public int Depth { get; private set; }
            public int Range { get; private set; }
            public int Severity => Depth * Range;

            public Layer(int depth, int range)
            {
                Depth = depth;
                Range = range;
            }
        }
    }
}
