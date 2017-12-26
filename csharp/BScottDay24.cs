using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2017
{
    class BScottDay24 : BScottSolution
    {
        private readonly List<Component> _components = new List<Component>();
        private uint _maxOverallStrength;
        private uint _maxLength;
        private uint _maxStrengthAmongLongest;

        public override string Name => "Day 24: Electromagnetic Moat";

        public override void Run()
        {
            string[] input = {
                "0/2",
                "2/2",
                "2/3",
                "3/4",
                "3/5",
                "0/1",
                "10/1",
                "9/10"
            };

            ProcessInput(input);
            Console.WriteLine($"Part 1 Example Answer: {_maxOverallStrength}");
            Console.WriteLine($"Part 2 Example Answer: {_maxStrengthAmongLongest}");

            ProcessInput(File.ReadAllLines("BScottDay24.txt"));
            Console.WriteLine($"Part 1 Answer: {_maxOverallStrength}");
            Console.WriteLine($"Part 2 Answer: {_maxStrengthAmongLongest}");
        }

        private void ProcessInput(string[] input)
        {
            _components.Clear();
            for (int i = 0; i < input.Length; i++)
            {
                string[] values = input[i].Split('/');
                _components.Add(new Component(uint.Parse(values[0]), uint.Parse(values[1]), false));
            }

            _maxOverallStrength = 0;
            _maxLength = 0;
            _maxStrengthAmongLongest = 0;

            RecursiveComponentScan(0, 0, 0);
        }

        private void RecursiveComponentScan(uint ports, uint length, uint strength)
        {
            _maxOverallStrength = Math.Max(strength, _maxOverallStrength);
            _maxLength = Math.Max(length, _maxLength);

            if (length == _maxLength)
                _maxStrengthAmongLongest = Math.Max(strength, _maxStrengthAmongLongest);

            foreach (var c in _components)
            {
                if (c.Used || (c.A != ports && c.B != ports)) continue;
                c.Used = true;
                RecursiveComponentScan((c.A == ports) ? c.B : c.A, length + 1, strength + c.A + c.B);
                c.Used = false;
            }
        }

        class Component
        {
            public uint A { get; }
            public uint B { get; }
            public bool Used { get; set; }

            public Component(uint a, uint b, bool used)
            {
                A = a;
                B = b;
                Used = used;
            }
        }
    }
}
