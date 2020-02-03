using System;
using System.Collections.Generic;

namespace KingTest {
    public static class Rules {
        public static readonly Dictionary<char, IEnumerable<Func<Square, IEnumerable<Square>>>> PieceAttacks
            = new Dictionary<char, IEnumerable<Func<Square, IEnumerable<Square>>>> {
                {'B', new Func<Square, IEnumerable<Square>>[] {
                    DiagonalUpLeft, DiagonalUpRight, DiagonalDownLeft, DiagonalDownRight
                }},
                {'K', new Func<Square, IEnumerable<Square>>[] {}},
                {'N', new Func<Square, IEnumerable<Square>>[] {
                    KnightUpUpLeft, KnightUpUpRight, KnightDownDownLeft, KnightDownDownRight, KnightUpLeftLeft, KnightUpRightRight, KnightDownLeftLeft, KnightDownRightRight
                }},
                {'P', new Func<Square, IEnumerable<Square>>[] {
                    PawnLeft, PawnRight
                }},
                {'Q', new Func<Square, IEnumerable<Square>>[] {
                    StraightUp, StraightLeft, StraightRight, StraightDown, DiagonalUpLeft, DiagonalUpRight, DiagonalDownLeft, DiagonalDownRight
                }},
                {'R', new Func<Square, IEnumerable<Square>>[] {
                    StraightUp, StraightLeft, StraightRight, StraightDown
                }}
            };

        static IEnumerable<Square> KnightUpUpLeft(Square location) {
            yield return location.Move(Movement.UpUpLeft);
        }

        static IEnumerable<Square> KnightUpUpRight(Square location) {
            yield return location.Move(Movement.UpUpRight);
        }

        static IEnumerable<Square> KnightDownDownLeft(Square location) {
            yield return location.Move(Movement.DownDownLeft);
        }

        static IEnumerable<Square> KnightDownDownRight(Square location) {
            yield return location.Move(Movement.DownDownRight);
        }

        static IEnumerable<Square> KnightUpLeftLeft(Square location) {
            yield return location.Move(Movement.UpLeftLeft);
        }

        static IEnumerable<Square> KnightUpRightRight(Square location) {
            yield return location.Move(Movement.UpRightRight);
        }

        static IEnumerable<Square> KnightDownLeftLeft(Square location) {
            yield return location.Move(Movement.DownLeftLeft);
        }

        static IEnumerable<Square> KnightDownRightRight(Square location) {
            yield return location.Move(Movement.DownRightRight);
        }
        
        static IEnumerable<Square> StraightUp(Square location) {
            return MultipleMoves(location, Movement.Up);
        }
        
        static IEnumerable<Square> StraightLeft(Square location) {
            return MultipleMoves(location, Movement.Left);
        }
        
        static IEnumerable<Square> StraightRight(Square location) {
            return MultipleMoves(location, Movement.Right);
        }
        
        static IEnumerable<Square> StraightDown(Square location) {
            return MultipleMoves(location, Movement.Down);
        }
        
        static IEnumerable<Square> DiagonalUpLeft(Square location) {
            return MultipleMoves(location, Movement.UpLeft);
        }
        
        static IEnumerable<Square> DiagonalUpRight(Square location) {
            return MultipleMoves(location, Movement.UpRight);
        }
        
        static IEnumerable<Square> DiagonalDownLeft(Square location) {
            return MultipleMoves(location, Movement.DownLeft);
        }
        
        static IEnumerable<Square> DiagonalDownRight(Square location) {
            return MultipleMoves(location, Movement.DownRight);
        }
        
        static IEnumerable<Square> MultipleMoves(Square location, Movement movement) {
            var current = location;
            for (var i = 0; i < ChessBoard.Size - 1; i++) {
                current = current.Move(movement);
                yield return current;
            }
        }

        static IEnumerable<Square> PawnLeft(Square location) {
            yield return location.Move(Movement.DownLeft);
        }

        static IEnumerable<Square> PawnRight(Square location) {
            yield return location.Move(Movement.DownRight);
        }
    }
}