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

        [Fact]
        public void IsGameOver_ForEmptyBoard_ReturnsFalse()
        {
            Assert.False(_board.IsGameOver());
        }

        [Fact]
        public void IsGameOver_ForHorizontalBottomLeft_ReturnsTrue()
        {
            // Place counters in the first four columns of the bottom row
            for (var x = 0; x < 4; x++)
            {
                _board[x, 0] = Cell.Red;
            }

            Assert.True(_board.IsGameOver());
        }

        [Fact]
        public void IsGameOver_ForHorizontalTopRight_ReturnsTrue()
        {
            // Place counters in the first four columns of the bottom row
            for (var x = 3; x <= 6; x++)
            {
                _board[x, 5] = Cell.Red;
            }

            Assert.True(_board.IsGameOver());
        }

        [Fact]
        public void IsGameOver_ForVerticalBottomLeft_ReturnsTrue()
        {
            // Place counters in the first four columns of the bottom row
            for (var y = 0; y < 4; y++)
            {
                _board[0, y] = Cell.Yellow;
            }

            Assert.True(_board.IsGameOver());
        }

        [Fact]
        public void IsGameOver_ForVerticaltopRight_ReturnsTrue()
        {
            // Place counters in the first four columns of the bottom row
            for (var y = 2; y <= 5; y++)
            {
                _board[0, y] = Cell.Yellow;
            }

            Assert.True(_board.IsGameOver());
        }
    }
}
