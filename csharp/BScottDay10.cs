using System;
using System.IO;
using System.Text;

namespace AOC2017
{
    class BScottDay10 : BScottSolution
    {
        public override string Name => "Day 10: Knot Hash";

        public override void Run()
        {
            string input = File.ReadAllText("BScottDay10.txt");
            Console.WriteLine($"Part 1 Answer: {KnotHashPart1(input)}");
            Console.WriteLine($"Part 2 Answer: {KnotHashPart2(input)}");

            Console.WriteLine($"Part 2 Example Answer #1 (blank): {KnotHashPart2("")}");
            Console.WriteLine($"Part 2 Example Answer #2 (AoC 2017): {KnotHashPart2("AoC 2017")}");
            Console.WriteLine($"Part 2 Example Answer #3 (1,2,3): {KnotHashPart2("1,2,3")}");
            Console.WriteLine($"Part 2 Example Answer #4 (1,2,4): {KnotHashPart2("1,2,4")}");
        }

        private int KnotHashPart1(string input)
        {
            int[] lengths = Array.ConvertAll(input.Split(','), s => int.Parse(s));
            byte[] list = new byte[256];
            int pos = 0, skipSize = 0;

            // initialize list
            for (int i = 0; i < list.Length; i++)
                list[i] = (byte)i;

            for (int i = 0; i < lengths.Length; i++)
            {
                byte[] buff = new byte[lengths[i]];

                for (int j = 0; j < buff.Length; j++)
                    buff[j] = list[(pos + j) % list.Length];

                Array.Reverse(buff);

                for (int j = 0; j < buff.Length; j++)
                    list[(pos + j) % list.Length] = buff[j];

                pos += (lengths[i] + skipSize) % list.Length; // wrap around
                skipSize++; // increase skip size
            }

            return list[0] * list[1];
        }

        private string KnotHashPart2(string input)
        {
            String suffix = Encoding.ASCII.GetString(new byte[] { 17, 31, 73, 47, 23 });
            byte[] lengths = Encoding.ASCII.GetBytes(input + suffix); //string of bytes and add a suffix
            byte[] list = new byte[256];
            byte[] denseHash = new byte[16];
            int pos = 0, skipSize = 0;

            // initialize list
            for (int i = 0; i < list.Length; i++)
                list[i] = (byte)i;

            // 64 rounds of fun
            for (int x = 0; x < 64; x++)
            {
                for (int i = 0; i < lengths.Length; i++)
                {
                    byte[] buff = new byte[lengths[i]];

                    for (int j = 0; j < buff.Length; j++)
                        buff[j] = list[(pos + j) % list.Length];

                    Array.Reverse(buff);

                    for (int j = 0; j < buff.Length; j++)
                        list[(pos + j) % list.Length] = buff[j];

                    pos += (lengths[i] + skipSize) % list.Length; // wrap around
                    skipSize++; // increase skip size
                }
            }

            // do the Xor magic
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                    denseHash[i] ^= list[i * 16 + j];
            }

            return BitConverter.ToString(denseHash).Replace("-", "").ToLower();
        }
    }
}
