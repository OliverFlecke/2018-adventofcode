
with open('input/day2.txt') as f:
    data = f.read().split()

twos = 0
threes = 0

for id in data:
    chars = set(id)
    for c in chars:
        if id.count(c) == 2:
            twos += 1
            break
    for c in chars:
        if id.count(c) == 3:
            threes += 1
            break

print(str(twos * threes))

def string_diff(a: str, b: str) -> str:
    errors = 0
    for i in range(len(a)):
        if a[i] != b[i]:
            errors += 1
            index = i
        if errors > 1:
            return None
    return a[:index] + a[index + 1:]

for i in range(len(data)):
    for j in range(i + 1, len(data)):
        diff = string_diff(data[i], data[j])
        if diff:
            print('{0} {1}'.format(i, j))
            print(diff)
            break

