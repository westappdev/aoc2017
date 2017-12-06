let input = "12131415"
let length = input.count
let digits = Array(input)
var sum = 0

for index in 0..<length { 
  if digits[index] == digits[(index+length/2)%length] {
    sum += Int(String(digits[index]))!
  }
}

print(sum)
