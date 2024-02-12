
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text.Json;

var v=Vector256.Create(1,1,1,1);
var result= Vector256.Dot(v,v);
Console.WriteLine(result);
//int MaxCount = 10000;
//var option = new JsonSerializerOptions();
//option.IncludeFields = true;
//Random random = new Random();   
//List<Point> PointList = new List<Point>();
//for (int i = 0; i < MaxCount; i++)
//{

//    var x = random.NextDouble() * 1000 * (random.Next(-1,1)<0?-1:1);
//    var y = random.NextDouble() * 1000 * (random.Next(-1, 1) < 0 ? -1 : 1);
//    var z = random.NextDouble() * 1000 * (random.Next(-1, 1) < 0 ? -1 : 1);
//    PointList.Add(new Point(x,y,z));
//}
//using (var file = File.Open("../../../../../path1.json", FileMode.OpenOrCreate, FileAccess.Write))
//{

//    JsonSerializer.Serialize(file,PointList, option);
//}
//PointList.Clear();
//for (int i = 0; i < MaxCount; i++)
//{
//    var x = random.NextDouble() * 1000 * (random.Next(-1, 1) < 0 ? -1 : 1);
//    var y = random.NextDouble() * 1000 * (random.Next(-1, 1) < 0 ? -1 : 1);
//    var z = random.NextDouble() * 1000 * (random.Next(-1, 1) < 0 ? -1 : 1);
//    PointList.Add(new Point(x, y, z));
//}
//using (var file = File.Open("../../../../../path2.json", FileMode.OpenOrCreate, FileAccess.Write))
//{

//    JsonSerializer.Serialize(file, PointList, option);
//}

//public record class Point(double X,double Y,double Z);