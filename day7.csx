using System;
using System.IO;

List<string> input = File.ReadAllLines(@"./input/day7.txt").ToList();

class Node
{
    public char Value { get; set; }

    public List<char> requirements { get; set; }

    private int started = -1;

    public bool Started {
        get { return started != -1; }
    }

    public int Count
    {
        get
        {
            return this.requirements.Count;
        }
    }

    public Node(char value)
    {
        this.Value = value;
        this.requirements = new List<char>();
    }

    public void Add(char pre)
    {
        this.requirements.Add(pre);
    }

    public void Remove(char pre)
    {
        this.requirements.Remove(pre);
    }

    public bool Done(int step)
    {
        if (!this.Started)
        {
            return false;
        }

        return this.started + 60 + (this.Value - 'A') <= step;
    }

    public void Start(int step)
    {
        if (!this.Started)
        {
            this.started = step;
            Console.WriteLine("Starting {0} at {1}", this.Value, step);
        }
    }


    public override string ToString()
    {
        return String.Format("{0}\t{1}", this.Value,
            String.Join("  ", this.requirements));
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Node))
        {
            return false;
        }

        Node node = (Node)obj;

        return node.Value == this.Value;
    }
}

List<Node> CreateTree()
{
    List<Node> steps = new List<Node>();

    foreach (string line in input)
    {
        char pre = line[5];
        char con = line[36];

        if (!steps.Contains(new Node(con)))
        {
            steps.Add(new Node(con));
        }
        if (!steps.Contains(new Node(pre)))
        {
            steps.Add(new Node(pre));
        }

        steps.Find(node => node.Value == con).Add(pre);
    }

    return steps;
}

List<Node> steps = CreateTree();

Console.WriteLine(String.Join("\n", steps.Select(x => x.ToString())));

List<char> output = new List<char>();

while (steps.Count > 0)
{
    Node toRemove = null;

    for (int i = 0; i < steps.Count; i++)
    {
        if (steps[i].Count == 0)
        {
            if (toRemove == null || toRemove.Value > steps[i].Value)
            {
                toRemove = steps[i];
            }
        }
    }

    output.Add(toRemove.Value);
    steps.Remove(toRemove);

    foreach (Node node in steps)
    {
        node.Remove(toRemove.Value);
    }
}

Console.WriteLine(String.Join("", output));

// Part 2

steps = CreateTree();

int workers = 5;

int step = 0;
List<Node> canBeWorked = new List<Node>();

while (steps.Count > 0)
{

    for (int i = 0; i < steps.Count; i++)
    {
        if (steps[i].Count == 0)
        {
            if (canBeWorked.Contains(steps[i]))
            {
                continue;
            }

            if (canBeWorked.Count < workers)
            {
                canBeWorked.Add(steps[i]);
            }
            else
            {
                if (canBeWorked.Exists(node => node.Value > steps[i].Value
                    && !node.Started))
                {
                    canBeWorked.Remove(canBeWorked
                        .Find(node => node.Value > steps[i].Value
                            && !node.Started
                            )
                        );
                    canBeWorked.Add(steps[i]);
                }
            }
        }
    }

    foreach (Node node in canBeWorked)
    {
        node.Start(step);
    }

    foreach (Node finishedNode in canBeWorked.Where(node => node.Done(step)))
    {
        foreach (Node node in steps)
        {
            node.Remove(finishedNode.Value);
        }
        steps.Remove(finishedNode);
        Console.WriteLine("At {2}: Finished {0} Worked {1}",
            finishedNode.Value,
            String.Join(" ", canBeWorked.Select(x => x.Value)),
            step
        );
    }
    canBeWorked.RemoveAll(node => node.Done(step));
    step++;
}

Console.WriteLine("Done {0}", step);
