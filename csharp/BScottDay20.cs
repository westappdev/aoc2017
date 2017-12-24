using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2017
{
    class BScottDay20 : BScottSolution
    {
        public override string Name => "Day 20: Particle Swarm";

        public override void Run()
        {
            string[] input = File.ReadAllLines("BScottDay20.txt");

            Console.WriteLine($"Part 1 Answer: {ParticleSwarmPart1(input)}");
            Console.WriteLine($"Part 2 Answer: {ParticleSwarmPart2(input)}");
        }

        private List<Particle> LoadParticles(string[] input)
        {
            List<Particle> particleList = new List<Particle>();
            int index = 0;
            foreach (string s in input)
            {
                string[] args = s.Split(new string[] {", "}, StringSplitOptions.None);

                string[] positionArgs = args[0].Substring(3, args[0].Length - 4).Split(',');
                string[] velocityArgs = args[1].Substring(3, args[1].Length - 4).Split(',');
                string[] accelerationArgs = args[2].Substring(3, args[2].Length - 4).Split(',');

                particleList.Add(new Particle(
                    index++,
                    new Vector3D(long.Parse(positionArgs[0]), long.Parse(positionArgs[1]), long.Parse(positionArgs[2])),
                    new Vector3D(long.Parse(velocityArgs[0]), long.Parse(velocityArgs[1]), long.Parse(velocityArgs[2])),
                    new Vector3D(long.Parse(accelerationArgs[0]), long.Parse(accelerationArgs[1]), long.Parse(accelerationArgs[2]))
                ));
            }
            return particleList;
        }

        private int ParticleSwarmPart1(string[] input)
        {
            List<Particle> particleList = LoadParticles(input);
            for (int i = 0; i < 2000; i++)
            {
                foreach (Particle p in particleList)
                    p.Tick();
            }

            var test = particleList.ToList().OrderBy(p => p.Position.Distance).First();
            return test.Index;
        }

        private int ParticleSwarmPart2(string[] input)
        {
            List<Particle> particleList = LoadParticles(input);
            for (int i = 0; i < 2000; i++)
            {
                foreach (Particle p in particleList)
                    p.Tick();

                // finds anything that duplicates, removes anything with that position.
                particleList.GroupBy(txt => txt.Position.ToString())
                    .Where(grouping => grouping.Count() > 1)
                    .ToList()
                    .ForEach(groupItem => particleList.RemoveAll(item => item.Position.ToString() == groupItem.Key));
            }

            return particleList.Count;
        }

        class Vector3D
        {
            public long X { get; set; }
            public long Y { get; set; }
            public long Z { get; set; }
            public long Distance => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

            public override string ToString()
            {
                return $"{X},{Y},{Z}";
            }

            public Vector3D(long x, long y, long z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        class Particle
        {
            public int Index { get; private set; }
            public Vector3D Position { get; private set; }
            public Vector3D Velocity { get; private set; }
            public Vector3D Acceleration { get; private set; }

            public Particle(int index, Vector3D position, Vector3D velocity, Vector3D acceleration)
            {
                Index = index;
                Position = position;
                Velocity = velocity;
                Acceleration = acceleration;
            }

            public void Tick()
            {
                Velocity.X += Acceleration.X;
                Velocity.Y += Acceleration.Y;
                Velocity.Z += Acceleration.Z;
                Position.X += Velocity.X;
                Position.Y += Velocity.Y;
                Position.Z += Velocity.Z;
            }
        }
    }
}
