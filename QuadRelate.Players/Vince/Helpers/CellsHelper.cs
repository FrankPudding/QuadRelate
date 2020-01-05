using System;
using System.Collections.Generic;
using System.Linq;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince.Helpers
{
    internal static class CellsHelper
    {
        public static IList<Cell> GetPlayableCells(Board board)
        {
            var cells = new List<Cell>();
            foreach (var column in board.AvailableColumns())
            {
                var cell = GetPlayableCell(board, column);
                cells.Add(cell);
            }

            return cells;
        }

        private static Cell GetPlayableCell(Board board, int column)
        {
            for (var row = 0; row < Board.Height; row++)
            {
                if (board[column, row] == Counter.Empty)
                    return new Cell(column, row);
            }

            throw new InvalidOperationException($"Column {column} is full.");
        }

        public static IList<Cell> GetHighestScoringCells(IEnumerable<Cell> cells)
        {
            var scores = new Dictionary<Cell, int>();
            foreach (var cell in cells)
            {
                scores[cell] = CellEvaluator.GetValue(cell);
            }

            var bestScore = scores.Values.Max();

            var highestScoringCells = new List<Cell>();
            foreach(var (key, value) in scores)
            {
                if (value == bestScore)
                    highestScoringCells.Add(key);
            }

            return highestScoringCells;
        }
    }
}
