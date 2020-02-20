module KingTestF.IntegrationTest

open NUnit.Framework

type Movement = {RowMovement: int; ColumnMovement: int}
let up = {RowMovement = -1; ColumnMovement = 0}
let down = {RowMovement = 1; ColumnMovement = 0}
let left = {RowMovement = 0; ColumnMovement = -1}
let right = {RowMovement = 0; ColumnMovement = 1}
let upLeft = {RowMovement = -1; ColumnMovement = -1}
let upRight = {RowMovement = -1; ColumnMovement = 1}
let downLeft = {RowMovement = 1; ColumnMovement = -1}
let downRight = {RowMovement = 1; ColumnMovement = 1}
let upUpLeft = {RowMovement = -2; ColumnMovement = -1}
let upUpRight = {RowMovement = -2; ColumnMovement = 1}
let downDownLeft = {RowMovement = 2; ColumnMovement = -1}
let downDownRight = {RowMovement = 2; ColumnMovement = 1}
let upLeftLeft = {RowMovement = -1; ColumnMovement = -2}
let upRightRight = {RowMovement = -1; ColumnMovement = 2}
let downLeftLeft = {RowMovement = 1; ColumnMovement = -2}
let downRightRight = {RowMovement = 1; ColumnMovement = 2}

type Location = {Row: int; Column: int}  with
    static member isValid coordinate = coordinate >= 0 && coordinate < 8
    static member makeLocation row column =
        if Location.isValid row && Location.isValid column then {Row = row; Column = column} else {Row = -999999999; Column = -999999999}
    member this.move movement =
        Location.makeLocation (this.Row + movement.RowMovement) (this.Column + movement.ColumnMovement)
    member this.onBoard = Location.isValid this.Row && Location.isValid this.Column
    
type Square = {Location: Location; Piece: char} with
    static member hasPiece this = this.Piece <> ' '
    static member itemForMap this = (this.Location, this.Piece)
    
let singleMove (location: Location) movement = seq {location.move movement}
let move movement = fun location -> singleMove location movement
    
let nextLocation movement (location: Location) =
    let next = location.move movement
    if next.onBoard then Some (next, next) else None
    
let multipleMoves (location: Location) movement = Seq.unfold (nextLocation movement) location
let moves movement = fun location -> multipleMoves location movement
    
let linesOfAttack =
    ['B', [moves(upLeft); moves(upRight); moves(downLeft); moves(downRight)];
     'K', []
     'N', [move(upUpLeft); move(upUpRight); move(downDownLeft); move(downDownRight);
           move(upLeftLeft); move(upRightRight); move(downLeftLeft); move(downRightRight)];
     'P', [move(downLeft); move(downRight)];
     'Q', [moves(upLeft); moves(upRight); moves(downLeft); moves(downRight); moves(up); moves(right); moves(left); moves(down)];
     'R', [moves(up); moves(right); moves(left); moves(down)];]
    |> Map.ofList

let isKingInCheck rows =
   let makeSquares rowIndex row =
       let makeSquare columnIndex piece = {Location = {Row = rowIndex; Column = columnIndex;}; Piece = piece;}
       row |> Seq.mapi makeSquare
   let pieces = rows
                |> Seq.mapi makeSquares
                |> Seq.collect (fun x -> x)
                |> Seq.filter Square.hasPiece
                |> Seq.map Square.itemForMap
                |> Map.ofSeq
   let isPieceAttackingKing location piece =
       let doesLineAttackKing (line: Location -> seq<Location>) =
           let pieceOnSquare location = if pieces.ContainsKey location then pieces.[location] else ' '
           let determineTargetPiece previous current = if previous <> ' ' then previous else current
           ((line location) |> Seq.map pieceOnSquare |> Seq.fold determineTargetPiece ' ') = 'K'
       linesOfAttack.[piece] |> Seq.exists doesLineAttackKing
   pieces |> Map.exists isPieceAttackingKing

let assertCheck expected rows =
    Assert.AreEqual (expected, isKingInCheck (rows |> Seq.map Seq.toList))

[<Test>]
let checkByPawn() =
    assertCheck true
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
    assertCheck true
           ["        ";
            "        ";
            "        ";
            "        ";
            "        ";
            " K      ";
            "        ";
            "N       "]

[<Test>]
let checkByBishop() =
    assertCheck true
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
    assertCheck true
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
    assertCheck true
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
    assertCheck false
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
    assertCheck false
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
    assertCheck false
           ["        ";
            "        ";
            "R    P K";
            "        ";
            "        ";
            "        ";
            "        ";
            "        "]
