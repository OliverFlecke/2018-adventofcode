using System;
using System.IO;
using System.Collections.Generic;

List<string> lines = File.ReadAllLines(@"./input/day6.txt").ToList();

List<(int, int)> points = lines
    .Select(line => (Int32.Parse(line.Split(',')[0]), (Int32.Parse(line.Split(',')[1])))).ToList();

foreach ((int, int) point in points)
{
    Console.WriteLine(point);
}

int Xmax = points.Select(point => point.Item1).Max();
int Xmin = points.Select(point => point.Item1).Min();
int Ymax = points.Select(point => point.Item2).Max();
int Ymin = points.Select(point => point.Item2).Min();

int height = 1 + Ymax - Ymin;
int width = 1 + Xmax - Xmin;

int[,] grid = new int[width, height];

void printGrid(int[,] grid)
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            Console.Write(String.Format("{0}", (int)(grid[x, y])));
        }
        Console.WriteLine();
    }
}

int distance((int, int) point, int x, int y)
{
    return Math.Abs(point.Item1 - x) + Math.Abs(point.Item2 - y);
}

for (int x = 0; x < width; x++)
{
    for (int y = 0; y < height; y++)
    {
        int minDist = Int32.MaxValue;
        for (int i = 0; i < points.Count; i++)
        {
            (int, int) point = points[i];
            int dist = distance(point, x + Xmin, y + Ymin);
            if (dist < minDist)
            {
                minDist = dist;
                grid[x, y] = (int)('a' + i);
                // if (dist == 0)
                // {
                //     grid[x, y] = (int)('A' + i);
                // }
            }
            else if (dist == minDist)
            {
                grid[x, y] = '.';
            }
        }
    }
}

HashSet<int> infinites = new HashSet<int>();

for (int x = 0; x < width; x++)
{
    infinites.Add((grid[x, 0]));
    infinites.Add((grid[x, height - 1]));
}

for (int y = 0; y < height; y++)
{
    infinites.Add((grid[0, y]));
    infinites.Add((grid[width - 1, y]));
}

// printGrid(grid);

// Console.WriteLine("Infinitives");
// Console.WriteLine(String.Join(" ", infinites));

Dictionary<int, int> area = new Dictionary<int, int>();

for (int x = 0; x < width; x++)
{
    for (int y = 0; y < height; y++)
    {
        int key = (grid[x, y]);
        if (!area.ContainsKey(key))
        {
            area.Add(key, 0);
        }
        area[key] += 1;
    }
}

infinites.ToList().ForEach(x => area.Remove(x));

var largestArea = area.Aggregate((max, current) => max.Value > current.Value ? max : current);
Console.WriteLine(largestArea);
// Console.WriteLine(String.Join(" ", area));

// Part 2

int cells = 0;
int threshold = 10000;
for (int x = 0; x < width; x++)
{
    for (int y = 0; y < height; y++)
    {
        int totalDistance = points.Select((point) => distance(point, x, y)).Sum();
        if (threshold > totalDistance)
        {
            cells++;
        }
    }
}

Console.WriteLine("Region size: {0}", cells);
