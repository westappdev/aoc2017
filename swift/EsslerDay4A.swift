let input2 = """
aa bb cc dd ee
aa bb cc dd aa
aa bb cc dd aaa
"""

var sum = 0

let inputLines = input.split(separator:"\n")
for line in inputLines {
	let words = line.split(separator:" ")
	let uniqueWords = Set(words).count
	if (words.count == uniqueWords) {
		sum += 1
	}
}

print(sum)
