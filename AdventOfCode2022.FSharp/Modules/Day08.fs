namespace AdventOfCode2022.FSharp

open AdventOfCode2022.FSharp.Common.Types
open System.IO

module Day08 =
    let day = "08"

    let title = "Treetop Tree House [HARD]"

    let isTreeVisible (currX:int) (currY:int) (grid:list<list<int>>) : bool =
        let currValue = grid[currY][currX]
        if currX = 0 || currX = grid[0].Length - 1 || currY = 0 || currY = grid.Length - 1
            then true
        else
            let west = [0..(currX-1)] |> List.map (fun x -> currValue <= grid[currY][x]) |> List.forall (fun e -> e = false)
            let north = [0..(currY-1)] |> List.map (fun y -> currValue <= grid[y][currX]) |> List.forall (fun e -> e = false)
            let east = [(currX+1)..grid[0].Length-1] |> List.map (fun x -> currValue <= grid[currY][x]) |> List.forall (fun e -> e = false)
            let south = [(currY+1)..grid.Length-1] |> List.map (fun y -> currValue <= grid[y][currX]) |> List.forall (fun e -> e = false)

            west || north || east || south

    let west (currX:int) (currY:int) (grid:list<list<int>>) : int =
        let currValue = grid[currY][currX]
        if currX = 0 then 0
        else
            let range = [0..(currX-1)]
            let found = range |> List.tryFindIndexBack (fun x -> currValue <= grid[currY][x])
            if found.IsNone then currX
                else (range.Length - found.Value)

    let north (currX:int) (currY:int) (grid:list<list<int>>) : int =
        let currValue = grid[currY][currX]
        if currY = 0 then 0
        else
            let range = [0..(currY-1)]
            let found = range |> List.tryFindIndexBack (fun y -> currValue <= grid[y][currX])
            if found.IsNone then currY
                else (range.Length - found.Value)

    let east (currX:int) (currY:int) (grid:list<list<int>>) : int =
        let currValue = grid[currY][currX]
        if currY = grid[0].Length - 1 then 0
        else
            let range = [(currX+1)..(grid[0].Length-1)]
            let found = range |> List.tryFindIndex (fun x -> currValue <= grid[currY][x])
            if found.IsNone then (grid[0].Length - 1) - currX
                else found.Value + 1

    let south (currX:int) (currY:int) (grid:list<list<int>>) : int =
        let currValue = grid[currY][currX]
        if currY = grid.Length - 1 then 0
        else
            let range = [(currY+1)..(grid.Length-1)]
            let found = range |> List.tryFindIndex (fun y -> currValue <= grid[y][currX])
            if found.IsNone then (grid.Length - 1) - currY
                else found.Value + 1

    let calculateScenicScore (currX:int) (currY:int) (grid:list<list<int>>) : int =
        let westValue = west currX currY grid
        let northValue = north currX currY grid
        let eastValue = east currX currY grid
        let southValue = south currX currY grid
        let total = westValue * northValue * eastValue * southValue
        total

    let countRow (currY:int) (grid:list<list<int>>) =
        let foundRows = [0..grid[0].Length-1] |> List.filter (fun x -> isTreeVisible x currY grid = true)
        foundRows.Length

    let countVisibleTrees (input:string[]) =
        let grid = input |> Array.map (fun i -> i |> Seq.toList |> List.map (fun c -> c.ToString() |> int)) |> Array.toList
        let count = [0..grid.Length-1] |> List.map (fun y -> countRow y grid) |> List.sum
        count

    let calculateRow (currY:int) (grid:list<list<int>>) =
        let max = [0..grid[0].Length-1] |> List.map (fun x -> calculateScenicScore x currY grid) |> List.max
        max

    let totalScenicScores (input:string[]) =
        let grid = input |> Array.map (fun i -> i |> Seq.toList |> List.map (fun c -> c.ToString() |> int)) |> Array.toList
        let max = [0..grid.Length-1] |> List.map (fun y -> calculateRow y grid) |> List.max
        max

    let puzzle1 input =
        input
        |> countVisibleTrees

    let puzzle2 input =
        input
        |> totalScenicScores

    let actualInput =
        (File.ReadAllLines(@"Inputs\Input08.txt"))

    let Execute = ExecuteOutput day title (puzzle1 actualInput) (puzzle2 actualInput)