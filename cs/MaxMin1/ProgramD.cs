
//using Newtonsoft.Json;
//using System.Diagnostics;
//using System.Numerics;
//using System.Runtime.CompilerServices;
//using System.Runtime.Intrinsics;

//public unsafe static class MaxMin
//{

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static float CalMaxMin(Vector3[] path1, Vector3[] path2)
//    {
//        var max = 0.0F;

//        fixed (Vector3* pt1 = path1)
//        {
//            fixed (Vector3* pt2 = path2)
//            {
//                for (int i = 0; i < path1.Length; i++)
//                {
//                    var min = float.MaxValue;
//                    for (int j = 0; j < path2.Length; j++)
//                    {
//                        min = MathF.Min(min, Vector3.Distance(*(pt1 + i), *(pt2 + j)));
//                    }
//                    max = MathF.Max(max, min);
//                }
//            }
//        }
//        return max;
//    }
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static float HausdorffDistance(Vector3[] path1, Vector3[] path2)
//    {
//        return MathF.Max(CalMaxMin(path1, path2), CalMaxMin(path2, path1));
//    }

//    public static void Main(string[] args)
//    {

//        var p1 = File.ReadAllText("../../../../../path1.json");
//        var p2 = File.ReadAllText("../../../../../path2.json");

//        var path1 = JsonConvert.DeserializeObject<Vector3[]>(p1);
//        var path2 = JsonConvert.DeserializeObject<Vector3[]>(p2);

//        _ = HausdorffDistance(path1, path2);
//        _  = HausdorffDistance(path1, path2);
//        _  = HausdorffDistance(path1, path2);

//        var stopWatch = Stopwatch.StartNew();
//        var res = HausdorffDistance(path1, path2);
//        stopWatch.Stop();
//        Console.WriteLine($"res is {res}");
//        Console.WriteLine($"spend time is {stopWatch.ElapsedMilliseconds} ms");
//    }
//}


