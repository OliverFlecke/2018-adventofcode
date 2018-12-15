from typing import List

def printState(elves: List[int], recipes: str):
    for i, score in enumerate(recipes):
        if i == elves[0]:
            print(f'({score})', end='')
        elif i == elves[1]:
            print(f'[{score}]', end='')
        else:
            print(f' {score} ', end='')
    print()

def part1(learning: int) -> str:
    elves = [0, 1]
    recipes = [3, 7]

    while len(recipes) < learning + 10:
        recipes.extend(map(int, str(recipes[elves[0]] + recipes[elves[1]])))

        for i, elf in enumerate(elves):
            elves[i] = (elf + (recipes[elf] + 1)) % len(recipes)

    return ''.join(str(x) for x in recipes[learning:learning + 10])


def part2(learning: int) -> str:
    elves = [0, 1]
    recipes = '37'
    pattern = str(learning)

    while pattern not in recipes[-7:]:
        recipes += str(int(recipes[elves[0]]) + int(recipes[elves[1]]))

        for i, elf in enumerate(elves):
            elves[i] = (elf + (int(recipes[elf]) + 1)) % len(recipes)

    return recipes.index(pattern)

# My input: 165061
print(part1(165061))
print(part2(165061))
