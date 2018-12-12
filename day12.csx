
void printState(char[] state, int generation)
{
    Console.WriteLine($"{generation}: " + String.Join(" ", state));
}


string[] input = File.ReadAllLines(@"./input/day12.txt");

string initialState = input[0].Substring(15);

Dictionary<string, char> rules = new Dictionary<string, char>();

for (int i = 2; i < input.Length; i++)
{
    string[] rule = input[i].Split("=>");
    rules.Add(rule[0].Trim(), Char.Parse(rule[1].Trim()));
}

int padding = 1000;

char[] state = new char[2 * padding + initialState.Length];
for (int i = 0; i < padding; i++)
{
    state[i] = '.';
    state[padding + initialState.Length + i] = '.';
}
for (int i = 0; i < initialState.Length; i++)
{
    state[i + padding] = initialState[i];
}

// printState(state, 0);

int computeScore(char[] state)
{
    return state.Select((character, index) => character == '#' ? index - padding : 0).Sum();
}

List<int> scores = new List<int>();
scores.Add(computeScore(state));
List<int> diffs = new List<int>();

for (long g = 1; g <= 50000000000; g++)
{
    char[] nextState = new char[2 * padding + initialState.Length];
    nextState[0] = '.';
    nextState[1] = '.';
    nextState[nextState.Length - 1] = '.';
    nextState[nextState.Length - 2] = '.';

    for (int x = 2; x < nextState.Length - 2; x++)
    {
        string prev = String.Join("", state[x - 2], state[x - 1], state[x], state[x + 1], state[x + 2]);
        nextState[x] = (rules.GetValueOrDefault(prev) == '#') ? '#' : '.';
    }

    state = nextState;
    int score = computeScore(state);
    // Console.WriteLine($"{g:####}: Score {score} last: {scores.Last()} diff: {score - scores.Last()}");
    diffs.Add(score - scores.Last());
    scores.Add(score);

    if (diffs.Count > 100 && ((int) diffs.GetRange(diffs.Count - 100, 100).Average()) == diffs.Last())
    {
        break;
    }
}

long generation = 50000000000;
int lastDiff = (int) diffs.GetRange(diffs.Count - 100, 100).Average();
Console.WriteLine(lastDiff);
long total = (generation - (scores.Count - 1)) * lastDiff + scores.Last();
Console.WriteLine($"Score at generation {generation}: {total}");

