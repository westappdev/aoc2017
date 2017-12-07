let input = """
5	1	9	5
7	5	3
2	4	6	8
"""

var sum = 0
for row in input.split(separator:"\n") {
	let rowArr = row.split(separator:"\t")
	var rowArrInt = rowArr.map{Int($0)!}
	rowArrInt.sort()
	
	sum += rowArrInt[rowArrInt.count-1] - rowArrInt[0]
}

print(sum)
