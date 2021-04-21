using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace treeAndGraph
{
    public class GraphProgram
    {
        public static void Main(string[] args)
        {
            var graph = new Graph(new int[,]
            {
                {0, 0, 12, 0, 1},
                {0, 34, 0, 11, 1},
                {10, 0, 0, 0, 0},
                {21, 31, 0, 0, 0},
                {11, 35, 14, 12, 12},
            }, 5);
           // var bft = graph.breadthFirstTraverse("0");
            Console.WriteLine(graph.shortestPathDijkstra("0", "3"));
           //
        }

        class Vertex
        {
            public string value;

            public Vertex(string value)
            {
                this.value = value;
            }

            public override bool Equals(object? obj)
            {
                return value.Equals(((Vertex) obj).value);
            }

            public override int GetHashCode()
            {
                return 20 + value.GetHashCode();
            }
        }

        class Edge
        {
            public Vertex to;
            public int weight;

            public Edge(Vertex to, int weight)
            {
                this.to = to;
                this.weight = weight;
            }
        }

        class Graph
        {
            private Dictionary<Vertex, List<Edge>> edges;
            private List<VertexInfo> infos;

            public Graph()
            {
                edges = new Dictionary<Vertex, List<Edge>>();
            }

            public Graph(int[,] adjacency, int n)
            {
                edges = new Dictionary<Vertex, List<Edge>>();
                for (int i = 0; i < n; i++)
                {
                    var edgeList = new List<Edge>();
                    var vertex = new Vertex($"{i}");
                    for (int j = 0; j < n; j++)
                    {
                        var weigth = adjacency[i, j];
                        if (weigth > 0)
                        {
                            edgeList.Add(new Edge(new Vertex($"{j}"), weigth));
                        }
                    }

                    edges.Add(vertex, edgeList);
                }
            }

            public void AddVertex(string value)
            {
                var vertex = new Vertex(value);
                edges.Add(vertex, new List<Edge>());
            }

            public void AddEdge(string v1, string v2)
            {
                addEdge(v1, v2, 1);
            }

            public void addEdge(string v1, string v2, int weight)
            {
                var vertex1 = new Vertex(v1);
                var vertex2 = new Vertex(v2);
                if (edges.ContainsKey(vertex1) && edges.ContainsKey(vertex2))
                {
                    edges[vertex1].Add(new Edge(vertex2, weight));
                }
            }


            public List<String> depthFirstTraverse(string root)
            {
                if (!edges.ContainsKey(new Vertex(root)))
                {
                    throw new Exception("No such root in graph");
                }

                var visited = new List<string>();
                var stack = new Stack<string>();

                stack.Push(root);
                while (stack.Count != 0)
                {
                    var vertexName = stack.Pop();
                    if (!visited.Contains(vertexName))
                    {
                        visited.Add(vertexName);
                        foreach (var v in edges[new Vertex(vertexName)])
                        {
                            stack.Push(v.to.value);
                        }
                    }
                }

                return visited;
            }

            public List<String> breadthFirstTraverse(string root)
            {
                if (!edges.ContainsKey(new Vertex(root)))
                {
                    throw new Exception("No such root in graph");
                }

                var visited = new List<string>();
                var queue = new Queue<string>();

                queue.Enqueue(root);
                while (queue.Count != 0)
                {
                    var vertexName = queue.Dequeue();
                    if (!visited.Contains(vertexName))
                    {
                        visited.Add(vertexName);
                        foreach (var v in edges[new Vertex(vertexName)])
                        {
                            queue.Enqueue(v.to.value);
                        }
                    }
                }

                return visited;
            }

            public string breadthFirstSearch(string root, string searchValue)
            {
                if (!edges.ContainsKey(new Vertex(root)))
                {
                    throw new Exception("No such root in graph");
                }

                var visited = new List<string>();
                var queue = new Queue<string>();

                queue.Enqueue(root);
                while (queue.Count != 0)
                {
                    var vertexName = queue.Dequeue();
                    if (!visited.Contains(vertexName))
                    {
                        visited.Add(vertexName);
                        foreach (var v in edges[new Vertex(vertexName)])
                        {
                            if (v.to.value == searchValue)
                            {
                                return v.to.value;
                            }

                            queue.Enqueue(v.to.value);
                        }
                    }
                }

                return "No such value";
            }

            public string shortestPathDijkstra(string v1, string v2)
            {
                infos = new List<VertexInfo>();
                foreach (var ver in edges.Keys)
                {
                    infos.Add(new VertexInfo(ver));
                }

                var ver1 = new Vertex(v1);
                var ver2 = new Vertex(v2);
                var infoV1 = getInfoForVertex(ver1);
                infoV1.weightSum = 0;

                var current = findUnvisitedWithMinSum();

                while (current != null)
                {
                    setSumToNextElement(current);
                    current = findUnvisitedWithMinSum();
                }

                return getPath(ver1, ver2);
            }

            private string getPath(Vertex ver1, Vertex ver2)
            {
                var path = ver2.value;
                while (ver1.value != ver2.value)
                {
                    ver2 = getInfoForVertex(ver2).previous;
                    path = ver2.value + " " + path;
                }

                return path;
            }
            
            private void setSumToNextElement(VertexInfo info)
            {
                info.isUnvisited = false;
                foreach (var edge in edges[info.vertex])
                {
                    var nextInfo = getInfoForVertex(edge.to);
                    var sum = info.weightSum + edge.weight;
                    if (sum < nextInfo.weightSum)
                    {
                        nextInfo.weightSum = sum;
                        nextInfo.previous = info.vertex;
                    }
                }
            }

            private VertexInfo getInfoForVertex(Vertex ver1)
            {
                foreach (var v in infos)
                {
                    if (v.vertex.Equals(ver1))
                    {
                        return v;
                    }
                }

                return null;
            }

            private VertexInfo findUnvisitedWithMinSum()
            {
                var minValue = int.MaxValue;
                VertexInfo minVI = null;
                foreach (var vertexInfo in infos)
                {
                    if (vertexInfo.isUnvisited && vertexInfo.weightSum < minValue)
                    {
                        minVI = vertexInfo;
                        minValue = vertexInfo.weightSum;
                    }
                }

                return minVI;
            }
        }

        class VertexInfo
        {
            public Vertex vertex;
            public bool isUnvisited;
            public int weightSum;
            public Vertex previous;

            public VertexInfo(Vertex v)
            {
                vertex = v;
                isUnvisited = true;
                weightSum = int.MaxValue;
                previous = null;
            }
        }
    }
}