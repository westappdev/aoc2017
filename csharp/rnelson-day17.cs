using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2017
{
    class Day17
    {
        static void Main(string[] args)
        {
            var inputFile = @"../inputs/day17.txt";
            var part1 = 0;
            var part2 = 0;

            var iterations = Int32.Parse(File.ReadAllText(inputFile));

            var buffer = new List<int> { 0 };
            int size = 1, current = 0, value = 1;

            while (size < 2018)
            {
                current = ((current + iterations) % buffer.Count) + 1;
                buffer.Insert(current, value);
                size++; value++;
            }

            part1 = buffer.ElementAt(current + 1);

            // Don't actually do all the work, we only care if our element
            // gets put into position 1; the 0 is /always/ at position 0.
            current = 0;
            for (var i = 1; i <= 50000000 + 1; i++)
            {
                current = ((current + iterations) % i) + 1;
                if (current == 1)
                {
                    part2 = i;
                }
            }

            Console.WriteLine($"Part 1: {part1}\nPart 2: {part2}");
        }
    }
}
