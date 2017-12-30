using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2017
{
    class BScottDay14 : BScottSolution
    {
        public override string Name => "Day 14: Disk Defragmentation";

        public override void Run()
        {
            string input = "flqrgnkx";
            Console.WriteLine($"Part 1 Example Answer: {DiskDefragmentationPart1(input)}");
            Console.WriteLine($"Part 2 Example Answer: {DiskDefragmentationPart2(input)}");

            input = "oundnydw";
            Console.WriteLine($"Part 1 Answer: {DiskDefragmentationPart1(input)}");
            Console.WriteLine($"Part 2 Answer: {DiskDefragmentationPart2(input)}");
        }

        private int DiskDefragmentationPart1(string input)
        {
            byte[][] disk = new byte[128][];
            for (int i = 0; i < 128; i++)
                disk[i] = KnotHash($"{input}-{i}");

            int sum = 0;
            for (int i = 0; i < 128; i++)
                for (int j = 0; j < 128; j++)
                    sum += disk[i][j];

            return sum;
        }

        private int DiskDefragmentationPart2(string input)
        {
            byte[][] disk = new byte[128][];
            for (int i = 0; i < 128; i++)
                disk[i] = KnotHash($"{input}-{i}");

            List<Tuple<int, int>> map = new List<Tuple<int, int>>();
            for (int i = 0; i < 128; i++)
                for (int j = 0; j < 128; j++)
                    if (disk[i][j] == 1)
                        map.Add(new Tuple<int, int>(i, j));

            int regions = 0;
            while (map.Count > 0)
            {
                FindRegion(map);
                regions++;
            }

            return regions;
        }

        private void FindRegion(List<Tuple<int, int>> map)
        {
            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();

            stack.Push(map[map.Count - 1]);
            map.RemoveAt(map.Count - 1);

            int[,] deltas = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
            while (stack.Count > 0)
            {
                Tuple<int, int> fragment = stack.Pop();
                for (int i = 0; i < deltas.GetLength(0); i++)
                {
                    int dy = fragment.Item1 + deltas[i, 0];
                    int dx = fragment.Item2 + deltas[i, 1];

                    if (dy < 0 || dy >= 128 || dx < 0 || dy >= 128)
                        continue;

                    int index = map.FindIndex(eri => eri.Item1 == dy && eri.Item2 == dx);
                    if (index != -1)
                    {
                        stack.Push(map[index]);
                        map.RemoveAt(index);
                    }
                }
            }
        }

        // from day 10
        private byte[] KnotHash(string input)
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

                    for (int j = 0; j < buff.Length; j++)
                        list[(pos + j) % list.Length] = buff[(buff.Length - 1) - j];

                    pos += (lengths[i] + skipSize++) % list.Length; // wrap around, increase skipsize
                }
            }

            // do the Xor magic
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                    denseHash[i] ^= list[i * 16 + j];
            }

            byte[] output = new byte[128];
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                    output[i * 8 + (7 - j)] = (byte)((denseHash[i] >> j) & 0x1);
            }

            return output;
        }
    }
}
