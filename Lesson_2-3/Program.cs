using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Lesson_2_3
{
    public class PointClassF
    {
        public float X { get;}
        public float Y { get;}

        public PointClassF(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public struct PointStructF
    {
        public float X { get; set; }
        public float Y { get; set; }
        public PointStructF(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public struct PointStructD
    {
        public double X { get; set; }
        public double Y { get; set; }
        public PointStructD(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class BenchmarkClass
    {        
        public PointClassF[] PClassF = new PointClassF[100];
        public PointStructF[] PStructF = new PointStructF[100];
        public PointStructD[] PStructD = new PointStructD[100];
        
        public BenchmarkClass()
        {
            for (int i = 0; i < PClassF.Length; i++)
            {
                PClassF[i] = new PointClassF((float)(new Random().Next(10000) / 100), (float)(new Random().Next(10000) / 100));
                PStructF[i] = new PointStructF((float)(new Random().Next(10000) / 100), (float)(new Random().Next(10000) / 100));
                PStructD[i] = new PointStructD((double)(new Random().Next(10000) / 100), (double)(new Random().Next(10000) / 100));
            }
        }

        public static float GetDistance(PointClassF Point1, PointClassF Point2)
        {
            float X = Point1.X - Point2.X;
            float Y = Point1.Y - Point2.Y;
            return MathF.Sqrt((X * X) + (Y * Y));
        }
        public static float GetDistance(PointStructF Point1, PointStructF Point2)
        {
            float X = Point1.X - Point2.X;
            float Y = Point1.Y - Point2.Y;
            return MathF.Sqrt((X * X) + (Y * Y));
        }
        public static double GetDistance(PointStructD Point1, PointStructD Point2)
        {
            double X = Point1.X - Point2.X;
            double Y = Point1.Y - Point2.Y;
            return Math.Sqrt((X * X) + (Y * Y));
        }
        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct FloatIntUnion
        {
            [FieldOffset(0)]
            public int i;
            [FieldOffset(0)]
            public float f;
        }

        public static float fsrt(float z)
        {
            if (z == 0) return 0;
            FloatIntUnion u;
            u.i = 0;
            u.f = z;
            u.i -= 1 << 23;
            u.i >>= 1;
            u.i += 1 << 29;
            return u.f;
        }
        public static float GetSimpleDistance(PointStructF Point1, PointStructF Point2)
        {
            float X = Point1.X - Point2.X;
            float Y = Point1.Y - Point2.Y;
            return fsrt((X * X) + (Y * Y));
        }

        [Benchmark]
        public void TestDistanceFloatClass()
        {
            for (int i = 1; i < PClassF.Length; i++)
            {
                GetDistance(PClassF[i-1], PClassF[i]);
            } 
        }
        
        [Benchmark]
        public void TestDistanceFloatStruct()
        {
            for (int i = 1; i < PStructF.Length; i++)
            {
                GetDistance(PStructF[i - 1], PStructF[i]);
            }
        }
        [Benchmark]
        public void TestDistanceDoubleStruct()
        {
            for (int i = 1; i < PStructD.Length; i++)
            {
                GetDistance(PStructD[i - 1], PStructD[i]);
            }
        }
        [Benchmark]
        public void TestSimpleDistanceFloatStruct()
        {
            for (int i = 1; i < PStructF.Length; i++)
            {
                GetSimpleDistance(PStructF[i - 1], PStructF[i]);
            }
            
        }
        [Benchmark]
        public void TestSimpleDistanceFloatStruct2()
        {
            PointStructF P1 = new PointStructF(14.67F, 65.26F);
            PointStructF P2 = new PointStructF(13.60F, 95.12F);
            for (int i = 0; i < 100; i++)
            {
                GetSimpleDistance(P1, P2);
            }
            

        }
    }


    class Program
    {


        static void Main(string[] args)
        {
            //PointClassF P1 = new PointClassF(14.67F, 65.26F);
            //PointClassF P2 = new PointClassF(13.60F, 95.12F);


            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
