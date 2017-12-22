using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2017
{
    class BScottDay18 : BScottSolution
    {
        public override string Name => "Day 18: Duet";

        public override void Run()
        {
            string[] exampleInput = new[]
            {
                "set a 1",
                "add a 2",
                "mul a a",
                "mod a 5",
                "snd a",
                "set a 0",
                "rcv a",
                "jgz a -1",
                "set a 1",
                "jgz a -2"
            };

            Console.WriteLine($"Part 1 Example Answer: {DuetPart1(exampleInput)}");

            string[] exampleInput2 = new[]
            {
                "snd 1",
                "snd 2",
                "snd p",
                "rcv a",
                "rcv b",
                "rcv c",
                "rcv d"
            };

            Console.WriteLine($"Part 2 Example Answer: {DuetPart2(exampleInput2)}");

            string[] input = File.ReadAllLines("BScottDay18.txt");
            Console.WriteLine($"Part 1 Answer: {DuetPart1(input)}");
            Console.WriteLine($"Part 2 Answer: {DuetPart2(input)}");
        }

        private long DuetPart1(string[] instructions)
        {
            Dictionary<char, long> registers = new Dictionary<char, long>();

            // initialize registers
            for(char i = 'a'; i <= 'z'; i++)
                registers.Add(i,0);

            long snd = 0, rcv = 0;

            for(int i = 0; i < instructions.Length; i++)
            {
                string[] args = instructions[i].Split(' ');
                long value = 0;
                switch (args[0])
                {
                    case "snd":
                        snd = registers[args[1][0]];
                        break;

                    case "set":
                        if (long.TryParse(args[2], out value))
                            registers[args[1][0]] = value;
                        else
                            registers[args[1][0]] = registers[args[2][0]];
                        break;

                    case "add":
                        if (long.TryParse(args[2], out value))
                            registers[args[1][0]] += value;
                        else
                            registers[args[1][0]] += registers[args[2][0]];
                        break;

                    case "mul":
                        if (long.TryParse(args[2], out value))
                            registers[args[1][0]] *= value;
                        else
                            registers[args[1][0]] *= registers[args[2][0]];
                        break;

                    case "mod":
                        if (long.TryParse(args[2], out value))
                            registers[args[1][0]] %= value;
                        else
                            registers[args[1][0]] %= registers[args[2][0]];
                        break;

                    case "rcv":
                        if (registers[args[1][0]] > 0)
                            rcv = snd;
                        break;

                    case "jgz":
                        if (!long.TryParse(args[1], out value))
                            value = registers[args[1][0]];

                        if (value > 0)
                        {
                            if (!long.TryParse(args[2], out value))
                                value = registers[args[2][0]];
                            i += (int)(value-1);
                        }
                        break;
                }

                // recover operation ran
                if (rcv > 0)
                    break;
            }
            return rcv;
        }

        private long DuetPart2(string[] instructions)
        {
            Queue<long> programQueue0 = new Queue<long>();
            Queue<long> programQueue1 = new Queue<long>();

            Program program0 = new Program(0, instructions, programQueue0, programQueue1);
            Program program1 = new Program(1, instructions, programQueue1, programQueue0);

            while (!program0.Finished && !program1.Finished)
            {
                program0.RunInstruction();
                program1.RunInstruction();

                if (program0.PendingReceive && program1.PendingReceive)
                    break;
            }

            return program1.SendCount;
        }

        class Program
        {
            Dictionary<char, long> registers = new Dictionary<char, long>();
            private long pc = 0;
            private long sendCount = 0;
            private string[] instructions;
            private Queue<long> inputQueue;
            private Queue<long> outputQueue;
            private bool pendingReceive = false;
            private bool finished = false;

            public bool PendingReceive
            {
                get { return pendingReceive; }
            }

            public bool Finished
            {
                get { return finished; }
            }

            public long SendCount
            {
                get { return sendCount; }
            }

            public Program(int programId, string[] instructions, Queue<long> inputQueue, Queue<long> outputQueue)
            {
                for (char i = 'a'; i <= 'z'; i++)
                    this.registers.Add(i, 0);

                this.registers['p'] = programId;
                this.instructions = instructions;
                this.inputQueue = inputQueue;
                this.outputQueue = outputQueue;
            }

            public void RunInstruction()
            {
                string[] args = instructions[pc].Split(' ');
                long value = 0;
                switch (args[0])
                {
                    case "snd":
                        sendCount++;
                        if (long.TryParse(args[1], out value))
                            outputQueue.Enqueue(value);
                        else
                            outputQueue.Enqueue(registers[args[1][0]]);
                        break;

                    case "set":
                        if (long.TryParse(args[2], out value))
                            registers[args[1][0]] = value;
                        else
                            registers[args[1][0]] = registers[args[2][0]];
                        break;

                    case "add":
                        if (long.TryParse(args[2], out value))
                            registers[args[1][0]] += value;
                        else
                            registers[args[1][0]] += registers[args[2][0]];
                        break;

                    case "mul":
                        if (long.TryParse(args[2], out value))
                            registers[args[1][0]] *= value;
                        else
                            registers[args[1][0]] *= registers[args[2][0]];
                        break;

                    case "mod":
                        if (long.TryParse(args[2], out value))
                            registers[args[1][0]] %= value;
                        else
                            registers[args[1][0]] %= registers[args[2][0]];
                        break;

                    case "rcv":
                        if (inputQueue.Count == 0)
                        {
                            pendingReceive = true;
                            return;
                        }

                        registers[args[1][0]] = inputQueue.Dequeue();
                        break;

                    case "jgz":
                        if (!long.TryParse(args[1], out value))
                            value = registers[args[1][0]];

                        if (value > 0)
                        {
                            if (!long.TryParse(args[2], out value))
                                value = registers[args[2][0]];
                            pc += value;
                            return;
                        }
                        break;
                }

                if (++pc > instructions.Length)
                {
                    finished = true;
                    return;
                }
            }
        }
    }
}
