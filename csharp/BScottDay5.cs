using System;
using System.IO;

namespace AOC2017
{
    class BScottDay5
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("BScottDay5.txt");
            Console.WriteLine($"Part 1 Answer: {RunCpuPart1(input)}");
            Console.WriteLine($"Part 2 Answer: {RunCpuPart2(input)}");
            Console.ReadLine();
        }

        static int RunCpuPart1(string[] input)
        {
            int[] instructions = Array.ConvertAll(input, s => int.Parse(s));
            int count = 0, pc = 0;

            do
            {
                count++;
                pc = pc + instructions[pc]++;
            } while (pc < instructions.Length);

            return count;
        }

        static int RunCpuPart2(string[] input)

        {
            int[] instructions = Array.ConvertAll(input, s => int.Parse(s));
            int count = 0, pc = 0;

            do
            {
                count++;
                pc = pc + (instructions[pc] >= 3 ? instructions[pc]-- : instructions[pc]++);
            } while (pc < instructions.Length);

            return count;
        }
    }
}
