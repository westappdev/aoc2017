using System;
using System.IO;

namespace AOC2017
{
    class BScottDay22 : BScottSolution
    {
        public override string Name => "Day 22: Sporifica Virus";

        public override void Run()
        {
            string[] exampleInput = new[]
            {
                "..#",
                "#..",
                "..."
            };
            Console.WriteLine($"Part 1 Example Answer: {SporificaVirusPart1(exampleInput, 10000)}");
            Console.WriteLine($"Part 2 Example Answer: {SporificaVirusPart2(exampleInput, 10000000)}");

            string[] input = File.ReadAllLines("BScottDay22.txt");
            Console.WriteLine($"Part 1 Answer: {SporificaVirusPart1(input, 10000)}");
            Console.WriteLine($"Part 2 Answer: {SporificaVirusPart2(input, 10000000)}");
        }

        private const int NORTH = 0;
        private const int EAST = 1;
        private const int SOUTH = 2;
        private const int WEST = 3;

        private const int LEFT = 0;
        private const int RIGHT = 1;
        private const int REVERSE = 2;

        private char[][] CreateGrid(string[] input)
        {
            int expand = 1000;
            int infiniteGridHeight = input.Length * (expand + 1);
            int infiniteGridWidth = input[0].Length * (expand + 1);

            // create big grid
            char[][] grid = new char[infiniteGridHeight][];
            for (int i = 0; i < infiniteGridHeight; i++)
                grid[i] = new char[infiniteGridWidth];

            // initialize big grid
            for (int i = 0; i < infiniteGridHeight; i++)
                for (int j = 0; j < infiniteGridWidth; j++)
                    grid[i][j] = '.';

            // copy to the center
            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input[0].Length; j++)
                    grid[i + (input.Length * expand / 2)][j + (input[0].Length * expand / 2)] = input[i][j];

            return grid;
        }

        private int SporificaVirusPart1(string[] input, int bursts)
        {
            char[][] grid = CreateGrid(input);

            int currentX = 0, currentY = 0;
            int direction = NORTH;
            int infected = 0;
            int centerY = (grid.Length / 2);
            int centerX = (grid[0].Length / 2);

            for (int i = 0; i < bursts; i++)
            {
                if (grid[centerY + currentY][centerX + currentX] == '.') // Clean
                {
                    direction = FindNextDirection(direction, LEFT);
                    grid[centerY + currentY][centerX + currentX] = '#';
                    infected++;
                }
                else // Infected
                {
                    direction = FindNextDirection(direction, RIGHT);
                    grid[centerY + currentY][centerX + currentX] = '.';
                }

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
            }

            return infected;
        }

        private int SporificaVirusPart2(string[] input, int bursts)
        {
            char[][] grid = CreateGrid(input);

            int currentX = 0, currentY = 0;
            int direction = NORTH;
            int infected = 0;

            int centerY = (grid.Length / 2);
            int centerX = (grid[0].Length / 2);

            for (int i = 0; i < bursts; i++)
            {
                if (grid[centerY + currentY][centerX + currentX] == '.') // Clean
                {
                    direction = FindNextDirection(direction, LEFT);
                    grid[centerY + currentY][centerX + currentX] = 'W';
                }
                else if (grid[centerY + currentY][centerX + currentX] == 'W') // Weakened
                {
                    grid[centerY + currentY][centerX + currentX] = '#';
                    infected++;
                }
                else if (grid[centerY + currentY][centerX + currentX] == '#') // Infected
                {
                    direction = FindNextDirection(direction, RIGHT);
                    grid[centerY + currentY][centerX + currentX] = 'F';
                }
                else // Flagged
                {
                    direction = FindNextDirection(direction, REVERSE);
                    grid[centerY + currentY][centerX + currentX] = '.';
                }

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
            }

            return infected;
        }

        private int FindNextDirection(int currentDirection, int turn)
        {
            switch (currentDirection)
            {
                case NORTH:
                    if (turn == RIGHT) return EAST;
                    if (turn == LEFT) return WEST;
                    if (turn == REVERSE) return SOUTH;
                    break;
                case EAST:
                    if (turn == LEFT) return NORTH;
                    if (turn == RIGHT) return SOUTH;
                    if (turn == REVERSE) return WEST;
                    break;
                case SOUTH:
                    if (turn == LEFT) return EAST;
                    if (turn == RIGHT) return WEST;
                    if (turn == REVERSE) return NORTH;
                    break;
                case WEST:
                    if (turn == RIGHT) return NORTH;
                    if (turn == LEFT) return SOUTH;
                    if (turn == REVERSE) return EAST;
                    break;
            }

            return -1;
        }
    }
}
