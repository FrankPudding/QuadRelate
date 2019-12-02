using QuadRelate.Models;
using QuadRelate.Types;
using Xunit;

namespace QuadRelate.Tests
{
    public class CounterHelperTests
    {
        [Theory]
        [InlineData(Counter.Empty, Counter.Empty)]
        [InlineData(Counter.Yellow, Counter.Red)]
        [InlineData(Counter.Red, Counter.Yellow)]
        public void Invert_WhenCalled_ReturnsCorrectValue(Counter counter, Counter expected)
        {
            var actual = counter.Invert();

            Assert.Equal(expected, actual);
        }
    }
}