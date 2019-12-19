using System.Linq;
using QuadRelate.Models;
using QuadRelate.Types;
using Xunit;

namespace QuadRelate.Players.Vince.Tests
{
    public class LineFinderTests
    {
        [Fact]
        public void FindAllLines_WhenCalled_ReturnsTheCorrectNumberOfLines()
        {
            var board = CreateChequeredBoard();

            var lines = LineFinder.FindAllLines(board).ToArray();

            // Horizontal = 6
            // Vertical = 7
            // Diagonal = 8 + 8
            Assert.Equal(29, lines.Length);
        }
        
        [Fact]
        public void FindAllLines_WhenCalled_ReturnsHorizontalLinesFirst()
        {
            var board = CreateChequeredBoard();

            var lines = LineFinder.FindAllLines(board).ToArray();

            Assert.Equal(7, lines[0].Count);
            Assert.Equal(7, lines[1].Count);
            Assert.Equal(7, lines[2].Count);
            Assert.Equal(7, lines[3].Count);
            Assert.Equal(7, lines[4].Count);
            Assert.Equal(7, lines[5].Count);

            Assert.Equal(Counter.Yellow, lines[0][0]);
            Assert.Equal(Counter.Red, lines[1][0]);
            Assert.Equal(Counter.Yellow, lines[2][0]);
            Assert.Equal(Counter.Red, lines[3][0]);
            Assert.Equal(Counter.Yellow, lines[4][0]);
            Assert.Equal(Counter.Red, lines[5][0]);
        }

        [Fact]
        public void FindAllLines_WhenCalled_ReturnsVerticalLinesSecond()
        {
            var board = CreateChequeredBoard();

            var lines = LineFinder.FindAllLines(board).ToArray();

            Assert.Equal(6, lines[6].Count);
            Assert.Equal(6, lines[7].Count);
            Assert.Equal(6, lines[8].Count);
            Assert.Equal(6, lines[9].Count);
            Assert.Equal(6, lines[10].Count);
            Assert.Equal(6, lines[11].Count);
            Assert.Equal(6, lines[12].Count);
            
            Assert.Equal(Counter.Yellow, lines[6][0]);
            Assert.Equal(Counter.Red, lines[7][0]);
            Assert.Equal(Counter.Yellow, lines[8][0]);
            Assert.Equal(Counter.Red, lines[9][0]);
            Assert.Equal(Counter.Yellow, lines[10][0]);
            Assert.Equal(Counter.Red, lines[11][0]);
            Assert.Equal(Counter.Yellow, lines[12][0]);
        }

        private static Board CreateChequeredBoard()
        {
            var board = new Board();

            var counter = Counter.Yellow;
            for (var y = 0; y < Board.Height; y++)
            {
                for (var x = 0; x < Board.Width; x++)
                {
                    board[x, y] = counter;
                    counter = counter.Invert();
                }
            }

            return board;
        }
    }
}
