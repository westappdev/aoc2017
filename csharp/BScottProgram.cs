using System;
using System.Reflection;

namespace AOC2017
{
    class BScottProgram
    {
        static void Main(string[] args)
        {
            // Find all classes inheriting from BScottSolution, create an instance, and execute Run method.
            foreach (Type t in Assembly.GetAssembly(typeof(BScottSolution)).GetTypes())
            {
                if (t.IsSubclassOf(typeof(BScottSolution)))
                {
                    BScottSolution solution = Activator.CreateInstance(t) as BScottSolution;

                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine(solution.Name);
                    Console.WriteLine("-----------------------------------------------------------");

                    solution.Run();

                    Console.WriteLine();
                }
            }
            Console.ReadLine();
        }
    }
}
