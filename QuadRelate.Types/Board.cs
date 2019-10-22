namespace QuadRelate.Types
{
    public class Board
    {
        public static readonly int Width = 7;
        public static readonly int Height = 6;

        public Cell[,] Position = new Cell[Width, Height];

        public Cell this[int x, int y]
        {
            get
            {
                return Position[x, y];
            }

            set => Position[x, y] = value;
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