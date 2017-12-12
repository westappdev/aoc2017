using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2017
{
    class BScottDay6 : BScottSolution
    {
        public override string Name => "Day 6: Memory Reallocation";

        public override void Run()
        {
            string input = File.ReadAllText("BScottDay6.txt");
            int[] banks = Array.ConvertAll(input.Split(new char[] { '\t' }), s => int.Parse(s));

            RedistributeMemoryResult redistributeMemoryResult = RedistributeMemory(banks);
            Console.WriteLine($"Part 1 Answer: {redistributeMemoryResult.ReallocCount}");
            Console.WriteLine($"Part 2 Answer: {redistributeMemoryResult.RepeatLoopCount}");
        }

        static RedistributeMemoryResult RedistributeMemory(int[] banks)
        {
            bool done = false;
            int index = Array.IndexOf(banks, banks.Max()); // find largest bank
            int reallocBlocks = banks[index];
            int reallocCount = 0, repeatLoopCount =0;
            Dictionary<string, int> uniqueList = new Dictionary<string, int>();
            banks[index] = 0;
            do
            {
                for (int i = 0; i < reallocBlocks; i++)
                {
                    index++;
                    banks[index % banks.Length]++;
                }

                string banksString = string.Join(",", banks);
                if (uniqueList.ContainsKey(banksString))
                {
                    repeatLoopCount = reallocCount - uniqueList[banksString];
                    done = true;
                }
                else
                {
                    uniqueList.Add(banksString, reallocCount);
                    index = Array.IndexOf(banks, banks.Max()); // find largest bank
                    reallocBlocks = banks[index];
                    banks[index] = 0;
                }

                reallocCount++;
            } while (!done);

            return new RedistributeMemoryResult(reallocCount, repeatLoopCount);
        }

        class RedistributeMemoryResult
        {
            public int ReallocCount { get; private set; }
            public int RepeatLoopCount { get; private set; }

            public RedistributeMemoryResult(int reallocCount, int repeatLoopCount)
            {
                ReallocCount = reallocCount;
                RepeatLoopCount = repeatLoopCount;
            }
        }
    }
}
