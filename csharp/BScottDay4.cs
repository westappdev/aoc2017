using System;
using System.Linq;
using System.IO;

namespace AOC2017
{
    class BScottDay4 : BScottSolution
    {
        public override string Name => "Day 4: High-Entropy Passphrases";

        public override void Run()
        {
            string[] input = File.ReadAllLines("BScottDay4.txt");
            Console.WriteLine($"Part 1 Answer: {GetValidPassPhrasesPart1(input)}");
            Console.WriteLine($"Part 2 Answer: {GetValidPassPhrasesPart2(input)}");
        }

        static int GetValidPassPhrasesPart1(string[] input)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                string[] passStrings = input[i].Split(new char[] {' '});
                string[] distinctStrings = passStrings.Distinct().ToArray();
                if (passStrings.Length == distinctStrings.Length)
                    count++;
            }
            return count;
        }

        static int GetValidPassPhrasesPart2(string[] input)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                string[] passStrings = Array.ConvertAll(input[i].Split(new char[] { ' ' }), s => string.Concat(s.OrderBy(c => c)));
                string[] distinctStrings = passStrings.Distinct().ToArray();
                if (passStrings.Length == distinctStrings.Length)
                    count++;
            }
            return count;
        }
    }
}
