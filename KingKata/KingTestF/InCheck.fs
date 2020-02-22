module KingTestF.InCheck

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

let isValidIndex index = index >= 0 && index <= 7

type Location = {Row: int; Column: int}
let isOnBoard location = isValidIndex location.Row && isValidIndex location.Column
let makeLocation row column =
        if isValidIndex row && isValidIndex column
        then {Row = row; Column = column}
        else {Row = -999999999; Column = -999999999}
let moveFrom location movement =
        makeLocation (location.Row + movement.RowMovement) (location.Column + movement.ColumnMovement)
    
let singleMove location movement = seq {moveFrom location movement}
let move movement = fun location -> singleMove location movement
    
let nextLocation movement location =
    let next = moveFrom location movement
    if isOnBoard next then Some (next, next) else None
    
let multipleMoves location movement = Seq.unfold (nextLocation movement) location
let moves movement = fun location -> multipleMoves location movement
    
let linesOfAttack =
    ['B', [moves(upLeft); moves(upRight); moves(downLeft); moves(downRight)];
     'K', [];
     'N', [move(upUpLeft); move(upUpRight); move(downDownLeft); move(downDownRight);
           move(upLeftLeft); move(upRightRight); move(downLeftLeft); move(downRightRight)];
     'P', [move(downLeft); move(downRight)];
     'Q', [moves(upLeft); moves(upRight); moves(downLeft); moves(downRight)
           moves(up); moves(right); moves(left); moves(down)];
     'R', [moves(up); moves(right); moves(left); moves(down)];]
    |> Map.ofList
    
let makeSquares rowIndex row =
    let makeSquare columnIndex piece = (makeLocation rowIndex columnIndex, piece)
    row |> Seq.mapi makeSquare
    
let hasPiece (_, piece) = piece <> ' '

let makePieces rows =
    rows |> Seq.mapi makeSquares
         |> Seq.collect (fun x -> x)
         |> Seq.filter hasPiece
         |> Map.ofSeq
         
let determineTargetPiece previous current = if previous <> ' ' then previous else current
         
let isKingInCheck rows =
   let pieces = makePieces rows
   let pieceAtLocation location = if pieces.ContainsKey location then pieces.[location] else ' '
   let isPieceAttackingKing location piece =
       let doesLineAttackKing line =
           ((line location) |> Seq.map pieceAtLocation |> Seq.fold determineTargetPiece ' ') = 'K'
       linesOfAttack.[piece] |> Seq.exists doesLineAttackKing
   pieces |> Map.exists isPieceAttackingKing

