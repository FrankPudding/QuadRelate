namespace QuadRelate.Types
{
    public class Board
    {
        public static readonly int Width = 7;
        public static readonly int Height = 6;

        private readonly Counter[,] _position = new Counter[Width, Height];

        public Counter this[int x, int y]
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

        public void PlaceCounter(int column, Counter counter)
        {
            if (counter == Counter.Empty)
            {
                throw new System.ArgumentOutOfRangeException(nameof(counter), "Cannot place an empty counter");
            }
            else if (column < 0 || column >= Width)
            {
                throw new System.ArgumentOutOfRangeException(nameof(column), "Cannot place a counter in a column that does not exist");
            }
            else if (this[column, Height - 1] != Counter.Empty)
            {
                throw new System.ArgumentOutOfRangeException(nameof(column), "Cannot place a counter in a full column");
            }

            for (var y = 0; y < Height; y++)
            {
                if (this[column, y] == Counter.Empty)
                {
                    this[column, y] = counter;
                    break;
                }
            }
        }
    }
}