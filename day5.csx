using System;
using System.IO;
using System.Collections.Generic;

List<char> text = File.ReadAllText(@"./input/day5.txt").ToList();

// Console.WriteLine(String.Concat(Enumerable.Range(0, text.Count).ToList()));
// Console.WriteLine(String.Concat(text));

bool doesReact(char a, char b)
{
    if (char.ToUpper(a) != char.ToUpper(b))
    {
        return false;
    }

    if (char.IsUpper(a) && char.IsLower(b)
        || char.IsLower(a) && char.IsUpper(b))
    {
        return true;
    }

    return false;
}

string reactPolymer(List<char> text)
{
    for (int i = 0; i < text.Count - 1;)
    {
        if (doesReact(text[i], text[i + 1]))
        {
            // Console.WriteLine($"Removing {text[i]} {text[i + 1]} at {i},{i+1}");
            text.RemoveAt(i);
            text.RemoveAt(i);
            i = Math.Max(i - 1, 0);
            // Console.WriteLine(String.Concat(text));
        }
        else
        {
            i++;
        }
    }

    return String.Concat(text);
}

// Console.WriteLine(String.Concat(text));
Console.WriteLine($"Part 1 answer: {reactPolymer(text).Length}");

// Part 2
string baseText = File.ReadAllText(@"./input/day5.txt");

IEnumerable<char> distinct = baseText.ToUpper().Distinct();
Dictionary<char, int> map = new Dictionary<char, int>();
Console.WriteLine($"Distinct chars {String.Concat(distinct)}");

foreach (char c in distinct)
{
    List<Char> polymer = baseText.ToList();
    polymer.RemoveAll(x => Char.ToUpper(x) == c);
    string reduced = reactPolymer(polymer);

    map.Add(c, reduced.Count());
}

KeyValuePair<char, int> min = map.Aggregate((min, cur) => min.Value < cur.Value ? min : cur);

Console.WriteLine($"Part 2 answer: {min.Value}");
