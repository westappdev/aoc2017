let input = """
0
3
0
1
-3
"""

var inputArr = input.split(separator:"\n").map{Int($0)!}
var index = 0
var sum = 0

while(true) {
  sum += 1
  
  if (index + inputArr[index] > inputArr.count-1) {
    break
  }
  
  let move = inputArr[index]
  inputArr[index] += 1
  index += move
}

print(sum)
