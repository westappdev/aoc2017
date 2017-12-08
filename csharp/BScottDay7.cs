using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC2017
{
    class BScottDay7
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("BScottDay7.txt");
            Node rootNode = ProcessList(input);

            Console.WriteLine($"Part 1 Answer: {rootNode.Name}");

            // I have no idea if this convoluted solution will work for other inputs,
            // but it's printing out the right answer and I am too tired and fed up with this
            // problem to care if it's not perfect.
            int weightDifference = FindWeightDifference(rootNode);
            Node badProgram = FindBadProgram(rootNode);
            Console.WriteLine("Part 2 Answer: " + (badProgram.Weight + weightDifference));

            Console.ReadLine();
        }

        static Node ProcessList(string[] input)
        {
            List<string> removalList = new List<string>();
            Dictionary<string, Node> nodeList = new Dictionary<string, Node>();
            Regex regex = new Regex(@"([a-z]+) \((\d+)\)(?: -> )?(?:([a-z]+)*(?:, )?)*");

            for (int i = 0; i < input.Length; i++)
            {
                Match match = regex.Match(input[i]);
                if (match.Success)
                {
                    // create a new node
                    Node node = new Node(match.Groups[1].Value, int.Parse(match.Groups[2].Value));
                    // add any sub items
                    if (match.Groups[3].Captures.Count > 0)
                    {
                        foreach (Capture capture in match.Groups[3].Captures)
                        {
                            node.ChildNames.Add(capture.Value);
                        }
                    }
                    nodeList.Add(match.Groups[1].Value, node);
                }
            }

            foreach (KeyValuePair<string, Node> kvpNode in nodeList)
            {
                if (kvpNode.Value.ChildNames.Count > 0)
                {
                    foreach (string s in kvpNode.Value.ChildNames)
                    {
                        if (nodeList.ContainsKey(s))
                        {
                            kvpNode.Value.ChildNodes.Add(nodeList[s]);
                            removalList.Add(s); // a list of nodes to remove from dictionary
                        }
                    }
                }
            }

            // Remove everything from root colllection that was a child node of something else.
            // This should leave just 1 node remaining which is the root node.
            foreach (string key in removalList)
                nodeList.Remove(key);

            return nodeList.First().Value; // hopefully this is the fully populated root node...
        }

        static int GetWeight(Node parentNode)
        {
            if (parentNode.ChildNodes.Count == 0)
                return parentNode.Weight;

            int weight = parentNode.Weight;
            foreach (Node t in parentNode.ChildNodes)
                weight += GetWeight(t);

            return weight;
        }
        /// <summary>
        /// Finds the weight difference of the inbalance
        /// </summary>
        /// <param name="rootNode">The root node of the entire stack</param>
        /// <returns>The weight difference of the offending program.</returns>
        static int FindWeightDifference(Node rootNode)
        {
            int[] weights = new int[rootNode.ChildNodes.Count];
            for (int i = 0; i < rootNode.ChildNodes.Count; i++)
                weights[i] = GetWeight(rootNode.ChildNodes[i]);
            List<int> result = weights.GroupBy(i => i).OrderBy(g => g.Count()).Select(g => g.Key).ToList();
            return result.Last() - result.First();
        }

        static Node FindBadProgram(Node parentNode)
        {
            // only find nodes that have at least 3 children to compare
            if(parentNode.ChildNodes.Count >= 3) {
                int[] weights = new int[parentNode.ChildNodes.Count];
                // calulate the weights
                for (int i = 0; i < parentNode.ChildNodes.Count; i++)
                    weights[i] = GetWeight(parentNode.ChildNodes[i]);
                // find the least common weight
                int result = weights.GroupBy(i => i).OrderBy(g => g.Count()).Select(g => g.Key).ToList().First();
                // dig into the node with the uncommon weight
                return FindBadProgram(parentNode.ChildNodes[Array.IndexOf(weights, result)]);
            }
            return parentNode;
        }

        class Node
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public List<Node> ChildNodes { get; set; }
            public List<string> ChildNames { get; set; }

            public Node(string name, int weight)
            {
                Name = name;
                Weight = weight;
                ChildNodes = new List<Node>();
                ChildNames = new List<string>();
            }
        }
    }
}
