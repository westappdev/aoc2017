let input = "91212129"
let length = input.count
let digits = Array(input)
var sum = 0

for index in 0..<length { 
  if digits[index] == digits[(index+1)%length] {
    sum += Int(String(digits[index]))!
  }
}

print(sum)
