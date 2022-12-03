namespace AdventOfCode2022.FSharp.Tests

open System.IO
open Xunit
open AdventOfCode2022.FSharp.Day01

module Day01Tests =

    let testInput =
        (File.ReadAllLines(@"Inputs\Input01A.txt"))

    [<Fact>]
    let ``sumListRange`` () =
        Assert.Equal(6000L, sumListRange testInput (-1,"") (3,""))

    [<Fact>]
    let ``CalcCalorieTotals`` () =
        Assert.Equal(24000L, puzzle1 testInput)

    [<Fact>]
    let ``SumTopThree`` () =
        Assert.Equal(45000L, puzzle2 testInput)

    [<Fact>]
    let ``Puzzle1 Actual`` () =
        Assert.Equal(71924L, puzzle1 actualInput )

    [<Fact>]
    let ``Puzzle2 Actual`` () =
        Assert.Equal(210406L, puzzle2 actualInput )