namespace QuadRelate.Types
{
    public class Board
    {
        public static readonly int Width = 7;
        public static readonly int Height = 6;

        private readonly Cell[,] _position = new Cell[Width, Height];

        public Cell this[int x, int y]
        {
            get => _position[x, y];

            set => _position[x, y] = value;
        }

        public Board Clone()
        {
            var boardClone = new Board();

            for (var x = 0; x < Board.Width; x++)
            {
                for (var y = 0; y < Board.Height; y++)
                {
                    boardClone[x, y] = this[x, y];
                }
            }

            return boardClone;
        }

        public void PlaceCounter(int column, Cell counter)
        {
            if (counter == Types.Cell.Empty)
            {
                throw new System.ArgumentOutOfRangeException(nameof(counter), "Cannot place an empty counter");
            }
            else if (column < 0 || column >= Width)
            {
                throw new System.ArgumentOutOfRangeException(nameof(column), "Cannot place a counter in a column that does not exist");
            }
            else if (this[column, Height - 1] != Cell.Empty)
            {
                throw new System.ArgumentOutOfRangeException(nameof(column), "Cannot place a counter in a full column");
            }

            for (var y = 0; y < Height; y++)
            {
                if (this[column, y] == Cell.Empty)
                {
                    this[column, y] = counter;
                    break;
                }
            }
        }
    }
}