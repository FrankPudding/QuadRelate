using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerVince : IPlayer
    {
        private readonly IRandomizer _randomizer;
        private Counter _currentColour;

        public string Name => "Invincible";

        public CpuPlayerVince(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public int NextMove(Board board, Counter colour)
        {
            _currentColour = colour;
            var availableMoves = board.AvailableColumns();

            // 1. If only one possible move - play it.
            if (availableMoves.Count == 1)
                return availableMoves[0];

            // 2. Starting moves.
            if (MovesHelper.TryGetOpeningMove(board, colour, out var opening))
                return opening;

            // 3. If there's a winning move - play it.
            foreach (var myMove in availableMoves)
            {
                var clone = board.Clone();
                clone.PlaceCounter(myMove, colour);
                if (clone.IsGameOver())
                    return myMove;
            }

            // 4. If there's a blocking move - play it.
            var opponent = colour.Invert();
            foreach (var opponentMove in availableMoves)
            {
                var clone = board.Clone();
                clone.PlaceCounter(opponentMove, opponent);
                if (clone.IsGameOver())
                    return opponentMove;
            }

            // 5. ScoreEvaluator.
            var scores = new Dictionary<int, int>();
            foreach (var myMove in availableMoves)
            {
                var opponentTotal = 0;
                var clone = board.Clone();
                clone.PlaceCounter(myMove, colour);
                var myScore = ScoreEvaluator.GetScore(clone, colour);
                foreach (var opponentMove in clone.AvailableColumns())
                {
                    var innerClone = clone.Clone();
                    innerClone.PlaceCounter(opponentMove, opponent);
                    opponentTotal += ScoreEvaluator.GetScore(innerClone, opponent);
                }

                scores.Add(myMove, myScore - (opponentTotal / clone.AvailableColumns().Count));
            }

            //Debug.WriteLine(string.Join('.', scores));
            var bestScores = scores.Where(x => x.Value == scores.Values.Max());
            var bestMoves = bestScores.Select(x => x.Key).ToList();

            var centreMoves = MovesHelper.GetMovesClosestToCentre(bestMoves);
            return centreMoves[_randomizer.Next(centreMoves.Count)];
        }

        public void GameOver(GameResult result)
        {
            if (result.Winner == _currentColour.Invert())
            {
                Debug.WriteLine(string.Join('.', result.Moves));
            }
        }
    } 
}