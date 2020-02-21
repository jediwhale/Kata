module KingTestF.IntegrationTest

open NUnit.Framework

let assertInCheck expected rows =
    Assert.AreEqual (expected, InCheck.isKingInCheck (rows |> Seq.map Seq.toList))

[<Test>]
let checkByPawn() =
    assertInCheck true
           ["        ";
            "        ";
            "        ";
            "        ";
            "    P   ";
            "   K    ";
            "        ";
            "        "]

[<Test>]
let checkByKnight() =
    assertInCheck true
           ["        ";
            "        ";
            "        ";
            "        ";
            "        ";
            " K      ";
            "PP      ";
            "N       "]

[<Test>]
let checkByBishop() =
    assertInCheck true
           ["       B";
            "        ";
            "        ";
            "        ";
            "        ";
            "        ";
            "        ";
            "K       "]

[<Test>]
let checkByRook() =
    assertInCheck true
           ["        ";
            "        ";
            "        ";
            "        ";
            "  K  R  ";
            "        ";
            "        ";
            "        "]

[<Test>]
let checkByQueen() =
    assertInCheck true
           ["        ";
            "        ";
            "        ";
            " Q     K";
            "        ";
            "        ";
            "        ";
            "        "]

[<Test>]
let KingAlone() =
    assertInCheck false
           ["        ";
            "        ";
            "        ";
            "    K   ";
            "        ";
            "        ";
            "        ";
            "        "]

[<Test>]
let noChecks() =
    assertInCheck false
           ["        ";
            "        ";
            "Q    N Q";
            "    K   ";
            "        ";
            "        ";
            "        ";
            "        "]

[<Test>]
let blockedByPiece() =
    assertInCheck false
           ["        ";
            "        ";
            "R    P K";
            "        ";
            "        ";
            "        ";
            "        ";
            "        "]
