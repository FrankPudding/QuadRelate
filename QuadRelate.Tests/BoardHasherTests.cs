using System.Collections.Generic;
using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;
using Xunit;

namespace QuadRelate.Tests
{
    public class BoardHasherTests
    {
        private readonly IBoardHasher _boardHasher;

        public BoardHasherTests()
        {
            _boardHasher = new BoardHasher();
        }

        [Theory]
        [InlineData(Counter.Empty, ' ')]
        [InlineData(Counter.Yellow, 'Y')]
        [InlineData(Counter.Red, 'R')]
        public void GetCellHash_WhenExecuted_ReturnsCorrectResult(Counter counter, char expected)
        {
            var actual = _boardHasher.GetHash(counter);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "YYYYYYY                                   ")]
        [InlineData(1, "       YYYYYYY                            ")]
        [InlineData(2, "              YYYYYYY                     ")]
        [InlineData(3, "                     YYYYYYY              ")]
        [InlineData(4, "                            YYYYYYY       ")]
        [InlineData(5, "                                   YYYYYYY")]
        public void GetGetBoardHash_WhenRowsContainAllYellowValues_ReturnsCorrectResult(int row, string expected)
        {
            var board = new Board();
            for (var column = 0; column < Board.Width; column++)
            {
                board[column, row] = Counter.Yellow;
            }

            var actual = _boardHasher.GetHash(board);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "RRRRRRR                                   ")]
        [InlineData(1, "       RRRRRRR                            ")]
        [InlineData(2, "              RRRRRRR                     ")]
        [InlineData(3, "                     RRRRRRR              ")]
        [InlineData(4, "                            RRRRRRR       ")]
        [InlineData(5, "                                   RRRRRRR")]
        public void GetGetBoardHash_WhenRowsContainAllRedValues_ReturnsCorrectResult(int row, string expected)
        {
            var board = new Board();
            for (var column = 0; column < Board.Width; column++)
            {
                board[column, row] = Counter.Red;
            }

            var actual = _boardHasher.GetHash(board);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(" YRY ")]
        [InlineData(" RYR ")]
        public void IsSubset_WhenHashesAreEqual_ReturnsTrue(string hash)
        {
            var actual = _boardHasher.IsSubset(hash, hash);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("YRY ", "YRYY")]
        [InlineData("YRY ", "YRYR")]
        [InlineData("RYR ", "RYRY")]
        [InlineData("RYR ", "RYRR")]
        public void IsSubset_WhenHashesMatchAndFullHashIsLarger_ReturnsTrue(string hash1, string hash2)
        {
            var actual = _boardHasher.IsSubset(hash1, hash2);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("YRY ", "YRYY")]
        [InlineData("YRY ", "YRYR")]
        [InlineData("RYR ", "RYRY")]
        [InlineData("RYR ", "RYRR")]
        public void IsSubset_WhenHashesMatchButSubsetIsLarger_ReturnsFalse(string hash1, string hash2)
        {
            var actual = _boardHasher.IsSubset(hash2, hash1);   // Swapped.

            Assert.False(actual);
        }

        [Theory]
        [InlineData("YRY", "RYR")]
        [InlineData("RYR", "YRY")]
        public void IsSubset_WhenHashesDoNotMatch_ReturnsFalse(string hash1, string hash2)
        {
            var actual = _boardHasher.IsSubset(hash1, hash2);

            Assert.False(actual);
        }

        [Theory]
        [InlineData(new[] { Counter.Yellow, Counter.Red, Counter.Yellow })]
        public void IsSubset_WhenBoardHashesAreEqual_ReturnsTrue(Counter[] counters)
        {
            var subsetBoard = CreateBoardWithFilledColumns(counters);
            var fullBoard = CreateBoardWithFilledColumns(counters);

            var actual = _boardHasher.IsSubset(subsetBoard, fullBoard);

            Assert.True(actual);
        }

        [Theory]
        [InlineData(new[] { Counter.Yellow }, new[] { Counter.Yellow, Counter.Red })]
        [InlineData(new[] { Counter.Yellow }, new[] { Counter.Yellow, Counter.Red, Counter.Yellow })]
        [InlineData(new[] { Counter.Yellow, Counter.Red }, new[] { Counter.Yellow, Counter.Red, Counter.Yellow })]
        public void IsSubset_WhenBoardIsSubset_ReturnsTrue(Counter[] subsetCounters, Counter[] fullCounters)
        {
            var subsetBoard = CreateBoardWithFilledColumns(subsetCounters);
            var fullBoard = CreateBoardWithFilledColumns(fullCounters);

            var actual = _boardHasher.IsSubset(subsetBoard, fullBoard);

            Assert.True(actual);
        }

        [Theory]
        [InlineData(new[] { Counter.Yellow, Counter.Red }, new[] { Counter.Yellow })]
        [InlineData(new[] { Counter.Yellow, Counter.Red, Counter.Yellow }, new[] { Counter.Yellow })]
        [InlineData(new[] { Counter.Yellow, Counter.Red, Counter.Yellow }, new[] { Counter.Yellow, Counter.Red })]
        public void IsSubset_WhenBoardMatchesButIsNotSubset_ReturnsFalse(Counter[] subsetCounters, Counter[] fullCounters)
        {
            var subsetBoard = CreateBoardWithFilledColumns(subsetCounters);
            var fullBoard = CreateBoardWithFilledColumns(fullCounters);

            var actual = _boardHasher.IsSubset(subsetBoard, fullBoard);

            Assert.False(actual);
        }

        [Theory]
        [InlineData(new[] { Counter.Red }, new[] { Counter.Yellow })]
        [InlineData(new[] { Counter.Yellow }, new[] { Counter.Red })]
        [InlineData(new[] { Counter.Red, Counter.Yellow }, new[] { Counter.Red, Counter.Red })]
        [InlineData(new[] { Counter.Yellow, Counter.Red }, new[] { Counter.Yellow, Counter.Yellow })]

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