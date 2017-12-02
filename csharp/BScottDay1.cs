using System;
using System.IO;

namespace AOC2017
{
    class BScottDay1
    {
        static void Main(string[] args)
        {
            string input = "1122";
            Console.WriteLine($"Example 1: {CalculateSumPart1(input)}");

            input = "1111";
            Console.WriteLine($"Example 2: {CalculateSumPart1(input)}");

            input = "1234";
            Console.WriteLine($"Example 3: {CalculateSumPart1(input)}");

            input = "91212129";
            Console.WriteLine($"Example 4: {CalculateSumPart1(input)}");

            input = File.ReadAllText("BScottDay1.txt");
            Console.WriteLine($"Part 1 Answer: {CalculateSumPart1(input)}");
            Console.WriteLine($"Part 2 Answer: {CalculateSumPart2(input)}");

            Console.ReadLine();
        }

        static long CalculateSumPart1(string input)
        {
            long sum = 0;
            for (int i = 0; i < input.Length; i++)
                if (input[i] == input[(i + 1) % input.Length])
                    sum += int.Parse(input[i].ToString());
            return sum;
        }

        static long CalculateSumPart2(string input)
        {
            long sum = 0;
            int step = input.Length / 2;
            for (int i = 0; i < input.Length; i++)
                if (input[i] == input[(i + step) % input.Length])
                    sum += int.Parse(input[i].ToString());
            return sum;
        }
    }
}
