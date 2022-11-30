namespace AdventOfCode2022.FSharp

open AdventOfCode2022.FSharp.Common.Types
open System.IO

module Day99 =
    let day = "99"

    let title = "Sonar Sweep"

    let calculate windowSize (array:string[]) = 
        array
        |> Seq.map int
        |> Seq.windowed windowSize
        |> Seq.map Array.sum
        |> Seq.pairwise
        |> Seq.countBy(fun (a,b) -> a < b)
        |> Seq.find fst
        |> snd

    let puzzle1 input = input |> calculate 1

    let puzzle2 input = input |> calculate 3

    let actualInput =
        (File.ReadAllLines(@"Inputs\Input99.txt"))

    let Execute = ExecuteOutput day title (puzzle1 actualInput) (puzzle2 actualInput)

