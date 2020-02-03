namespace KingTest {
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
}