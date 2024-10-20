using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning;

public static class PathFinderTask
{
	public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
	{
		List<int> bestOrder = new List<int>(checkpoints.Length);
		List<int> points = new List<int>(checkpoints.Length);
		for(int i = 1; i < checkpoints.Length; i++)
		{
			points.Add(i);
		}
        double minDistance=MakeMinPath(points, 0, checkpoints, bestOrder);
		bestOrder.Insert(0, 0);
		Console.WriteLine(minDistance);
		return bestOrder.ToArray();
	}
	public static double MakeMinPath(List<int> points,int currentPoint, Point[] checkpoints, List<int> bestOrder)
	{
		if(points.Count == 0)
		{
			return 0;
		}
		var min = double.MaxValue;
		List<int> minPath = new List<int>(); 
		int index = 0;
		for(int j=0;j<points.Count;j++)
		{
			var i = points[0];
			var distance = checkpoints[currentPoint].DistanceTo(checkpoints[i]);
            points.RemoveAt(0);
			var temp = new List<int>();
            distance += MakeMinPath(points, i, checkpoints, temp);
			if(distance<min)
			{
                min = distance;
				index=i;
				minPath = temp;
            }
            points.Add(i);
        }
		minPath.Insert(0,index);
		foreach(int i in minPath) bestOrder.Add(i);
		return min;
	}
	private static int[] MakeTrivialPermutation(int size)
	{
		var bestOrder = new int[size];
		for (var i = 0; i < bestOrder.Length; i++)
			bestOrder[i] = i;
		return bestOrder;
	}
}