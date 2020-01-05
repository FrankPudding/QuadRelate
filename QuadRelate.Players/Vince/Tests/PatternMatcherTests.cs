using QuadRelate.Players.Vince.Helpers;
using QuadRelate.Types;
using Xunit;

namespace QuadRelate.Players.Vince.Tests
{
    public class PatternMatcherTests
    {
        [Theory]
        [InlineData(new[] { Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Red, Counter.Red })]
        [InlineData(new[] { Counter.Red, Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Red })]
        [InlineData(new[] { Counter.Red, Counter.Red, Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Yellow })]
        public void CountMatches_WhenMatchingFour_ReturnsCorrectValue(Counter[] line)
        {
            var actual = PatternMatcher.CountMatches(line, new []{ Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Yellow });
            
            Assert.Equal(1, actual);
        }

        [Theory]
        [InlineData(new[] { Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Red, Counter.Red, Counter.Red, Counter.Red }, 1)]
        [InlineData(new[] { Counter.Red, Counter.Red, Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Red, Counter.Red }, 1)]        
        [InlineData(new[] { Counter.Red, Counter.Red, Counter.Red, Counter.Red, Counter.Yellow, Counter.Yellow, Counter.Yellow }, 1)]
        [InlineData(new[] { Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Red, Counter.Yellow, Counter.Yellow, Counter.Yellow }, 2)]
        public void CountMatches_WhenMatchingThree_ReturnsCorrectValue(Counter[] line, int expected)
        {
            var actual = PatternMatcher.CountMatches(line, new []{ Counter.Yellow, Counter.Yellow, Counter.Yellow });
            
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(new[] { Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Empty, Counter.Red, Counter.Red, Counter.Red }, 1)]
        [InlineData(new[] { Counter.Red, Counter.Red, Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Empty, Counter.Red }, 1)]        
        [InlineData(new[] { Counter.Red, Counter.Red, Counter.Red, Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Empty }, 1)]
        public void CountMatches_WhenMatchingThreePlusEmpty_ReturnsCorrectValue(Counter[] line, int expected)
        {
            var actual = PatternMatcher.CountMatches(line, new []{ Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Empty });
            
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(new[] { Counter.Empty, Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Red, Counter.Red, Counter.Red }, 1)]
        [InlineData(new[] { Counter.Red, Counter.Empty, Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Yellow, Counter.Red }, 1)]        
        [InlineData(new[] { Counter.Red, Counter.Red, Counter.Red, Counter.Empty, Counter.Yellow, Counter.Yellow, Counter.Yellow }, 1)]
        public void CountMatches_WhenMatchingEmptyPlusThree_ReturnsCorrectValue(Counter[] line, int expected)
        {
            var actual = PatternMatcher.CountMatches(line, new []{ Counter.Empty, Counter.Yellow, Counter.Yellow, Counter.Yellow });
            
            Assert.Equal(expected, actual);
        }
    }
}
