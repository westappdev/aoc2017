let input = """
5	9	2	8
9	4	7	3
3	8	6	5
"""

var sum = 0
for row in input.split(separator:"\n") {
	let rowArr = row.split(separator:"\t")
	var rowArrInt = rowArr.map{Int($0)!}
	rowArrInt.sort(by: >)
	
	for i in 0...rowArrInt.count-1 {
		for j in 0...rowArrInt.count-1 {
			if (i != j) {
				if (rowArrInt[i] % rowArrInt[j] == 0) {
					sum += rowArrInt[i] / rowArrInt[j]
					break 
				}
			}
		}
	}
}

print(sum)
