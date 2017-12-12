using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2017
{
    class Day05
    {
        static void Main(string[] args)
        {
            var inputFile = @"../inputs/day05.txt";
            var part1 = 0;
            var part2 = 0;

            try
            {
                var input = Array.ConvertAll(File.ReadAllLines(inputFile), s => int.Parse(s));
                var idx = 0;

                for (;;)
                {
                    idx += input[idx]++;
                    part1++;
                }
            }
            catch (IndexOutOfRangeException) { }

            try
            {
                var input = Array.ConvertAll(File.ReadAllLines(inputFile), s => int.Parse(s));
                var idx = 0;

                for (;;)
                {
                    idx += (input[idx] > 2 ? input[idx]-- : input[idx]++);
                    part2++;
                }
            }
            catch (IndexOutOfRangeException) { }

            Console.WriteLine($"Part 1: {part1}\nPart 2: {part2}");
        }
    }
}
