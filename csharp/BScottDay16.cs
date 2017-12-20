using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2017
{
    class BScottDay16 : BScottSolution
    {
        public override string Name => "Day 16: Permutation Promenade";

        public override void Run()
        {
            string[] input = File.ReadAllText("BScottDay16.txt").Split(',');

            Console.WriteLine($"Part 1 Answer: {PermutationPromenadePart1(input)}");
            Console.WriteLine($"Part 2 Answer: {PermutationPromenadePart2(input)}");
        }

        private string PermutationPromenadePart1(string[] instructions)
        {
            char[] programs = new char[16];

            for (int i = 0; i < programs.Length; i++)
                programs[i] = (char)('a' + i);

            for (int i = 0; i < instructions.Length; i++)
            {
                string[] args = instructions[i].Substring(1).Split('/');
                int a, b;
                char tmp;
                switch (instructions[i][0])
                {
                    case 's': // Spin
                        int startPos = 16 - int.Parse(args[0]);
                        char[] temp = new char[16];
                        for (int j = 0; j < temp.Length; j++)
                            temp[j] = programs[(j + startPos) % temp.Length];
                        for (int j = 0; j < temp.Length; j++)
                            programs[j] = temp[j];
                        break;

                    case 'x': // Exchange
                        a = int.Parse(args[0]);
                        b = int.Parse(args[1]);
                        tmp = programs[b];
                        programs[b] = programs[a];
                        programs[a] = tmp;
                        break;

                    case 'p': // Partner
                        a = Array.IndexOf(programs, args[0][0]);
                        b = Array.IndexOf(programs, args[1][0]);
                        tmp = programs[b];
                        programs[b] = programs[a];
                        programs[a] = tmp;
                        break;
                }
            }

            return new String(programs);
        }

        private string PermutationPromenadePart2(string[] instructions)
        {
            char[] programs = new char[16];

            List<string> cache = new List<string>();

            for (int i = 0; i < programs.Length; i++)
                programs[i] = (char)('a' + i);

            // add initial state to cache
            cache.Add(new string(programs));

            for (int x = 0; x < 10000; x++)
            {
                for (int i = 0; i < instructions.Length; i++)
                {
                    string[] args = instructions[i].Substring(1).Split('/');
                    char tmp;
                    int a, b;
                    switch (instructions[i][0])
                    {
                        case 's': // Spin
                            int startPos = 16 - int.Parse(args[0]);
                            char[] temp = new char[16];
                            for (int j = 0; j < temp.Length; j++)
                                temp[j] = programs[(j + startPos) % temp.Length];
                            for (int j = 0; j < temp.Length; j++)
                                programs[j] = temp[j];
                            break;

                        case 'x': // Exchange
                            a = int.Parse(args[0]);
                            b = int.Parse(args[1]);
                            tmp = programs[b];
                            programs[b] = programs[a];
                            programs[a] = tmp;
                            break;

                        case 'p': // Partner
                            a = Array.IndexOf(programs, args[0][0]);
                            b = Array.IndexOf(programs, args[1][0]);
                            tmp = programs[b];
                            programs[b] = programs[a];
                            programs[a] = tmp;
                            break;
                    }
                }

                // eventually the same sequence will happen again, and when it does you have
                // considerably reduced the number of times you actually have to perform the sequence.
                string s = new string(programs);
                if (cache.Contains(s))
                    break;
                cache.Add(s);
            }

            return cache[1000000000 % cache.Count];
        }
    }
}
