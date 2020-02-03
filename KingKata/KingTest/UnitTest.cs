using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace KingTest {
    [TestFixture]
    public class UnitTest {

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
