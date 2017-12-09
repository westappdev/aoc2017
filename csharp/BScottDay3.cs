using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2017
{
    class BScottDay3 : BScottSolution
    {
        public override string Name => "Day 3: Spiral Memory";

        public override void Run()
        {
            int value = 1;
            Console.WriteLine($"Value: {value}, Distance: {GetDistanceToAccessPortPart1(value)}");

            value = 12;
            Console.WriteLine($"Value: {value}, Distance: {GetDistanceToAccessPortPart1(value)}");

            value = 23;
            Console.WriteLine($"Value: {value}, Distance: {GetDistanceToAccessPortPart1(value)}");

            value = 1024;
            Console.WriteLine($"Value: {value}, Distance: {GetDistanceToAccessPortPart1(value)}");

            value = 325489;
            Console.WriteLine($"Value: {value}, Distance: {GetDistanceToAccessPortPart1(value)}");
        }

        static int GetDistanceToAccessPortPart1(int value)
        {
            Point start = new Point(0, 0);
            Point x = GetPosition(value);
            return Math.Abs(start.X - x.X) + Math.Abs(start.Y - x.Y);
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
