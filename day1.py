
with open('input/day1.txt') as f:
    data = [int(x) for x in f.read().split()]

i = 0
seen = set()
freq = 0
while freq not in seen:
    seen.add(freq)
    freq += data[i]

    i += 1
    if i >= len(data):
        i = 0

print(freq)


i = 0
s = 0
while i < len(data):
    s += data[i]
    i += 1

print(int(s))