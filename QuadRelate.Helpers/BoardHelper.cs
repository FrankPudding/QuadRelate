using QuadRelate.Types;
using System.Collections.Generic;

namespace QuadRelate.Helpers
{
    public static class BoardHelper
    {
        public static List<int> AvailableColumns(this Board board)
        {
            var availableColumns = new List<int>();

            for (var x = 0; x < Board.Width; x++)
            {
                if (board[x, Board.Height - 1] == Cell.Empty)
                {
                    availableColumns.Add(x);
                }
            }

            return availableColumns;
        }

        public static void Fill(this Board board, Cell colour)
        {
            for (var x = 0; x < Board.Width; x++)
            {
                for (var y = 0; y < Board.Height; y++)
                {
                    board[x, y] = colour;
                }
            }
        }
    }
}
