using System;
using System.IO;
using System.Text;

namespace AOC2017
{
    class BScottDay9 : BScottSolution
    {
        public override string Name => "Day 9: Stream Processing";

        public override void Run()
        {
            string input = File.ReadAllText("BScottDay9.txt");

            Tuple<int, int> processStreamResult = ProcessStream(input);
            Console.WriteLine($"Part 1 Answer: {processStreamResult.Item1}");
            Console.WriteLine($"Part 2 Answer: {processStreamResult.Item2}");
        }

        static Tuple<int, int> ProcessStream(string input)
        {
            int total = 0, nestingLevel = 0;
            Tuple<int, string> filterGarbageResult = FilterGarbage(input);
            int filteredCount = filterGarbageResult.Item1;
            string filteredInput = filterGarbageResult.Item2;

            for (int i = 0; i < filteredInput.Length; i++)
            {
                if (filteredInput[i] == '{')
                    nestingLevel++;
                else if (filteredInput[i] == '}')
                    total += nestingLevel--;
            }

            return Tuple.Create(total, filteredCount);
        }

        static Tuple<int, string> FilterGarbage(string input)
        {
            bool inGarbage = false;
            StringBuilder filteredInput = new StringBuilder(input.Length);
            int filteredCount = 0;

            // filter out garbage
            for (int i = 0; i < input.Length; i++)
            {
                if (!inGarbage && input[i] == '<')
                    inGarbage = true;
                else if (input[i] == '>')
                    inGarbage = false;
                else if (input[i] == '!')
                    i++;
                else
                {
                    if (!inGarbage)
                        filteredInput.Append(input[i]);
                    else
                        filteredCount++;
                }
            }

            return Tuple.Create(filteredCount, filteredInput.ToString());
        }
    }
}
