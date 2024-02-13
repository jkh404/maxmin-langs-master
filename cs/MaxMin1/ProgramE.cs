//using BenchmarkDotNet.Attributes;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Diagnostics;
//using System.Net;
//using System.Numerics;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;
//using System.Runtime.Intrinsics;
//using System.Runtime.Intrinsics.X86;


//public readonly struct Point
//{
//    public readonly double X;
//    public readonly double Y;
//    public readonly double Z;
//    [JsonIgnore]
//    public readonly Vector256<double> Vector;

//    public Point(double x, double y, double z)
//    {
//        X = x;
//        Y = y;
//        Z = z;
//    }

//    private Point(Vector256<double> vector)
//    {
//        this.Vector = vector;
//    }

//    public static Point NewPoint(Vector256<double> vector)
//    {
//        return new Point(vector);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double Distance(Point value1, Point value2)
//    {
//        var vA = Avx2.Subtract(value1.Vector, value2.Vector);
//        return Math.Sqrt(Vector256.Dot<double>(vA, vA));
//    }
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static (double A, double B, double C, double D) DistanceX4(
//        Point pointA1, Point pointA2,
//        Point pointB1, Point pointB2,
//        Point pointC1, Point pointC2,
//        Point pointD1, Point pointD2
//        )
//    {
//        var vA = Avx2.Subtract(pointA1.Vector, pointA2.Vector);
//        var vB = Avx2.Subtract(pointB1.Vector, pointB2.Vector);
//        var vC = Avx2.Subtract(pointC1.Vector, pointC2.Vector);
//        var vD = Avx2.Subtract(pointD1.Vector, pointD2.Vector);
//        var valueA = Vector256.Dot<double>(vA, vA);
//        var valueB = Vector256.Dot<double>(vB, vB);
//        var valueC = Vector256.Dot<double>(vC, vC);
//        var valueD = Vector256.Dot<double>(vD, vD);
//        //return (Math.Sqrt(valueA), Math.Sqrt(valueB), Math.Sqrt(valueC), Math.Sqrt(valueD));
//        return (valueA, valueB, valueC, valueD);
//    }
//}
//public unsafe static class MaxMin
//{


//    public static double CalMaxMin(Point[] path1, Point[] path2)
//    {
//        var max = 0.0D;
//        var p1Len = path1.Length;
//        var p2Len = path2.Length;
//        fixed (Point* pt1 = path1)
//        {
//            fixed (Point* pt2 = path2)
//            {
//                for (int i = 0; i < p1Len; i++)
//                {
//                    var min = double.MaxValue;
//                    for (int j = 0; j < p2Len;)
//                    {
//                        if (p2Len - j < 4)
//                        {
//                            var dis = Point.Distance(*(pt1 + i), *(pt2 + j));
//                            if (dis < min)
//                            {
//                                min = dis;
//                            }
//                            j++;
//                        }
//                        else
//                        {

//                            var pointA1 = *(pt1 + i);
//                            var pointA2 = *(pt2 + j);
//                            var pointB1 = *(pt1 + i);
//                            var pointB2 = *(pt2 + j + 1);
//                            var pointC1 = *(pt1 + i);
//                            var pointC2 = *(pt2 + j + 2);
//                            var pointD1 = *(pt1 + i);
//                            var pointD2 = *(pt2 + j + 3);
//                            var result = Point.DistanceX4(pointA1, pointA2, pointB1, pointB2, pointC1, pointC2, pointD1, pointD2);
//                            if (result.A < min)
//                            {
//                                min = result.A;
//                            }
//                            if (result.B < min)
//                            {
//                                min = result.B;
//                            }
//                            if (result.C < min)
//                            {
//                                min = result.C;
//                            }
//                            if (result.D < min)
//                            {
//                                min = result.D;
//                            }
//                            j += 4;
//                        }

//                    }
//                    if (min > max)
//                    {
//                        max = min;
//                    }
//                }
//            }
//        }
//        return Math.Sqrt(max);
//    }

//    public static double HausdorffDistance(Point[] path1, Point[] path2)
//    {
//        var r1 = CalMaxMin(path1, path2);
//        var r2 = CalMaxMin(path2, path1);
//        if (r1 < r2)
//        {
//            return r2;
//        }
//        else
//        {
//            return r1;
//        }
//    }


//}

//static class Program
//{
//    public static void Main(string[] args)
//    {

//        if (!Vector256.IsHardwareAccelerated) Console.WriteLine("不支持Vector256");
//        if (!Vector128.IsHardwareAccelerated) Console.WriteLine("不支持Vector128");
//        if (!Vector64.IsHardwareAccelerated) Console.WriteLine("不支持Vector64");
//        if (!Avx2.IsSupported) Console.WriteLine("不支持Avx2");
//        var p1 = File.ReadAllText("../../../../../path1.json");
//        var p2 = File.ReadAllText("../../../../../path2.json");

//        var path1 = JsonConvert.DeserializeObject<Point[]>(p1)!;
//        var path2 = JsonConvert.DeserializeObject<Point[]>(p2)!;
//        for (int i = 0; i < path1.Length; i++)
//        {
//            path1[i] = Point.NewPoint(Vector256.Create(path1[i].X, path1[i].Y, path1[i].Z, 1));
//        }
//        for (int i = 0; i < path2.Length; i++)
//        {
//            path2[i] = Point.NewPoint(Vector256.Create(path2[i].X, path2[i].Y, path2[i].Z, 1));
//        }
//        for (int i = 0; i < 3; i++)
//        {
//            _ = MaxMin.HausdorffDistance(path1, path2);
//        }

//        var stopWatch = Stopwatch.StartNew();
//        var res = MaxMin.HausdorffDistance(path1, path2);
//        stopWatch.Stop();
//        Console.WriteLine($"res is {res}");
//        Console.WriteLine($"spend time is {stopWatch.ElapsedMilliseconds} ms");
//    }
//}