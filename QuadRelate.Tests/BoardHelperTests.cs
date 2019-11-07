using Moq;
using QuadRelate.Models;
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
        public void IsGameOver_ForNewBoard_ReturnsFalse()
        {
            Assert.False(_board.IsGameOver());
        }

        [Fact]
        public void IsGameOver_ForEmptyBoard_ReturnsFalse()
        {
            _board.Fill(Cell.Empty);

            Assert.False(_board.IsGameOver());
        }

        [Theory]
        [InlineData(0, 0)] // Bottom row
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        [InlineData(0, 5)] // Top row
        [InlineData(1, 5)]
        [InlineData(2, 5)]
        [InlineData(3, 5)]
        public void IsGameOver_ForHorizontalWin_ReturnsTrue(int xStart, int y)
        {
            _board[xStart, y] = Cell.Red;
            _board[xStart + 1, y] = Cell.Red;
            _board[xStart + 2, y] = Cell.Red;
            _board[xStart + 3, y] = Cell.Red;

            Assert.True(_board.IsGameOver());
        }

        [Theory]
        [InlineData(0, 0)] // Left column
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(6, 0)] // Right column
        [InlineData(6, 1)]
        [InlineData(6, 2)]
        public void IsGameOver_ForVerticalWin_ReturnsTrue(int x, int yStart)
        {
            _board[x, yStart] = Cell.Red;
            _board[x, yStart + 1] = Cell.Red;
            _board[x, yStart + 2] = Cell.Red;
            _board[x, yStart + 3] = Cell.Red;

            Assert.True(_board.IsGameOver());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void IsGameOver_ForDiagonalNorthEastWin_ReturnsTrue(int xStart, int yStart)
        {
            _board[xStart, yStart] = Cell.Red;
            _board[xStart + 1, yStart + 1] = Cell.Red;
            _board[xStart + 2, yStart + 2] = Cell.Red;
            _board[xStart + 3, yStart + 3] = Cell.Red;

            Assert.True(_board.IsGameOver());
        }

        [Theory]
        [InlineData(6, 0)]
        [InlineData(5, 1)]
        [InlineData(4, 2)]
        public void IsGameOver_ForDiagonalNorthWestWin_ReturnsTrue(int xStart, int yStart)
        {
            _board[xStart, yStart] = Cell.Red;
            _board[xStart - 1, yStart + 1] = Cell.Red;
            _board[xStart - 2, yStart + 2] = Cell.Red;
            _board[xStart - 3, yStart + 3] = Cell.Red;

            Assert.True(_board.IsGameOver());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        [InlineData(4, 0)]
        public void IsGameOver_ForHorizontalThree_ReturnsFalse(int xStart, int y)
        {
            _board[xStart, y] = Cell.Red;
            _board[xStart + 1, y] = Cell.Red;
            _board[xStart + 2, y] = Cell.Red;

            Assert.False(_board.IsGameOver());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(0, 3)]
        public void IsGameOver_ForVerticalThree_ReturnsFalse(int x, int yStart)
        {
            _board[x, yStart] = Cell.Red;
            _board[x, yStart + 1] = Cell.Red;
            _board[x, yStart + 2] = Cell.Red;
           
            Assert.False(_board.IsGameOver());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void IsGameOver_ForDiagonalNorthEastThree_ReturnsFalse(int xStart, int yStart)
        {
            _board[xStart, yStart] = Cell.Red;
            _board[xStart + 1, yStart + 1] = Cell.Red;
            _board[xStart + 2, yStart + 2] = Cell.Red;

            Assert.False(_board.IsGameOver());
        }

        [Theory]
        [InlineData(6, 0)]
        [InlineData(5, 1)]
        [InlineData(4, 2)]
        [InlineData(3, 3)]
        public void IsGameOver_ForDiagonalNorthWestThree_ReturnsFalse(int xStart, int yStart)
        {
            _board[xStart, yStart] = Cell.Red;
            _board[xStart - 1, yStart + 1] = Cell.Red;
            _board[xStart - 2, yStart + 2] = Cell.Red;

            Assert.False(_board.IsGameOver());
        }
    }
}
