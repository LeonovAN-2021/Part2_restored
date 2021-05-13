using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lesson_2_8
{
    class TestCase
    {
        public int Elements { get; set; }
        public int Bucket { get; set; }
        public Exception Exc { get; set; }
        public TimeSpan Expected { get; set; }

    }
    class Program
    {
        public static void TestSort(TestCase test)
        {
            try
            {
                int[] src = new int[test.Elements];
                for (int i = 0; i < src.Length; i++) { src[i] = new Random().Next(0, 1000000); }

                TimeSpan result = BucketSort(ref src, test.Bucket);
                string expect = (test.Expected > result) ? "VALID RESULT" : "INVALID RESULT";
                Console.Write("Получено: "+result + " Ожидалось: "+test.Expected +" "+ expect);
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
        public static TimeSpan BucketSort(ref int[] source, int bucketrange)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            int maxvalue = 0;
            int minvalue = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] < minvalue) { minvalue = source[i]; }
                if (source[i] > maxvalue) { maxvalue = source[i]; }
            }
            
            var buckets = new List<int>[((maxvalue - minvalue) / bucketrange) + 1];
            for (int i = 0; i < buckets.Length; i++) { buckets[i] = new List<int>(); }

            for (int i = 0; i < source.Length; i++)
            {
                buckets[source[i] / bucketrange].Add(source[i]);
            } //раскидываем по диапазонам 
            for (int i = 0; i < source.Length; i++) { buckets[source[i] / bucketrange].Sort(); }

            int k = 0;
            for (int i = 0; i < buckets.Length; i++)
            {
                int[] tmp = buckets[i].ToArray();
                for (int j = 0; j < tmp.Length; j++)
                {
                    source[k] = tmp[j];
                    k++;
                }
            }
            sw.Stop();            
            //Console.WriteLine();
            //Console.WriteLine("Отсоритровано за "+sw.Elapsed);
            return sw.Elapsed;
        }


        public static void DisplayArray(int[] array)
        {
            Console.WriteLine();
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("\t" + array[i]);
                if (i % 14 == 0) Console.WriteLine();
            }
        }

        public static TimeSpan SimpleSort(int ArraySize)
        {
            int[] src = new int[ArraySize];
            for (int i = 0; i < src.Length; i++) { src[i] = new Random().Next(0, 1000000); }
            Stopwatch s = new Stopwatch();
            s.Start();
            Array.Sort(src);
            s.Stop();
            return s.Elapsed;
        }
        static void Main(string[] args)
        {
            
            

            TestCase[] test = new TestCase[7];
            test[0] = new TestCase() { Elements = 100, Bucket = 20,Expected=SimpleSort(100) };
            test[1] = new TestCase() { Elements = 1000, Bucket = 20, Expected = SimpleSort(1000) };
            test[2] = new TestCase() { Elements = 10000, Bucket = 200, Expected = SimpleSort(10000) };
            test[3] = new TestCase() { Elements = 1000000, Bucket = 50, Expected = SimpleSort(1000000) };
            test[4] = new TestCase() { Elements = 1000000, Bucket = 100, Expected = SimpleSort(1000000) };
            test[5] = new TestCase() { Elements = 1000000, Bucket = 200, Expected = SimpleSort(1000000) };
            test[6] = new TestCase() { Elements = 1000000, Bucket = 300, Expected = SimpleSort(1000000) };

            //BucketSort(ref src, 200);
            //Console.ReadLine();
            //DisplayArray(src);
            foreach (var item in test)
            {
                Console.WriteLine();
                TestSort(item);
            }


        }
    }
}
