using System.Collections.Generic;
using System.Linq;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    internal static class PatternMatcher
    {
        public static int CountMatches(IList<Counter> line, Counter[] pattern)
        {
            if (pattern.Length > line.Count) return 0;

            var count = 0;
            for (var start = 0; start < line.Count - pattern.Length+1; start++)
            {
                if (IsMatch(line, pattern, start))
                    count++;
            }

            return count;
        }

        private static bool IsMatch(IList<Counter> line, IEnumerable<Counter> pattern, int start)
        {
            return !pattern.Where((t, i) => line[start + i] != t).Any();
        }
    }
}
