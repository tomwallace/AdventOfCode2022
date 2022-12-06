namespace AdventOfCode2022.FSharp.Tests

open System.IO
open Xunit
open AdventOfCode2022.FSharp.Day05

module Day05Tests =

    let (testStackInput:list<list<char>>) = [
        ['Z'; 'N'];
        ['M'; 'C'; 'D'];
        ['P']
    ]

    let testInput =
        (File.ReadAllLines(@"Inputs\Input05A.txt"))

    [<Fact>]
    let ``moveAndGetTopCrates`` () =
        Assert.Equal("CMZ", moveAndGetTopCrates testStackInput true testInput)

    [<Fact>]
    let ``Puzzle1 Actual`` () =
        Assert.Equal("RNZLFZSJH", puzzle1 actualInput )

    [<Fact>]
    let ``Puzzle2 Actual`` () =
        Assert.Equal("CNSFCGJSM", puzzle2 actualInput )