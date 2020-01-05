using System.Collections.Generic;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince.Helpers
{
    internal static class LineFinder
    {
        private const int _minimumLineSize = 3;
        private static Board _board;
        private static IList<IList<Counter>> _lines;

        public static IEnumerable<IList<Counter>> FindAllLines(Board board)
        {
            _board = board;
            _lines = new List<IList<Counter>>();
            AddHorizontalLines();
            AddVerticalLines();
            AddDiagonalLines();

            return _lines;
        }

        private static void AddHorizontalLines()
        {
            for (var y = 0; y < Board.Height; y++)
            {
                var line = new List<Counter>();
                for (var x = 0; x < Board.Width; x++)
                {
                    line.Add(_board[x, y]);
                }

                if (line.Count >= _minimumLineSize)
                    _lines.Add(line);
            }
        }

        private static void AddVerticalLines()
        {
            for (var x = 0; x < Board.Width; x++)
            {
                var line = new List<Counter>();
                for (var y = 0; y < Board.Height; y++)
                {
                    line.Add(_board[x, y]);
                }

                if (line.Count >= _minimumLineSize)
                    _lines.Add(line);
            }
        }

        private static void AddDiagonalLines()
        {
            // Bottom row.
            for (var x = 0; x < Board.Width; x++)
            {
                AddBottomLeftToTopRightLine(x, 0);
                AddBottomRightToTopLeftLine(x, 0);
            }

            // Left/right hand columns.
            for (var y = 1; y < Board.Height-1; y++)
            {
                AddBottomLeftToTopRightLine(0, y);
                AddBottomRightToTopLeftLine(Board.Width-1, y);
            }
        }

        private static void AddBottomLeftToTopRightLine(int startX, int startY)
        {
            var x = startX;
            var y = startY;

            var line = new List<Counter>();
            while (IsValidCell(x, y))
            {
                line.Add(_board[x, y]);
                x++;
                y++;
            }

            if (line.Count >= _minimumLineSize)
                _lines.Add(line);
        }

        private static void AddBottomRightToTopLeftLine(int startX, int startY)
        {
            var x = startX;
            var y = startY;

            var line = new List<Counter>();
            while (IsValidCell(x, y))
            {
                line.Add(_board[x, y]);
                x--;
                y++;
            }

            if (line.Count >= _minimumLineSize)
                _lines.Add(line);
        }

        private static bool IsValidCell(int x, int y)
        {
            return 
                (x >= 0 && y >= 0) &&
                (x < Board.Width && y < Board.Height);
        }
    }
}
