using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2017
{
    class BScottDay19 : BScottSolution
    {
        public override string Name => "Day 19: A Series of Tubes";

        public override void Run()
        {

            string[] input = File.ReadAllLines("BScottDay19.txt");

            char[][] map = new char[input.Length][];
            for (int i = 0; i < input.Length; i++)
                map[i] = input[i].ToCharArray();

            Console.WriteLine($"Part 1 Answer: {TubesPart1(map)}");
            Console.WriteLine($"Part 2 Answer: {TubesPart2(map)}");
        }

        private const int NORTH = 0;
        private const int EAST = 1;
        private const int SOUTH = 2;
        private const int WEST = 3;

        private string TubesPart1(char[][] map)
        {
            int currentX = 0, currentY = 0;
            int direction = SOUTH;
            List<char> letters = new List<char>();

            // get entry position
            currentY = 0; // first row
            currentX = Array.IndexOf(map[currentY], '|');
            direction = SOUTH;

            if (currentX == -1)
                return String.Empty;

            while (true)
            {
                switch (direction)
                {
                    case NORTH:
                        currentY--;
                        break;
                    case EAST:
                        currentX++;
                        break;
                    case SOUTH:
                        currentY++;
                        break;
                    case WEST:
                        currentX--;
                        break;
                }

                if (currentX < 0 || currentY < 0)
                    break;

                char c = map[currentY][currentX];
                switch (c)
                {
                    case ' ':
                        return new string(letters.ToArray());
                    case '-':
                    case '|':
                        break;
                    case '+':
                        direction = FindNextDirection(currentX, currentY, direction, map);
                        break;

                    default:
                        letters.Add(c);
                        break;
                }

            }

            return new string(letters.ToArray());
        }

        private int TubesPart2(char[][] map)
        {
            int currentX = 0, currentY = 0;
            int direction = SOUTH;
            int distance = 1;

            // get entry position
            currentY = 0; // first row
            currentX = Array.IndexOf(map[currentY], '|');
            direction = SOUTH;

            if (currentX == -1)
                return 0;

            while (true)
            {
                switch (direction)
                {
                    case NORTH:
                        currentY--;
                        break;
                    case EAST:
                        currentX++;
                        break;
                    case SOUTH:
                        currentY++;
                        break;
                    case WEST:
                        currentX--;
                        break;
                }

                if (currentX < 0 || currentY < 0)
                    break;

                char c = map[currentY][currentX];
                switch (c)
                {
                    case ' ':
                        return distance;
                    case '+':
                        direction = FindNextDirection(currentX, currentY, direction, map);
                        break;
                }

                distance++;
            }

            return distance;
        }

        private int FindNextDirection(int x, int y, int currentDirection, char[][] map)
        {
            char north = map[y - 1][x];
            char south = map[y + 1][x];
            char east = map[y][x + 1];
            char west = map[y][x - 1];

            switch (currentDirection)
            {
                case NORTH:
                    if (east == '-') return EAST;
                    if (west == '-') return WEST;
                    break;
                case EAST:
                    if (north == '|') return NORTH;
                    if (south == '|') return SOUTH;
                    break;
                case SOUTH:
                    if (east == '-') return EAST;
                    if (west == '-') return WEST;
                    break;
                case WEST:
                    if (north == '|') return NORTH;
                    if (south == '|') return SOUTH;
                    break;
            }

            return -1;
        }
    }
}