using Moq;
using QuadRelate.Helpers;
using QuadRelate.Types;
using System.Collections.Generic;
using Xunit;

namespace QuadRelate.Tests
{
    public class BoardHelperTests
    {
        private readonly BoardHelper _boardHelper;
        private readonly Board _board;

        public BoardHelperTests()
        {
            _boardHelper = new BoardHelper();
            _board = new Board();
        }

        [Fact]
        public void AvailableColumns_ForFullBoard_ReturnsEmptyList()
        {
            // Fill board in with red counters
            for (var x = 0; x < Board.Width; x++)
            {
                for (var y = 0; y < Board.Height; y++)
                {
                    _board[x, y] = Cell.Red;
                }
            }

            var actual = _boardHelper.AvailableColumns(_board);

            Assert.Equal(new List<int>(), actual);
        }

        [Fact]
        public void AvailableColumns_ForIncompleteBoard_ReturnsFullList()
        {
            var expected = new List<int>();

            // Expected list of all columns
            for (var i = 0; i < Board.Width; i++)
            {
                expected.Add(i);
            }

            // Fill board in with red counters
            for (var x = 0; x < Board.Width; x++)
            {
                for (var y = 0; y < Board.Height - 1; y++)
                {
                    _board[x, y] = Cell.Red;
                }
            }

            var actual = _boardHelper.AvailableColumns(_board);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AvailableColumns_ForPartlyFullBoard_ReturnsFullList()
        {
            var expected = new List<int>();

            // Expected list of final column only
            expected.Add(Board.Width - 1);

            // Fill board in with red counters
            for (var x = 0; x < Board.Width - 1; x++)
            {
                for (var y = 0; y < Board.Height; y++)
                {
                    _board[x, y] = Cell.Red;
                }
            }

            var actual = _boardHelper.AvailableColumns(_board);

            Assert.Equal(expected, actual);
        }
    }
}
