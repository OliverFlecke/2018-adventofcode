using System;
using System.IO;
using System.Text.RegularExpressions;

class Point
{
    public int x;
    public int y;

    private int dx;
    private int dy;

    public Point(string line)
    {
        MatchCollection mc = Regex.Matches(line, @"(-?\d+)");

        this.x = Int32.Parse(mc[0].Value);
        this.y = Int32.Parse(mc[1].Value);
        this.dx = Int32.Parse(mc[2].Value);
        this.dy = Int32.Parse(mc[3].Value);
    }

    public (int, int) GetLocation(int step)
    {
        return (this.x + step * this.dx, this.y + step * this.dy);
    }

    public override string ToString()
    {
        return String.Format("({0}, {1}) - ({2}, {3})",
            this.x, this.y, this.dx, this.dy);
    }
}

(int, int, int) calculateSize(List<Point> points)
{
    int width = 0;
    int height = 0;
    int step = 0;
    int lastScore = Int32.MaxValue;
    int score = Int32.MaxValue - 1;

    while (lastScore > score)
    {
        step++;
        IEnumerable<(int, int)> locations = points.Select(point => point.GetLocation(step));
        width = locations.Select(point => point.Item1).Max() -
            locations.Select(point => point.Item2).Min();
        height = locations.Select(point => point.Item2).Max() -
            locations.Select(point => point.Item2).Min();
        // Console.WriteLine("{0}, {1}", width, height);
        lastScore = score;
        score = width + height;

        if (step % 1000 == 0)
        {
            Console.WriteLine("Step {0} {1}", step, score);
        }
    }

    return (width, height, step);
}

void printMap(List<Point> points, int step, int width, int height)
{
    width *= 4;
    height *= 4;
    Console.WriteLine("Step {0} ({1}, {2})", step, width, height);

    IEnumerable<(int, int)> locs = points.Select(point => point.GetLocation(step));
    int xMax = locs.Select(point => point.Item1).Max();
    int xMin = locs.Select(point => point.Item2).Min();
    int yMax = locs.Select(point => point.Item2).Max();
    int yMin = locs.Select(point => point.Item2).Min();
    char[,] map = new char[width, height];

    foreach (Point point in points)
    {
        var location = point.GetLocation(step);

        map[location.Item1 + width / 2 - xMin, location.Item2 + height / 2 - yMin] = '#';
    }

    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (map[x, y] == '#')
            {
                Console.Write(map[x, y]);
            }
            else
            {
                Console.Write(".");
            }
        }
        Console.WriteLine();
    }
}

string[] input = File.ReadAllLines(@"./input/day10.txt");

List<Point> points = input.Select(line => new Point(line)).ToList();


var size = calculateSize(points);

for (int step = size.Item3 - 1; step < size.Item3 + 1; step++)
{
    printMap(points, step, size.Item1, size.Item2);
}

