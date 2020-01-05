using QuadRelate.Models;
using QuadRelate.Players.Vince.Helpers;
using QuadRelate.Types;
using Xunit;

namespace QuadRelate.Players.Vince.Tests
{
    public class MovesHelperTests
    {
        [Theory]
        [InlineData(Counter.Yellow)]
        [InlineData(Counter.Red)]
        public void TryGetOpeningMove_WhenCentreColumnIsEmpty_ReturnsCentreColumn(Counter colour)
        {
            var board = new Board();

            var result = MovesHelper.TryGetOpeningMove(board, colour, out var actual);

            Assert.True(result);
            Assert.Equal(3, actual);
        }

        [Theory]
        [InlineData(Counter.Yellow)]
        [InlineData(Counter.Red)]
        public void TryGetOpeningMove_WhenCentreColumnIsNotEmpty_ReturnsFalse(Counter colour)
        {
            var board = new Board { [3, 0] = colour };

            var result = MovesHelper.TryGetOpeningMove(board, colour, out _);

            Assert.False(result);
        }

        [Theory]
        [InlineData(Counter.Yellow)]
        [InlineData(Counter.Red)]
        public void TryGetOpeningMove_WhenTheCentreContainsSpecificPattern_ReturnsCentreColumn(Counter colour)
        {
            var board = new Board { [3, 0] = colour, [3, 1] = colour.Invert() };

            var result = MovesHelper.TryGetOpeningMove(board, colour, out var actual);

            Assert.True(result);
            Assert.Equal(3, actual);
        }
    }
}