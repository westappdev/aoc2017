using System;
using System.IO;

namespace AoC2017
{
    class Day02b
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(@"../inputs/day02.txt");

            var sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int[] values = Array.ConvertAll(input[i].Split(new char[] { ' ', '\t' }), s => int.Parse(s));

                for (int j = 0; j < values.Length; j++)
                {
                    var thisValue = 0;

                    for (int k = 0; k < values.Length; k++)
                    {
                        if (values[j] > values[k] && (values[j] % values[k]) == 0)
                        {
                            thisValue = values[j] / values[k];
                            break;
                        }
                    }

                    if (thisValue > 0)
                    {
                        sum += thisValue;
                        break;
                    }
                }
            }

            Console.WriteLine($"Part 2: {sum}");
            Console.ReadKey();
        }
    }
}
