namespace AdventOfCode2022.FSharp

open AdventOfCode2022.FSharp.Common.Types
open System.IO

module Day02 =
    let day = "02"

    let title = "Rock Paper Scissors"

    let calculateSelectionPoints you =
        if you = 'X' then 1 else if you = 'Y' then 2 else 3

    let calculateWinPoints them you =
        match them with
        | 'A' -> if you = 'Y' then 6 else if you = 'X' then 3 else 0
        | 'B' -> if you = 'Z' then 6 else if you = 'Y' then 3 else 0
        | 'C' -> if you = 'X' then 6 else if you = 'Z' then 3 else 0
        | _ -> raise (System.ArgumentOutOfRangeException())

    let makeShape them winCondition =
        match them with
        | 'A' -> if winCondition = 'X' then 'Z' else if winCondition = 'Y' then 'X' else 'Y'
        | 'B' -> if winCondition = 'X' then 'X' else if winCondition = 'Y' then 'Y' else 'Z'
        | 'C' -> if winCondition = 'X' then 'Y' else if winCondition = 'Y' then 'Z' else 'X'
        | _ -> raise (System.ArgumentOutOfRangeException())

    let scoreRound (round:string) (determineShape:bool) =
        let them = round[0]
        let you = if determineShape then makeShape them round[2] else round[2]
        calculateWinPoints them you + calculateSelectionPoints you

    let scoreGame (determineShape:bool) (lines:string[]) =
        lines
        |> Seq.map (fun l -> scoreRound l determineShape)
        |> Seq.sum

    let puzzle1 input =
        input
        |> scoreGame false

    let puzzle2 input =
        input
        |> scoreGame true

    let actualInput =
        (File.ReadAllLines(@"Inputs\Input02.txt"))

    let Execute = ExecuteOutput day title (puzzle1 actualInput) (puzzle2 actualInput)