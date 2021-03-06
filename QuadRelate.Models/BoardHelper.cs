﻿using System.Collections.Generic;
using QuadRelate.Types;

namespace QuadRelate.Models
{
    public static class BoardHelper
    {
        public static List<int> AvailableColumns(this Board board)
        {
            var availableColumns = new List<int>();

            for (var x = 0; x < Board.Width; x++)
            {
                if (board[x, Board.Height - 1] == Counter.Empty)
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

                if (board[3, y] == Counter.Empty)
                    continue;

                for (var x = 0; x < Board.Width; x++)
                {
                    if (board[x, y] == board[3, y])
                        inARowCount++;
                    else
                        inARowCount = 0;

                    if (inARowCount == 4)
                        return true;
                }
            }

            return false;
        }

        private static bool DoesVerticalWinExist(this Board board)
        {
            for (var x = 0; x < Board.Width; x++)
            {
                var inARowCount = 0;

                if (board[x, 2] != board[x, 3] || board[x, 2] == Counter.Empty)
                    continue;

                for (var y = 0; y < Board.Height; y++)
                {
                    if (board[x, y] == board[x, 2])
                        inARowCount++;
                    else
                        inARowCount = 0;

                    if (inARowCount == 4)
                        return true;
                }
            }

            return false;
        }

        private static bool DoesDiagonalWinExist(this Board board)
        {
            // Check North-East
            for (var x = 0; x < Board.Width - 3; x++)
            {
                for (var y = 0; y < Board.Height - 3; y++)
                {
                    if (board[x, y] != Counter.Empty && board[x + 1, y + 1] == board[x, y] && board[x + 2, y + 2] == board[x, y] && board[x + 3, y + 3] == board[x, y])
                        return true;
                }
            }

            // Check North-West
            for (var x = 0; x < Board.Width - 3; x++)
            {
                for (var y = 3; y < Board.Height; y++)
                {
                    if (board[x, y] != Counter.Empty && board[x + 1, y - 1] == board[x, y] && board[x + 2, y - 2] == board[x, y] && board[x + 3, y - 3] == board[x, y])
                        return true;
                }
            }

            return false;
        }

        public static bool IsGameOver(this Board board)
        {
            return board.AvailableColumns().Count == 0 || board.DoesWinnerExist();
        }

        public static bool DoesWinnerExist(this Board board)
        {
            return board.DoesHorizontalWinExist() || board.DoesVerticalWinExist() || board.DoesDiagonalWinExist();
        }

        public static void Fill(this Board board, Counter colour)
        {
            for (var x = 0; x < Board.Width; x++)
            {
                for (var y = 0; y < Board.Height; y++)
                {
                    board[x, y] = colour;
                }
            }
        }

        public static Counter ReverseCounter(this Counter colour)
        {
            if (colour == Counter.Yellow)
                return Counter.Red;

            if (colour == Counter.Red)
                return Counter.Yellow;

            throw new System.ArgumentOutOfRangeException(nameof(colour), "Can't reverse an empty counter.");
        }
    }
}
