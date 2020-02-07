using System.Collections.Generic;
using QuadRelate.Models;
using QuadRelate.Types;
using Xunit;

namespace QuadRelate.Tests
{
    public class BoardHasherTests
    {
        private readonly BoardHasher _boardHasher;

        public BoardHasherTests()
        {
            _boardHasher = new BoardHasher();
        }

        [Theory]
        [InlineData(Counter.Empty, 0)]
        [InlineData(Counter.Yellow, 1)]
        [InlineData(Counter.Red, 2)]
        public void GetCellHash_WhenExecuted_ReturnsCorrectResult(Counter counter, int expected)
        {
            var actual = _boardHasher.GetCellHash(counter);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 3)]
        [InlineData(2, 9)]
        [InlineData(3, 27)]
        [InlineData(4, 81)]
        [InlineData(5, 243)]
        public void GetColumnHash_WhenColumnContainsOneYellowValue_ReturnsCorrectResult(int row, int expected)
        {
            var board = new Board {[0, row] = Counter.Yellow};

            var actual = _boardHasher.GetColumnHash(board, 0);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 6)]
        [InlineData(2, 18)]
        [InlineData(3, 54)]
        [InlineData(4, 162)]
        [InlineData(5, 486)]
        public void GetColumnHash_WhenColumnContainsOneRedValue_ReturnsCorrectResult(int row, int expected)
        {
            var board = new Board {[0, row] = Counter.Red};

            var actual = _boardHasher.GetColumnHash(board, 0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetColumnHash_WhenColumnContainsAllYellowValues_ReturnsCorrectResult()
        {
            var board = new Board();
            for (var row = 0; row < Board.Height; row++)
            {
                board[0, row] = Counter.Yellow;
            }

            var actual = _boardHasher.GetColumnHash(board, 0);

            Assert.Equal(364, actual);
        }      
        
        [Fact]
        public void GetColumnHash_WhenColumnContainsAllRedValues_ReturnsCorrectResult()
        {
            var board = new Board();
            for (var row = 0; row < Board.Height; row++)
            {
                board[0, row] = Counter.Red;
            }

            var actual = _boardHasher.GetColumnHash(board, 0);

            Assert.Equal(728, actual);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 3)]
        [InlineData(2, 9)]
        [InlineData(3, 27)]
        [InlineData(4, 81)]
        [InlineData(5, 243)]
        public void GetGetBoardHash_WhenRowsContainAllYellowValues_ReturnsCorrectResult(int row, int expected)
        {
            var board = new Board();
            for (var column = 0; column < Board.Width; column++)
            {
                board[column, row] = Counter.Yellow;
            }

            var actual = _boardHasher.GetBoardHash(board);

            Assert.Equal(new []{expected, expected, expected, expected, expected, expected, expected}, actual);
        } 
        
        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 6)]
        [InlineData(2, 18)]
        [InlineData(3, 54)]
        [InlineData(4, 162)]
        [InlineData(5, 486)]
        public void GetGetBoardHash_WhenRowsContainAllRedValues_ReturnsCorrectResult(int row, int expected)
        {
            var board = new Board();
            for (var column = 0; column < Board.Width; column++)
            {
                board[column, row] = Counter.Red;
            }

            var actual = _boardHasher.GetBoardHash(board);

            Assert.Equal(new []{expected, expected, expected, expected, expected, expected, expected}, actual);
        } 

        [Theory]
        [InlineData(1)]  //YRY
        [InlineData(7)]
        [InlineData(16)]
        [InlineData(2)]  //RYR
        [InlineData(5)]
        [InlineData(23)]
        public void IsSubset_WhenHashesAreEqual_ReturnsTrue(int hash)
        {
            var actual = _boardHasher.IsSubset(hash, hash);

            Assert.True(actual);
        }

        [Theory]
        [InlineData(1, 7)]  //YRY
        [InlineData(1, 16)]
        [InlineData(7, 16)]
        [InlineData(2, 5)]  //RYR
        [InlineData(2, 23)]
        [InlineData(5, 23)]
        public void IsSubset_WhenHashesMatchAndFullHashIsLarger_ReturnsTrue(int subset, int full)
        {
            var actual = _boardHasher.IsSubset(subset, full);

            Assert.True(actual);
        } 

        [Theory]
        [InlineData(7, 1)]  //YRY
        [InlineData(16, 1)]
        [InlineData(16, 7)]
        [InlineData(5, 2)]  //RYR
        [InlineData(23, 2)]
        [InlineData(23, 5)]
        public void IsSubset_WhenHashesMatchButSubsetIsLarger_ReturnsFalse(int subset, int full)
        {
            var actual = _boardHasher.IsSubset(subset, full);

            Assert.False(actual);
        } 

        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 5)]
        [InlineData(7, 23)]
        [InlineData(16, 23)]
        public void IsSubset_WhenHashesDoNotMatch_ReturnsFalse(int subset, int full)
        {
            var actual = _boardHasher.IsSubset(subset, full);

            Assert.False(actual);
        } 

        [Theory]
        [InlineData(new[]{Counter.Yellow, Counter.Red, Counter.Yellow})]
        public void IsSubset_WhenBoardHashesAreEqual_ReturnsTrue(Counter[] counters)
        {
            var subsetBoard = CreateBoardWithFilledColumns(counters);
            var fullBoard = CreateBoardWithFilledColumns(counters);
            
            var actual = _boardHasher.IsSubset(subsetBoard, fullBoard);

            Assert.True(actual);
        }

        [Theory]
        [InlineData(new[]{Counter.Yellow}, new[]{Counter.Yellow, Counter.Red})]
        [InlineData(new[]{Counter.Yellow}, new[]{Counter.Yellow, Counter.Red, Counter.Yellow})]
        [InlineData(new[]{Counter.Yellow, Counter.Red}, new[]{Counter.Yellow, Counter.Red, Counter.Yellow})]
        public void IsSubset_WhenBoardIsSubset_ReturnsTrue(Counter[] subsetCounters, Counter[] fullCounters)
        {
            var subsetBoard = CreateBoardWithFilledColumns(subsetCounters);
            var fullBoard = CreateBoardWithFilledColumns(fullCounters);
            
            var actual = _boardHasher.IsSubset(subsetBoard, fullBoard);

            Assert.True(actual);
        }

        [Theory]
        [InlineData(new[]{Counter.Yellow, Counter.Red}, new[]{Counter.Yellow})]
        [InlineData(new[]{Counter.Yellow, Counter.Red, Counter.Yellow}, new[]{Counter.Yellow})]
        [InlineData(new[]{Counter.Yellow, Counter.Red, Counter.Yellow}, new[]{Counter.Yellow, Counter.Red})]
        public void IsSubset_WhenBoardMatchesButIsNotSubset_ReturnsFalse(Counter[] subsetCounters, Counter[] fullCounters)
        {
            var subsetBoard = CreateBoardWithFilledColumns(subsetCounters);
            var fullBoard = CreateBoardWithFilledColumns(fullCounters);
            
            var actual = _boardHasher.IsSubset(subsetBoard, fullBoard);

            Assert.False(actual);
        }

        [Theory]
        [InlineData(new[]{Counter.Red}, new[]{Counter.Yellow})]
        [InlineData(new[]{Counter.Yellow}, new[]{Counter.Red})]
        [InlineData(new[]{Counter.Red, Counter.Yellow}, new[]{Counter.Red, Counter.Red})]
        [InlineData(new[]{Counter.Yellow, Counter.Red}, new[]{Counter.Yellow, Counter.Yellow})]

        public void IsSubset_WhenBoardDoesNotMatch_ReturnsFalse(Counter[] subsetCounters, Counter[] fullCounters)
        {
            var subsetBoard = CreateBoardWithFilledColumns(subsetCounters);
            var fullBoard = CreateBoardWithFilledColumns(fullCounters);

            var actual = _boardHasher.IsSubset(subsetBoard, fullBoard);

            Assert.False(actual);
        }

        private static Board CreateBoardWithFilledColumns(IReadOnlyList<Counter> counters)
        {
            var board = new Board();
            for (var column = 0; column < Board.Width; column++)
            {
                for (var row = 0; row < counters.Count; row++)
                {
                    board[column, row] = counters[row];
                }
            }

            return board;
        }
    }
}