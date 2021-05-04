using System;
using System.Collections.Generic;

namespace Lesson_2_5
{
    public class Tree
    {
        public Node Root;


        public Node BFS(int needvalue)
        {
            var queue = new Queue<Node>();
            Node tmp = Root;
            Console.WriteLine("BFS обход");
            int stepnum = 1;
            Console.Write("Шаг " + stepnum + ". Добавление коренвого элемента в очередь. ");
            queue.Enqueue(tmp);
            Console.WriteLine("Значение: " + tmp.Value); stepnum++;
            while (true)
            {
                Console.WriteLine("Шаг " + stepnum + ". Проверка размера очереди. В очереди " + queue.Count + " элементов."); stepnum++;
                if (queue.Count == 0) { return null; }
                tmp = queue.Dequeue();
                Console.WriteLine("Шаг " + stepnum + ". Проверка элемента очереди. Значение " + tmp.Value + "."); stepnum++;
                if (tmp.Value == needvalue) { Console.WriteLine("Шаг " + stepnum + ". Искомый элемент найден."); return tmp; }
                if (tmp.Left != null) { queue.Enqueue(tmp.Left); Console.WriteLine("Шаг " + stepnum + ". Добавлен левый потомок."); stepnum++; }
                if (tmp.Right != null) { queue.Enqueue(tmp.Right); Console.WriteLine("Шаг " + stepnum + ". Добавлен правый потомок."); stepnum++; }
            }
        }

        public Node DFS(int needvalue)
        {
            var stack = new Stack<Node>();
            Node tmp = Root;
            Console.WriteLine("DFS обход");
            int stepnum = 1;
            Console.Write("Шаг " + stepnum + ". Добавление коренвого элемента в очередь. ");
            stack.Push(tmp);
            Console.WriteLine("Значение: " + tmp.Value); stepnum++;
            while (true)
            {
                Console.WriteLine("Шаг " + stepnum + ". Проверка размера очереди. В очереди " + stack.Count + " элементов."); stepnum++;
                if (stack.Count == 0) { return null; }
                tmp = stack.Pop();
                Console.WriteLine("Шаг " + stepnum + ". Проверка элемента очереди. Значение " + tmp.Value + "."); stepnum++;
                if (tmp.Value == needvalue) { Console.WriteLine("Шаг " + stepnum + ". Искомый элемент найден."); return tmp; }
                if (tmp.Right != null) { stack.Push(tmp.Right); Console.WriteLine("Шаг " + stepnum + ". Добавлен правый потомок."); stepnum++; }
                if (tmp.Left != null) { stack.Push(tmp.Left); Console.WriteLine("Шаг " + stepnum + ". Добавлен левый потомок."); stepnum++; }

            }
        }


    }
    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    class TestCase
    {
        public int Argument { get; set; }
        public Tree Array { get; set; }
        public bool Expected { get; set; }
        public Exception Exc { get; set; }
    }
    class Program
    {
        public static void TestBFS(TestCase test)
        {       

            try
            {
                bool result = BFSearch(test.Argument, test.Array);
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
                bool result = DFSearch(test.Argument, test.Array);
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

        public static bool BFSearch(int what, Tree where)
        {
            return (where.BFS(what) != null);
        }
        public static bool DFSearch(int what, Tree where)
        {
            return (where.DFS(what) != null);
        }

        static void Main(string[] args)
        {
            Tree tre = new Tree() { Root = new Node() { Value = 20 } };
            tre.Root.Left = new Node() { Value = 10 };
            tre.Root.Right = new Node() { Value = 30 };
            tre.Root.Left.Left = new Node() { Value = 5 };
            tre.Root.Left.Right = new Node() { Value = 15 };
            tre.Root.Left.Left.Right = new Node() { Value = 9 };
            tre.Root.Left.Right.Left = new Node() { Value = 11 };
            tre.Root.Left.Right.Left.Right = new Node() { Value = 12 };
            tre.Root.Right.Left = new Node() { Value = 25 };
            tre.Root.Right.Right = new Node() { Value = 35 };

            TestCase[] test = new TestCase[10];
            test[0] = new TestCase() { Argument = 15, Array=tre,Expected=true};
            test[1] = new TestCase() { Argument = 30, Array = tre, Expected = true };
            test[2] = new TestCase() { Argument = 20, Array = tre, Expected = true };
            test[3] = new TestCase() { Argument = 11, Array = tre, Expected = true };
            test[4] = new TestCase() { Argument = 12, Array = tre, Expected = true };
            test[5] = new TestCase() { Argument = 135, Array = tre, Expected = false };
            test[6] = new TestCase() { Argument = 5, Array = tre, Expected = true };
            test[7] = new TestCase() { Argument = 0, Array = tre, Expected = false };
            test[8] = new TestCase() { Argument = -15, Array = tre, Expected = false };
            test[9] = new TestCase() { Argument = 51, Array = tre, Expected = false };

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
