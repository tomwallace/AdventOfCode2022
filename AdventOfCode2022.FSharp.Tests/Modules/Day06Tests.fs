namespace AdventOfCode2022.FSharp.Tests

open Xunit
open AdventOfCode2022.FSharp.Day06

module Day06Tests =

    [<Fact>]
    let ``findRepeatMarker_4`` () =
        Assert.Equal(7, findRepeatMarker 4 "mjqjpqmgbljsphdztnvjfqwrcgsmlb")

    [<Fact>]
    let ``findRepeatMarker_14`` () =
        Assert.Equal(19, findRepeatMarker 14 "mjqjpqmgbljsphdztnvjfqwrcgsmlb")

    [<Fact>]
    let ``Puzzle1 Actual`` () =
        Assert.Equal(1282, puzzle1 inputString )

    [<Fact>]
    let ``Puzzle2 Actual`` () =
        Assert.Equal(3513, puzzle2 inputString )