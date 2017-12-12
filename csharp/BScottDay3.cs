using System;

namespace AOC2017
{
    class BScottDay3 : BScottSolution
    {
        public override string Name => "Day 3: Spiral Memory";

        public override void Run()
        {
            Console.WriteLine($"Part 1 Example Answer #1 (1): {SpiralMemoryPart1(1)}");
            Console.WriteLine($"Part 1 Example Answer #2 (12): {SpiralMemoryPart1(12)}");
            Console.WriteLine($"Part 1 Example Answer #3 (23): {SpiralMemoryPart1(23)}");
            Console.WriteLine($"Part 1 Example Answer #4 (1024): {SpiralMemoryPart1(1024)}");

            int value = 325489; // Puzzle Input
            Console.WriteLine($"Part 1 Answer: {SpiralMemoryPart1(value)}");
            Console.WriteLine($"Part 2 Answer: {SpiralMemoryPart2(value)}");
        }

        static int SpiralMemoryPart1(int value)
        {
            Point position = GetPosition(value);
            return Math.Abs(position.X) + Math.Abs(position.Y);
        }

        // Unfortunatly I can't take credit for solving this. This was ported and optimized slightly from a C function posted to github.
        // Included this just for the sake of completion.
        // Source: https://github.com/vesche/adventofcode-2017/blob/master/day03.c
        // Integer Sequence: https://oeis.org/A141481/b141481.txt
        static int SpiralMemoryPart2(int value)
        {
            int x = 0, y = 0, dx = 0, dy = -1;
            int[,] array = new int[1000, 3];
            int[,] coords = new int[,] { { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 } };

            for (int step = 0; step < 1000; step++)
            {
                int total = 0;

                for (int i = 0; i < 1000; i++)
                {
                    int tx = array[i, 0];
                    int ty = array[i, 1];

                    for (int j = 0; j < 8; j++)
                    {
                        if ((x + coords[j, 0] == tx) && (y + coords[j, 1] == ty))
                            total += array[i, 2];
                    }
                }

                array[step, 0] = x;
                array[step, 1] = y;
                array[step, 2] = (x == 0 && y == 0) ? 1 : total;

                if (total > value)
                    return total;

                if ((x == y) || ((x < 0) && (x == -y)) || ((x > 0) && (x == 1 - y)))
                {
                    int dxtmp = dx;
                    dx = -dy;
                    dy = dxtmp;
                }

                x += dx;
                y += dy;
            }

            return -1;
        }

        // Inverse ulam spiral coords
        static Point GetPosition(int n)
        {
            double k = Math.Ceiling((Math.Sqrt(n) - 1) / 2);
            double t = 2 * k + 1;
            double m = Math.Pow(t, 2);

            t = t - 1;
            if (n >= m - t)
                return new Point((int)(k - (m - n)), (int)-k);

            m = m - t;
            if (n >= m - t)
                return new Point((int)-k, (int)(-k + (m - n)));

            m = m - t;
            if (n >= m - t)
                return new Point((int)(-k + (m - n)), (int)k);

            return new Point((int)k, (int)(k - (m - n - t)));
        }

        struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
    }
}
