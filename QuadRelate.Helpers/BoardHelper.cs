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

        public static bool IsGameOver(this Board board)
        {
            // Check for horizontal wins
            for (var y = 0; y < Board.Height; y++)
            {
                // Check left
                if (board[3, y] == board[2, y] && board[2, y] == board[1, y] && board[1, y] == board[0, y])
                {
                    return true;
                }

                // Check right
                if (board[3, y] == board[4, y] && board[4, y] == board[5, y] && board[5, y] == board[6, y])
                {
                    return true;
                }
            }

            // Check for vertical wins
            for (var x = 0; x < Board.Width; x++)
            {
                if (board[x, 2] == board[x, 3])
                {
                    // Check down
                    if (board[x, 2] == board[x, 1] && board[x, 1] == board[x, 0])
                    {
                        return true;
                    }

                    // Check up
                    if (board[x, 3] == board[x, 4] && board[x, 4] == board[x, 5])
                    {
                        return true;
                    }
                }
            }

            // Check bottom half diagonals
            for (var y = 0; y < Board.Height / 2; y++)
            {
                // Check left-up
                if (board[3, y] == board[2, y + 1] && board[2, y + 1] == board[1, y + 2] && board[1, y + 2] == board[0, y + 3])
                {
                    return true;
                }

                // Check right-up
                if (board[3, y] == board[4, y + 1] && board[4, y + 1] == board[5, y + 2] && board[5, y + 2] == board[6, y + 3])
                {
                    return true;
                }
            }

            // Check top half diagonals
            for (var y = Board.Height; y >= Board.Height / 2; y--)
            {
                // Check left-down
                if (board[3, y] == board[2, y - 1] && board[2, y - 1] == board[1, y - 2] && board[1, y - 2] == board[0, y - 3])
                {
                    return true;
                }

                // Check right- down
                if (board[3, y] == board[4, y - 1] && board[4, y - 1] == board[5, y - 2] && board[5, y - 2] == board[6, y - 3])
                {
                    return true;
                }
            }

            return false;
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
