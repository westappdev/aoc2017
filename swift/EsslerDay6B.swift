let input = """
0	2	7	0
"""

var cycles = 0
var cyclesDelta = 0

var inputArr = input.split(separator:"\t").map{Int($0)!}
var stateHx  = [[Int]]()
stateHx.append(inputArr)

while (true) {
	let maxVal = inputArr.max()!
	let maxIndex = inputArr.index(of:maxVal)!
	
	// Clean out bank with highest number of blocks
	inputArr[maxIndex] = 0
	
	// Destribute blocks one-by-one, index-by-index until gone
	var index = maxIndex
	for _ in 1...maxVal {
		index = (index + 1) % inputArr.count
		inputArr[index] += 1
	}
	
	cycles += 1
	
	// Have we seen this state before?
	if (stateHx.contains{$0 == inputArr}) {
		let cycleHxIndex = stateHx.index(where: { $0 == inputArr })!
		cyclesDelta = cycles - cycleHxIndex
		break
	}
	
	stateHx.append(inputArr)
}

print(cyclesDelta)
