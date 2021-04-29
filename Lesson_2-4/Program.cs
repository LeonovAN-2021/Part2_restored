using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Lesson_2_4
{
    public class BenchmarkClass
    {
        HashSet<string> HSet;
        string[] Ar;

        public BenchmarkClass()
        {
            Ar = GenerateArray(10000);
            HSet = GenerateHashSet(10000);
        }
        [Benchmark]
        public void TestArraySearch()
        {

            InArray(Ar, "462"); //возможно да
            InArray(Ar, "4632"); // точно нет

        }

        [Benchmark]
        public void TestHashSetSearch()
        {

            InHashSet(HSet, "462"); // возможно да
            InHashSet(HSet, "4632"); // точно нет

        }

        public string[] GenerateArray(int arraysize)
        {
            string[] ret = new string[arraysize];
            for (int i = 0; i < arraysize; i++)
            {
                ret[i] = Convert.ToString(new Random().Next(1000));
            }
            return ret;
        }
        public HashSet<string> GenerateHashSet(int size)
        {
            var hset = new HashSet<string>();
            for (int i = 0; i < size; i++)
            {
                hset.Add(Convert.ToString(new Random().Next(1000)));
            }
            return hset;
        }

        public bool InArray(string[] array, string somestr)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == somestr) return true;
            }
            return false;
        }

        public bool InHashSet(HashSet<string> hset, string somestr)
        {
            return hset.Contains(somestr);
        }
    }

    class Program
    {


        static void Main(string[] args)
        {
            BenchmarkClass bc = new BenchmarkClass();
            var summary = BenchmarkRunner.Run<BenchmarkClass>();
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            Console.ReadLine();
            Tree tre = new Tree();
            
            tre.AddItem(8);
            tre.AddItem(6);
            tre.AddItem(10);
            tre.AddItem(25);
            TreeNode par = new TreeNode();
            TreeNode cur = new TreeNode();
            //cur=tre.GetParent(10,out par);
            Console.WriteLine(tre.GetNodeByValue(10).RightChild.Value);
            //Console.ReadLine();
        }
    }


    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode LeftChild { get; set; }
        public TreeNode RightChild { get; set; }
        public override bool Equals(object obj)
        {
            var node = obj as TreeNode; 
            if (node == null) return false; 
            return node.Value == Value;
        }       
    }

    public class Tree:ITree
    {
        TreeNode First;

        public Tree()
        {
            First = null;
        }

        public TreeNode GetRoot()
        {
            return First;
        }



        public void AddItem(int value)
        {
            if (First == null) { First = new TreeNode() { Value = value, LeftChild = null, RightChild = null }; }
            else { PlaceItem(First, value); }
        }
        public void PlaceItem(TreeNode parent, int value)
        {
            if (value.CompareTo(parent.Value)<0)
            {
                if (parent.LeftChild==null)
                {
                    parent.LeftChild = new TreeNode() { Value = value, RightChild = null, LeftChild = null };
                } else
                {
                    PlaceItem(parent.LeftChild, value);
                }
            } else
            {
                if (parent.RightChild==null)
                {
                    parent.RightChild= new TreeNode() { Value = value, RightChild = null, LeftChild = null };
                }else
                {
                    PlaceItem(parent.RightChild, value);
                }
            }
            
        }
        

        public TreeNode GetParent(int value, out TreeNode parent)
        {
            TreeNode current = First;
            parent = null;
            while (current!=null)
            {
                
                if (current.Value<value) 
                {
                    
                    parent = current;
                    current = current.RightChild;
                } else if (current.Value>value) 
                { 
                   
                    parent = current;
                    current = current.LeftChild;
                } else 
                { 
                    
                    break; 
                }
            }
            return current;
        }
        public bool Exists(int value)
        {
            TreeNode parent;
            return GetParent(value,out parent) != null;
        }
        public TreeNode GetNodeByValue(int value)
        {
            if (!Exists(value)) return null;
            TreeNode parent;
            return GetParent(value, out parent);
        }
        public void RemoveItem(int value)
        {

        }

    }

    public interface ITree 
    {
        TreeNode GetRoot(); 
        void AddItem(int value); // добавить узел
        void RemoveItem(int value);// удалить узел по значению
        TreeNode GetNodeByValue(int value); //получить узел дерева по значению
        //void PrintTree(); //вывести дерево в консоль
    }
    public class NodeInfo
    {
        public int Depth { get; set; }
        public TreeNode Node { get; set; }
    }

    public static class TreeHelper
    {
        public static NodeInfo[] GetTreeInLine(ITree tree)
        {
            var bufer = new Queue<NodeInfo>();
            var returnArray = new List<NodeInfo>();
            var root = new NodeInfo() { Node = tree.GetRoot() };
            bufer.Enqueue(root);

            while (bufer.Count!=0)
            {
                var element = bufer.Dequeue();
                returnArray.Add(element);
                var depth = element.Depth + 1;
                if (element.Node.LeftChild!=null)
                {
                    var left = new NodeInfo() { Node = element.Node.LeftChild, Depth = depth };
                    bufer.Enqueue(left);
                }
                if (element.Node.RightChild != null)
                {
                    var right = new NodeInfo() { Node = element.Node.RightChild, Depth = depth };
                    bufer.Enqueue(right);
                }
            }
            return returnArray.ToArray();
        }
    }


}
