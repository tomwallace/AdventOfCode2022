namespace AdventOfCode2022.FSharp

open AdventOfCode2022.FSharp.Common.Types
open System.IO

module Day03 =
    let day = "03"

    let title = "Rucksack Reorganization"

    let calculatePriorityScore item =
        if System.Char.IsLower item then (int)item - 96 else (int)item - 38

    let findDuplicateItem (sack:string) =
        let split = sack.Length/2
        let halves = seq {
            yield sack.[..split-1]|>  Seq.toList
            yield sack.Substring split |> Seq.toList
        }
        // Transform into sequence of sets, so we can reduce to find common element
        let halvesSet = Seq.map Set.ofList halves
        let duplicateItems = Seq.reduce Set.intersect halvesSet
        duplicateItems |> Seq.head

    // Recursive method that iterates over the seq and chops it into sections of segmentSize
    // https://codereview.stackexchange.com/questions/45668/splitting-a-sequence-into-equal-segments
    let rec chop segmentSize source =
        seq {
                if Seq.isEmpty source then () else
                let segment = source |> Seq.truncate segmentSize
                let rest = source |> Seq.skip (Seq.length segment)
                yield segment |> Seq.toList
                yield! chop segmentSize rest
        }

    let findGroupCommonItem (group:seq<char list>) =
        // Transform into sequence of sets, so we can reduce to find common element
        let groupSet = Seq.map Set.ofList group
        let duplicateItems = Seq.reduce Set.intersect groupSet
        duplicateItems |> Seq.head

    let groupCommonItemsPrioritySum (sacks:string[]) =
        // Get groups of 3, but convert to lists of char first
        let groups = sacks |> Seq.map (fun s -> Seq.toList s) |> chop 3
        let commonItems  = Seq.map findGroupCommonItem groups
        commonItems
        |> Seq.map (fun r -> calculatePriorityScore r)
        |> Seq.sum

    let duplicateItemsPrioritySum (rucksacks:string[]) =
        rucksacks
        |> Seq.map (fun r -> findDuplicateItem r)
        |> Seq.map (fun r -> calculatePriorityScore r)
        |> Seq.sum

    let puzzle1 input =
        input
        |> duplicateItemsPrioritySum

    let puzzle2 input =
        input
        |> groupCommonItemsPrioritySum

    let actualInput =
        (File.ReadAllLines(@"Inputs\Input03.txt"))

    let Execute = ExecuteOutput day title (puzzle1 actualInput) (puzzle2 actualInput)