using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2017
{
    class BScottDay25 : BScottSolution
    {
        public override string Name => "Day 25: The Halting Problem";

        public override void Run()
        {
            TuringMachine machine = new TuringMachine(File.ReadAllLines("BScottDay25.txt"));
            Console.WriteLine($"Part 1 Answer: {machine.RunPart1()}");
        }
    }

    enum InstructionType
    {
        Write,
        Move,
        Continue
    }

    class State
    {
        public string Id { get; set; }
        public Dictionary<int, List<Instruction>> ConditionActions { get; set; }

        public State(string id)
        {
            Id = id;
            ConditionActions = new Dictionary<int, List<Instruction>>();
        }
    }

    class Instruction
    {
        public InstructionType InstructionType { get; set; }
        public object Value { get; set; }

        public Instruction(InstructionType instructionType, object value)
        {
            InstructionType = instructionType;
            Value = value;
        }
    }
    class TuringMachine
    {
        private readonly Dictionary<string, State> _states = new Dictionary<string, State>();
        private readonly string _initialState = "";
        private readonly long _checksumSteps = 0;

        public TuringMachine(string[] input)
        {
            State currentState = null;
            int currentCondition = -1;
            for (int i = 0; i < input.Length; i++)
            {
                if(input[i].Trim().Length == 0)
                    continue;

                string[] args = input[i].Trim().Replace(".", "").Replace(":", "").Split(' ');

                if (args[0] == "Begin")
                {
                    _initialState = args[3].Substring(0, 1);
                }
                else if (args[0] == "Perform")
                {
                    _checksumSteps = int.Parse(args[5]);
                }
                else if (args[0] == "In")
                {
                    if (currentState != null)
                        _states.Add(currentState.Id, currentState);
                    currentState = new State(args[2]);
                }
                else if (args[0] == "If")
                {
                    currentCondition = int.Parse(args[5]);
                    currentState?.ConditionActions.Add(currentCondition, new List<Instruction>());
                }
                else if (args[0] == "-")
                {
                    if (args[1] == "Write")
                        currentState?.ConditionActions[currentCondition].Add(new Instruction(InstructionType.Write, int.Parse(args[4])));
                    else if (args[1] == "Move")
                        currentState?.ConditionActions[currentCondition].Add(new Instruction(InstructionType.Move, args[6]));
                    else if (args[1] == "Continue")
                        currentState?.ConditionActions[currentCondition].Add(new Instruction(InstructionType.Continue, args[4]));
                }
            }

            // add final state
            if (currentState != null)
                _states.Add(currentState.Id, currentState);
        }

        public long RunPart1()
        {
            int[] tape = new int[_checksumSteps];
            int pos = tape.Length / 2;
            string state = _initialState;
            for (int i = 0; i < _checksumSteps; i++)
            {
                int value = tape[pos];
                for (int j = 0; j < _states[state].ConditionActions[value].Count; j++)
                {
                    switch (_states[state].ConditionActions[value][j].InstructionType)
                    {
                        case InstructionType.Move:
                            pos += ((string)_states[state].ConditionActions[value][j].Value == "left" ? -1 : 1);
                            break;
                        case InstructionType.Write:
                            tape[pos] = (int)_states[state].ConditionActions[value][j].Value;
                            break;
                        case InstructionType.Continue:
                            state = (string) _states[state].ConditionActions[value][j].Value;
                            break;
                    }
                }
            }
            return tape.Sum();
        }
    }
}
