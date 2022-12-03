namespace AdventOfCode2022.FSharp.Tests

open System.IO
open Xunit
open AdventOfCode2022.FSharp.Day99

module Day99Tests =

    let testInput =
        (File.ReadAllLines(@"Inputs\Input99A.txt"))

    [<Fact>]
    let ``CountSweepIncreases`` () =
        Assert.Equal(7L, puzzle1 testInput)

    [<Fact>]
    let ``CountSweepGroupIncreases`` () =
        Assert.Equal(5L, puzzle2 testInput)

    [<Fact>]
    let ``Puzzle1 Actual`` () =
        Assert.Equal(1215L, puzzle1 actualInput )

    [<Fact>]
    let ``Puzzle2 Actual`` () =
        Assert.Equal(1150L, puzzle2 actualInput )