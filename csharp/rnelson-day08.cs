using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2017
{
    enum Command
    {
        Unknown,
        Increment,
        Decrement
    }

    enum Comparison
    {
        Unknown,
        LessThan,
        LessThanOrEqualTo,
        EqualTo,
        NotEqualTo,
        GreaterThan,
        GreaterThanOrEqualTo
    }

    class Condition
    {
        public string Left;
        public Comparison Comparison;
        public long Right;

        private Condition() {}

        public Condition(string left, Comparison comparison, long right)
        {
            Left = left;
            Comparison = comparison;
            Right = right;
        }
    }

    class Statement
    {
        public string Register { private set; get; }
        public Command Command { private set; get; }
        public long Amount { private set; get; }
        public Condition Condition { private set; get; }
        public Comparison Comparison { private set; get; }

        private Statement() {}

        public Statement(string register, Command command, long amount, Condition condition, Comparison comparison)
        {
            Register = register;
            Command = command;
            Amount = amount;
            Condition = condition;
            Comparison = comparison;
        }
    }

    class Day08
    {
        private static Dictionary<string, long> Registers { get; set; }
        private static List<Statement> Statements { get; set; }

        static void Main(string[] args)
        {
            var inputFile = @"../inputs/day08.txt";
            var part1 = long.MinValue;
            var part2 = long.MinValue;

            Registers = new Dictionary<string, long>();
            Statements = new List<Statement>();

            var lines = File.ReadAllLines(inputFile);
            foreach (var line in lines)
            {
                Statements.Add(ParseStatement(line));
            }

            foreach (var statement in Statements)
            {
                long regValue = GetRegister(statement.Condition.Left);
                long cmpValue = statement.Condition.Right;
                bool conditional = false;

                switch (statement.Condition.Comparison)
                {
                    case Comparison.LessThan:
                        conditional = regValue < cmpValue;
                        break;
                    case Comparison.LessThanOrEqualTo:
                        conditional = regValue <= cmpValue;
                        break;
                    case Comparison.GreaterThan:
                        conditional = regValue > cmpValue;
                        break;
                    case Comparison.GreaterThanOrEqualTo:
                        conditional = regValue >= cmpValue;
                        break;
                    case Comparison.EqualTo:
                        conditional = regValue == cmpValue;
                        break;
                    case Comparison.NotEqualTo:
                        conditional = regValue != cmpValue;
                        break;
                    case Comparison.Unknown:
                    default:
                        Console.Error.WriteLine("error: unknown comparison operator");
                        conditional = false;
                        break;
                }

                long currentValue = GetRegister(statement.Register);
                long incValue = 0;

                if (conditional)
                {
                    switch (statement.Command)
                    {
                        case Command.Increment:
                            incValue = statement.Amount;
                            break;
                        case Command.Decrement:
                            incValue = -1 * statement.Amount;
                            break;
                        case Command.Unknown:
                        default:
                            Console.Error.WriteLine("error: unknown command");
                            break;
                    }
                }

                long newValue = currentValue + incValue;
                SetRegister(statement.Register, newValue);

                if (newValue > part2)
                {
                    part2 = newValue;
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (var key in Registers.Keys)
            {
                var value = GetRegister(key);
                if (value > part1)
                {
                    part1 = value;
                    Console.WriteLine($"New largest value = {part1} ({key})");
                }
            }

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var key in Registers.Keys)
            {
                Console.WriteLine($"{key} = {GetRegister(key)}");
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Part 1: {part1}\nPart 2: {part2}");
        }

        static Statement ParseStatement(string line)
        {
            var parts = line.Split(' ');

            var register = parts[0];
            var amount = Int64.Parse(parts[2]);
            var left = parts[4];
            var right = Int64.Parse(parts[6]);

            Command command;
            switch (parts[1])
            {
                case "inc":
                    command = Command.Increment;
                    break;
                case "dec":
                    command = Command.Decrement;
                    break;
                default:
                    Console.Error.WriteLine("error: unsupported command \"{}\"", parts[1]);
                    command = Command.Unknown;
                    break;
            }

            Comparison comparison;
            switch (parts[5])
            {
                case "<":
                    comparison = Comparison.LessThan;
                    break;
                case "<=":
                    comparison = Comparison.LessThanOrEqualTo;
                    break;
                case ">":
                    comparison = Comparison.GreaterThan;
                    break;
                case ">=":
                    comparison = Comparison.GreaterThanOrEqualTo;
                    break;
                case "==":
                    comparison = Comparison.EqualTo;
                    break;
                case "!=":
                    comparison = Comparison.NotEqualTo;
                    break;
                default:
                    Console.Error.WriteLine("error: unsupported command \"{}\"", parts[1]);
                    comparison = Comparison.Unknown;
                    break;
            }

            Condition condition = new Condition(left, comparison, right);
            Statement statement = new Statement(register, command, amount, condition, comparison);

            return statement;
        }

        static long GetRegister(string register)
        {
            if (!Registers.ContainsKey(register))
            {
                Registers[register] = 0;
            }

            return Registers[register];
        }

        static void SetRegister(string register, long value)
        {
            Registers[register] = value;
        }
    }
}
