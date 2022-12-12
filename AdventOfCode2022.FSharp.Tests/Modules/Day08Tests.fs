namespace AdventOfCode2022.FSharp.Tests

open System.IO
open Xunit
open AdventOfCode2022.FSharp.Day08

module Day08Tests =

    let testInput =
        (File.ReadAllLines(@"Inputs\Input08A.txt"))

    [<Fact>]
    let ``countVisibleTrees`` () =
        Assert.Equal(21, countVisibleTrees testInput)

    [<Fact>]
    let ``west_for_scenicScores`` () =
        let grid = testInput |> Array.map (fun i -> i |> Seq.toList |> List.map (fun c -> c.ToString() |> int)) |> Array.toList
        let currX = 4
        let currY = 3
        Assert.Equal(4, west currX currY grid)

    [<Fact>]
    let ``north_for_scenicScores`` () =
        let grid = testInput |> Array.map (fun i -> i |> Seq.toList |> List.map (fun c -> c.ToString() |> int)) |> Array.toList
        let currX = 3
        let currY = 4
        Assert.Equal(4, north currX currY grid)

    [<Fact>]
    let ``east_for_scenicScores`` () =
        let grid = testInput |> Array.map (fun i -> i |> Seq.toList |> List.map (fun c -> c.ToString() |> int)) |> Array.toList
        let currX = 1
        let currY = 2
        Assert.Equal(3, east currX currY grid)

    [<Fact>]
    let ``south_for_scenicScores`` () =
        let grid = testInput |> Array.map (fun i -> i |> Seq.toList |> List.map (fun c -> c.ToString() |> int)) |> Array.toList
        let currX = 1
        let currY = 2
        Assert.Equal(2, south currX currY grid)

    [<Fact>]
    let ``totalScenicScores`` () =
        Assert.Equal(8, totalScenicScores testInput)

    [<Fact>]
    let ``Puzzle1 Actual`` () =
        Assert.Equal(1816, puzzle1 actualInput )

    [<Fact>]
    let ``Puzzle2 Actual`` () =
        Assert.Equal(383520, puzzle2 actualInput )