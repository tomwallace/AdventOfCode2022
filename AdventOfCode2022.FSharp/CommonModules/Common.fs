namespace AdventOfCode2022.FSharp.Common

open System.IO

module Types =
    let ExecuteOutput day title puzzle1 puzzle2 =
        let titlePad = title |> string |> fun c -> c.PadRight(20, ' ')
        let puzzle1Pad = puzzle1 |> string |> fun c -> c.PadLeft(15, ' ')
        let puzzle2Pad = puzzle2 |> string |> fun c -> c.PadLeft(15, ' ')
        printfn $"Day {day} - {titlePad} | 1: {puzzle1Pad} | 2: {puzzle2Pad}"

    // TODO: Remove
    type ISolution =
        interface
            abstract member Execute : unit
    end

    // TODO: Try string substitution with  {} in filename
    let private execute day puzzle1 puzzle2 =
        (File.ReadAllLines (@"Inputs\Input" + (day |> string |> fun c -> c.PadLeft(2,'0')) + ".txt"))
        |> fun i -> (day, (puzzle1 i |> string |> fun c -> c.PadLeft(15, ' ')), (puzzle2 i |> string |> fun c -> c.PadLeft(15, ' ')))
        |||> printfn "Day %2d | 1: %20s | 2: %20s"

    type Solution (executor)=
        interface ISolution with
            member x.Execute:unit = executor

        new (day:int, puzzle1: string[] -> int, puzzle2: string[] -> int) = new Solution(execute day puzzle1 puzzle2)
        new (day:int, puzzle1: string[] -> int64, puzzle2: string[] -> int64) = new Solution(execute day puzzle1 puzzle2)
        new (day:int, puzzle1: string[] -> string, puzzle2: string[] -> string) = new Solution(execute day puzzle1 puzzle2)

