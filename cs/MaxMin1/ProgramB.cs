
//using Newtonsoft.Json;
//using System.Diagnostics;
//using System.Numerics;
//using System.Runtime.CompilerServices;
//using System.Runtime.Intrinsics;
//public readonly struct Point
//{
//    public readonly double X;
//    public readonly double Y;
//    public readonly double Z;

//    public Point(double x, double y, double z)
//    {
//        X = x;
//        Y = y;
//        Z = z;
//    }

//    public static double Distance(Point value1, Point value2)
//    {
//        Vector128<double> vector128A = Vector128.Create(value1.X, value1.Y);
//        Vector64<double> vector64A = Vector64.Create(value1.Z);
//        Vector128<double> vector128B = Vector128.Create(value2.X, value2.Y);
//        Vector64<double> vector64B = Vector64.Create(value2.Z);
//        var v1 = vector128A - vector128B;
//        var v2 = vector64A - vector64B;
//        return Math.Sqrt(Vector128.Dot(v1, v1) + Vector64.Dot(v2, v2));
//    }
//}
//public unsafe static class MaxMin
//{


//    public static double CalMaxMin(Point[] path1, Point[] path2)
//    {
//        var max = 0.0;
//        fixed (Point* pt1 = path1)
//        {
//            fixed (Point* pt2 = path2)
//            {
//                for (int i = 0; i < path1.Length; i++)
//                {
//                    var min = double.MaxValue;
//                    for (int j = 0; j < path2.Length; j++)
//                    {
//                        min = Math.Min(min, Point.Distance(*(pt1 + i), *(pt2 + j)));
//                    }
//                    max = Math.Max(max, min);
//                }
//            }
//        }
//        return max;
//    }

//    public static double HausdorffDistance(Point[] path1, Point[] path2)
//    {
//        return Math.Max(CalMaxMin(path1, path2), CalMaxMin(path2, path1));
//    }

//    public static void Main(string[] args)
//    {

//        var p1 = File.ReadAllText("../../../../../path1.json");
//        var p2 = File.ReadAllText("../../../../../path2.json");

//        var path1 = JsonConvert.DeserializeObject<Point[]>(p1);
//        var path2 = JsonConvert.DeserializeObject<Point[]>(p2);

//        var _a = HausdorffDistance(path1, path2);
//        var _b = HausdorffDistance(path1, path2);
//        var _c = HausdorffDistance(path1, path2);

//        var stopWatch = Stopwatch.StartNew();
//        var res = HausdorffDistance(path1, path2);
//        stopWatch.Stop();
//        Console.WriteLine($"res is {res}");
//        Console.WriteLine($"spend time is {stopWatch.ElapsedMilliseconds} ms");
//    }
//}



