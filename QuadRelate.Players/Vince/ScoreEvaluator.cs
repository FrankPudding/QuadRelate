using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    internal static class ScoreEvaluator
    {
        private const int _fullyOpenThree = 10;
        private const int _halfOpenThree = 7;
        private const int _fullyOpenTwo = 5;
        private const int _halfOpenTwo = 3;

        public static int GetScore(Board board, Counter colour)
        {
            var lines = LineFinder.FindAllLines(board);
            var score = 0;
            foreach (var line in lines)
            {
                var count = PatternMatcher.CountMatches(line, new[] { Counter.Empty, colour, colour, colour, Counter.Empty });
                score += count * _fullyOpenThree;
                count = PatternMatcher.CountMatches(line, new[] { colour, colour, colour, Counter.Empty });
                score += count * _halfOpenThree;
                count = PatternMatcher.CountMatches(line, new[] { Counter.Empty, colour, colour, colour });
                score += count * _halfOpenThree;

                count = PatternMatcher.CountMatches(line, new[] { Counter.Empty, colour, colour, Counter.Empty });
                score += count * _fullyOpenTwo;
                count = PatternMatcher.CountMatches(line, new[] { colour, colour, Counter.Empty });
                score += count * _halfOpenTwo;
                count = PatternMatcher.CountMatches(line, new[] { Counter.Empty, colour, colour });
                score += count * _halfOpenTwo;
            }

            return score;
        }
    }
}