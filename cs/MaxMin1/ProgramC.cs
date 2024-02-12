//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using Newtonsoft.Json;

//public readonly record struct Point(double X, double Y, double Z)
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double Dis(Point other)
//    {
//        var (x1, y1, z1) = this;
//        var (x2, y2, z2) = other;
//        var (xd, yd, zd) = (x1 - x2, y1 - y2, z1 - z2);
//        return Math.Sqrt(xd * xd + yd * yd + zd * zd);
//    }

//}

//public static class MaxMin
//{


//    public static double CalMaxMin(List<Point> path1, List<Point> path2)
//    {
//        var max = 0.0;
//        foreach (var p1 in path1)
//        {
//            var min = double.MaxValue;
//            foreach (var p2 in path2)
//            {
//                var dis = p1.Dis(p2);
//                min = Math.Min(min,dis);
//            }
//            max = Math.Max(max, min);
//        }
//        return max;
//    }

//    public static double HausdorffDistance(List<Point> path1, List<Point> path2)
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

//    public static void Main(string[] args)
//    {
//        var p1 = File.ReadAllText("../../../../../path1.json");
//        var p2 = File.ReadAllText("../../../../../path2.json");

//        var path1 = JsonConvert.DeserializeObject<List<Point>>(p1);
//        var path2 = JsonConvert.DeserializeObject<List<Point>>(p2);

//        var _a = HausdorffDistance(path1, path2);
//        var _b = HausdorffDistance(path1, path2);
//        var _c = HausdorffDistance(path1, path2);

//        var stopWatch = Stopwatch.StartNew();
//        var res = HausdorffDistance(path1, path2);
//        Console.WriteLine($"res is {res}");
//        Console.WriteLine($"spend time is {stopWatch.ElapsedMilliseconds} ms");
//    }
//}