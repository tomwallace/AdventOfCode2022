namespace AdventOfCode2022.FSharp.Tests

open System.IO
open Xunit
open AdventOfCode2022.FSharp.Day02

module Day02Tests =

    let testInput =
        (File.ReadAllLines(@"Inputs\Input02A.txt"))

    [<Fact>]
    let ``ScoreGame`` () =
        Assert.Equal(15L, puzzle1 testInput)

    [<Fact>]
    let ``CountSweepGroupIncreases`` () =
        Assert.Equal(12L, puzzle2 testInput)

    [<Fact>]
    let ``Puzzle1 Actual`` () =
        Assert.Equal(14375L, puzzle1 actualInput )

    [<Fact>]
    let ``Puzzle2 Actual`` () =
        Assert.Equal(10274L, puzzle2 actualInput )