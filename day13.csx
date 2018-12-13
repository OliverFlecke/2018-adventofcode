public class Cart
{
    public int x { get; set; }
    public int y { get; set; }

    public char direction { get; set; }

    public int state { get; set; }

    public Cart(int x, int y, char direction)
    {
        this.x = x;
        this.y = y;
        this.direction = direction;
    }

    public void Step(char[,] map)
    {
        switch (this.direction)
        {
            case '>':
                this.x++;
                break;
            case '<':
                this.x--;
                break;
            case '^':
                this.y--;
                break;
            case 'v':
                this.y++;
                break;
            default:
                break;
        }

        if (map[this.x, this.y] == '+')
        {
            // Console.WriteLine("Intersection");
            switch (this.state % 3)
            {
                case 0:
                    this.turnLeft();
                    break;
                case 1:
                    // Go straight
                    break;
                case 2:
                    this.turnRight();
                    break;
                default:
                    break;
            }
            this.state++;
        }
        else
        {
            this.changeDirection(map);
        }
    }

    private void turnRight()
    {
        switch (this.direction)
        {
            case '>':
                this.direction = 'v';
                break;
            case 'v':
                this.direction = '<';
                break;
            case '<':
                this.direction = '^';
                break;
            case '^':
                this.direction = '>';
                break;
            default:
                break;
        }
    }

    private void turnLeft()
    {
        switch (this.direction)
        {
            case '>':
                this.direction = '^';
                break;
            case '^':
                this.direction = '<';
                break;
            case '<':
                this.direction = 'v';
                break;
            case 'v':
                this.direction = '>';
                break;
            default:
                break;
        }
    }

    private void changeDirection(char[,] map)
    {
        if (map[this.x, this.y] == '\\')
        {
            switch (this.direction)
            {
                case '>':
                    this.direction = 'v';
                    break;
                case '<':
                    this.direction = '^';
                    break;
                case 'v':
                    this.direction = '>';
                    break;
                case '^':
                    this.direction = '<';
                    break;
                default:
                    break;
            }
        }
        else if (map[this.x, this.y] == '/')
        {
            switch (this.direction)
            {
                case '^':
                    this.direction = '>';
                    break;
                case 'v':
                    this.direction = '<';
                    break;
                case '<':
                    this.direction = 'v';
                    break;
                case '>':
                    this.direction = '^';
                    break;
                default:
                    break;
            }
        }
    }
}

string[] input = File.ReadAllLines(@"./input/day13.txt");

int width = input.ToList().Select(x => x.Length).Max();
int height = input.Length;
char[,] map = new char[width, height];

for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        map[x, y] = x < input[y].Length ? input[y][x] : ' ';
    }
}

void printState(char[,] state, List<Cart> carts)
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (carts.Any(cart => cart.x == x && cart.y == y))
            {
                Console.Write(carts.Find(cart => cart.x == x && cart.y == y).direction);
            }
            else
            {
                Console.Write(state[x, y]);
            }
        }
        Console.WriteLine();
    }
}

List<Cart> carts = new List<Cart>();

for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        if (map[x, y] == '>' || map[x, y] == '<' ||
            map[x, y] == 'v' || map[x, y] == '^')
        {
            carts.Add(new Cart(x, y, map[x, y]));

            if (map[x, y] == '>' || map[x, y] == '<') map[x, y] = '-';
            else if (map[x, y] == 'v' || map[x, y] == '^') map[x, y] = '|';
        }
    }
}

// printState(map, carts);

void simulate()
{

    for (int tick = 0; true; tick++)
    {
        carts = carts.OrderBy(cart => (cart.y, cart.x)).ToList();
        carts.ForEach(cart => cart.Step(map));

        // printState(map, carts);
        foreach (Cart cart in carts)
        {
            foreach (Cart other in carts)
            {
                if (other == cart) continue;

                if (cart.x == other.x && cart.y == other.y)
                {
                    printState(map, carts);
                    Console.WriteLine($"Crash ({cart.x},{cart.y})");
                    return;
                }
            }
        }
    }
}

simulate();
