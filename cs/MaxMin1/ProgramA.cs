using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


public readonly struct Point
{
    public readonly double X;
    public readonly double Y;
    public readonly double Z;

    public Point(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point operator -(Point left, Point right)
    {
        return new Point(
            left.X - right.X,
            left.Y - right.Y,
            left.Z - right.Z
        );
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Distance(Point value1, Point value2)
    {
        var v = value1 - value2;
        var value = (v.X * v.X)
                 + (v.Y * v.Y)
                 + (v.Z * v.Z);
        return Math.Sqrt(value);
    }
}
public unsafe static class MaxMin
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CalMaxMin(Point[] path1, Point[] path2)
    {
        var max = 0.0D;
        foreach (var p1 in path1)
        {
            var min = double.MaxValue;
            foreach (var p2 in path2)
            {
                var dis = Point.Distance(p1, p2);
                if (dis < min)
                {
                    min = dis;
                }

            }
            if (min > max)
            {
                max = min;
            }
        }
        //fixed (Point* pt1 = path1)
        //{
        //    fixed (Point* pt2 = path2)
        //    {
        //        for (int i = 0; i < path1.Length; i++)
        //        {
        //            var min = double.MaxValue;
        //            for (int j = 0; j < path2.Length; j++)
        //            {
        //                var dis = Point.Distance(*(pt1 + i), *(pt2 + j));
        //                if (dis < min)
        //                {
        //                    min = dis;
        //                }
        //            }
        //            if (min > max)
        //            {
        //                max = min;
        //            }
        //        }
        //    }
        //}
        return max;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double HausdorffDistance(Point[] path1, Point[] path2)
    {
        var r1 = CalMaxMin(path1, path2);
        var r2 = CalMaxMin(path2, path1);
        if (r1 < r2)
        {
            return r2;
        }
        else
        {
            return r1;
        }
    }

    public static void Main(string[] args)
    {

        var p1 = File.ReadAllText("../../../../../path1.json");
        var p2 = File.ReadAllText("../../../../../path2.json");

        var path1 = JsonConvert.DeserializeObject<Point[]>(p1);
        var path2 = JsonConvert.DeserializeObject<Point[]>(p2);

        _ = HausdorffDistance(path1, path2);
        _ = HausdorffDistance(path1, path2);
        _ = HausdorffDistance(path1, path2);

        var stopWatch = Stopwatch.StartNew();
        var res = HausdorffDistance(path1, path2);
        stopWatch.Stop();
        Console.WriteLine($"res is {res}");
        Console.WriteLine($"spend time is {stopWatch.ElapsedMilliseconds} ms");
    }
}
