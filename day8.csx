using System;
using System.IO;

string[] input = File.ReadAllText(@"./input/day8.txt").Split(" ");
// foreach (string item in input)
// {
//     Console.Write(item + " ");
// }
// Console.WriteLine();

class Node
{
    public int childrenCount;
    public int metadataCount;

    public List<Node> children = new List<Node>();
    public List<int> metadata = new List<int>();

    public Node(int childrenCount, int metadataCount)
    {
        this.childrenCount = childrenCount;
        this.metadataCount = metadataCount;
    }

    public void Parse(ref int index, string[] input)
    {
        for (int i = 0; i < this.childrenCount; i++)
        {
            Node node = new Node(Int16.Parse(input[index++]), Int16.Parse(input[index++]));
            node.Parse(ref index, input);
            this.children.Add(node);
        }
        for (int i = 0; i < this.metadataCount; i++)
        {
            this.metadata.Add(Int16.Parse(input[index++]));
        }
    }

    public int Sum()
    {
        return this.metadata.Sum() + this.children.Select(node => node.Sum()).Sum();
    }

    public int AdvanceSum()
    {
        if (this.childrenCount == 0)
        {
            return this.metadata.Sum();
        }

        int sum = 0;
        foreach (int id in this.metadata)
        {
            if (0 < id && id <= this.children.Count)
            {
                sum += this.children[id - 1].AdvanceSum();
            }
        }

        return sum;
    }
}

int index = 0;
Node root = new Node(Int32.Parse(input[index++]), Int32.Parse(input[index++]));

root.Parse(ref index, input);

// Part 1
Console.WriteLine(root.Sum());

// Part 2
Console.WriteLine(root.AdvanceSum());