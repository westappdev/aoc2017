import math


class Coprocessor:
    def __init__(self, instructions):
        self.instructions = instructions
        self.pc = 0
        self.finished = False
        self.registers = dict.fromkeys(['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'], 0)

    def value(self, input_reg):
        if input_reg.isalpha():
            return self.registers[input_reg]
        else:
            return int(input_reg)

    def is_prime(self, x):
        prime = False
        if x > 1:
            prime = True
            k = 2
            n = math.sqrt(x)
            while k <= n and prime == True:
                if x % k == 0:
                    prime = False
                k += 1
        return prime

    def part1(self):
        mul_count = 0
        while not self.finished:
            args = self.instructions[self.pc].split(' ')
            if args[0] == 'set':
                self.registers[args[1]] = self.value(args[2])
            elif args[0] == 'sub':
                self.registers[args[1]] -= self.value(args[2])
            elif args[0] == 'mul':
                self.registers[args[1]] *= self.value(args[2])
                mul_count += 1
            elif args[0] == 'jnz':
                if self.value(args[1]) != 0:
                    self.pc += self.value(args[2]) - 1

            self.pc += 1
            if self.pc >= len(self.instructions) or self.pc < 0:
                self.finished = True

        return mul_count

    def part2(self):
        primecount = 0
        start = int(self.instructions[0].split(' ')[2]) * 100 + 100000
        for i in range(start, start + 17000 + 1, 17):
            if not self.is_prime(i):
                primecount += 1

        return primecount


with open("BScottDay23.txt") as f:
    instructions = f.read().splitlines()

solution = Coprocessor(instructions)
print('Part 1 Answer: ' + str(solution.part1()))
print('Part 2 Answer: ' + str(solution.part2()))