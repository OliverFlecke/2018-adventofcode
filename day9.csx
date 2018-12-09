

int players = 424;
int last = 71144;

void printList(LinkedListNode<int> currentMarble, LinkedList<int> marbles, int i)
{
    Console.Write("[{0}] ", i % players);
    for (int j = 0; j < marbles.Count; j++)
    {
        var marble = marbles.ElementAt(j);
        if (marble == currentMarble.Value)
        {
            Console.Write("({0}) ", marble);
        }
        else
        {
            Console.Write("{0} ", marble);
        }
    }
    Console.WriteLine();
}


LinkedList<int> marbles = new LinkedList<int>();
LinkedListNode<int> node = marbles.AddFirst(0);

Dictionary<int, long> scores = new Dictionary<int, long>();
for (int i = 0; i < players; i++)
{
    scores.Add(i, 0);
}

for (int i = 1; i <= last * 100; i++)
{
    if (i % 23 != 0)
    {
        node = marbles.AddAfter(node.Next == null ? marbles.First : node.Next, i);
    }
    else
    {
        for (int r = 0; r < 7; r++)
        {
            if (node.Previous != null)
            {

                node = node.Previous;
            }
            else
            {
                node = marbles.Last;
            }
        }
        scores[i % players] += node.Value + i;
        var temp = node;
        node = temp.Next;
        marbles.Remove(temp);
    }


    // printList(node, marbles, i);
}

Console.WriteLine("Max {0}", scores.Values.Max());