using System;
using System.IO;
using System.Linq;

namespace AOC2017
{
    class BScottDay2 : BScottSolution
    {
        public override string Name => "Day 2: Corruption Checksum";

        public override void Run()
        {
            string[] input = File.ReadAllLines("BScottDay2.txt");

            Console.WriteLine($"Part 1 Answer: {CalculateChecksumPart1(input)}");
            Console.WriteLine($"Part 2 Answer: {CalculateChecksumPart2(input)}");
        }

        static long CalculateChecksumPart1(string[] rows)
        {
            long checksum = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                int min = int.MaxValue, max = 0;
                int[] cols = Array.ConvertAll(rows[i].Split(new char[] {'\t'}), s => int.Parse(s));
                for (int j = 0; j < cols.Length; j++)
                {
                    if (cols[j] < min)
                        min = cols[j];
                    if (cols[j] > max)
                        max = cols[j];
                }
                checksum += Math.Abs(max - min);
            }
            return checksum;
        }

        static long CalculateChecksumPart2(string[] rows)
        {
            long checksum = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                int value = 0;
                int[] cols = Array.ConvertAll(rows[i].Split(new char[] { '\t' }), s => int.Parse(s));
                for (int j = 0; j < cols.Length; j++)
                {
                    for (int k = 0; k < cols.Length; k++)
                    {
                        if (cols[j] > cols[k] && (cols[j] % cols[k]) == 0)
                        {
                            value = cols[j] / cols[k];
                            break;
                        }
                    }
                    if (value > 0)
                        break;
                }
                checksum += value;
            }
            return checksum;
        }
    }
}