
int powerLevel(int x, int y, int serial)
{
    int rackId = x + 10;

    return (((((rackId * y) + serial) * rackId) % 1000) / 100) - 5;
}

// Tests
// Console.WriteLine(powerLevel(3, 5, 8));
// Console.WriteLine(powerLevel(122, 79, 57));

// int gridSerialNumber = 4455;
int gridSerialNumber = 18; // Test
int size = 300;
int[,] grid = new int[size, size];

for (int y = 0; y < size; y++)
{
    for (int x = 0; x < size; x++)
    {
        grid[x, y] = powerLevel(x, y, gridSerialNumber);
    }
}

int max = Int16.MinValue;
(int, int) cell = (0, 0);
int bestMask = -1;


for (int maskSize = 0; maskSize < 150; maskSize++)
{
    for (int y = maskSize; y < size - maskSize; y++)
    {
        for (int x = maskSize; x < size - maskSize; x++)
        {
            int sum = 0;
            for (int i = -maskSize; i <= maskSize; i++)
            {
                for (int j = -maskSize; j <= maskSize; j++)
                {
                    sum += grid[x + i, y + j];
                }
            }

            if (sum > max)
            {
                max = sum;
                cell = (x - maskSize, y - maskSize);
                bestMask = maskSize;
            }
        }
    }
    Console.WriteLine("{0} mask done", maskSize);
}


Console.WriteLine("{0},{1},{2}  - {3}",
    cell.Item1, cell.Item2, 2 * bestMask + 1, max);