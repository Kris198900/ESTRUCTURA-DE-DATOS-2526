using System;

namespace PracticoGrafos
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();

            graph.AddEdge("A", "B");
            graph.AddEdge("A", "C");
            graph.AddEdge("B", "C");
            graph.AddEdge("B", "D");
            graph.AddEdge("C", "E");
            graph.AddEdge("D", "E");
            graph.AddEdge("D", "F");
            graph.AddEdge("E", "F");
            graph.AddEdge("E", "G");
            graph.AddEdge("F", "H");
            graph.AddEdge("G", "H");

            CentralityService cs = new CentralityService(graph);
            ReportService rs = new ReportService(graph, cs);

            rs.ShowGraph();
            rs.ShowCentralityReport();
            rs.ShowImportantNodes();
            rs.ShowExecutionTime();

            Console.ReadKey();
        }
    }
}