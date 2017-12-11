using System;
using System.IO;

namespace AOC2017
{
    class BScottDay11 : BScottSolution
    {
        public override string Name => "Day 11: Hex Ed";

        public override void Run()
        {
            Console.WriteLine($"Part 1 Example Answer #1 (ne,ne,ne):\t\t{CalculateDistances("ne,ne,ne").Item1}");
            Console.WriteLine($"Part 1 Example Answer #2 (ne,ne,sw,sw):\t\t{CalculateDistances("ne,ne,sw,sw").Item1}");
            Console.WriteLine($"Part 1 Example Answer #3 (ne,ne,s,s):\t\t{CalculateDistances("ne,ne,s,s").Item1}");
            Console.WriteLine($"Part 1 Example Answer #4 (se,sw,se,sw,sw):\t{CalculateDistances("se,sw,se,sw,sw").Item1}");

            string input = File.ReadAllText("BScottDay11.txt");
            Console.WriteLine($"Part 1 Answer:\t{CalculateDistances(input).Item1}");
            Console.WriteLine($"Part 2 Answer:\t{CalculateDistances(input).Item2}");
        }

        private static Tuple<int,int> CalculateDistances(string input)
        {
            string[] commands = input.Split(',');

            int x = 0, y = 0, distance = 0, maxDistance = 0;
            foreach (string cmd in commands)
            {
                switch (cmd)
                {
                    case "n":
                        y--;
                        break;
                    case "s":
                        y++;
                        break;

                    case "ne":
                        x++;
                        break;
                    case "sw":
                        x--;
                        break;

                    case "se":
                        y++;
                        x++;
                        break;

                    case "nw":
                        y--;
                        x--;
                        break;
                }

                distance = Math.Max(Math.Abs(y), Math.Max(Math.Abs(x), Math.Abs((x - y) * -1)));

                if (distance > maxDistance)
                    maxDistance = distance;
            }

            return Tuple.Create(distance,maxDistance);
        }
    }
}
