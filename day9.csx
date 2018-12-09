

int players = 424;
int last = 71144;

void printList(int currentMarble, List<int> marbles, int i)
{
    Console.Write("[{0}] ", i % players);
    for (int j = 0; j < marbles.Count; j++)
    {
        if (j == currentMarble)
        {
            Console.Write("({0}) ", marbles[j]);
        }
        else
        {
            Console.Write("{0} ", marbles[j]);
        }
    }
    Console.WriteLine();
}


List<int> marbles = new List<int>();
marbles.Capacity = last - last / 23 + 100;
marbles.Add(0);

int currentMarble = 1;
Dictionary<int, int> scores = new Dictionary<int, int>();
for (int i = 0; i < players; i++)
{
    scores.Add(i, 0);
}

Console.WriteLine("Starting game");
for (int i = 1; i <= last * 100; i++)
{
    if (i % 23 != 0)
    {
        currentMarble = (currentMarble + 1) % (marbles.Count) + 1;
        marbles.Insert(currentMarble, i);
    }
    else
    {
        currentMarble = ((currentMarble - 7) + marbles.Count) % (marbles.Count);
        scores[i % players] += marbles[currentMarble] + i;
        marbles.RemoveAt(currentMarble);
    }


    // printList(currentMarble, marbles, i);
    if (i % last == 0)
    {
        Console.WriteLine("i at {0} ({1}%)", i, i / last);
    }
}

// Console.WriteLine();
// Console.WriteLine(String.Join(" ", marbles));

// Console.WriteLine(String.Join(" ", scores.Values));
Console.WriteLine("Max {0}", scores.Values.Max());