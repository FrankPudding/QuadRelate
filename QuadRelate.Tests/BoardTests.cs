using QuadRelate.Types;
using System;
using Xunit;

namespace QuadRelate.Tests
{
    public class BoardTests
    {
        private readonly Board _board;

        public BoardTests()
        {
            _board = new Board();
        }

        [Fact]
        public void PlaceCounter_ForEmptyCounter_ThrowsArgumentOutOfRangeException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _board.PlaceCounter(0, Cell.Empty));

            Assert.Equal("counter", exception.ParamName);
            Assert.Equal("Cannot place an empty counter\r\nParameter name: counter", exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(7)]
        public void PlaceCounter_ForOutOfBoundsColumn_ThrowsArgumentException(int column)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _board.PlaceCounter(column, Cell.Red));

            Assert.Equal("column", exception.ParamName);
            Assert.Equal("Cannot place a counter in a column that does not exist\r\nParameter name: column", exception.Message);
        }

        [Fact]
        public void PlaceCounter_ForFullColumn_ThrowsArgumentException()
        {
            for (var y = 0; y < Board.Height; y++)
            {
                _board[0, y] = Cell.Red;
            }

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _board.PlaceCounter(0, Cell.Red));

            Assert.Equal("column", exception.ParamName);
            Assert.Equal("Cannot place a counter in a full column\r\nParameter name: column", exception.Message);
        }

        [Fact]
        public void PlaceCounter_ForEmptyRow_PlacesCounterInBottomRow()
        {
            var boardExpected = new Board {[0, 0] = Cell.Red};

            _board.PlaceCounter(0, Cell.Red);

            for (var x = 0; x < Board.Width; x++)
            {
                for (var y = 0; y < Board.Height; y++)
                {
                    Assert.Equal(boardExpected[x,y], _board[x,y]);
                }
            }           
        }

        [Fact]
        public void PlaceCounter_ForHalfFullRow_PlacesCounterInBottomRow()
        {
            var boardExpected = new Board();
            for (var y = 0; y < 3; y++)
            {
                _board[0, y] = Cell.Red;
                boardExpected[0, y] = Cell.Red;
            }

            boardExpected[0, 3] = Cell.Red;

            _board.PlaceCounter(0, Cell.Red);

            for (var x = 0; x < Board.Width; x++)
            {
                for (var y = 0; y < Board.Height; y++)
                {
                    Assert.Equal(boardExpected[x, y], _board[x, y]);
                }
            }
        }
    }
}