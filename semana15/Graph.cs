using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticoGrafos
{
    public class Graph
    {
        private readonly Dictionary<string, List<string>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<string, List<string>>();
        }

        public void AddNode(string node)
        {
            if (!adjacencyList.ContainsKey(node))
            {
                adjacencyList[node] = new List<string>();
            }
        }

        public void AddEdge(string node1, string node2)
        {
            AddNode(node1);
            AddNode(node2);

            if (!adjacencyList[node1].Contains(node2))
            {
                adjacencyList[node1].Add(node2);
            }

            if (!adjacencyList[node2].Contains(node1))
            {
                adjacencyList[node2].Add(node1);
            }
        }

        public List<string> GetNodes()
        {
            return adjacencyList.Keys.OrderBy(x => x).ToList();
        }

        public List<string> GetNeighbors(string node)
        {
            if (!adjacencyList.ContainsKey(node))
            {
                return new List<string>();
            }

            return adjacencyList[node].OrderBy(x => x).ToList();
        }

        public int NodeCount()
        {
            return adjacencyList.Count;
        }

        public int EdgeCount()
        {
            int total = adjacencyList.Values.Sum(list => list.Count);
            return total / 2;
        }

        public Dictionary<string, int> BreadthFirstSearch(string startNode)
        {
            Dictionary<string, int> distances = new Dictionary<string, int>();
            Queue<string> queue = new Queue<string>();

            foreach (var node in adjacencyList.Keys)
            {
                distances[node] = -1;
            }

            if (!adjacencyList.ContainsKey(startNode))
            {
                return distances;
            }

            distances[startNode] = 0;
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                foreach (var neighbor in adjacencyList[current])
                {
                    if (distances[neighbor] == -1)
                    {
                        distances[neighbor] = distances[current] + 1;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return distances;
        }
    }
}