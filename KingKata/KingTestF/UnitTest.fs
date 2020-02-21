module KingTestF.UnitTest

open NUnit.Framework
open KingTestF.InCheck

[<TestCase(-1,false)>]
[<TestCase(0,true)>]
[<TestCase(7,true)>]
[<TestCase(8,false)>]
let areIndexesValid index expected = Assert.AreEqual (expected, isValidIndex index)

let assertValidMove fromRow fromColumn movement toRow toColumn =
    let toLocation = moveFrom {Row = fromRow; Column = fromColumn;} movement
    Assert.AreEqual (toRow, toLocation.Row)
    Assert.AreEqual (toColumn, toLocation.Column)

[<Test>]
let validMovesAreMade() =
    assertValidMove 0 0 downRight 1 1
    assertValidMove 0 0 down 1 0
    assertValidMove 0 0 right 0 1
    assertValidMove 7 7 upLeft 6 6
    assertValidMove 7 7 up 6 7
    assertValidMove 7 7 left 7 6
    
let assertInvalidMove fromRow fromColumn movement =
    Assert.IsFalse (moveFrom {Row = fromRow; Column = fromColumn;} movement |> isOnBoard)

[<Test>]
let invalidMovesAreDetected() =
    assertInvalidMove 0 0 upLeft
    assertInvalidMove 0 0 up
    assertInvalidMove 0 0 left
    assertInvalidMove 7 7 downRight
    assertInvalidMove 7 7 down
    assertInvalidMove 7 7 right
        
 
let assertAttacks piece expected =
    let generateAttacks generator = generator {Row = 3; Column = 3;}
    let attacks = (linesOfAttack.[piece] |> Seq.map generateAttacks |> Seq.collect (fun x-> x)) |> Seq.toArray
    Assert.AreEqual (Array.length expected, Array.length attacks)
    for i = 0 to Array.length expected - 1 do
        Assert.AreEqual (expected.[i], attacks.[i])
    ()
    
[<Test>]
let pawnAttacksAreGenerated() =
    assertAttacks 'P' [| makeLocation 4 2; makeLocation 4 4; |]

[<Test>]
let bishopAttacksAreGenerated() =
    assertAttacks 'B' [| makeLocation 2 2; makeLocation 1 1; makeLocation 0 0
                         makeLocation 2 4; makeLocation 1 5; makeLocation 0 6
                         makeLocation 4 2; makeLocation 5 1; makeLocation 6 0
                         makeLocation 4 4; makeLocation 5 5; makeLocation 6 6; makeLocation 7 7; |]

[<Test>]
let rookAttacksAreGenerated() =
    assertAttacks 'R' [| makeLocation 2 3; makeLocation 1 3; makeLocation 0 3
                         makeLocation 3 4; makeLocation 3 5; makeLocation 3 6 ; makeLocation 3 7;
                         makeLocation 3 2; makeLocation 3 1; makeLocation 3 0
                         makeLocation 4 3; makeLocation 5 3; makeLocation 6 3; makeLocation 7 3; |]
[<Test>]
let queenAttacksAreGenerated() =
    assertAttacks 'Q' [| makeLocation 2 2; makeLocation 1 1; makeLocation 0 0
                         makeLocation 2 4; makeLocation 1 5; makeLocation 0 6
                         makeLocation 4 2; makeLocation 5 1; makeLocation 6 0
                         makeLocation 4 4; makeLocation 5 5; makeLocation 6 6; makeLocation 7 7
                         makeLocation 2 3; makeLocation 1 3; makeLocation 0 3
                         makeLocation 3 4; makeLocation 3 5; makeLocation 3 6 ; makeLocation 3 7;
                         makeLocation 3 2; makeLocation 3 1; makeLocation 3 0
                         makeLocation 4 3; makeLocation 5 3; makeLocation 6 3; makeLocation 7 3; |]
[<Test>]
let knightAttacksAreGenerated() =
    assertAttacks 'N' [| makeLocation 1 2; makeLocation 1 4; makeLocation 5 2; makeLocation 5 4
                         makeLocation 2 1; makeLocation 2 5; makeLocation 4 1; makeLocation 4 5; |]
