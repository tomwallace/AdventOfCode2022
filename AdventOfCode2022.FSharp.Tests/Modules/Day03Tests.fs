namespace AdventOfCode2022.FSharp.Tests

open System.IO
open Xunit
open AdventOfCode2022.FSharp.Day03

module Day03Tests =

    let testInput =
        (File.ReadAllLines(@"Inputs\Input03A.txt"))

    [<Fact>]
    let ``DuplicateItemsPrioritySum`` () =
        Assert.Equal(157L, duplicateItemsPrioritySum testInput)

    [<Fact>]
    let ``GroupCommonItemsPrioritySum`` () =
        Assert.Equal(70L, groupCommonItemsPrioritySum testInput)

    [<Fact>]
    let ``Puzzle1 Actual`` () =
        Assert.Equal(7763L, puzzle1 actualInput )

    [<Fact>]
    let ``Puzzle2 Actual`` () =
        Assert.Equal(2569L, puzzle2 actualInput )