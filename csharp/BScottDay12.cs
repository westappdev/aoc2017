using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC2017
{
    class BScottDay12 : BScottSolution
    {
        public override string Name => "Day 12: Digital Plumber";

        public override void Run()
        {
            string[] exampleInput = new[] {
                "0 <-> 2",
                "1 <-> 1",
                "2 <-> 0, 3, 4",
                "3 <-> 2, 4",
                "4 <-> 2, 3, 6",
                "5 <-> 6",
                "6 <-> 4, 5"
            };

            Console.WriteLine($"Part 1 Example Answer: {DigitalPlumberPart1(exampleInput)}");
            Console.WriteLine($"Part 2 Example Answer: {DigitalPlumberPart2(exampleInput)}");

            string[] input = File.ReadAllLines("BScottDay12.txt");

            Console.WriteLine($"Part 1 Answer: {DigitalPlumberPart1(input)}");
            Console.WriteLine($"Part 2 Answer: {DigitalPlumberPart2(input)}");
        }

        private int DigitalPlumberPart1(string[] input)
        {
            Dictionary<int, int[]> programDictionary = ParseInput(input);
            List<int> connected = new List<int>();
            GetConnectedPrograms(programDictionary, connected, 0, 0);
            return connected.Count;
        }

        private int DigitalPlumberPart2(string[] input)
        {
            Dictionary<int, int[]> programDictionary = ParseInput(input);
            List<int> connected = new List<int>();
            int groups = 0;
            foreach (var programId in programDictionary.Keys)
            {
                if (!connected.Contains(programId))
                {
                    GetConnectedPrograms(programDictionary, connected, programId, 0);
                    groups++;
                }
            }
            return groups;
        }

        private void GetConnectedPrograms(Dictionary<int, int[]> programDictionary, List<int> connected, int start, int level)
        {
            // stack overflow protection
            if (level > 100)
                return;

            foreach (var programId in programDictionary[start])
            {
                if (!connected.Contains(programId))
                {
                    connected.Add(programId);
                    GetConnectedPrograms(programDictionary, connected, programId, ++level);
                }
            }
        }

        private Dictionary<int, int[]> ParseInput(string[] input)
        {
            Dictionary<int, int[]> programDictionary = new Dictionary<int, int[]>();
            Regex regex = new Regex(@"([\d]+) <-> (?:([\d]+)*(?:, )?)*");

            for (int i = 0; i < input.Length; i++)
            {
                Match match = regex.Match(input[i]);
                if (match.Success)
                {
                    List<int> programList = new List<int>();

                    foreach (Capture capture in match.Groups[2].Captures)
                        programList.Add(int.Parse(capture.Value));

                    programDictionary.Add(int.Parse(match.Groups[1].Value), programList.ToArray());
                }
            }

            return programDictionary;
        }
    }
}
