namespace AdventOfCode2022.FSharp.Tests

open System.IO
open Xunit
open AdventOfCode2022.FSharp.Day04

module Day04Tests =

    let testInput =
        (File.ReadAllLines(@"Inputs\Input04A.txt"))

    [<Fact>]
    let ``countOverlapSections`` () =
        Assert.Equal(2L, puzzle1 testInput)

    [<Fact>]
    let ``countOverlapSections_AtAll`` () =
        Assert.Equal(4L, puzzle2 testInput)

    [<Fact>]
    let ``Puzzle1 Actual`` () =
        Assert.Equal(459L, puzzle1 actualInput )

    [<Fact>]
    let ``Puzzle2 Actual`` () =
        Assert.Equal(779L, puzzle2 actualInput )