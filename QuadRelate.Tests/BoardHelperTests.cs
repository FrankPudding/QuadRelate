using QuadRelate.Types;
using System.Collections.Generic;
using QuadRelate.Models;
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
            _board.Fill(Counter.Red);

            Assert.Equal(Counter.Red, _board[0, 0]);
            Assert.Equal(Counter.Red, _board[Board.Width - 1, Board.Height - 1]);
        }

        [Fact]
        public void FillBoard_ForFullBoard_RefillsBoard()
        {
            _board.Fill(Counter.Red);
            _board.Fill(Counter.Yellow);

            Assert.Equal(Counter.Yellow, _board[0, 0]);
            Assert.Equal(Counter.Yellow, _board[Board.Width - 1, Board.Height - 1]);
        }

        [Fact]
        public void AvailableColumns_ForFullBoard_ReturnsEmptyList()
        {
            // Fill board in with red counters
            _board.Fill(Counter.Red);

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
            _board.Fill(Counter.Red);
            
            // Remove top row of counters
            for (var x = 0; x < Board.Width; x++)
            {
                _board[x, Board.Height - 1] = Counter.Empty;
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
            _board.Fill(Counter.Red);

            // Remove top right counter
            _board[Board.Width - 1, Board.Height - 1] = Counter.Empty;

            Assert.Equal(expected, _board.AvailableColumns());
        }

        [Fact]
        public void DoesWinnerExist_ForNewBoard_ReturnsFalse()
        {
            Assert.False(_board.DoesWinnerExist());
        }

        [Fact]
        public void DoesWinnerExist_ForEmptyBoard_ReturnsFalse()
        {
            _board.Fill(Counter.Empty);

            Assert.False(_board.DoesWinnerExist());
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
        public void DoesWinnerExist_ForHorizontalWin_ReturnsTrue(int xStart, int y)
        {
            _board[xStart, y] = Counter.Red;
            _board[xStart + 1, y] = Counter.Red;
            _board[xStart + 2, y] = Counter.Red;
            _board[xStart + 3, y] = Counter.Red;

            Assert.True(_board.DoesWinnerExist());
        }

        [Theory]
        [InlineData(0, 0)] // Left column
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(6, 0)] // Right column
        [InlineData(6, 1)]
        [InlineData(6, 2)]
        public void DoesWinnerExist_ForVerticalWin_ReturnsTrue(int x, int yStart)
        {
            _board[x, yStart] = Counter.Red;
            _board[x, yStart + 1] = Counter.Red;
            _board[x, yStart + 2] = Counter.Red;
            _board[x, yStart + 3] = Counter.Red;

            Assert.True(_board.DoesWinnerExist());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void DoesWinnerExist_ForDiagonalNorthEastWin_ReturnsTrue(int xStart, int yStart)
        {
            _board[xStart, yStart] = Counter.Red;
            _board[xStart + 1, yStart + 1] = Counter.Red;
            _board[xStart + 2, yStart + 2] = Counter.Red;
            _board[xStart + 3, yStart + 3] = Counter.Red;

            Assert.True(_board.DoesWinnerExist());
        }

        [Theory]
        [InlineData(6, 0)]
        [InlineData(5, 1)]
        [InlineData(4, 2)]
        public void DoesWinnerExist_ForDiagonalNorthWestWin_ReturnsTrue(int xStart, int yStart)
        {
            _board[xStart, yStart] = Counter.Red;
            _board[xStart - 1, yStart + 1] = Counter.Red;
            _board[xStart - 2, yStart + 2] = Counter.Red;
            _board[xStart - 3, yStart + 3] = Counter.Red;

            Assert.True(_board.DoesWinnerExist());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        [InlineData(4, 0)]
        public void DoesWinnerExist_ForHorizontalThree_ReturnsFalse(int xStart, int y)
        {
            _board[xStart, y] = Counter.Red;
            _board[xStart + 1, y] = Counter.Red;
            _board[xStart + 2, y] = Counter.Red;

            Assert.False(_board.DoesWinnerExist());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(0, 3)]
        public void DoesWinnerExist_ForVerticalThree_ReturnsFalse(int x, int yStart)
        {
            _board[x, yStart] = Counter.Red;
            _board[x, yStart + 1] = Counter.Red;
            _board[x, yStart + 2] = Counter.Red;
           
            Assert.False(_board.DoesWinnerExist());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void DoesWinnerExist_ForDiagonalNorthEastThree_ReturnsFalse(int xStart, int yStart)
        {
            _board[xStart, yStart] = Counter.Red;
            _board[xStart + 1, yStart + 1] = Counter.Red;
            _board[xStart + 2, yStart + 2] = Counter.Red;

            Assert.False(_board.DoesWinnerExist());
        }

        [Theory]
        [InlineData(6, 0)]
        [InlineData(5, 1)]
        [InlineData(4, 2)]
        [InlineData(3, 3)]
        public void DoesWinnerExist_ForDiagonalNorthWestThree_ReturnsFalse(int xStart, int yStart)
        {
            _board[xStart, yStart] = Counter.Red;
            _board[xStart - 1, yStart + 1] = Counter.Red;
            _board[xStart - 2, yStart + 2] = Counter.Red;

            Assert.False(_board.DoesWinnerExist());
        }

        [Fact]
        public void DoesWinnerExist_ForDraw_ReturnsFalse()
        {
            _board.Fill(Counter.Red);

            _board[3, 0] = Counter.Yellow;
            _board[3, 1] = Counter.Yellow;
            _board[3, 2] = Counter.Yellow;
            _board[3, 4] = Counter.Yellow;
            _board[3, 5] = Counter.Yellow;
            _board[2, 2] = Counter.Yellow;
            _board[2, 3] = Counter.Yellow;
            _board[2, 4] = Counter.Yellow;
            _board[4, 2] = Counter.Yellow;
            _board[4, 3] = Counter.Yellow;
            _board[4, 4] = Counter.Yellow;
            _board[0, 3] = Counter.Yellow;
            _board[1, 3] = Counter.Yellow;
            _board[5, 3] = Counter.Yellow;
            _board[6, 3] = Counter.Yellow;

            Assert.False(_board.DoesWinnerExist());
            Assert.True(_board.IsGameOver());
        }

        [Fact]
        public void IsGameOver_ForDraw_ReturnsTrue()
        {
            _board.Fill(Counter.Red);

            _board[3, 0] = Counter.Yellow;
            _board[3, 1] = Counter.Yellow;
            _board[3, 2] = Counter.Yellow;
            _board[3, 4] = Counter.Yellow;
            _board[3, 5] = Counter.Yellow;
            _board[2, 2] = Counter.Yellow;
            _board[2, 3] = Counter.Yellow;
            _board[2, 4] = Counter.Yellow;
            _board[4, 2] = Counter.Yellow;
            _board[4, 3] = Counter.Yellow;
            _board[4, 4] = Counter.Yellow;
            _board[0, 3] = Counter.Yellow;
            _board[1, 3] = Counter.Yellow;
            _board[5, 3] = Counter.Yellow;
            _board[6, 3] = Counter.Yellow;

            Assert.True(_board.IsGameOver());
            Assert.False(_board.DoesWinnerExist());

        }

        [Fact]
        public void CloneBoard_ForEmptyBoard_ReturnsEmptyBoard()
        {
            Assert.Equal(_board[0, 0], _board.Clone()[0, 0]);
            Assert.Equal(_board[0, Board.Height - 1], _board.Clone()[0, Board.Height - 1]);
            Assert.Equal(_board[Board.Width - 1, 0], _board.Clone()[Board.Width - 1, 0]);
            Assert.Equal(_board[Board.Width - 1, Board.Height - 1], _board.Clone()[Board.Width - 1, Board.Height - 1]);
        }

        [Fact]
        public void CloneBoard_ForFullBoard_ReturnsSameBoard()
        {
            _board.Fill(Counter.Red);

            Assert.Equal(_board[0, 0], _board.Clone()[0, 0]);
            Assert.Equal(_board[0, Board.Height - 1], _board.Clone()[0, Board.Height - 1]);
            Assert.Equal(_board[Board.Width - 1, 0], _board.Clone()[Board.Width - 1, 0]);
            Assert.Equal(_board[Board.Width - 1, Board.Height - 1], _board.Clone()[Board.Width - 1, Board.Height - 1]);
        }
    }
}
