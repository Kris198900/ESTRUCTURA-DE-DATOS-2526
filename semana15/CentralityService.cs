using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticoGrafos
{
    public class CentralityService
    {
        private readonly Graph graph;

        public CentralityService(Graph graph)
        {
            this.graph = graph;
        }

        public Dictionary<string, double> DegreeCentrality()
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            int n = graph.NodeCount();

            foreach (var node in graph.GetNodes())
            {
                int degree = graph.GetNeighbors(node).Count;
                result[node] = n > 1 ? (double)degree / (n - 1) : 0.0;
            }

            return result;
        }

        public Dictionary<string, double> ClosenessCentrality()
        {
            Dictionary<string, double> result = new Dictionary<string, double>();

            foreach (var node in graph.GetNodes())
            {
                var distances = graph.BreadthFirstSearch(node);

                int sumDistances = distances
                    .Where(x => x.Value > 0)
                    .Sum(x => x.Value);

                int reachableNodes = distances
                    .Count(x => x.Value >= 0) - 1;

                if (sumDistances > 0 && reachableNodes > 0)
                {
                    result[node] = (double)reachableNodes / sumDistances;
                }
                else
                {
                    result[node] = 0.0;
                }
            }

            return result;
        }

        public Dictionary<string, double> BetweennessCentrality()
        {
            Dictionary<string, double> centrality = new Dictionary<string, double>();
            List<string> nodes = graph.GetNodes();

            foreach (var node in nodes)
            {
                centrality[node] = 0.0;
            }

            foreach (var source in nodes)
            {
                Stack<string> stack = new Stack<string>();
                Dictionary<string, List<string>> predecessors = new Dictionary<string, List<string>>();
                Dictionary<string, int> sigma = new Dictionary<string, int>();
                Dictionary<string, int> distance = new Dictionary<string, int>();

                foreach (var v in nodes)
                {
                    predecessors[v] = new List<string>();
                    sigma[v] = 0;
                    distance[v] = -1;
                }

                sigma[source] = 1;
                distance[source] = 0;

                Queue<string> queue = new Queue<string>();
                queue.Enqueue(source);

                while (queue.Count > 0)
                {
                    string v = queue.Dequeue();
                    stack.Push(v);

                    foreach (var w in graph.GetNeighbors(v))
                    {
                        if (distance[w] < 0)
                        {
                            queue.Enqueue(w);
                            distance[w] = distance[v] + 1;
                        }

                        if (distance[w] == distance[v] + 1)
                        {
                            sigma[w] += sigma[v];
                            predecessors[w].Add(v);
                        }
                    }
                }

                Dictionary<string, double> dependency = new Dictionary<string, double>();
                foreach (var v in nodes)
                {
                    dependency[v] = 0.0;
                }

                while (stack.Count > 0)
                {
                    string w = stack.Pop();

                    foreach (var v in predecessors[w])
                    {
                        if (sigma[w] != 0)
                        {
                            dependency[v] += ((double)sigma[v] / sigma[w]) * (1.0 + dependency[w]);
                        }
                    }

                    if (w != source)
                    {
                        centrality[w] += dependency[w];
                    }
                }
            }

            foreach (var node in nodes)
            {
                centrality[node] /= 2.0;
            }

            return centrality;
        }
    }
}