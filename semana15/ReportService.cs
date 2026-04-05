using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PracticoGrafos
{
    public class ReportService
    {
        private readonly Graph graph;
        private readonly CentralityService centralityService;

        public ReportService(Graph graph, CentralityService centralityService)
        {
            this.graph = graph;
            this.centralityService = centralityService;
        }

        public void ShowGraph()
        {
            Console.WriteLine("GRAPH STRUCTURE");

            foreach (var node in graph.GetNodes())
            {
                Console.WriteLine(node + " -> " + string.Join(", ", graph.GetNeighbors(node)));
            }

            Console.WriteLine("Nodes: " + graph.NodeCount());
            Console.WriteLine("Edges: " + graph.EdgeCount());
            Console.WriteLine();
        }

        public void ShowCentralityReport()
        {
            var degree = centralityService.DegreeCentrality();
            var closeness = centralityService.ClosenessCentrality();
            var betweenness = centralityService.BetweennessCentrality();

            Console.WriteLine("CENTRALITY REPORT");
            Console.WriteLine("Node     Degree     Closeness     Betweenness");

            foreach (var node in graph.GetNodes())
            {
                Console.WriteLine(
                    $"{node}        {degree[node]:F3}       {closeness[node]:F3}        {betweenness[node]:F3}"
                );
            }

            Console.WriteLine();
        }

        public void ShowImportantNodes()
        {
            var degree = centralityService.DegreeCentrality();
            var closeness = centralityService.ClosenessCentrality();
            var betweenness = centralityService.BetweennessCentrality();

            string bestDegree = degree.OrderByDescending(x => x.Value).First().Key;
            string bestCloseness = closeness.OrderByDescending(x => x.Value).First().Key;
            string bestBetweenness = betweenness.OrderByDescending(x => x.Value).First().Key;

            Console.WriteLine("MOST IMPORTANT NODES");
            Console.WriteLine("Degree: " + bestDegree);
            Console.WriteLine("Closeness: " + bestCloseness);
            Console.WriteLine("Betweenness: " + bestBetweenness);
            Console.WriteLine();
        }

        public void ShowExecutionTime()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            centralityService.DegreeCentrality();
            sw.Stop();
            long t1 = sw.ElapsedTicks;

            sw.Restart();
            centralityService.ClosenessCentrality();
            sw.Stop();
            long t2 = sw.ElapsedTicks;

            sw.Restart();
            centralityService.BetweennessCentrality();
            sw.Stop();
            long t3 = sw.ElapsedTicks;

            Console.WriteLine("EXECUTION TIME");
            Console.WriteLine("Degree: " + t1);
            Console.WriteLine("Closeness: " + t2);
            Console.WriteLine("Betweenness: " + t3);
            Console.WriteLine();
        }
    }
}