using System;
using System.Collections.Generic;
using System.Linq;

namespace KingTest {
    public class ChessBoard {
        public const int Size = 8;
        
        public ChessBoard(char[,] contents) {
            for (var row = 0; row < Size; row++) {
                for (var col = 0; col < Size; col++) {
                    if (contents[row, col] != ' ') {
                        pieces.Add(Square.Make(row, col), contents[row, col]);
                    }
                }
            }
        }

        public bool IsKingInCheck => pieces.Select(IsPieceAttackingKing).Any(IsTrue);

        bool IsPieceAttackingKing(KeyValuePair<Square, char> pieceLocation) {
            return Rules.PieceAttacks[pieceLocation.Value].Select(line => DoesLineAttackKing(line, pieceLocation.Key)).Any(IsTrue);
        }

        bool DoesLineAttackKing(Func<Square, IEnumerable<Square>> lineOfAttack, Square location) {
            return lineOfAttack(location).Select(PieceOnSquare).Aggregate(DetermineTargetPiece) == 'K';
        }

        char PieceOnSquare(Square location) {
            return pieces.ContainsKey(location) ? pieces[location] : ' ';
        }

        static char DetermineTargetPiece(char previous, char current) {
            return previous != ' ' ? previous : current;
        }

        static bool IsTrue(bool result) { return result;}

        readonly Dictionary<Square, char> pieces = new Dictionary<Square, char>();
    }
}