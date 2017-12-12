open System
open System.IO
open System.Linq
open System.Text.RegularExpressions

// See https://gist.github.com/rnelson/e7a93d28c6bbd56653ea2e8de4d98164 for attempts at
// part b in F#

let readInput(filename:string) =
    let lines = File.ReadAllLines filename
    [|
        for l in (lines |> Array.toSeq) do
            yield Regex.Split(l, "\s+") |> Array.map int
    |]
let getDifferencCount arr =
    let values = [|
        for row in arr do
            let minValue = Array.min row
            let maxValue = Array.max row

            yield maxValue - minValue
    |]
    let sum = values |> Seq.sum
    sum

// Read the input file
let input = readInput("../inputs/day02.txt")

let part1 = getDifferencCount(input)
let part2 = 0

printfn "Part 1: %i\nPart 2: %i" part1 part2
0 |> ignore