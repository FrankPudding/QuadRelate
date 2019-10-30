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

        private static bool DoesHorizontalWinExist(this Board board)
        {
            for (var y = 0; y < Board.Height; y++)
            {
                var inARowCount = 0;

                if (board[3, y] == Cell.Empty)
                    continue;

                inARowCount++;

                // Check left
                if (board[2, y] == board[3, y])
                {
                    inARowCount++;

                    if (board[1, y] == board[3, y])
                    {
                        inARowCount++;

                        if (board[0, y] == board[3, y])
                        {
                            inARowCount++;
                        }
                    }
                }

                // Check right
                if (board[4, y] == board[3, y])
                {
                    inARowCount++;

                    if (board[5, y] == board[3, y])
                    {
                        inARowCount++;

                        if (board[6, y] == board[3, y])
                        {
                            inARowCount++;
                        }
                    }
                }

                if (inARowCount >= 4)
                    return true;
            }

            return false;
        }

        private static bool DoesVerticalWinExist(this Board board)
        {
            for (var x = 0; x < Board.Width; x++)
            {
                var inARowCount = 0;

                if (board[x, 2] == Cell.Empty)
                    continue;

                inARowCount++;

                if (board[x, 2] != board[x, 3])
                    continue;

                inARowCount++;

                // Check down
                if (board[x, 1] == board[x, 2])
                {
                    inARowCount++;

                    if (board[x, 0] == board[x, 2])
                        inARowCount++;
                }

                // Check up
                if (board[x, 4] == board[x, 3])
                {
                    inARowCount++;

                    if (board[x, 5] == board[x, 3])
                        inARowCount++;
                }

                if (inARowCount >= 4)
                    return true;
            }

            return false;
        }

        private static bool DoesDiagonalWinExist(this Board board)
        {
            for (var y = 0; y < Board.Height; y++)
            {
                var inARowCount = 0;

                if (board[3, y] == Cell.Empty)
                    continue;

                inARowCount++;

                // Check North East
                if (y < Board.Height - 1)
                {
                    if (board[4, y + 1] == board[3, y])
                    {
                        inARowCount++;

                        if (y < Board.Height - 2)
                        {
                            if (board[5, y + 2] == board[3, y])
                            {
                                inARowCount++;

                                if (y < Board.Height - 3)
                                {
                                    if (board[6, y + 3] == board[3, y])
                                        inARowCount++;
                                }
                            }
                        }
                    }
                }

                // Check South West
                if (y > 0)
                {
                    if (board[2, y - 1] == board[3, y])
                    {
                        inARowCount++;

                        if (y > 1)
                        {
                            if (board[1, y - 2] == board[3, y])
                            {
                                inARowCount++;

                                if (y > 2)
                                {
                                    if (board[0, y - 3] == board[3, y])
                                        inARowCount++;
                                }
                            }
                        }
                    }
                }

                if (inARowCount >= 4)
                    return true;

                inARowCount = 1;

                // Check North West diagonals
                if (y < Board.Height - 1)
                {
                    if (board[2, y + 1] == board[3, y])
                    {
                        inARowCount++;

                        if (y < Board.Height - 2)
                        {
                            if (board[1, y + 2] == board[3, y])
                            {
                                inARowCount++;

                                if (y < Board.Height - 3)
                                {
                                    if (board[0, y + 3] == board[3, y])
                                        inARowCount++;
                                }
                            }
                        }
                    }
                }

                // Check South-East
                if (y > 0)
                {
                    if (board[4, y - 1] == board[3, y])
                    {
                        inARowCount++;

                        if (y > 1)
                        {
                            if (board[5, y - 2] == board[3, y])
                            {
                                inARowCount++;

                                if (y > 2)
                                {
                                    if (board[6, y - 3] == board[3, y])
                                        inARowCount++;
                                }
                            }
                        }
                    }
                }

                if (inARowCount >= 4)
                    return true;
            }

            return false;
        }

        public static bool IsGameOver(this Board board)
        {
            return board.DoesHorizontalWinExist() || board.DoesVerticalWinExist() || board.DoesDiagonalWinExist();
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
