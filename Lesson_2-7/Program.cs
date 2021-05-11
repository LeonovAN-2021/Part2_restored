using System;

namespace Lesson_2_7
{
    class TestCase
    {
        public int Height { get; set; }
        public int Width { get; set; }    
        public string[] Blocks { get; set; }
        public int Expected { get; set; }        
        public Exception Exc { get; set; }
    }
    class Program
    {
        public static void TestWays(TestCase test)
        {
            try
            {
                int result = SumWays(test.Height, test.Width, test.Blocks);
                string expect = (test.Expected == result) ? "VALID RESULT" : "INVALID RESULT";
                Console.Write("["+test.Height+"-"+test.Width + "]: " + result + " " + expect);
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

        public static int SumWays(int Height, int Width, string[] Blocks)
        {
            int[,] WaysMap = new int[Height, Width];
            for (int i = 0; i < Width; i++) {WaysMap[0,i] = BlockCoord(Blocks, 0,i) ? 0 : 1;}

            for (int i = 1; i < Height; i++)
            {
                WaysMap[i, 0] = BlockCoord(Blocks, i, 0) ? 0 : 1;
                for (int j = 1; j < Width; j++)
                {
                    WaysMap[i, j] = BlockCoord(Blocks, i, j) ? 0 : WaysMap[i-1, j]+ WaysMap[i, j-1];
                }
            }

            for (int i = 0; i < Height; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(WaysMap[i,j]+"\t");
                }
            }
            Console.WriteLine();
            return WaysMap[Height - 1, Width - 1];
        }
        
        public static bool BlockCoord(string[] Blocks, int Row, int Col)
        {
            try
            {
                foreach (var item in Blocks)
                {
                    string[] coord = item.Split(':');
                    if ((coord[0] == (Row+1).ToString()) && (coord[1] == (Col+1).ToString())) { return true; }
                }
            } catch (NullReferenceException) { return false; }
            
            return false;
        }




        static void Main(string[] args)
        {
            //Для прямоугольного поля размера M на N клеток, подсчитать количество путей из верхней левой клетки в правую нижнюю.
            //Известно что ходить можно только на одну клетку вправо или вниз. 
            Console.WriteLine("Hello World!");

            string[] bl = {"1:3","1:4","1:8","2:1","2:11","3:10"};
            

            TestCase[] test = new TestCase[7];
            test[0] = new TestCase() { Height = 3, Width = 3, Blocks = null, Expected = 6, Exc=new NullReferenceException() };
            test[1] = new TestCase() { Height = 13, Width = 13, Blocks = bl, Expected = 1465126 };
            test[2] = new TestCase() { Height = 3, Width = 3, Blocks = new string[1]{ "1:3"}, Expected = 5 };
            test[3] = new TestCase() { Height = -10, Width = 2, Blocks = null, Exc = new OverflowException() };
            test[4] = new TestCase() { Height = 1, Width = 1, Blocks = new string[1] { "1:1" }, Expected=0 };
            test[5] = new TestCase() { Height = 1, Width = 1, Blocks = null, Expected = 1 };
            test[6] = new TestCase() { Height = 10, Width = 0, Blocks = null, Exc=new IndexOutOfRangeException()};


            foreach (var item in test)
            {
                Console.WriteLine();
                TestWays(item);
            }

        }
    }
}
