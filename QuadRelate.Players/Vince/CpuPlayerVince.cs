using System.Collections.Generic;
using System.Linq;
using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerVince : IPlayer
    {
        public string Name => "Invincible";

        public int NextMove(Board board, Counter colour)
        {
            var available = board.AvailableColumns();

            // 1. If only one possible move - play it.
            if (available.Count == 1)
                return available[0];

            // 2. If there's a winning move - play it.
            foreach (var m in available)
            {
                var clone = board.Clone();
                clone.PlaceCounter(m, colour);
                if (clone.IsGameOver())
                    return m;
            }

            // 3. If there's a blocking move - play it.
            var opponent = colour.Invert();
            foreach (var m in available)
            {
                var clone = board.Clone();
                clone.PlaceCounter(m, opponent);
                if (clone.IsGameOver())
                    return m;
            }

            // 4. ScoreEvaluator.
            var scores = new Dictionary<int, int>();
            foreach (var move in available)
            {
                var clone = board.Clone();
                clone.PlaceCounter(move, colour);
                var myScore = ScoreEvaluator.GetScore(clone, colour);
                var opponentsScore = ScoreEvaluator.GetScore(clone, colour.Invert());
                scores.Add(move, myScore - opponentsScore);
            }

            return scores.FirstOrDefault(x => x.Value == scores.Values.Max()).Key;
        }
    } 
}