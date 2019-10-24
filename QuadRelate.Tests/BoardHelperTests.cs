using Moq;
using QuadRelate.Helpers;
using QuadRelate.Types;
using System.Collections.Generic;
using Xunit;

namespace QuadRelate.Tests
{
    public class BoardHelperTests
    {
        private readonly Board _board;

        public BoardHelperTests()
        {
            _board = new Board();
        }

        [Fact]
        public void FillBoard_ForNewBoard_FillsBoard()
        {
            _board.Fill(Cell.Red);

            Assert.Equal(Cell.Red, _board[0, 0]);
            Assert.Equal(Cell.Red, _board[Board.Width - 1, Board.Height - 1]);
        }

        [Fact]
        public void FillBoard_ForFullBoard_RefillsBoard()
        {
            _board.Fill(Cell.Red);
            _board.Fill(Cell.Yellow);

            Assert.Equal(Cell.Yellow, _board[0, 0]);
            Assert.Equal(Cell.Yellow, _board[Board.Width - 1, Board.Height - 1]);
        }

        [Fact]
        public void AvailableColumns_ForFullBoard_ReturnsEmptyList()
        {
            // Fill board in with red counters
            _board.Fill(Cell.Red);

            Assert.Equal(new List<int>(), _board.AvailableColumns());
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
            _board.Fill(Cell.Red);
            
            // Remove top row of counters
            for (var x = 0; x < Board.Width; x++)
            {
                _board[x, Board.Height - 1] = Cell.Empty;
            }

            Assert.Equal(expected, _board.AvailableColumns());
        }

        [Fact]
        public void AvailableColumns_ForPartlyFullBoard_ReturnsFullList()
        {
            // Expected list of final column only
            var expected = new List<int>
            {
                Board.Width - 1
            };

            // Fill board in with red counters
            _board.Fill(Cell.Red);

            // Remove top right counter
            _board[Board.Width - 1, Board.Height - 1] = Cell.Empty;

            Assert.Equal(expected, _board.AvailableColumns());
        }
    }
}
