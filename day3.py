import re

with open('input/day3.txt', 'r') as f:
    input = f.read().split('\n')

def parse(line: str, data: dict):
    m = re.search(r'\#(?P<id>\d*) @ (?P<x>\d*),(?P<y>\d*): (?P<width>\d*)x(?P<height>\d*)', line)

    data[int(m.group('id'))] = {
        'x': int(m.group('x')),
        'y': int(m.group('y')),
        'width': int(m.group('width')),
        'height': int(m.group('height'))
    }

data = {}
for line in input:
    parse(line, data)

width = max(v['x'] + v['width'] for v in data.values()) + 1
height = max(v['y'] + v['height'] for v in data.values()) + 1

grid = [[0 for _ in range(width)] for _ in range(height)]
counter = {}
overlaps = {}
for id, value in data.items():
    overlaps[id] = set()
    for i in range(value['x'], value['x'] + value['width']):
        for j in range(value['y'], value['y'] + value['height']):
            grid[j][i] += 1
            if (i,j) in counter:
                for number in counter[(i, j)]:
                    overlaps[number].add(id)
                    overlaps[id].add(number)
            else:
                counter[(i,j)] = []
            counter[(i,j)].append(id)


count = sum([1 for i in range(width) for j in range(height) if grid[j][i] > 1])
no_overlap_id = [id for id in overlaps if len(overlaps[id]) == 0][0]

def printGrid():
    for row in grid:
        for cell in row:
            if cell == 0:
                output = '.'
            else:
                output = cell
            print(output, end=' ')
        print()

print(count)
