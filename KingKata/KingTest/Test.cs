using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace KingTest {
    public class ChessBoard {
        public const int Size = 8;
        
        public static IEnumerable<Square> Squares {
            get {
                for (var row = 0; row < Size; row++) {
                    for (var col = 0; col < Size; col++) {
                        yield return Square.Make(row, col);
                    }
                }
            }
        }

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
            return Rules.PieceAttacks[pieceLocation.Value]().Select(line => DoesLineAttackKing(line, pieceLocation.Key)).Any(IsTrue);
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

        const int noSquare = int.MinValue;

        static bool IsValid(int coordinate) {
            return coordinate >= 0 && coordinate < ChessBoard.Size;
        }

        readonly int row;
        readonly int column;

        Square(int row, int column) {
            this.row =  row;
            this.column = column;
        }
    }

    public struct Movement {
        public static Movement Left = new Movement(0, -1);
        public static Movement Right = new Movement(0, 1);
        public static Movement Up = new Movement(-1, 0);
        public static Movement Down = new Movement(1, 0);

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
        public static readonly Dictionary<char, Func<IEnumerable<Func<Square, IEnumerable<Square>>>>> PieceAttacks
            = new Dictionary<char, Func<IEnumerable<Func<Square, IEnumerable<Square>>>>> {
                {'B', BishopAttacks},
                {'K', KingAttacks},
                {'P', PawnAttacks}
            };
        
        static IEnumerable<Func<Square, IEnumerable<Square>>> BishopAttacks() {
            yield return DiagonalUpLeft;
            yield return DiagonalUpRight;
            yield return DiagonalDownLeft;
            yield return DiagonalDownRight;
        }
        
        static IEnumerable<Square> DiagonalUpLeft(Square location) {
            return MultipleMoves(location, Movement.Up, Movement.Left);
        }
        
        static IEnumerable<Square> DiagonalUpRight(Square location) {
            return MultipleMoves(location, Movement.Up, Movement.Right);
        }
        
        static IEnumerable<Square> DiagonalDownLeft(Square location) {
            return MultipleMoves(location, Movement.Down, Movement.Left);
        }
        
        static IEnumerable<Square> DiagonalDownRight(Square location) {
            return MultipleMoves(location, Movement.Down, Movement.Right);
        }
        
        static IEnumerable<Square> MultipleMoves(Square location, params Movement[] movements) {
            var current = location;
            for (var i = 0; i < ChessBoard.Size - 1; i++) {
                for (var j = 0; j < movements.Length; j++) {
                    current = current.Move(movements[j]);
                }
                yield return current;
            }
        }
        
        static IEnumerable<Func<Square, IEnumerable<Square>>> KingAttacks() {
            yield break;
        }
        
        static IEnumerable<Func<Square, IEnumerable<Square>>> PawnAttacks() {
            yield return PawnLeft;
            yield return PawnRight;
        }

        static IEnumerable<Square> PawnLeft(Square location) {
            yield return location.Move(Movement.Left).Move(Movement.Down);
        }

        static IEnumerable<Square> PawnRight(Square location) {
            yield return location.Move(Movement.Right).Move(Movement.Down);
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

        void AssertCheck(bool expected, params string[] rows) {
            var squares = new char[8, 8];
            for (var row = 0; row < 8; row++)
            for (var col = 0; col < 8; col++)
                squares[row, col] = rows[row][col];
            Assert.AreEqual(expected, new ChessBoard(squares).IsKingInCheck);
                
        }
    }
    
    [TestFixture]
    public class Test {
        [Test]
        public void SquaresAreEnumerated() {
            var squares = ChessBoard.Squares.ToList();
            Assert.AreEqual(64, squares.Count);
            Assert.AreEqual(Square.Make(0,0), squares[0]);
            Assert.AreEqual(Square.Make(7,7), squares[63]);
        }

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
        public void PawnCapturesAreGenerated() {
            var captures = Rules.PieceAttacks['P']().ToList();
            AssertCaptures(captures[0], Square.Make(4,2));
            AssertCaptures(captures[1], Square.Make(4,4));
        }

        [Test]
        public void BishopCapturesAreGenerated() {
            var captures = Rules.PieceAttacks['B']().ToList();
            AssertCaptures(captures[0], Square.Make(2,2), Square.Make(1,1), Square.Make(0,0), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[1], Square.Make(2,4), Square.Make(1,5), Square.Make(0,6), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[2], Square.Make(4,2), Square.Make(5,1), Square.Make(6,0), Square.None, Square.None, Square.None, Square.None);
            AssertCaptures(captures[3], Square.Make(4,4), Square.Make(5,5), Square.Make(6,6), Square.Make(7,7), Square.None, Square.None, Square.None);
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
