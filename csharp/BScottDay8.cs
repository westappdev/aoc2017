using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2017
{
    class BScottDay8
    {
        private const int ARG_REG = 0;
        private const int ARG_OP = 1;
        private const int ARG_VALUE = 2;
        private const int ARG_KEYWORD = 3;
        private const int ARG_COND_REG = 4;
        private const int ARG_COND = 5;
        private const int ARG_COND_VAL = 6;

        static void Main(string[] args)
        {
            string[] example =  new string[]
            {
                "b inc 5 if a > 1",
                "a inc 1 if b < 5",
                "c dec -10 if a >= 1",
                "c inc -20 if c == 10"
            };

            Tuple<int, int> exampleResult = RunCpu(example);
            Console.WriteLine($"Part 1 Example Answer: {exampleResult.Item1}");
            Console.WriteLine($"Part 2 Example Answer: {exampleResult.Item2}");

            string[] input = File.ReadAllLines("BScottDay8.txt");

            Tuple<int, int> result = RunCpu(input);
            Console.WriteLine($"Part 1 Answer: {result.Item1}");
            Console.WriteLine($"Part 2 Answer: {result.Item2}");

            Console.ReadLine();
        }

        static Tuple<int,int> RunCpu(string[] input)
        {
            int highestValue = 0;
            Dictionary<string, int> r = new Dictionary<string, int>();
            for (int i = 0; i < input.Length; i++)
            {
                string[] args = input[i].Split(' ');

                // create any missing registers
                if(!r.ContainsKey(args[ARG_REG]))
                    r.Add(args[ARG_REG], 0);
                if (!r.ContainsKey(args[ARG_COND_REG]))
                    r.Add(args[ARG_COND_REG], 0);

                // evaluate the condition
                int x = r[args[ARG_COND_REG]];
                int y = int.Parse(args[ARG_COND_VAL]);
                bool cond = false;
                switch (args[ARG_COND])
                {
                    case ">":
                        cond = x > y;
                        break;
                    case ">=":
                        cond = x >= y;
                        break;
                    case "==":
                        cond = x == y;
                        break;
                    case "<=":
                        cond = x <= y;
                        break;
                    case "<":
                        cond = x < y;
                        break;
                    case "!=":
                        cond = x != y;
                        break;
                }

                // execute the operation if the condition is met
                if (cond)
                {
                    switch (args[ARG_OP])
                    {
                        case "inc":
                            r[args[ARG_REG]] += int.Parse(args[ARG_VALUE]);
                            break;
                        case "dec":
                            r[args[ARG_REG]] -= int.Parse(args[ARG_VALUE]);
                            break;
                    }

                    // keep track of the highest register
                    if (r[args[ARG_REG]] > highestValue)
                        highestValue = r[args[ARG_REG]];
                }
            }

            return Tuple.Create(r.OrderByDescending(x => x.Value).First().Value, highestValue);
        }
    }
}
