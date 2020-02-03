namespace KingTest {
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
}