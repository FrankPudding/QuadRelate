using QuadRelate.Types;
using System.Collections.Generic;

namespace QuadRelate.Helpers
{
    public class BoardHelper
    {
        public List<int> AvailableColumns(Board board)
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
    }
}
