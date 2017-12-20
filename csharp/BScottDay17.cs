using System;

namespace AOC2017
{
    class BScottDay17 : BScottSolution
    {
        public override string Name => "Day 17: Spinlock";

        public override void Run()
        {
            int input = 355;

            Console.WriteLine($"Part 1 Example Answer: {SpinlockPart1(3, 2017)}");

            Console.WriteLine($"Part 1 Answer: {SpinlockPart1(input, 2017)}");
            Console.WriteLine($"Part 2 Answer: {SpinlockPart2(input, 50000000)}");
        }

        private int SpinlockPart1(int input, int count)
        {
            int pos = 0, size = 1;
            int[] buffer = new int[count+1];

            for (int i = 0; i < count; i++)
            {
                pos = (pos + input + 1) % size;
                // move everything over 1
                for (int j = size - 1; j > pos; j--)
                    buffer[j + 1] = buffer[j];
                // set value, increase size
                buffer[pos + 1] = size++;
            }

            return buffer[Array.IndexOf(buffer, count) + 1];
        }

        private int SpinlockPart2(int input, int count)
        {
            int pos = 0, size = 1;
            int value = 0;
            for (int i = 0; i < count; i++)
            {
                pos = (pos + input + 1) % size;
                if(pos + 1 == 1)
                    value = size;
                size++;
            }
            return value;
        }
    }
}
