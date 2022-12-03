namespace AdventOfCode2022.FSharp

open AdventOfCode2022.FSharp.Common.Types
open System.IO

module Day01 =
    let day = "01"

    let title = "Calorie Counting"

    // Finds the indexes of all the empty lines, so we can group the inbetween
    let emptyIndexes (lines:string[]) =
        lines
        |> Array.indexed
        |> Array.filter (fun (i, x) -> x = "")
        |> Array.toList

    let sumListRange (lines:string[]) (first:(int * string)) (last:(int * string)) =
        // Indexes will be one to early in start and one too late at the end, so adjust
        let f = (fst first) + 1
        let l = (fst last) - 1
        lines[f..l]
        |> Seq.map int
        |> Seq.sum

    let calcCalorieTotals (lines:string[]) =
        // We have to add one before the beginning and one more than the end to include
        // the first and last group
        let emptyIndexWithFirst = (-1,"")::emptyIndexes lines
        let emptyIndexWithLast = emptyIndexWithFirst @ [(lines.Length,"")]
        emptyIndexWithLast
        |> List.pairwise
        |> List.map (fun (a, b) -> sumListRange lines a b)

    let puzzle1 input =
        input
        |> calcCalorieTotals
        |> List.max

    let puzzle2 input =
        input
        |> calcCalorieTotals
        |> List.sortDescending
        |> List.take 3
        |> List.sum

    let actualInput =
        (File.ReadAllLines(@"Inputs\Input01.txt"))

    let Execute = ExecuteOutput day title (puzzle1 actualInput) (puzzle2 actualInput)