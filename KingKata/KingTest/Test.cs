using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

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
            return lineOfAttack(location).Select(ContentOfSquare).Aggregate(AggregateTargetPiece) == 'K';
        }

        char ContentOfSquare(Square location) {
            return pieces.ContainsKey(location) ? pieces[location] : ' ';
        }

        static char AggregateTargetPiece(char previous, char current) {
            return previous != ' ' ? previous : current;
        }

        static bool IsTrue(bool result) { return result;}

        readonly Dictionary<Square, char> pieces = new Dictionary<Square, char>();
    }

    public struct Square {
        public static Square None = new Square(noSquare, noSquare);

        public static Square Make(int row, int column) {
            return IsValid(row) && IsValid(column) ? new Square(row, column) : None;
        }

        public Square Move(Movement movement) {
            return movement.Apply(row, column);
        }

        public override string ToString() {
            return $"[{row},{column}]";
        }

        const int noSquare = -999999999;

        static bool IsValid(int coordinate) {
            return coordinate >= 0 && coordinate < ChessBoard.Size;
        }

        Square(int row, int column) {
            this.row =  row;
            this.column = column;
        }

        readonly int row;
        readonly int column;
    }

    public struct Movement {
        public static Movement Left = new Movement(0, -1);
        public static Movement Right = new Movement(0, 1);
        public static Movement Up = new Movement(-1, 0);
        public static Movement Down = new Movement(1, 0);
        public static Movement UpLeft = new Movement(-1, -1);
        public static Movement UpRight = new Movement(-1, 1);
        public static Movement DownLeft = new Movement(1, -1);
        public static Movement DownRight = new Movement(1, 1);
        public static Movement UpUpLeft = new Movement(-2, -1);
        public static Movement UpUpRight = new Movement(-2, 1);
        public static Movement DownDownLeft = new Movement(2, -1);
        public static Movement DownDownRight = new Movement(2, 1);
        public static Movement UpLeftLeft = new Movement(-1, -2);
        public static Movement UpRightRight = new Movement(-1, 2);
        public static Movement DownLeftLeft = new Movement(1, -2);
        public static Movement DownRightRight = new Movement(1, 2);

        public Square Apply(int row, int column) {
            return Square.Make(row + rowMovement, column + columnMovement);
        }

        Movement(int rowMovement, int columnMovement) {
            this.rowMovement = rowMovement;
            this.columnMovement = columnMovement;
        }
        
        readonly int rowMovement;
        readonly int columnMovement;
    }

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

    [TestFixture]
    public class IntegrationTest {
        
        [Test]
        public void CheckByPawn() {
            AssertCheck(true, 
                "        ",
                "        ",
                "        ",
                "        ",
                "    P   ",
                "   K    ",
                "        ",
                "        ");
        }
        
        [Test]
        public void CheckByBishop() {
            AssertCheck(true, 
                "       B",
                "        ",
                "        ",
                "        ",
                "        ",
                "        ",
                "        ",
                "K       ");
        }
        
        [Test]
        public void CheckByRook() {
            AssertCheck(true, 
                "        ",
                "        ",
                "        ",
                "        ",
                "  K  R  ",
                "        ",
                "        ",
                "        ");
        }
        
        [Test]
        public void CheckByKnight() {
            AssertCheck(true, 
                "        ",
                "        ",
                "        ",
                "        ",
                "        ",
                " K      ",
                "        ",
                "N       ");
        }
        
        [Test]
        public void CheckByQueen() {
            AssertCheck(true, 
                "        ",
                "        ",
                "        ",
                " Q     K",
                "        ",
                "        ",
                "        ",
                "        ");
        }

        static void AssertCheck(bool expected, params string[] rows) {
            var squares = new char[8, 8];
            for (var row = 0; row < 8; row++) {
                for (var col = 0; col < 8; col++) {
                    squares[row, col] = rows[row][col];
                }
            }
            Assert.AreEqual(expected, new ChessBoard(squares).IsKingInCheck);
        }
    }
    
    [TestFixture]
    public class Test {

        [Test]
        public void LeftMovementIsCalculated() {
            Assert.AreEqual(Square.Make(3,2), Square.Make(3,3).Move(Movement.Left));
            Assert.AreEqual(Square.None, Square.Make(3,0).Move(Movement.Left));
        }

        [Test]
        public void RightMovementIsCalculated() {
            Assert.AreEqual(Square.Make(3,4), Square.Make(3,3).Move(Movement.Right));
            Assert.AreEqual(Square.None, Square.Make(3,7).Move(Movement.Right));
        }

        [Test]
        public void DownMovementIsCalculated() {
            Assert.AreEqual(Square.Make(4,3), Square.Make(3,3).Move(Movement.Down));
            Assert.AreEqual(Square.None, Square.Make(7,3).Move(Movement.Down));
        }

        [Test]
        public void UpMovementIsCalculated() {
            Assert.AreEqual(Square.Make(2,3), Square.Make(3,3).Move(Movement.Up));
            Assert.AreEqual(Square.None, Square.Make(0,3).Move(Movement.Up));
        }

        [Test]
        public void PawnCapturesAreGenerated() {
            var captures = Rules.PieceAttacks['P'].ToList();
            AssertCaptures(captures[0], Square.Make(4,2));
            AssertCaptures(captures[1], Square.Make(4,4));
        }

        [Test]
        public void BishopCapturesAreGenerated() {
            var captures = Rules.PieceAttacks['B'].ToList();
            AssertCaptures(captures[0], Square.Make(2,2), Square.Make(1,1), Square.Make(0,0), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[1], Square.Make(2,4), Square.Make(1,5), Square.Make(0,6), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[2], Square.Make(4,2), Square.Make(5,1), Square.Make(6,0), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[3], Square.Make(4,4), Square.Make(5,5), Square.Make(6,6), Square.Make(7,7), Square.None, Square.None, Square.None);
        }

        [Test]
        public void RookCapturesAreGenerated() {
            var captures = Rules.PieceAttacks['R'].ToList();
            AssertCaptures(captures[0], Square.Make(2,3), Square.Make(1,3), Square.Make(0,3), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[1], Square.Make(3,2), Square.Make(3,1), Square.Make(3,0), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[2], Square.Make(3,4), Square.Make(3,5), Square.Make(3,6), Square.Make(3,7), Square.None, Square.None, Square.None);
            AssertCaptures(captures[3], Square.Make(4,3), Square.Make(5,3), Square.Make(6,3), Square.Make(7,3), Square.None, Square.None, Square.None);
        }

        [Test]
        public void QueenCapturesAreGenerated() {
            var captures = Rules.PieceAttacks['Q'].ToList();
            AssertCaptures(captures[0], Square.Make(2,3), Square.Make(1,3), Square.Make(0,3), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[1], Square.Make(3,2), Square.Make(3,1), Square.Make(3,0), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[2], Square.Make(3,4), Square.Make(3,5), Square.Make(3,6), Square.Make(3,7), Square.None, Square.None, Square.None);
            AssertCaptures(captures[3], Square.Make(4,3), Square.Make(5,3), Square.Make(6,3), Square.Make(7,3), Square.None, Square.None, Square.None);
            AssertCaptures(captures[4], Square.Make(2,2), Square.Make(1,1), Square.Make(0,0), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[5], Square.Make(2,4), Square.Make(1,5), Square.Make(0,6), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[6], Square.Make(4,2), Square.Make(5,1), Square.Make(6,0), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[7], Square.Make(4,4), Square.Make(5,5), Square.Make(6,6), Square.Make(7,7), Square.None, Square.None, Square.None);
        }

        [Test]
        public void KnightCapturesAreGenerated() {
            var captures = Rules.PieceAttacks['N'].ToList();
            AssertCaptures(captures[0], Square.Make(1,2));
            AssertCaptures(captures[1], Square.Make(1,4));
            AssertCaptures(captures[2], Square.Make(5,2));
            AssertCaptures(captures[3], Square.Make(5,4));
            AssertCaptures(captures[4], Square.Make(2,1));
            AssertCaptures(captures[5], Square.Make(2,5));
            AssertCaptures(captures[6], Square.Make(4,1));
            AssertCaptures(captures[7], Square.Make(4,5));
        }
 
        static void AssertCaptures(Func<Square, IEnumerable<Square>> generator, params Square[] expected) {
            var squares = generator(Square.Make(3, 3)).ToList();
            Assert.AreEqual(expected.Length, squares.Count);
            for (var i = 0; i < expected.Length; i++) {
                Assert.AreEqual(expected[i], squares[i]);
            }
        }
    }
}
