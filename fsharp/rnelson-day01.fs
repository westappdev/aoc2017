open System.IO
open System.Linq

// When running from LINQPad, we need to change the working directory to where our file is saved
//Directory.SetCurrentDirectory (Path.GetDirectoryName (Util.CurrentQueryPath));

// Read the input file
let input = File.ReadAllText("../inputs/day01.txt")

// Part 1
let sequence = Seq.append (input.Select (fun c -> System.Int32.Parse(string c))) [input.[0] |> string |> System.Int32.Parse]
let part1 =
    sequence
    |> Seq.take input.Length
    |> Seq.mapi (fun i n ->
        if n = (sequence |> Seq.item (i + 1))
        then n
        else 0)
    |> Seq.sum

// Part 2
let infiniteSequence = Seq.initInfinite (fun i -> input.[i % input.Length] |> string |> System.Int32.Parse)
let part2 =
    sequence
    |> Seq.take input.Length
    |> Seq.mapi (fun i n ->
        if n = (infiniteSequence |> Seq.item (i + (input.Length / 2)))
        then n
        else 0)
    |> Seq.sum

printfn "Part 1: %i\nPart 2: %i" part1 part2
0 |> ignore