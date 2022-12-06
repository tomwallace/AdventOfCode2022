namespace AdventOfCode2022.FSharp

open AdventOfCode2022.FSharp.Common.Types
open System.IO

module Day04 =
    let day = "04"

    let title = "Camp Cleanup"

    let fullyContained (first:int*int) (second:int*int) =
        if fst second >= fst first && snd second <= snd first
            then true
        else if fst first >= fst second && snd first <= snd second
            then true
        else false

    let containedAtAll (first:int*int) (second:int*int) =
        let firstRange = [(fst first)..(snd first)]
        let secondRange = [(fst second)..(snd second)]
        let intersection = Set.intersect (Set.ofList firstRange) (Set.ofList secondRange) |> Set.toList
        if intersection.Length > 0 then true else false

    let evaluateSection (section:string) (atAll:bool) =
        let firstStr = (section.Split ',')[0]
        let secondStr = (section.Split ',')[1]
        let first = ((int)(firstStr.Split '-').[0],(int)(firstStr.Split '-').[1])
        let second = ((int)(secondStr.Split '-').[0],(int)(secondStr.Split '-').[1])
        if atAll && containedAtAll first second then 1
        else if atAll = false && fullyContained first second then 1
        else 0

    let countOverlapSections (atAll:bool) (sections:string[]) =
        sections
        |> Array.map (fun s -> evaluateSection s atAll)
        |> Array.sum

    let puzzle1 input =
        input
        |> countOverlapSections false

    let puzzle2 input =
        input
        |> countOverlapSections true

    let actualInput =
        (File.ReadAllLines(@"Inputs\Input04.txt"))

    let Execute = ExecuteOutput day title (puzzle1 actualInput) (puzzle2 actualInput)