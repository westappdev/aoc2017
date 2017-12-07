let input = """
abcde fghij
abcde xyz ecdab
a ab abc abd abf
iiii oiii ooii oooi oooo
oiii ioii iioi iiio
"""

var sum = 0

let inputLines = input.split(separator:"\n")
for line in inputLines {
	var sortedWords = [String]()
	let words = line.split(separator:" ")
	
	for word in words {
		let letters = Array(word.characters)
		let sortedLetters = String(letters.sorted())
		sortedWords.append(sortedLetters)
	}
	
	let uniqueWords = Set(sortedWords).count
	if (sortedWords.count == uniqueWords) {
		sum += 1
	}
}

print(sum)
