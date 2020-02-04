using System;
using System.Collections.Generic;

namespace KingTest {
    public static class Rules {
        public static readonly Dictionary<char, IEnumerable<Func<Square, IEnumerable<Square>>>> PieceAttacks
            = new Dictionary<char, IEnumerable<Func<Square, IEnumerable<Square>>>> {
                {'B', new [] {
                    Moves(Movement.UpLeft), Moves(Movement.UpRight), Moves(Movement.DownLeft), Moves(Movement.DownRight)
                }},
                {'K', new Func<Square, IEnumerable<Square>>[] {}},
                {'N', new [] {
                    Move(Movement.UpUpLeft), Move(Movement.UpUpRight), Move(Movement.DownDownLeft), Move(Movement.DownDownRight),
                    Move(Movement.UpLeftLeft), Move(Movement.UpRightRight), Move(Movement.DownLeftLeft), Move(Movement.DownRightRight)
                }},
                {'P', new [] {
                    Move(Movement.DownLeft), Move(Movement.DownRight)
                }},
                {'Q', new [] {
                    Moves(Movement.Up), Moves(Movement.Left), Moves(Movement.Right), Moves(Movement.Down),
                    Moves(Movement.UpLeft), Moves(Movement.UpRight), Moves(Movement.DownLeft), Moves(Movement.DownRight)
                }},
                {'R', new [] {
                    Moves(Movement.Up), Moves(Movement.Left), Moves(Movement.Right), Moves(Movement.Down)
                }}
            };

        static Func<Square, IEnumerable<Square>> Move(Movement movement) {
            return location => SingleMove(location, movement);
        }

        static Func<Square, IEnumerable<Square>> Moves(Movement movement) {
            return location => MultipleMoves(location, movement);
        }

        static IEnumerable<Square> SingleMove(Square location, Movement movement) {
            yield return location.Move(movement);
        }
        
        static IEnumerable<Square> MultipleMoves(Square location, Movement movement) {
            var current = location;
            for (var i = 0; i < ChessBoard.Size - 1; i++) {
                current = current.Move(movement);
                yield return current;
            }
        }
    }
}