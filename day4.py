import re
from datetime import datetime

AWAKE = 1
SLEEP = 2

regex = r'\[(?P<year>\d*)-(?P<month>\d*)-(?P<day>\d*) (?P<hour>\d*):(?P<minute>\d*)\]'
pattern = re.compile(regex)

def mapData(line: str):
    match = pattern.match(line)
    timestamp = datetime(
        year   = int(match.group('year')),
        month  = int(match.group('month')),
        day    = int(match.group('day')),
        hour   = int(match.group('hour')),
        minute = int(match.group('minute'))
    )

    m = re.search(r'Guard #(?P<id>\d*) begins shift', line)
    if m:
        return {
            'id': int(m.group('id')),
            'timestamp': timestamp
        }

    m = re.search(r'wakes up', line)
    if m:
        type = AWAKE
    m = re.search('falls asleep', line)
    if m:
        type = SLEEP

    return {
        'type': type,
        'timestamp': timestamp
    }

with open('./input/day4.txt', 'r') as f:
    data = f.read().split('\n')
    data = list(map(mapData, sorted(data)))

hours = {}
timesheet = {}
current_id = None
for entry in data:
    if 'id' in entry:
        current_id = entry['id']
        if current_id not in timesheet:
            timesheet[current_id] = [{
                'type': AWAKE,
                'timestamp': entry['timestamp']
            }]
    else:
        timesheet[current_id].append({
            'type': entry['type'],
            'timestamp': entry['timestamp']
        })

sleep = {}
for key, values in timesheet.items():
    isSleeping = False
    sleeping = []
    lastTimestamp = None

    for value in values:
        if value['type'] == SLEEP:
            lastTimestamp = value['timestamp'].minute
        elif lastTimestamp:
            sleeping.append((lastTimestamp, value['timestamp'].minute))
            lastTimestamp = None

    sleep[key] = sleeping

sleepCount = {}
for key, values in sleep.items():
    sleepCount[key] = sum(map(lambda x: x[1] - x[0], values))

most_sleeping_guard = max(sleepCount, key=sleepCount.get)

minutes = {}
for minute in range(0, 60):
    minutes[minute] = sum([1 for value in sleep[most_sleeping_guard]
        if minute in range(value[0], value[1])])

most_sleeping_minute = max(minutes, key=minutes.get)

print('Guard {0}, minute {1}, answer {2}'.format(most_sleeping_guard, most_sleeping_minute,
    most_sleeping_guard * most_sleeping_minute))

# PART 2

sleeping_minute = {}
for guard in timesheet.keys():
    minutes = {}
    for minute in range(0, 60):
        minutes[minute] = sum([1 for value in sleep[guard]
            if minute in range(value[0], value[1])])

    minute = max(minutes, key=minutes.get)
    sleeping_minute[guard] = (minute, minutes[minute])

guard = max(sleeping_minute, key=lambda x: sleeping_minute[x][1])
minute = sleeping_minute[guard][0]
print('Guard {0}, minute {1}, answer {2}'.format(guard, minute, guard * minute))
