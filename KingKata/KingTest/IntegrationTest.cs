using NUnit.Framework;

namespace KingTest {
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
        
        [Test]
        public void KingAlone() {
            AssertCheck(false, 
                "        ",
                "        ",
                "        ",
                "    K   ",
                "        ",
                "        ",
                "        ",
                "        ");
        }
        
        [Test]
        public void NoChecks() {
            AssertCheck(false, 
                "        ",
                "        ",
                "Q    N Q",
                "    K   ",
                "        ",
                "        ",
                "        ",
                "        ");
        }
        
        [Test]
        public void BlockedByPiece() {
            AssertCheck(false, 
                "        ",
                "        ",
                "R    P K",
                "        ",
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
}