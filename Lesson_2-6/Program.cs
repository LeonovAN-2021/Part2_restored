using System;
using System.Collections.Generic;

namespace Lesson_2_6
{
    public class Node
    {
        public int Value { get; set; }
        public List<Edge> Edges { get; set; }
        public bool Visited { get; set; }
        public Node() { Edges = new List<Edge>(); }
    }
    public class Edge
    {
        public int Weight { get; set; }
        public Node TargetNode { get; set; }        
    }
    public class Graph
    {
        public List<Node> Nodes { get; set; }
        public Graph() { Nodes = new List<Node>(); }
        
        public Node BFS(int needvalue, Node root)
        {
            var queue = new Queue<Node>();
            Node tmp = root;

            Console.WriteLine("BFS обход графа");
            int stepnum = 1;
            int valsum = 0;
            int weightsum = 0;
            Console.Write("Шаг " + stepnum + ". Добавление первой вершины в очередь. ");
            queue.Enqueue(tmp);
            Console.WriteLine("Значение: " + tmp.Value);  stepnum++;
            while (true)
            {
                Console.WriteLine("Шаг " + stepnum + ". В очереди (" + queue.Count + ") элементов. Сумма значений: ("+valsum+")."); stepnum++;
                if (queue.Count == 0) {
                    Console.WriteLine();
                    Console.WriteLine("Обход закончен. Элемент НЕ найден."); return null; }
                tmp = queue.Dequeue();
                if (tmp.Visited) { continue; } else { tmp.Visited = true; } //проверка посещенности вершин
                valsum += tmp.Value;
                
                Console.WriteLine("Шаг " + stepnum + ". Проверка элемента очереди. Значение " + tmp.Value + "."); stepnum++;
                if (tmp.Value == needvalue) { Console.WriteLine("Обход закончен. Элемент найден."); return tmp; }
                tmp.Visited = true;
                Console.WriteLine("Шаг " + stepnum + ". Обновление признака посещения."); stepnum++;
                foreach (var item in tmp.Edges)
                {                    
                    queue.Enqueue(item.TargetNode); Console.WriteLine("Шаг " + stepnum + ". Добавлена вершина."); stepnum++;
                    weightsum += item.Weight;
                    Console.WriteLine("Шаг " + stepnum + ". Пройденный путь увеличен на ("+item.Weight+"). Общая длина пути: ("+weightsum+")."); stepnum++;
                }
                
            }           
            
        }
        public Node DFS(int needvalue, Node root)
        {
            var stack = new Stack<Node>();
            Node tmp = root;
            Console.WriteLine();
            Console.WriteLine("DFS обход графа");
            int stepnum = 1;
            int valsum = 0;
            int weightsum = 0;
            Console.Write("Шаг " + stepnum + ". Добавление первой вершины в стек. ");
            stack.Push(tmp);
            Console.WriteLine("Значение: " + tmp.Value); stepnum++;
            while (true)
            {
                Console.WriteLine("Шаг " + stepnum + ". В стеке (" + stack.Count + ") элементов. Сумма значений: (" + valsum + ")."); stepnum++;
                if (stack.Count == 0) {
                    Console.WriteLine();
                    Console.WriteLine("Обход закончен. Элемент НЕ найден."); return null; }
                tmp = stack.Pop();
                if (tmp.Visited) { continue; } else { tmp.Visited = true; } //проверка посещенности вершин

                valsum += tmp.Value;
                Console.WriteLine("Шаг " + stepnum + ". Проверка элемента стека. Значение " + tmp.Value + "."); stepnum++;
                if (tmp.Value == needvalue) { Console.WriteLine("Обход закончен. Элемент найден."); return tmp; }
                tmp.Visited = true;
                Console.WriteLine("Шаг " + stepnum + ". Обновление признака посещения."); stepnum++;
                foreach (var item in tmp.Edges)
                {
                    stack.Push(item.TargetNode); Console.WriteLine("Шаг " + stepnum + ". Добавлена вершина."); stepnum++;
                    weightsum += item.Weight;
                    Console.WriteLine("Шаг " + stepnum + ". Пройденный путь увеличен на (" + item.Weight + "). Общая длина пути: (" + weightsum + ")."); stepnum++;
                }
            }
        }

        

    }
    class TestCase
    {
        public int Argument { get; set; }
        public Graph TestGraph { get; set; }
        public bool Expected { get; set; }
        public Node Root { get; set; }
        public Exception Exc { get; set; }
    }
    class Program
    {
        public static void TestBFS(TestCase test)
        {

            try
            {
                bool result = BFSearch(test.Argument, test.TestGraph,test.Root);
                string expect = (test.Expected == result) ? "VALID RESULT" : "INVALID RESULT";
                Console.Write(test.Argument + ": " + result + " " + expect);
            }
            catch (Exception ex)
            {
                if ((test.Exc != null) && (test.Exc.GetType() == ex.GetType()))
                {
                    Console.Write("VALID EXCEPTION");
                }
                else { Console.Write("INVALID EXCEPTION"); }
            }
            Console.WriteLine();
        }
        public static void TestDFS(TestCase test)
        {

            try
            {
                bool result = DFSearch(test.Argument, test.TestGraph, test.Root);
                string expect = (test.Expected == result) ? "VALID RESULT" : "INVALID RESULT";
                Console.Write(test.Argument + ": " + result + " " + expect);
            }
            catch (Exception ex)
            {
                if ((test.Exc != null) && (test.Exc.GetType() == ex.GetType()))
                {
                    Console.Write("VALID EXCEPTION");
                }
                else { Console.Write("INVALID EXCEPTION"); }
            }
            Console.WriteLine();
        }
        public static bool BFSearch(int what, Graph where, Node root)
        {
            foreach (var item in where.Nodes) { item.Visited = false; }
            return (where.BFS(what,root) != null);
        }
        public static bool DFSearch(int what, Graph where, Node root)
        {
            foreach (var item in where.Nodes) { item.Visited = false; }
            return (where.DFS(what,root) != null);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Graph gr = new Graph();
            
            Node[] Nodelist = new Node[8];

            Nodelist[0] = new Node() { Value = 5,  Visited = false };
            Nodelist[1] = new Node() { Value = 8,  Visited = false };
            Nodelist[2] = new Node() { Value = 12, Visited = false };
            Nodelist[3] = new Node() { Value = 3,  Visited = false };
            Nodelist[4] = new Node() { Value = 15, Visited = false };
            Nodelist[5] = new Node() { Value = 31, Visited = false };
            Nodelist[6] = new Node() { Value = 42, Visited = false };
            Nodelist[7] = new Node() { Value = 26, Visited = false };
                        
            Nodelist[0].Edges.Add(new Edge() { TargetNode = Nodelist[1], Weight = 1 });
            Nodelist[0].Edges.Add(new Edge() { TargetNode = Nodelist[4], Weight = 1 });
            Nodelist[0].Edges.Add(new Edge() { TargetNode = Nodelist[5], Weight = 1 });

            Nodelist[1].Edges.Add(new Edge() { TargetNode = Nodelist[2], Weight = 1 });

            Nodelist[2].Edges.Add(new Edge() { TargetNode = Nodelist[1], Weight = 1 });
            Nodelist[2].Edges.Add(new Edge() { TargetNode = Nodelist[3], Weight = 1 });

            Nodelist[3].Edges.Add(new Edge() { TargetNode = Nodelist[3], Weight = 1 });
            Nodelist[3].Edges.Add(new Edge() { TargetNode = Nodelist[6], Weight = 1 });
            Nodelist[3].Edges.Add(new Edge() { TargetNode = Nodelist[7], Weight = 1 });

            Nodelist[4].Edges.Add(new Edge() { TargetNode = Nodelist[0], Weight = 1 });
            
            Nodelist[5].Edges.Add(new Edge() { TargetNode = Nodelist[0], Weight = 1 });
            Nodelist[5].Edges.Add(new Edge() { TargetNode = Nodelist[4], Weight = 1 });
            Nodelist[5].Edges.Add(new Edge() { TargetNode = Nodelist[7], Weight = 1 });

            Nodelist[6].Edges.Add(new Edge() { TargetNode = Nodelist[3], Weight = 1 });
            
            foreach (var item in Nodelist) { gr.Nodes.Add(item); }

            TestCase[] test = new TestCase[10];
            test[0] = new TestCase() { Argument = 15, Expected = true, TestGraph = gr,Root=Nodelist[0] };
            test[1] = new TestCase() { Argument = 12, Expected = false, TestGraph = gr, Root = Nodelist[1] };
            test[2] = new TestCase() { Argument = 42, Expected = true, TestGraph = gr, Root = Nodelist[0] };
            test[3] = new TestCase() { Argument = 0, Expected = false, TestGraph = gr, Root = Nodelist[3] };
            test[4] = new TestCase() { Argument = -11, Expected = false, TestGraph = gr, Root = Nodelist[0] };
            test[5] = new TestCase() { Argument = 5, Expected = true, TestGraph = gr, Root = Nodelist[0] };
            test[6] = new TestCase() { Argument = 15, Expected = false, TestGraph = gr, Root = Nodelist[7] };
            test[7] = new TestCase() { Argument = 21, Expected = false, TestGraph = gr, Root = Nodelist[0] };
            test[8] = new TestCase() { Argument = 26, Expected = true, TestGraph = gr, Root = Nodelist[0] };
            test[9] = new TestCase() { Argument = 500000, Expected = false, TestGraph = gr, Root = Nodelist[0] };

            foreach (var item in test)
            {
                TestBFS(item);
            }

            foreach (var item in test)
            {
                TestDFS(item);
            }
        }
    }
}
