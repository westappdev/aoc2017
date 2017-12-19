using System;
using System.Collections.Generic;

namespace AOC2017
{
    class BScottDay15 : BScottSolution
    {
        public override string Name => "Day 15: Dueling Generators";

        public override void Run()
        {
            int sampleSize = 40000000;
            int inputA = 679;
            int inputB = 771;

            Console.WriteLine($"Part 1 Example Answer: {DuelingGeneratorsPart1(sampleSize, 65, 8921)}");
            Console.WriteLine($"Part 1 Answer: {DuelingGeneratorsPart1(sampleSize, inputA, inputB)}");

            Console.WriteLine($"Part 2 Example Answer: {DuelingGeneratorsPart2(sampleSize, 65, 8921)}");
            Console.WriteLine($"Part 2 Answer: {DuelingGeneratorsPart2(sampleSize, inputA, inputB)}");
        }

        private int DuelingGeneratorsPart1(int sampleSize, int generatorA, int generatorB)
        {
            int count = 0;
            for (int i = 0; i < sampleSize; i++)
            {
                generatorA = (int)(((ulong) generatorA * 16807ul) % int.MaxValue);
                generatorB = (int)(((ulong) generatorB * 48271ul) % int.MaxValue);

                if ((generatorA & 0xffff) == (generatorB & 0xffff))
                    count++;
            }
            return count;
        }

        private int DuelingGeneratorsPart2(int sampleSize, int generatorA, int generatorB)
        {
            int count = 0;
            List<int> cmpA = new List<int>();
            List<int> cmpB = new List<int>();

            for (int i = 0; i < sampleSize; i++)
            {
                generatorA = (int)(((ulong)generatorA * 16807ul) % int.MaxValue);
                generatorB = (int)(((ulong)generatorB * 48271ul) % int.MaxValue);

                if (generatorA % 4 == 0) cmpA.Add(generatorA);
                if (generatorB % 8 == 0) cmpB.Add(generatorB);
            }

            for (int i = 0; i < Math.Min(cmpA.Count, cmpB.Count); i++)
            {
                if ((cmpA[i] & 0xffff) == (cmpB[i] & 0xffff))
                    count++;
            }

            return count;
        }
    }
}
