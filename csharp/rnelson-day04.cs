using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2017
{
    class Day04
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(@"../inputs/day04.txt");
            var part1 = 0;
            var part2 = 0;

            foreach (var line in input)
            {
                var split = line.Split(new char[] { ' ', '\t' });
                var uniques = split.Distinct().ToArray().Length;

                if (split.Length == uniques)
                {
                    part1++;
                }
            }

            foreach (var line in input)
            {
                var sets = Array.ConvertAll(line.Split(new char[] { ' ', '\t' }), s => string.Concat(s.OrderBy(c => c)));
                var uniq = sets.Distinct().ToArray().Length;

                if (sets.Length == uniq)
                {
                    part2++;
                }
            }

            Console.WriteLine($"Part 1: {part1}\nPart 2: {part2}");
        }
    }
}
