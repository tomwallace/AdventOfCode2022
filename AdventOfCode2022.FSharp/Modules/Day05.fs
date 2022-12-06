namespace AdventOfCode2022.FSharp

open AdventOfCode2022.FSharp.Common.Types
open System.IO

module Day05 =
    let day = "05"

    let title = "Supply Stacks"

    let (stackInput:list<list<char>>) = [
        ['H'; 'R'; 'B'; 'D'; 'Z'; 'F'; 'L'; 'S'];
        ['T'; 'B'; 'M'; 'Z'; 'R'];
        ['Z'; 'L'; 'C'; 'H'; 'N'; 'S'];
        ['S'; 'C'; 'F'; 'J'];
        ['P'; 'G'; 'H'; 'W'; 'R'; 'Z'; 'B'];
        ['V';'J';'Z';'G';'D';'N';'M';'T'];
        ['G';'L';'N';'W';'F';'S';'P';'Q'];
        ['M';'Z';'R'];
        ['M';'C';'L';'G';'V';'R';'T']
    ]

    // Returns tuple of the moved crates and the updated list
    let remove (stacks:list<list<char>>) (number:int) (from:int) (reorder:bool) =
        let crates = stacks[from] |> List.skip (stacks[from].Length - number) |> List.take number
        let modCrates = if reorder then crates |> List.rev else crates
        let updatedList = stacks[from] |> List.removeManyAt (stacks[from].Length - number) number
        (modCrates,updatedList)

    // Returns the updated list
    let add (stacks:list<list<char>>) (going:int) (crates:list<char>) =
        let updatedList = List.append stacks[going] crates
        updatedList

    // Returns updated stacks
    let followDirection (direction:string) (stacks:list<list<char>>) (reorder:bool) =
        let splits = direction.Split ' '
        let number = (int)splits[1]
        // Adjust for zero indexed list
        let from = (int)splits[3] - 1
        let going = (int)splits[5] - 1
        let removed = remove stacks number from reorder
        // Update the removed stack
        let stacks = stacks |> List.mapi (fun i v -> if i = from then snd removed else v)
        // Update the added to stack
        let stacks = stacks |> List.mapi (fun i v -> if i = going then (add stacks going (fst removed)) else v)
        stacks

    let moveAndGetTopCrates (stacks:list<list<char>>) (reorder:bool) (directions:string[]) =
        directions
        |> Array.fold (fun s d -> followDirection d s reorder) stacks
        |> List.map (fun s -> s[s.Length - 1])

    let puzzle1 input =
        input
        |> moveAndGetTopCrates stackInput true

    let puzzle2 input =
        input
        |> moveAndGetTopCrates stackInput false

    let actualInput =
        (File.ReadAllLines(@"Inputs\Input05.txt"))

    let Execute = ExecuteOutput day title (puzzle1 actualInput) (puzzle2 actualInput)